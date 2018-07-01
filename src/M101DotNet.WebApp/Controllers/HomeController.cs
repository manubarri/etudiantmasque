using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq.Expressions;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace EtudiantMasque.Models.Home
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var recentArticles = await DB.getInstance().Articles.Find(x => true).SortByDescending(x => x.CreatedAtUtc).Limit(3).ToListAsync();
            
            recentArticles.ForEach(x => x.Content = Encoding.Unicode.GetString(Convert.FromBase64String(x.Content)));

            return View(recentArticles);
        }
    }
}