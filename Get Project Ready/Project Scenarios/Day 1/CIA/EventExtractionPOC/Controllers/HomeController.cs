using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartnerTechSeries.AI.Demo;
using System.Web.Mvc;
using System.Threading.Tasks;
using PartnerTechSeries.AI.Demo.FaceAPI;

namespace EventExtractionPOC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        

        // POST: ImageAnalyse
        [HttpPost]
        public async Task<JsonResult> ImageAnalyse(string data)
        {
            try
            {
                AnalyseImage Ai = new AnalyseImage();
                string imgefile = "Img" + $@"{System.DateTime.Now.Ticks}.jpg";
                string Url = Server.MapPath(@"~\Images\" + imgefile);
                System.IO.File.WriteAllBytes(Url, Convert.FromBase64String(data));
                await Ai.ImageAnalyse(data);
                if (Ai.Erorr == "")  //converting all object array to Json and returning the Json
                    //return Json(new { Brand = Ai.Brandarray, Tag = Ai.Tagarray, Object = Ai.Objectarray });
                    return Json(new { Error = "", JsonResponse = new {Brand= Ai.Brandarray,Color=Ai.Color }, ImgName = imgefile });
                //return Json(new { Erorr = Ai.Erorr });
                return Json(new { Error = Ai.Erorr, JsonResponse = "", ImgName = "" });
            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new {Error = e.Message, JsonResponse = "", ImgName = "" });
            }

        }
    }
}