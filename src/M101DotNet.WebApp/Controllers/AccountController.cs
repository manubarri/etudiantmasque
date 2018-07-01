using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq.Expressions;
using M101DotNet.WebApp.Models.Account;
using M101DotNet.WebApp.Models.ViewModel;

namespace EtudiantMasque.Models.Account
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View(new User { ReturnUrl = returnUrl });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Login(Login _model)
        {
            if (!ModelState.IsValid)
                return View(_model);

            User user = (User)await DB.getInstance().Users.Find(x => x.Email == _model.Email && x.Password == _model.Password).SingleOrDefaultAsync();
            
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email ou password non reconnu.");
                return View(_model);
            }

            ClaimsIdentity identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                },
                "ApplicationCookie");

            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignIn(identity);

            return Redirect(GetRedirectUrl(_model.ReturnUrl));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View(new User());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Register(Register _model)
        {
            if (!ModelState.IsValid)
                return View(_model);

            await DB.getInstance().Users.InsertOneAsync(_model);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> User(string id)
        {
            Register user = await DB.getInstance().Users.Find(x => x.Id == id).SingleOrDefaultAsync();

            if (user == null)
                return RedirectToAction("Index");

            return View(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> NewPassword(ViewModelNewPassword _model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("User", new { id = _model.Id });

            await DB.getInstance().Users.UpdateOneAsync(x => x.Id == _model.Id && x.Name == _model.Name && x.Password == _model.Password,
            Builders<Register>.Update.Set(x => x.Password,
                _model.NewPassword));

            return RedirectToAction("User", new { id = _model.Id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> NewEmail(ViewModelNewEmail _model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("User", new { id = _model.Id });

            await DB.getInstance().Users.UpdateOneAsync(x => x.Id == _model.Id,
            Builders<Register>.Update.Set(x => x.Email,
                _model.NewEmail));

            return RedirectToAction("User", new { id = _model.Id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> NewName(ViewModelNewName _model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("User", new { id = _model.Id });

            await DB.getInstance().Users.UpdateOneAsync(x => x.Id == _model.Id,
            Builders<Register>.Update.Set(x => x.Name,
                _model.NewName));

            await DB.getInstance().Articles.UpdateManyAsync(x => x.Author == _model.Name,
            Builders<Article>.Update.Set(x => x.Author,
                _model.NewName));

            var context = Request.GetOwinContext();
            var authManager = context.Authentication;

            authManager.SignOut("ApplicationCookie");

            var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, _model.NewName),
                    new Claim(ClaimTypes.NameIdentifier, _model.Id),
                    new Claim(ClaimTypes.Email, _model.Email)
                },
                "ApplicationCookie");

            context = Request.GetOwinContext();
            authManager = context.Authentication;

            authManager.SignIn(identity);
            
            return RedirectToAction("User", new { id = _model.Id });
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                return Url.Action("Index", "Home");

            return returnUrl;
        }
    }
}