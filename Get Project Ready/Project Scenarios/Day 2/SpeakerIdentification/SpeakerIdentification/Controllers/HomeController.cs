using System;
using System.Web.Mvc;
using PartnerTechSeries.AI.Demo.VoiceAPI;

namespace SpeakerIdentification.Controllers
{
    public class HomeController : Controller
    {
        // GET: Index View
        public ActionResult Index()
        {
            return View();
        }

        // GET: Speaker Registration View
        public ActionResult Speaker_register()
        {
            return View();
        }

        // GET: Speaker Identification View
        public ActionResult Speaker_identify()
        {
            return View();
        }

        // POST: Speaker Registration
        [HttpPost]
        public JsonResult SpeakerRegistration(string data, string name)
        {
            try
            {
                VoiceIdentification vi = new VoiceIdentification();//Creating object for VoiceIdentification Class
                vi.SpeakerRegistration(data, name);// calling Speaker Registration function and passing voice data and name
                if (vi.Error == "")// Returning Result as a json if no error occur
                    return Json(new { Result = vi.Result, Error = "" });
                return Json(new { Result = "", Error = vi.Error });// Returning  Error Result as a json if any error occur
            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { Result = "", Error = e.Message });
            }
        }

        // POST: Speaker Identification
        [HttpPost]
        public JsonResult SpeakerIdentification(string data)
        {
            try
            {
                VoiceIdentification vi = new VoiceIdentification();//Creating object for VoiceIdentification Class
                vi.SpeakerIdentification(data);
                if (vi.Error == "")// Returning Result as a json if no error occur
                    return Json(new { Result = vi.Result, Error = "" });
                return Json(new { Result = "", Error = vi.Error });// Returning  Error Result as a json if any error occur

            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { Result = "", Error = e.Message });
            }
        }
    }
}