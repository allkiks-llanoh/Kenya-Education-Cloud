using CsvHelper;
using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using KEC.Voucher.Web.Api.Models;
using System;
using System.Collections.Generic;
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
        [HttpGet]
        public IEnumerable<School> Get(string countycode)
        {
            var schools = _uow.SchoolRepository
                .Find(p => p.County.CountyCode.Equals(countycode))
                .Select(p => new School(p)).ToList();
            return schools;
        }
        //GET api/<controller>/schoolcode
        [HttpGet, Route("{schoolcode}")]
        public School School(string schoolcode)
        {
            var school = _uow.SchoolRepository
               .Find(p => p.SchoolCode.Equals(schoolcode))
               .Select(p => new School(p)).SingleOrDefault();
            return school;
        }

        //POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post()
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
            var postedFile = httpRequest.Files[0];
            if (!postedFile.FileName.EndsWith(".csv"))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new Exception("The file format is not supported"));
            }
            try
            {
                StreamReader streamReader = new StreamReader(postedFile.InputStream);
                using (CsvReader csvReader = new CsvReader(streamReader, false))
                {
                    csvReader.Read();
                    csvReader.ReadHeader();
                    while (csvReader.Read())
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
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}