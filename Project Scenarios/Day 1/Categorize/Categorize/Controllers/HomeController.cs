using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using PartnerTechSeries.AI.Demo.FaceAPI;

namespace Categorize.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Categorize_image()
        {
            return View();
        }

        // POST: ImageCategorize
        [HttpPost]
        public async Task<JsonResult> ImageCategorize(string data, bool flag)
        {
            try
            {
                CategorizeImage ci = new CategorizeImage();
                await ci.ImageCategorize(data, flag);
                if (ci.Erorr == "")
                    return Json(new { Categorize = ci.Catearray, Description = ci.Descriparray });//converting all object array to Json and returning the Json
                return Json(new { Erorr = ci.Erorr });

            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { Erorr = e.Message });
            }

        }

    }
}