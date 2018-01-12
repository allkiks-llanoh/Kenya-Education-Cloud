using CsvHelper;
using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Web.Api.Models;
using MultipartDataMediaFormatter.Infrastructure;
using System;
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
        public HttpResponseMessage SchoolsByCountyCode(string countycode)
        {
            var schools = _uow.SchoolRepository
                .Find(p => p.County.CountyCode.Equals(countycode));
            return schools.Any() ? Request.CreateResponse(HttpStatusCode.OK, schools.Select(p => new School(p))) :
                Request.CreateResponse(HttpStatusCode.NotFound);


        }
        //GET api/<controller>/schoolcode
        [HttpGet, Route("{schoolcode}")]
        public HttpResponseMessage SchoolByCode(string schoolcode)
        {
            var school = _uow.SchoolRepository
               .Find(p => p.SchoolCode.Equals(schoolcode)).FirstOrDefault();
            return school == null ? Request.CreateResponse(HttpStatusCode.NotFound) :
                Request.CreateResponse(HttpStatusCode.OK, new School(school));
        }

        //Get api/<controller>/id
        [HttpGet, Route("{id:int}")]
        public HttpResponseMessage SchoolById(int id)
        {
            var dbschool = _uow.SchoolRepository.Get(id);
            return dbschool == null ? Request.CreateResponse(HttpStatusCode.NotFound) :
                Request.CreateResponse(HttpStatusCode.OK, new School(dbschool));
        }
        //Get api/<controller>?countyCode=value&TypeId=value
        [HttpGet, Route("")]
        public HttpResponseMessage SchoolsByCountyCodeAndTypeId(string countyCode, int typeId)
        {
            var schools = _uow.SchoolRepository
                .Find(p => p.County.CountyCode.Equals(countyCode)
                && p.SchoolTypeId == typeId).ToList();

            return schools.Any() ? Request.CreateResponse(HttpStatusCode.OK,
                                    schools.Select(s => new School(s)).ToList()) :
                                 Request.CreateResponse(HttpStatusCode.NotFound);
        }
        //POST api/<controller>
        [HttpPost, Route("")]
        public async Task<HttpResponseMessage> SchoolsUpload()
        {

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count <= 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please upload your csv file");
            }
            if (httpRequest.Files.Count < 1)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Multiple file upload is not supported");
            }
            var postedFile = httpRequest.Files["postedFile"];
            if (!postedFile.FileName.EndsWith(".csv"))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new Exception("The file format is not supported"));
            }
            try
            {
                using (var streamReader = new StreamReader(postedFile.InputStream))
                {
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
                            _uow.SchoolRepository.AddFromCSV(school, fundAllocation);

                        }
                        _uow.Complete();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }
                }
               

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}