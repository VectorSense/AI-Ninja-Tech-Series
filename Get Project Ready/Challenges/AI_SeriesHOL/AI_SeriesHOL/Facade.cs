using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AI_SeriesHOL.PartnerTechSeries.AI.HOL.FaceAPI;
using PartnerTechSeries.AI.HOL.FaceAPI;

namespace PartnerTechSeries
{
    public class Facade
    {
        public static byte[] storetoserver(string base64data)
        {
            return StorageHandler.SaveToFile(base64data);
        }

        public static List<List<string>> User_ImageValidation(string realfakecheck,byte[] imagebyte,string url)
        {
            List<List<string>> err = new List<List<string>>();
            err.Add(new List<string>());

            ImageValidationHandler ivhobj = new ImageValidationHandler();

            ImageValidationTable ivtobj = new ImageValidationTable();

            ImageDepthHandler idhobj = new ImageDepthHandler();

            GestureHandler gsobj = new GestureHandler();

            FaceRegistrationHandler fcobj = new FaceRegistrationHandler();


            List<bool> flag = ivtobj.UserList();
            if (ivtobj.error != "")
            {
                err[0].Add("");
                err[0].Add(ivtobj.error);
                return err;
            }

            string result = ivhobj.Validate(url,imagebyte, flag[0], flag[2], flag[1], flag[3]);

            if (result == "0")
            {
                //Real or Face CheckBox
                if (realfakecheck == "Yes")
                {
                    if (idhobj.Validate(imagebyte, url))
                    {
                        if (gsobj.GenerateDefaultGesture(url,imagebyte))
                        {
                            err[0].Add("Success");
                            err[0].Add("");
                            return err;
                        }
                        else
                        {
                            if (gsobj.error != "")
                            {
                                err[0].Add("");
                                err[0].Add(gsobj.error);
                                return err;
                            }
                            err[0].Add("Please follow the Gesture");
                            err[0].Add("");
                            return err;
                        }
                    }
                    else
                    {
                        err[0].Add("You are trying to spoof");
                        err[0].Add("");
                        return err;
                    }

                }
                else
                {
                    if (gsobj.GenerateDefaultGesture(url,imagebyte))
                    {
                        err[0].Add("Success");
                        err[0].Add("");
                        return err;
                    }
                    else
                    {
                        if (gsobj.error != "")
                        {
                            err[0].Add("");
                            err[0].Add(gsobj.error);
                            return err;
                        }
                        err[0].Add("Please follow the Gesture");
                        err[0].Add("");
                        return err;
                    }
                }
            }
            else
            {
                err[0].Add(result);
                err[0].Add("");
                return err;
            }
        }

        public static List<List<string>> User_Registration(string name, string gender, string phone, string email, byte[] ImageUrl)
        {
            FaceRegistrationHandler fc_obj = new FaceRegistrationHandler();
            FaceRegistrationUserTable frt = new FaceRegistrationUserTable();
            List<List<string>> err = new List<List<string>>();
            err.Add(new List<string>());
            string faceid = fc_obj.RegisterFace(ImageUrl, name);
            if (faceid != "")
            {
                frt.Add(name, gender, phone, email, faceid);
                err[0].Add("Registered Successfully");
                err[0].Add("");
                return err;
            }
            else
            {
                if (fc_obj.error != "")
                {
                    err[0].Add("Face Not Found");
                    err[0].Add("");
                    return err;
                }
                err[0].Add("Face Not Found");
                err[0].Add("");
                return err;
            }
        }

        public static List<List<string>> RandomGestureShow()
        {
            GestureTable gtobj = new GestureTable();
            List<List<string>> err = new List<List<string>>();
            err.Add(new List<string>());
            List<string> gsres = gtobj.GenerateRandomGesture();

            if (gtobj.error != "")
            {
                err[0].Add("");
                err[0].Add(gtobj.error);
                return err;
            }
            err[0].Add(gsres[0]);
            err[0].Add(gsres[1]);
            return err;
        }

