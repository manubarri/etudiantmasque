using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using EtudiantMasque.Models.Home;

namespace EtudiantMasque.Models
{
    public class Article : Captcha
    {
        [HiddenInput(DisplayValue = false)]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Image { get; set; }

        public string Author { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; }
        
        public string[] Tags { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAtUtc { get; set; }

        [DataType(DataType.Custom)]
        public Comment[] Comments { get; set; }
    }
}