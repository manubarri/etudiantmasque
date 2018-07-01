using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EtudiantMasque.Models
{
    public class Index
    {
        public List<Article> RecentPosts { get; set; }
    }
}