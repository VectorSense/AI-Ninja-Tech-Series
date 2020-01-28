using System;
using System.Web.Mvc;
using PartnerTechSeries.AI.Demo.VoiceAPI;

namespace SpeakerVerification.Controllers
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

        // GET: Speaker Verification View
        public ActionResult Speaker_verify()
        {
            return View();
        }

        // POST: Speaker Registration
        [HttpPost]
        public JsonResult SpeakerRegistration(string data,string ProfileId)
        {
            try
            {
                VoiceVerification vi = new VoiceVerification();//Creating object for VoiceVerification Class
                vi.SpeakerRegistration(data, ProfileId); // calling Speaker Registration function and passing voice data and ProfileId
                if (vi.Error == "")// Returning Result as a json if no error occur
                    return Json(new { Result = vi.Result, Phrase=vi.Phrase, EnrolledId=vi.EnrollmentId, Error = "" });
                return Json(new { Result = "", EnrolledId = vi.EnrollmentId, Error = vi.Error });// Returning  Error Result as a json if any error occur
            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { Result = "", Error = e.Message });
            }
        }

        // POST: Speaker Identification
        [HttpPost]
        public JsonResult SpeakerVerification(string data)
        {
            try
            {
                VoiceVerification vi = new VoiceVerification();//Creating object for VoiceVerification Class
                vi.SpeakerVerification(data);// calling Speaker Verification function and passing voice data
                if (vi.Error == "")
                    return Json(new { Result = vi.Result, EnrolledId = vi.EnrollmentId, Phrase = vi.Phrase, Error = "" });// Returning Result as a json if no error occur
                return Json(new { Result = vi.Result,  Error = vi.Error });// Returning  Error Result as a json if any error occur
            }
            catch (Exception e)// handling runtime errors and returning error as a Json
            {
                return Json(new { Result = "", Error = e.Message });
            }
        }
    }
}