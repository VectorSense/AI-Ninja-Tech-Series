using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartnerTechSeries.AI.Demo;
using System.Web.Mvc;

namespace EventExtractionPOC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        public JsonResult EventExtraction(string data)
        {
            EventExtractionHandler eeh = new EventExtractionHandler();
            string imgefile = "Img" + $@"{System.DateTime.Now.Ticks}.jpg";
            string Url = Server.MapPath(@"~\Images\" + imgefile);
            System.IO.File.WriteAllBytes(Url, Convert.FromBase64String(data));
            eeh.EventExtraction(data);

            if (eeh.error == "")
            {
                return Json(new { TagName = eeh.TagName, Error = "", JsonResponse = eeh.JsonResponse,ImgName= imgefile });
            }
            return Json(new { TagName = "",Error= eeh.error, JsonResponse = "", ImgName = "" });
        }
    }
}