using System;
using System.Web.Mvc;
using PartnerTechSeries.AI.Demo.FaceAPI;

namespace FaceIdPOC.Controllers
{
    public class FaceIDController : Controller
    {
        // GET: FaceID
        public ActionResult FaceID_index()
        {
            return View();
        }

        // GET: FaceID
        public ActionResult FaceID_register()
        {
            return View();
        }

        // GET: FaceID
        public ActionResult FaceID_verify()
        {
            return View();
        }
        
        // POST: FaceRegistration
        [HttpPost]
        public JsonResult FaceRegistration(string data,string name)
        {
            try
            {
                FaceId fi = new FaceId();
                fi.FaceRegistration(data, name);
                if(fi.Error=="")
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