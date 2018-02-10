using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;
using CsvHelper;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using KEC.Curation.Services.AfricasTalking;
using KEC.Curation.Web.Api.Serializers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEC.Curation.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Curator")]
    public class CuratorController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IHostingEnvironment _env;
      

        public CuratorController(IUnitOfWork uow, IHostingEnvironment env)
        {
            _uow = uow;
            _env = env;
        }

        //Get All Curator Types
        [HttpGet,Route("GetCuratorTypes")]
        public IActionResult GetAll()
        {
            var curators = _uow.CuratorTypeRepository.GetAll().ToList();
            var list = curators.Any() ?
                curators.Select(p => new CuratorTypeDownloadSerilizer(p)).ToList() : new List<CuratorTypeDownloadSerilizer>();
            return Ok(value: list);
        }
        // GET: api/Subjects/5
        [HttpGet("{typeid}", Name = "CuratorsByTypeId")]
        public IActionResult CuratorsByTypeId(int TypeId)
        {

            var curators = _uow.CuratorRepository.Find(p =>
                          p.TypeId.Equals(TypeId)).ToList();
            return Ok(value: curators.ToList());

        }
        // POST: api/Curator
        [HttpPost, Route("")]
        public async Task<IActionResult> PostCSVAsync()
        {
           

            var httpRequest = HttpContext.Request.Form.Files;
            if (httpRequest.Count <= 0)
            {
                return BadRequest("Please upload your csv file");
            }
            if (httpRequest.Count > 1)
            {
                return BadRequest("Multiple file upload is not supported");
            }
            var postedFile = httpRequest["postedFile"];
            if (!postedFile.FileName.EndsWith(".csv"))
            {
                return BadRequest("The file format is not supported");
            }
           
            var smsService = new AfricasTalkingSmsService();
            try
            {
               
                using (var streamReader = new StreamReader(postedFile.OpenReadStream()))
                using (CsvReader csvReader = new CsvReader(streamReader, false))
                {
                    csvReader.Read();
                    csvReader.ReadHeader();
                    while (csvReader.Read())
                    {
                        var curatorType = csvReader.GetField<string>("Type");
                        var curatorTypeId = _uow.CuratorTypeRepository.Find(p =>
                                             p.TypeName.Equals(curatorType))
                                            .FirstOrDefault()?.Id;
                        var curatorCreation = new CuratorCreation
                        {
                            PhoneNumber = csvReader.GetField<string>("PhoneNumber"),
                            EmailAddress = csvReader.GetField<string>("EmailAddress"),
                            SirName = csvReader.GetField<string>("SirName"),
                            FirstName = csvReader.GetField<string>("FirstName"),
                            LastName = csvReader.GetField<string>("LastName"),
                            CreatedAt = DateTime.Now,
                            TypeId = curatorTypeId.GetValueOrDefault()
                        };
                        _uow.CuratorRepository.AddFromCSV(curatorCreation);
                    }
                    _uow.Complete();
                    //TODO: use logged in user number
                    //smsService.SendSms("0704033581", "Curators data processed successfully");
                    var filePath = $"{_env.ContentRootPath}/Controllers/CsvFiles/{DateTime.Now.ToString("yyyMMddHHmmss")}{postedFile.FileName}";
                    using (var memoryStream = new MemoryStream())
                    {
                        await postedFile.CopyToAsync(memoryStream);
                        var fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.ReadWrite);
                        memoryStream.WriteTo(fileStream);
                    }
                    return Ok("Curators Created Succesfully");

                }
            }
            catch (Exception ex)
            {
                //TODO: use logged in user number
                //smsService.SendSms("704033581", "An error occured while processing cutrators data csv");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPost, Route("curatortype")]
        public IActionResult CreateType([FromForm]CuratorTypeSerilizer model)
        {
            var TypeName = model.TypeName;
            if (TypeName == null)
            {
                return BadRequest("Name is required");

            }
          
            try
            {
                var exist = _uow.CuratorTypeRepository.Find(p => p.TypeName.Equals(model.TypeName)).Any();
                if (exist)
                {
                    return BadRequest("Curator Type already exists");
                }
                var CuratorType = new CuratorType
                {
                    TypeName = model.TypeName
                };
                _uow.CuratorTypeRepository.Add(CuratorType);
                _uow.Complete();
                return Ok("Curator Type Added Succesfully");
            }
            catch
            {
                return BadRequest("Something Went Wrong");
            }
            
        }
        // PUT: api/Curator/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
      
    }
}
