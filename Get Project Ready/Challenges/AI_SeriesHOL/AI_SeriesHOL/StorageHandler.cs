using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace HOL
        {
            namespace FaceAPI
            {

                public class StorageHandler
                {
                    public static byte[] SaveToFile(string base64data)
                    {
                        return Convert.FromBase64String(base64data);
                    }
                }

                public class VerifyTimeTable
                {
                    //Connection String
                    private static string connectionString = ConfigurationManager.AppSettings["AzureSqlConnectionString"];
                    public string error = "";

                    public bool CheckIn(string personid, string date, string time)
                    {

                        try
                        {
                            // Initialization 
                            SqlConnection conn;
                            SqlCommand cmd;

                            using (conn = new SqlConnection(connectionString))
                            {
                                // Selecting the perticular row in the table and updating that using particular ID 
                                cmd = new SqlCommand("IF NOT EXISTS(SELECT * FROM verifytime WHERE personid = '" + personid + "' AND date = '"+date+ "') INSERT INTO verifytime(personid, date, checkin) VALUES('" + personid + "','" + date + "','" + time + "')", conn);
                                //connection open
                                conn.Open();
                                var temp = cmd.ExecuteNonQuery();
                                //connection close
                                conn.Close();
                                if (temp > 0)
                                    return true;
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            // Returning the result
                            return false;
                        }
                    }


                    public bool CheckOut(string personid,string date,string time)
                    {

                        try
                        {
                            // Initialization 
                            SqlConnection conn;
                            SqlCommand cmd;

                            using (conn = new SqlConnection(connectionString))
                            {
                                // Selecting the perticular row in the table and updating that using particular ID 
                                cmd = new SqlCommand("IF EXISTS(SELECT * FROM verifytime WHERE personid = '" + personid + "' AND date = '" + date + "') UPDATE verifytime set checkout ='" + time + "' WHERE personid = '" + personid + "' AND date = '" + date + "'", conn);
                                //connection open
                                conn.Open();
                                var temp = cmd.ExecuteNonQuery();
                                //connection close
                                conn.Close();
                                if (temp > 0)
                                    return true;
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            // Returning the result
                            return false;
                        }
                    }

                }



                public class FaceRegistrationUserTable
                {
                    //Connection String
                    private static string connectionString = ConfigurationManager.AppSettings["AzureSqlConnectionString"];
                    public string error = "";

                    public bool Add(string name, string phone, string gender, string email, string faceid)
                    {

                        try
                        {
                            // Initialization 
                            SqlConnection conn;
                            SqlCommand cmd;

                            using (conn = new SqlConnection(connectionString))
                            {
                                // Selecting the perticular row in the table and updating that using particular ID 
                                cmd = new SqlCommand("INSERT INTO usertable(name, Phone, gender, email, faceid) VALUES('" + name + "','" + phone + "','" + gender + "','" + email + "','" + faceid + "')", conn);
                                //connection open
                                conn.Open();
                                var temp = cmd.ExecuteNonQuery();
                                //connection close
                                conn.Close();
                                if (temp != 0)
                                    return true;
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            // Returning the result
                            return false;
                        }
                    }
                }


                //Image validation Class - initialization
                public class image_validation
                {
                    public int id { get; set; }

                    public string validation_type { get; set; }

                    public string validation_message { get; set; }

                    public int isactive { get; set; }
                }

                // Image validation - table operations 
                public class ImageValidationTable
                {
                    //Connection String
                    private static string connectionString = ConfigurationManager.AppSettings["AzureSqlConnectionString"];
                    public string error = "";


                    // Select function
                    public List<image_validation>AdminList()
                    {
                        // Image Validation List creation
                        var imagevalidation_list = new List<image_validation>();
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                // Selecting all rows in image validation table
                                SqlCommand cmd = new SqlCommand("SELECT * FROM imagevalidation", conn);
                                //Connection Open 
                                conn.Open();
                                SqlDataReader rdr = cmd.ExecuteReader();
                                while (rdr.Read())
                                {
                                    //Creating Image Validation Object
                                    var imagevalidation_obj = new image_validation();
                                    imagevalidation_obj.id = (int)rdr["id"];
                                    imagevalidation_obj.validation_type = rdr["validation_type"].ToString();
                                    imagevalidation_obj.validation_message = rdr["validation_message"].ToString();
                                    imagevalidation_obj.isactive = (int)rdr["isactive"];
                                    // Adding object file to Model file
                                    imagevalidation_list.Add(imagevalidation_obj);
                                }
                                //Connection Close
                                conn.Close();
                            }
                            // returning the List
                            return imagevalidation_list;
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return imagevalidation_list;
                        }
                    }


                    // Select function
                    public List<bool> UserList()
                    {
                        // Image Validation List creation
                        var imagevalidation_list = new List<bool>();
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                // Selecting all rows in image validation table
                                SqlCommand cmd = new SqlCommand("SELECT * FROM imagevalidation", conn);
                                //Connection Open 
                                conn.Open();
                                SqlDataReader rdr = cmd.ExecuteReader();
                                while (rdr.Read())
                                {                                   
                                    // Adding object file to Model file
                                    if ((int)rdr["isactive"]==0)
                                        imagevalidation_list.Add(true);
                                    else
                                        imagevalidation_list.Add(false);
                                }
                                //Connection Close
                                conn.Close();
                            }
                            // returning the List
                            return imagevalidation_list;
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return imagevalidation_list;
                        }
                    }


                    // Select function by ID
                    public image_validation AdminListById(string data)
                    {
                        // Image Validation object creation
                        var imagevalidation_obj = new image_validation();
                        try
                        {
                            // Initialization
                            SqlConnection conn;
                            SqlDataReader rdr;
                            SqlCommand cmd;

                            var id = Convert.ToInt32(data);

                            using (conn = new SqlConnection(connectionString))
                            {
                                // Selecting all the rows in the image validation 
                                cmd = new SqlCommand("SELECT * FROM imagevalidation where id ='" + id + "'", conn);
                                conn.Open();

                                rdr = cmd.ExecuteReader();
                                while (rdr.Read())
                                {
                                    imagevalidation_obj.id = (int)rdr["id"];
                                    imagevalidation_obj.validation_type = rdr["validation_type"].ToString();
                                    imagevalidation_obj.validation_message = rdr["validation_message"].ToString();
                                    imagevalidation_obj.isactive = (int)rdr["isactive"];
                                }
                                conn.Close();
                            }
                            // Returning object
                            return imagevalidation_obj;
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return imagevalidation_obj;
                        }
                    }


                    // Update function 
                    public bool Modify(string data, string isactive)
                    {
                        try
                        {
                            // Initialization 
                            SqlConnection conn;
                            SqlCommand cmd;
                            var id = Convert.ToInt32(data);

                            using (conn = new SqlConnection(connectionString))
                            {
                                // Selecting the perticular row in the table and updating that using particular ID 
                                cmd = new SqlCommand("update imagevalidation set isactive ='" + isactive + "' where id = '" + id + "'", conn);
                                //connection open
                                conn.Open();
                                var temp = cmd.ExecuteNonQuery();
                                //connection close
                                conn.Close();
                                if (temp != 0)
                                    return true;
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return false;
                        }
                    }
                }


                //Gesture Management Class - initialization

                public class gesture_management
                {

                    public int id { get; set; }

                    public string gesture_name { get; set; }

                    public string thumbnail_url { get; set; }

                    public string gesture_message { get; set; }

                    public int isactive { get; set; }
                }

                // Gesture - table operations 
                class GestureTable
                {
                    //Connection String
                    private static string connectionString = ConfigurationManager.AppSettings["AzureSqlConnectionString"];
                    public string error = "";

                    // Select function
                    public List<gesture_management> List()
                    {
                        // Gesture Management Model Initialization 
                        var gesturemanagement_list = new List<gesture_management>();

                        try
                        {
                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                // Selecting all the rows in the gesture table
                                SqlCommand cmd = new SqlCommand("SELECT * FROM gesture", conn);
                                //connection open
                                conn.Open();
                                SqlDataReader rdr = cmd.ExecuteReader();
                                while (rdr.Read())
                                {
                                    // Creating Gesture management Object file
                                    var gesturemanagement_obj = new gesture_management();
                                    gesturemanagement_obj.id = (int)rdr["id"];
                                    gesturemanagement_obj.gesture_name = rdr["gesture_name"].ToString();
                                    gesturemanagement_obj.thumbnail_url = rdr["thumbnail_url"].ToString();
                                    gesturemanagement_obj.gesture_message = rdr["gesture_message"].ToString();
                                    gesturemanagement_obj.isactive = (int)rdr["isactive"];
                                    // Adding the Object to model file 
                                    gesturemanagement_list.Add(gesturemanagement_obj);
                                }
                                //connection close
                                conn.Close();
                            }
                            // Returning the model file
                            return gesturemanagement_list;
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return gesturemanagement_list;
                        }
                    }

                    // Edit function
                    public gesture_management Edit(string data)
                    {
                        // Image Validation object creation
                        var gesturemanagement_obj = new gesture_management();
                        try
                        {
                            // Initialization
                            SqlConnection conn;
                            SqlDataReader rdr;
                            SqlCommand cmd;

                            var id = Convert.ToInt32(data);

                            using (conn = new SqlConnection(connectionString))
                            {
                                // Selecting all the rows in the image validation 
                                cmd = new SqlCommand("SELECT * FROM gesture where id ='" + id + "'", conn);
                                conn.Open();

                                rdr = cmd.ExecuteReader();
                                while (rdr.Read())
                                {
                                    gesturemanagement_obj.id = (int)rdr["id"];
                                    gesturemanagement_obj.gesture_name = rdr["gesture_name"].ToString();
                                    gesturemanagement_obj.thumbnail_url = rdr["thumbnail_url"].ToString();
                                    gesturemanagement_obj.gesture_message = rdr["gesture_message"].ToString();
                                    gesturemanagement_obj.isactive = (int)rdr["isactive"];
                                }
                                conn.Close();
                            }
                            // Returning object
                            return gesturemanagement_obj;
                        }
                        
                        catch (Exception e)
                        {
                            error = e.Message;
                            // Returning the result
                            return gesturemanagement_obj;
                        }
                    }

                    //Update function
                    public bool Update(string data, string isactive)
                    {
                        try
                        {
                            // Initialization 
                            SqlConnection conn;
                            SqlCommand cmd;
                            var id = Convert.ToInt32(data);

                            using (conn = new SqlConnection(connectionString))
                            {
                                // Selecting the perticular row in the table and updating that using particular ID 
                                cmd = new SqlCommand("update gesture set isactive ='" + isactive + "' where id = '" + id + "'", conn);
                                //connection open
                                conn.Open();
                                var temp = cmd.ExecuteNonQuery();
                                //connection close
                                conn.Close();
                                if (temp != 0)
                                    return true;
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return false;
                        }
                    }

                        //Insert function
                        public bool Add(string name, string url, string message, string active)
                        {
                            try
                            {
                              // Initialization 
                                SqlConnection conn;
                                SqlCommand cmd;

                                using (conn = new SqlConnection(connectionString))
                                {
                                    // Selecting the perticular row in the table and updating that using particular ID 
                                    cmd = new SqlCommand("insert into gesture(gesture_name,thumbnail_url,gesture_message,isactive) values('" + name + "','" + url + "','" + message + "','" + active + "') ", conn);
                                    //connection open
                                    conn.Open();
                                    var temp = cmd.ExecuteNonQuery();
                                    //connection close
                                    conn.Close();
                                    if (temp != 0)
                                        return true;
                                    return false;
                                }
                            }
                            catch (Exception e)
                            {
                                error = e.Message;
                                // Returning the result
                                return false;
                            }
                        }

                    //Random Gestures selection function
                    public List<string> GenerateRandomGesture()
                    {
                        var gesturemanagement_list = new List<string>();
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                // Selecting the all the rows in the Audit Log table
                                SqlCommand cmd = new SqlCommand("SELECT top 10 percent id, gesture_name, thumbnail_url from gesture where isactive = 0 order by newid()", conn);
                                conn.Open();
                                SqlDataReader rdr = cmd.ExecuteReader();
                                while (rdr.Read())
                                {
                                    // Adding the Object to model file 
                                    gesturemanagement_list.Add(rdr["gesture_name"].ToString());
                                    gesturemanagement_list.Add(rdr["thumbnail_url"].ToString());
                                }
                                //connection close
                                conn.Close();
                                return gesturemanagement_list;
                            }
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return gesturemanagement_list;
                        }
                    }
                }



                // Audit Log class - initialization
                public class audit_log
                {
                    public int id { get; set; }

                    public string layer { get; set; }

                    public string result_type { get; set; }

                    public string device_type { get; set; }

                    public string userimage { get; set; }
                }

                class AuditLoggerTable
                {
                    //Connection string
                    private static string connectionString = ConfigurationManager.AppSettings["AzureSqlConnectionString"];
                    public string error = "";

                    // User Insert
                    public bool Add(string layer, string result_type, string imageurl, string device_type = "Web")
                    {
                        try
                        {
                            // Initialization 
                            SqlConnection conn;
                            SqlCommand cmd;

                            using (conn = new SqlConnection(connectionString))
                            {
                                // Selecting the perticular row in the table and updating that using particular ID 
                                cmd = new SqlCommand("INSERT INTO auditlog(layer, result_type, device_type, userimage) VALUES('" + layer + "', '" + result_type + "', '" + device_type + "', '" + imageurl + "')", conn);
                                //connection open
                                conn.Open();
                                var temp = cmd.ExecuteNonQuery();
                                //connection close
                                conn.Close();
                                if (temp != 0)
                                    return true;
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            // Returning the result
                            return false;
                        }
                    }


                    public List<audit_log> List()
                    {
                        // Audit log list creation
                        var auditlog_list = new List<audit_log>();
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                // Selecting the all the rows in gthe Audit Log table
                                SqlCommand cmd = new SqlCommand("SELECT * FROM auditlog order by id desc", conn);
                                conn.Open();
                                SqlDataReader rdr = cmd.ExecuteReader();
                                while (rdr.Read())
                                {
                                    // Creating audit log object file
                                    var auditlog_obj = new audit_log();
                                    auditlog_obj.id = (int)rdr["id"];
                                    auditlog_obj.layer = rdr["layer"].ToString();
                                    auditlog_obj.result_type = rdr["result_type"].ToString();
                                    auditlog_obj.device_type = rdr["device_type"].ToString();
                                    auditlog_obj.userimage = rdr["userimage"].ToString();
                                    // Adding the audit log object file into list file
                                    auditlog_list.Add(auditlog_obj);
                                }
                                //connection close
                                conn.Close();
                            }
                            // returning the list file
                            return auditlog_list;
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return auditlog_list;
                        }
                    }
                }

            }
        }
    }
}