using EtudiantMasque.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M101DotNet.WebApp.Models.ViewModel
{
    public class ViewModelNewArticle : Captcha
    {
        [HiddenInput(DisplayValue = false)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image { get; set; }

        public string ImageString { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        [DataType(DataType.Text)]
        public string Tags { get; set; }
    }
}