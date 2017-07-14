using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimbirSoft.Models;

namespace SimbirSoft.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadDbData()
        {
            AddressesEntities db = new AddressesEntities();
            var JsonResult = Json(db.Address.ToList(), JsonRequestBehavior.AllowGet);

            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }
    }
}