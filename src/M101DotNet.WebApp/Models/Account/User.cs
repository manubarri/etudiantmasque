using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using M101DotNet.WebApp.Models.Account;

namespace EtudiantMasque.Models
{
    public class User : Login
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}