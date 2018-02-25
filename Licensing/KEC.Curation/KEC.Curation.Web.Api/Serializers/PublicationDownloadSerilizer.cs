﻿using KEC.Curation.Data.Database;
using KEC.Curation.Data.Models;
using KEC.Curation.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KEC.Curation.Web.Api.Serializers
{
    public class PublicationDownloadSerilizer
    {
        private readonly Publication _publication;
        private readonly IUnitOfWork _uow;

        public PublicationDownloadSerilizer(Publication publication, IUnitOfWork uow)
        {
            _publication = publication;
            _uow = uow;

        }
        public int Id
        {
            get
            {
                return _publication.Id;
            }
        }
        [DataType(DataType.Url)]
        public string Url
        {
            get
            {
                return _publication.Url;
            }
        }
        public string Title
        {
            get
            {
                return _publication.Title;
            }
        }
        public string ISBNNumber
        {
            get
            {
                return _publication.ISBNNumber;
            }
        }
        public string KICDNumber
        {
            get
            {
                return _publication.KICDNumber;
            }
        }

        public decimal Price
        {
            get
            {
                return _publication.Price;
            }
        }
        public string Description
        {
            get
            {
                return _publication.Description;
            }
        }
        public int SubjectId
        {
            get
            {
                return _publication.SubjectId;
            }
        }
        public int LevelId
        {
            get
            {
                return _publication.LevelId;
            }
        }


    }
}
