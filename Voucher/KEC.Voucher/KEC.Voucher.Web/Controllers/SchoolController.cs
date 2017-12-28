using CsvHelper;
using KEC.Voucher.Data.Models;
using KEC.Voucher.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KEC.Voucher.Web.Controllers
{
    [Route("school")]
    public class SchoolController : Controller
    {
        private readonly IUnitOfWork _uow;

        public SchoolController()
        {
            _uow = new EFUnitOfWork();
        }
        [Route("upload"), HttpGet]
        public ActionResult Upload()
        {
            return View();
        }
        [Route("upload"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase postedFile)
        {
            if (!ModelState.IsValid) return View();
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                if (postedFile.FileName.EndsWith(".csv"))
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
                    }
                    return View();
                }
                else
                {
                    ModelState.AddModelError("File", "The file format is not supported");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please select the schools csv file");
                return View();
            }
        }
    }
}
