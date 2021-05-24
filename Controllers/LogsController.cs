using GuitarBazar.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuitarBazar.Controllers
{
    public class LogsController : Controller
    {
        private GuitarBazarContext db = new GuitarBazarContext();
        // GET: Logs
        public ActionResult Index()
        {

            ViewBag.Logs = db.log.Dump();
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }


}