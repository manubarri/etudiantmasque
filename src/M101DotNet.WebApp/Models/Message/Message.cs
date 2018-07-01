using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using EtudiantMasque.Models.Home;
using System.ComponentModel.DataAnnotations;

namespace EtudiantMasque.Models
{
    public class Message : Captcha
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; }
    }
}