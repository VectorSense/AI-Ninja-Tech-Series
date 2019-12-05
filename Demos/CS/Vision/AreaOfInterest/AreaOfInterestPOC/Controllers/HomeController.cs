using PartnerTechSeries.AI.Demo.FaceAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AreaOfInterestPOC.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            
            return View();
        }
        // Area of Interest function
        public JsonResult AreaofInterest(string data, bool flag)
        {
            try
            {
                // Creating object for AreaOfInterest class
                AreaOfInterest aoi_obj = new AreaOfInterest();
                // Calling the GetAreaOfInterest 
                aoi_obj.GetAreaofInterest(data, flag);
                //Checking error is empty or not 
                if (aoi_obj.Error == "")  //converting all object array to Json and returning the Json
                    return Json(new { Left = aoi_obj.Left, Top = aoi_obj.Top, Width = aoi_obj.Width,  Height = aoi_obj.Height, Error = "" });
                else
                    return Json(new { Left = "", Top = "", Width = "", Height = "", Error = aoi_obj.Error });   
            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { Erorr = e.Message });
            }
        }
    }
}