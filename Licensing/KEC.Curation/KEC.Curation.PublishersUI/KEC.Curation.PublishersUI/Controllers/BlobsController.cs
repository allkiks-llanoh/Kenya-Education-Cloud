using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace KEC.Curation.PublishersUI.Controllers
{
    public class BlobsController : Controller
    {
        // GET: Blobs
        public ActionResult Index()
        {
            return View();
        }

        private CloudBlobContainer GetCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("keccuration_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("publications");
            return container;
        }

        public ActionResult CreateBlobContainer()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            ViewBag.Success = container.CreateIfNotExists();
            ViewBag.BlobContainerName = container.Name;

            return View();
        }

        public string UploadBlob()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference("header_one.jpg");
            using (var fileStream = System.IO.File.OpenRead(@"C:\Users\collo\Source\Repos\Kenya Education Cloud\Licensing\KEC.Curation\KEC.Curation.PublishersUI\KEC.Curation.PublishersUI\Content\landing\header_one.jpg"))
            {
                blob.UploadFromStream(fileStream);
            }
            return "success!";
        }

        public ActionResult ListBlobs()
        {
            CloudBlobContainer container = GetCloudBlobContainer();

            List<string> blobs = new List<string>();
            foreach (IListBlobItem item in container.ListBlobs())
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob blob = (CloudPageBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory dir = (CloudBlobDirectory)item;
                    blobs.Add(dir.Uri.ToString());
                }
            }

            return View(blobs);
        }

        public string DownloadBlob()
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference("header_one.jpg");
            using (var fileStream = System.IO.File.OpenWrite(@"c:\header_one.jpg"))
            {
                blob.DownloadToStream(fileStream);
            }
            return "success!";
        }
    }
}