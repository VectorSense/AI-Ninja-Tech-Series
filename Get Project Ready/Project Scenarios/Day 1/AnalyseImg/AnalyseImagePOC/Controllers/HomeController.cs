using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PartnerTechSeries.AI.Demo.FaceAPI;


namespace AnalyseImagePOC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Analyse_image()
        {
            return View();
        }

        // POST: ImageAnalyse
        [HttpPost]
        public async Task<JsonResult> ImageAnalyse(string data, bool flag)
        {
            try
            {
                AnalyseImage Ai = new AnalyseImage();
                await Ai.ImageAnalyse(data, flag);
                if (Ai.Erorr == "")  //converting all object array to Json and returning the Json
                    return Json(new { Brand = Ai.Brandarray, Tag = Ai.Tagarray, Object = Ai.Objectarray });
                return Json(new { Erorr = Ai.Erorr });
            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { Erorr = e.Message });
            }

        }
    }
}
       