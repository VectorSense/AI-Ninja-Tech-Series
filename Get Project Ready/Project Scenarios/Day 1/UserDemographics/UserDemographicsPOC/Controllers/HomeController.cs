using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using PartnerTechSeries.AI.Demo.FaceAPI;

namespace UserDemographicsPOC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult User_demographics()
        {
            return View();
        }

        // POST: FaceAttribute
        [HttpPost]
        public async Task<JsonResult> FaceAttribute(string data, bool flag)
        {
            try
            {
                UserDemographics ud = new UserDemographics();
                await ud.DetectFaceAttribute(data, flag);
                if(ud.Erorr=="")//converting all face's attribute as Json and returning the Json
                    return Json(new { Face = ud.Jarray, MaleCount = ud.MCount, FemaleCount = ud.FCount, Total = ud.MCount+ud.FCount });
                return Json(new { Erorr = ud.Erorr });
            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { Erorr = e.Message });
            }

        }

    }
}