        public static List<List<string>> User_Verification(string url,byte[] imagebyte, string gesture, string check,bool CheckIn)
        {
            List<List<string>> err = new List<List<string>>();
            err.Add(new List<string>());

            GestureHandler gshobj = new GestureHandler();
            FaceRegistrationHandler frhobj = new FaceRegistrationHandler();
            ImageDepthHandler idh = new ImageDepthHandler();

            if (check == "Yes")
            {
                if (idh.Validate(imagebyte,url))
                {

                    if (gshobj.Validate(url,imagebyte, gesture))
                    {
                        string result = frhobj.VerifyFace(imagebyte, CheckIn);
                        err[0].Add(result);
                        err[0].Add("");
                        return err;
                    }
                    else
                    {
                        if (gshobj.error != "")
                        {
                            err[0].Add("Failed");
                            err[0].Add(gshobj.error);
                            return err;
                        }

                        err[0].Add("Failed");
                        err[0].Add("Please follow the gesture and try again");
                        return err;
                    }
                }
                else
                {
                    err[0].Add("You are trying to spoof");
                    err[0].Add("");
                    return err;
                }
            }
            else
            {
                if (gshobj.Validate(url,imagebyte, gesture))
                {
                    string result = frhobj.VerifyFace(imagebyte, CheckIn);
                    err[0].Add(result);
                    err[0].Add("");
                    return err;

                }
                else
                {
                    if (gshobj.error != "")
                    {
                        err[0].Add("Failed");
                        err[0].Add(gshobj.error);
                        return err;
                    }

                    err[0].Add("Failed");
                    err[0].Add("Please follow the gesture and try again");
                    return err;
                }
            }
        }

        // Image Validation 

        public static List<image_validation> Admin_ImageShow()
        {
            ImageValidationTable ivtobj = new ImageValidationTable();
            
            return ivtobj.AdminList(); 
        }

        public static image_validation Admin_ImageEdit(string id)
        {
            ImageValidationTable ivtobj = new ImageValidationTable();

            return ivtobj.AdminListById(id);
        }
        

        public static bool Admin_ImageUpdate(string id, string isactive)
        {
            ImageValidationTable ivtobj = new ImageValidationTable();

            return ivtobj.Modify(id, isactive);
        }

        //Gesture management

        public static List<gesture_management> Admin_GestureShow()
        {
            GestureTable gstobj = new GestureTable();

            return gstobj.List();
        }

        public static gesture_management Admin_GestureEdit(string id)
        {

            GestureTable gt = new GestureTable();
            return gt.Edit(id);
        }

        public static bool Admin_GestureUpdate(string id, String isactive)
        {

            GestureTable gt = new GestureTable();
            return gt.Update(id,isactive);
        }



        public static bool Admin_GestureAdd(string gesture_name, string thumbnail_url, string gesture_message, string isactive)
        {
            GestureTable gstobj = new GestureTable();

            if (gstobj.Add (gesture_name, thumbnail_url, gesture_message, isactive))
            {
                return true;
            }

            return false;
        }


        public static List<audit_log> Admin_AuditLogShow()
        {
            AuditLoggerTable altobj = new AuditLoggerTable();

            return altobj.List();
        }

        public static QualityControlChecker QualityControlChecker(string base64, bool flag, string check)
        {
            //creating object for Quality control checker 
            QualityControlChecker qcc_obj = new QualityControlChecker();
            //calling the quality check function
            qcc_obj.Quality_Check(base64, flag, check);
            //returning the result
            return (qcc_obj);
        }


        public async Task<DocumentVerificationHandler> DocumentVerificationHandler(string base64, bool flag)
        {
            DocumentVerificationHandler dvh_obj = new DocumentVerificationHandler();
            await dvh_obj.ExtractText(base64, flag);
            return (dvh_obj);
        }

    }
}