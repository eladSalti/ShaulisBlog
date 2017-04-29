using ShaulisBlogMvc_3.Models.Fans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ShaulisBlogMvc_3.Controllers
{
    public class FansController : Controller
    {
        //
        // GET: /Fans/
        public FansController()
        {
            ViewBag.FansSelected = "selected";
        }
        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        public ViewResult Create()
        {
            return View("CreateFan");
        }

        [HttpPost]
        public  ActionResult Create(Fan Fan)
        {
            using (var context = new ShauliBlogContext())
            {
                context.Fans.Add(Fan);
                context.SaveChanges();
                return RedirectToAction("ThankYou");
            }
        }

        public ViewResult ThankYou()
        {
            return View("ThankYou");

        }





    }
}
