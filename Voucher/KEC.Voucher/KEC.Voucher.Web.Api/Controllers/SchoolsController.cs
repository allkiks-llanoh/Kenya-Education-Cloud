using CsvHelper;
using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Web.Api.Models;
using MultipartDataMediaFormatter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace KEC.Voucher.Web.Api.Controllers
{
    [RoutePrefix("api/schools")]
    public class SchoolsController : ApiController
    {
        private readonly IUnitOfWork _uow = new EFUnitOfWork();

        //GET api/<controller>?countrycode=value
        [HttpGet, Route("")]
        public IEnumerable<School> SchoolsByCountyCode(string countycode)
        {
            var schools = _uow.SchoolRepository
                .Find(p => p.County.CountyCode.Equals(countycode))
                .Select(p => new School(p)).ToList();
            return schools;
        }
        //GET api/<controller>/schoolcode
        [HttpGet, Route("{schoolcode}")]
        public School SchoolByCode(string schoolcode)
        {
            var school = _uow.SchoolRepository
               .Find(p => p.SchoolCode.Equals(schoolcode))
               .Select(p => new School(p)).SingleOrDefault();
            return school;
        }

        //Get api/<controller>/id
        [HttpGet, Route("{id:int}")]
        public School SchoolById(int id)
        {
            var dbschool = _uow.SchoolRepository.Get(id);
            return dbschool == null ? null : new School(dbschool);
        }
        //Get api/<controller>?countyCode=value&TypeId=value
        [HttpGet,Route("")]
        public ICollection<School> SchoolsByCountyCodeAndTypeId(string countyCode, int typeId)
        {
            var schools = _uow.SchoolRepository
                .Find(p => p.County.CountyCode.Equals(countyCode)
                && p.SchoolTypeId == typeId).Select(p=> new School(p)).ToList();
            
            return schools;
        }
        //POST api/<controller>
        [HttpPost, Route("")]
        public async Task<HttpResponseMessage> SchoolsUpload(FormData formData)
        {
            
            formData.TryGetValue("postedFile", CultureInfo.CurrentCulture, out HttpFile postedFile);
            if (postedFile==null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please upload your csv file");
            }
            if (!postedFile.FileName.EndsWith(".csv"))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new Exception("The file format is not supported"));
            }
            try
            {
                StreamReader streamReader = new StreamReader(postedFile.FileName);
                using (CsvReader csvReader = new CsvReader(streamReader, false))
                {
                    await csvReader.ReadAsync();
                    csvReader.ReadHeader();
                    while (await csvReader.ReadAsync())
                    {
                        var schoolType = csvReader.GetField<string>("SchoolType");
                        var schoolTypeId = _uow.SchoolTypeRepository
                            .Find(p => p.SchoolType.Equals(schoolType))
                            .FirstOrDefault()?.Id;
                        var county = csvReader.GetField<string>("County");
                        var countyId = _uow.CountyRepository
                            .Find(p => p.CountyName.Equals(county))
                            .FirstOrDefault()?.Id;
                        var school = new DbSchool
                        {
                            SchoolName = csvReader.GetField<string>("SchoolName"),
                            SchoolCode = csvReader.GetField<string>("SchoolCode"),
                            SchoolTypeId = schoolTypeId.GetValueOrDefault(),
                            CountyId = countyId.GetValueOrDefault(),
                            DateCreated = DateTime.Now,
                            DateChanged = DateTime.Now,
                        };
                        var fundAllocation = new DbFundAllocation
                        {
                            Amount = csvReader.GetField<Decimal>("Amount"),
                            Year = csvReader.GetField<int>("Year")
                        };
                        var schoolAdmin = new DbSchoolAdmin
                        {
                            Email = csvReader.GetField<string>("SchoolAdminPhoneNumber"),
                            FirstName = csvReader.GetField<string>("SchoolAdminFirstName"),
                            LastName = csvReader.GetField<string>("SchoolAdminLastName"),
                            PhoneNumber = csvReader.GetField<string>("SchoolAdminPhoneNumber"),
                            guid = Guid.NewGuid().ToString()
                        };
                        _uow.SchoolRepository.AddFromCSV(school, fundAllocation);

                    }
                    _uow.Complete();
                    return Request.CreateResponse(HttpStatusCode.OK);

                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}