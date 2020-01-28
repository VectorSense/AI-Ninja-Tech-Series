using PartnerTechSeries.AI.HOL.FaceAPI;
using PartnerTechSeries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AI_Series_Starter_Kit.Controllers
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
            return View();
        }

        // Admin - Gesture Management Page
        public ActionResult gesture_management()
        {
            return View();
        }

        // Admin - Audit Log Page
        public ActionResult audit_log()
        {
            return View();
        }


        // User Index
        public ActionResult user_index()
        {
            return View();
        }

        // User - Register Page
        public ActionResult register()
        {
            return View();
        }

        // User - Verify Page
        public ActionResult verify()
        {
            return View();
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


        //Paste the 'ImageValidation_FetchByID' Function code here...

        //Paste the 'ImageValidation_FetchByIsActive' Function code here...

        //Paste the 'GestureManagement_FetchByID' Function code here...

        //Paste the 'GestureManagement_FetchByIsActive' Function code here...

        //Paste the 'GestureManagement_InsertByIsID' Function code here...

        //Paste the 'ImageValidationAPI' Function code here...

        //Paste the 'FaceRegisterAPI' Function code here...

        //Paste the 'VerifyAPI' Function code here...

        //Paste the 'RandomGestureAPI' Function code here...

        //Paste the 'QualityChecking' Function code here...

        //Paste the 'DocumentVerification' Function code here...

    }
}