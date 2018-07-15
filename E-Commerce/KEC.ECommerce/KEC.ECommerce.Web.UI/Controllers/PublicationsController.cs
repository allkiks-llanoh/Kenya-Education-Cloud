using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KEC.ECommerce.Data.Models;
using KEC.ECommerce.Data.UnitOfWork.Core;
using KEC.ECommerce.Web.UI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace KEC.ECommerce.Web.UI.Controllers
{

    public class PublicationsController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IHostingEnvironment _env;

        public PublicationsController(IUnitOfWork uow, IHostingEnvironment env)
        {
            _uow = uow;
            _env = env;
        }
        public List<string> Hello()
        {
            return new List<string> { "Tesst" };
        }
        [HttpPost]
        public async Task<IActionResult> Upload(PublicationUploadSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            else
            {
                var thumbnailUrl = await UploadThumbnail(model);
                var publisher = _uow.PublishersRepository
                                    .AddPublicationPublisher(model.PublisherName, model.PublisherGuid);
                var author = _uow.AuthorsRepository.AddPublicationAuthor(model.AuthorFirstName, model.AuthorLastName);
                var category = _uow.CategoriesRepository.AddPublicationCategory(model.Category);
                var subject = _uow.SubjectsRepository.AddPublicationSubject(model.Subject);
                var level = _uow.LevelsRepository.AddPublicationLevel(model.Level);
                var added = AddToStore(model, thumbnailUrl, publisher, author, subject, level, category);
                if (added)
                {
                    return Ok($"Publication number {model.ContentNumber} added to E-Commerce store successfully");
                }
                else
                {
                    return BadRequest($"Publication number {model.ContentNumber}  already exists in E-Commerce store");
                }
            }
        }

        private bool AddToStore(PublicationUploadSerializer model, string thumbnailUrl, Publisher publisher, Author author, Subject subject, Level level, Category category)
        {
            var publication = new Publication
            {
                ContentNumber = model.ContentNumber,
                Author = author,
                Category = category,
                Publisher = publisher,
                Subject = subject,
                Level = level,
                UnitPrice = model.UnitPrice,
                Title = model.Title,
                Description = model.Description,
                ThumbnailUrl = thumbnailUrl,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Quantity = model.Quantity,
                ContentUrl = model.ContentUrl,
                Available = true
            };
            return _uow.PublicationsRepository.AddPublicationToStore(publication);
        }

        private async Task<string> UploadThumbnail(IPublicationSerializer model)
        {
            using (var memoryStream = new MemoryStream())
            {
                await model.ThumbnailImage.CopyToAsync(memoryStream);
                var savePath = Path.Combine(_env.WebRootPath, "Images", model.ThumbnailImage.FileName);
                var urlPath = $"~/Images/{model.ThumbnailImage.FileName}";
                var image = Image.FromStream(memoryStream);
                image.Save(savePath);
                return urlPath;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(PublicationUpdateSerializer model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            else
            {
                var publication = _uow.PublicationsRepository.Find(p =>
                                       p.ContentNumber.Equals(model.ContentNumber)
                                       && p.Publisher.Guid.Equals(model.PublisherGuid)).FirstOrDefault();
                if (publication == null)
                {
                    return BadRequest("Publication could not be retrieved for update");
                }
                else
                {
                    publication.Quantity = model.Quantity;
                    publication.ThumbnailUrl = await UploadThumbnail(model);
                    publication.UnitPrice = model.UnitPrice;
                    publication.Quantity += model.Quantity;
                    _uow.Complete();
                    return Ok("Publication updated successfully");
                }
            }
        }

        [HttpDelete]
        public IActionResult Delete(string contentNumber, string publisherGuid)
        {
            var publication = _uow.PublicationsRepository.Find(p =>
            p.ContentNumber.Equals(contentNumber)
            && p.Publisher.Guid.Equals(publisherGuid)).FirstOrDefault();
            if (publication != null)
            {
                if (DateTime.Now.Subtract(publication.CreatedAt).TotalDays < 365)
                {
                    return Forbid("Publication must at least one year old to be removed");
                }
                else
                {
                    publication.Available = false;
                    _uow.Complete();
                    return Ok("Publication removed successfully from E-Commerce");
                }
            }
            else
            {
                return BadRequest("Invalid content number");
            }

        }
        [HttpGet]
        public IActionResult Read(PublicationAccessModel model)
        {
            var publicationUrl = _uow.LicencesRepository.GetContentUrl(model.IdentificationCodes, model.LicenceKey);
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            if (publicationUrl == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(publicationUrl);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Sales(SalesQueryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            var sales = await _uow.LineItemsRepository.GetPublisherSales(model.PublisherGuid, model.StartDate, model.EndDate, model.PaymentMethod);
            var salesList = sales.Any() ? sales.Select(p => new SaleItemViewModel(p, _uow)).ToList() : new List<SaleItemViewModel>();
            return Ok(salesList);
        }
    }
}
