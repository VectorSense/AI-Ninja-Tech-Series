using PartnerTechSeries.AI.Demo.FaceAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImageBasedInsuranceFormFilling.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //POST: OCR
        [HttpPost]
        public async Task<JsonResult> InsuranceFormFill(string data)
        {
            try
            {
                OCR ocr = new OCR();
                await ocr.ExtractText(data);
                if (ocr.Error == "")
                {
                    Luis ls = new Luis();
                    ls.DoLuis(ocr.OcrResult);
                    return Json(new { StatusCode = "S", Name = ls.InsName, Number = ls.InsuranceNo, DOB = ls.DOB, Premium = ls.Premium });
                }
                return Json(new { StatusCode = "F", Message = ocr.Error});
            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { StatusCode = "F", Message = e.Message });
            }
        }

    }
}