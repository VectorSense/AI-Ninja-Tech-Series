using AI_SeriesHOL.PartnerTechSeries.AI.HOL.FaceAPI;
using PartnerTechSeries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AI_SeriesHOL.Controllers
{
    public class HomeController : Controller
    {
        // Home Page
        public ActionResult Index()
        {
            return View();
        }

        // Admin Index
        public ActionResult admin_index()
        {
            return View();
        }

        // Admin - Image Validation Page
        public ActionResult image_validation()
        {
            // Calling the intermediator file and Returing the List file to the View 
            return View(Facade.Admin_ImageShow());
        }


        public JsonResult ImageValidation_FetchByID(string data)
        {
            // Calling the intermediator file and get the object file 
            var ImgvalID_obj = Facade.Admin_ImageEdit(data);
            //Returing the Json to view
            return Json(new { id = ImgvalID_obj.id, validation_type = ImgvalID_obj.validation_type, validation_message = ImgvalID_obj.validation_message, isactive = ImgvalID_obj.isactive });

        }

        public JsonResult ImageValidation_FetchByIsActive(string data, string value)
        {
            // Calling the intermediator file and get the object file
            var ImgvalIsActive = Facade.Admin_ImageUpdate(data, value);
            //Returing the Json to view
            return Json(new { Result = ImgvalIsActive });
        }

        //Gesture Management Page - Admin
        public ActionResult gesture_management()
        {
            // Calling the intermediator file and Returing the List file to the View 
            return View(Facade.Admin_GestureShow());
        }

        //Gesture Management dependency Functions
        public JsonResult GestureManagement_FetchByID(string data)
        {
            // Calling the intermediator file and get the object file 
            var gestureManagementID_obj = Facade.Admin_GestureEdit(data);
            //Returing the Json to view
            return Json(new { id = gestureManagementID_obj.id, gesture_name = gestureManagementID_obj.gesture_name, thumbnail_url = gestureManagementID_obj.thumbnail_url, gesture_message = gestureManagementID_obj.gesture_message, isactive = gestureManagementID_obj.isactive });
        }


        public JsonResult GestureManagement_FetchByIsActive(string data, string value)
        {
            // Calling the intermediator file and get the Boolean value 
            var GestureUpdateIsActive = Facade.Admin_GestureUpdate(data, value);

            //Returing the Json to view
            return Json(new { Result = GestureUpdateIsActive });
        }

        public JsonResult GestureManagement_InsertByIsID(string name, string url, string message, string active)
        {
            // Calling the intermediator file and get the Boolean value 
            var gesman_obj = Facade.Admin_GestureAdd(name, url, message, active);

            //Returing the Json to view
            return Json(new { Result = gesman_obj });
        }

        //Audit Log Page - Admin
        public ActionResult audit_log()
        {
            // Calling the intermediator file and Returing the List file to the View 
            return View(Facade.Admin_AuditLogShow());
        }


        //User

        public ActionResult user_index()
        {
            return View();
        }


        public ActionResult register()
        {
            return View();
        }

        public ActionResult verify()
        {
            return View();
        }

        public JsonResult ImageValidationAPI(string data, string check)
        {
            string imgefile = "Img" + $@"{System.DateTime.Now.Ticks}.jpg";
            string Url = Server.MapPath(@"~\Images\" + imgefile);
            System.IO.File.WriteAllBytes(Url, Convert.FromBase64String(data));
            var imagebyte = Facade.storetoserver(data);

            List<List<string>> result = Facade.User_ImageValidation(check, imagebyte, imgefile);

            if (result[0][1] == "")
            {
                return Json(new { Result = result[0][0] });
            }

            return Json(new { Result = "Failed" });
        }

        //Face Register API
        public JsonResult FaceRegisterAPI(string data, string name, string gender, string phone, string email)
        {
            var imagebyte = Facade.storetoserver(data);

            List<List<string>> result = Facade.User_Registration(name, gender, phone, email, imagebyte);

            if (result[0][1] == "")
            {
                return Json(new { Result = result[0][0] });
            }   

            return Json(new { Result = result[0][1] });

        }

        //Verify API
        public JsonResult VerifyAPI(string data, string check, string random_gesture,bool CheckIn)
        {
            var imagebyte = Facade.storetoserver(data);
            string imgefile = "Img" + $@"{System.DateTime.Now.Ticks}.jpg";
            string Url = Server.MapPath(@"~\Images\" + imgefile);
            System.IO.File.WriteAllBytes(Url, Convert.FromBase64String(data));

            List<List<string>> result = Facade.User_Verification(imgefile, imagebyte, random_gesture, check,CheckIn);

            if (result[0][1] == "")
            {
                return Json(new { Result = "Success", VerifiedName = result[0][0] });
            }

            return Json(new { Result = "Failed", VerifiedName = result[0][1] });
        }

        //Random Gesture API
        public JsonResult RandomGestureAPI()
        {
            List<List<string>> result = Facade.RandomGestureShow();

            return Json(new { GestureName = result[0][0], GestureUrl = result[0][1] });
        }

        // Legal Document Verification
        public ActionResult document_verification()
        {
            return View();
        }

        //Quality Control Check
        public ActionResult quality_control_check()
        {
            return View();
        }

        public JsonResult QualityChecking(String data, bool flag, string check)
        {
            // Calling the fuction from facade class
            var fa_obj = Facade.QualityControlChecker(data, flag, check);

            try
            {
                // Checking the error
                if (fa_obj.error == "")
                {
                    // returning the result
                    return Json(new { TagName = fa_obj.TagName, Error = "" });
                }
                return Json(new { TagName = "", Error = fa_obj.error });
            }
            catch(Exception e)
            {
                return Json(new { TagName = "", Error = e.Message });
            }
        }

        public async Task<JsonResult> DocumentVerification(string data, bool flag)
        {
            DocumentVerificationHandler dvh_obj = new DocumentVerificationHandler();

            await dvh_obj.ExtractText(data, flag);

            if (dvh_obj.Error == "")
            {
                return Json(new { Contract_Date = dvh_obj.ContractDate, Vendor_Name = dvh_obj.VendorName, Client_Name = dvh_obj.ClientName, Service_Description = dvh_obj.Services, Contract_Value = dvh_obj.ContractValue, End_Date = dvh_obj.EndDate, Penalty_Value = dvh_obj.PenaltyValue, Jurisdiction_Place = dvh_obj.JurisdictionPlace, Vendor_Email = dvh_obj.VendorEmail, Vendor_Phone = dvh_obj.VendorPhone, Client_Email = dvh_obj.ClientEmail, Client_Phone = dvh_obj.ClientPhone, Error = "" });
            }
            return Json(new { Summary = "", Error = dvh_obj.Error });
        }

    }
}
