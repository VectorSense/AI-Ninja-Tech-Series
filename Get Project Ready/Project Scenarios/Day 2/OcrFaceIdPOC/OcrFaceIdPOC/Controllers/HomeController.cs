using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PartnerTechSeries.AI.Demo.FaceAPI;

namespace OcrFaceIdPOC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult OcrFaceId_Index()
        {
            return View();
        }
        // GET: FaceID
        public ActionResult OcrFaceId_Register()
        {
            return View();
        }
        // GET: FaceID
        public ActionResult OcrFaceId_Verify()
        {
            return View();
        }


        //POST: OCR
        [HttpPost]
        public async Task<JsonResult> OCR(string data)
        {
            try
            {
                OCR ocr = new OCR();
                await ocr.ExtractText(data);
                if (ocr.Error == "")
                {
                    DataExtraction de = new DataExtraction();
                    de.BusinessCardReader(ocr.OCRList);
                    return Json(new { ExtractedText= ocr.OcrResult,Name = de.name, Address = de.address, Phone = de.mobile, CompName = de.company });
                }
                return Json(new { Error = ocr.Error });
            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { Error = e.Message });
            }
        }



        // POST: FaceRegistration
        [HttpPost]
        public JsonResult FaceRegistration(string data, string name)
        {
            try
            {
                FaceId fi = new FaceId();
                fi.FaceRegistration(data, name);
                if (fi.Error == "")
                    return Json(new { Result = fi.Result, Error = "" });
                return Json(new { Result = "", Error = fi.Error });
            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { Result = "", Error = e.Message });
            }
        }

        // POST: FaceIdentification
        [HttpPost]
        public JsonResult FaceIdentification(string data)
        {
            try
            {
                FaceId fi = new FaceId();
                fi.FaceIdentification(data);
                if (fi.Error == "")
                    return Json(new { Result = fi.Result, Error = "" });
                return Json(new { Result = "", Error = fi.Error });

            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { Result = "", Error = e.Message });
            }
        }
    }
}