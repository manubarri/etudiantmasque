using EtudiantMasque.Models.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EtudiantMasque.Models
{
    public class Comment : Captcha
    {
        [HiddenInput(DisplayValue = false)]
        public string ArticleId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}