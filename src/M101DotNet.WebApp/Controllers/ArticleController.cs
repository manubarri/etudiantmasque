using EtudiantMasque.Models;
using M101DotNet.WebApp.Infrastructure;
using M101DotNet.WebApp.Models.ViewModel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace M101DotNet.WebApp.Controllers
{
    public class ArticleController : Controller
    {
        #region Articles
        [HttpGet]
        public ActionResult NewArticle()
        {
            return View(new Article());
        }

        [HttpPost]
        public async Task<ActionResult> NewArticle(ViewModelNewArticle _model)
        {
            if (!ModelState.IsValid)
                return View(_model);

            string b64Img = String.Empty;

            if (_model.Image != null)
                b64Img = Convertions.InputStreamToB64String(_model.Image.InputStream);

            Article article = new Article
            {
                Author = this.User.Identity.Name,
                Image = b64Img,
                CreatedAtUtc = DateTime.UtcNow,
                Comments = new Comment[] { },
                Tags = String.IsNullOrEmpty(_model.Tags) ? _model.Tags.Split(new char[] { ',', ';' }) : new String[] { },
                Title = _model.Title,
                Content = Convert.ToBase64String(Encoding.Unicode.GetBytes(_model.Content))
            };

            await DB.getInstance().Articles.InsertOneAsync(article);

            return RedirectToAction("Article", new { id = article.Id });
        }

        [HttpGet]
        public async Task<ActionResult> Article(string _id)
        {
            Article article = await DB.getInstance().Articles.Find(x => x.Id == _id).SingleOrDefaultAsync();

            if (article == null)
                return RedirectToAction("Articles");

            article.Content = Encoding.Unicode.GetString(Convert.FromBase64String(article.Content));
            article.Content = article.Content.Replace(System.Environment.NewLine, "<br/>");

            return View(article);
        }

        [HttpGet]
        public async Task<ActionResult> Articles(string tag = null)
        {
            Expression<Func<Article, bool>> filter = x => true;

            if (tag != null)
                filter = x => x.Tags.Contains(tag);

            var posts = await DB.getInstance().Articles.Find(filter)
                .SortByDescending(x => x.CreatedAtUtc)
                .ToListAsync();

            posts.ForEach(x => x.Content = Encoding.Unicode.GetString(Convert.FromBase64String(x.Content)));

            return View(posts);
        }

        [HttpPost]
        public async Task<ActionResult> EditArticle(ViewModelNewArticle _model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Article", new { _model.Id });

            string b64Img = _model.ImageString ;
            if (_model.Image != null)
                b64Img = Convertions.InputStreamToB64String(_model.Image.InputStream);

            await DB.getInstance().Articles.UpdateOneAsync(x => x.Id == _model.Id,
            Builders<Article>.Update.Set(x => x.Image, b64Img)
                                    .Set(x => x.Title, _model.Title)
                                    .Set(x => x.Content, Convert.ToBase64String(Encoding.Unicode.GetBytes(_model.Content)))
                                    .Set(x => x.Tags, _model.Tags != null ? _model.Tags.Split(new char[]{',',';'}) :  new string[]{}));

            return RedirectToAction("Article", new { id = _model.Id });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteArticle(ViewModelDeleteArticle _model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Article", new { _model.Id });

            await DB.getInstance().Articles.DeleteOneAsync(x => x.Id == _model.Id);

            return RedirectToAction("Articles");
        }
        #endregion

        #region Comments
        [HttpPost]
        public async Task<ActionResult> NewComment(Comment _model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Article", new { id = _model.ArticleId });
            }

            _model.Validate(Request["g-recaptcha-response"]);

            if (_model.BoolSuccess)
            {
                _model.CreatedAtUtc = DateTime.UtcNow;

                await DB.getInstance().Articles.UpdateOneAsync(x => x.Id == _model.ArticleId,
                    Builders<Article>.Update.Push(x => x.Comments, _model));
            }

            return RedirectToAction("Article", new { _id = _model.ArticleId });
        }
        #endregion
    }
}