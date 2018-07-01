using EtudiantMasque.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace M101DotNet.WebApp.Controllers
{
    public class MessageController : Controller
    {
        #region Messages
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View(new Message());
        }

        [HttpPost]
        public async Task<ActionResult> NewMessage(Message _model)
        {
            if (!ModelState.IsValid)
                return View(_model);

            _model.Validate(Request["g-recaptcha-response"]);

            if (_model.BoolSuccess)
            {
                _model.CreatedAtUtc = DateTime.UtcNow;

                await DB.getInstance().Messages.InsertOneAsync(_model);

                return RedirectToAction("Message", new { id = _model.Id });
            }

            return View(_model);
        }

        [HttpGet]
        public async Task<ActionResult> Message(string id)
        {
            Message message = await DB.getInstance().Messages.Find(x => x.Id == id).SingleOrDefaultAsync();

            if (message == null)
                return RedirectToAction("Index");

            return View(message);
        }

        [HttpGet]
        public async Task<ActionResult> Messages()
        {
            List<Message> messages = await DB.getInstance().Messages.Find(x => true).SortByDescending(x => x.CreatedAtUtc).ToListAsync();
            return View(messages);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMessage(Message _model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Messages");

            await DB.getInstance().Messages.DeleteOneAsync(x => x.Id == _model.Id);

            return RedirectToAction("Messages");
        }
        #endregion
    }
}