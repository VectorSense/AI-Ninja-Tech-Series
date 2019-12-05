using System;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Data.SqlClient;
using System.Threading;


namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace VoiceAPI
            {
                public class VoiceIdentification
                {
                    public string Error = "", Result = "";
                    //Assigning Subscription Key and Voice Endpoint from web.config file
                    private string subscriptionKey = ConfigurationManager.AppSettings["VoiceKey"], VoiceIDEndpoint = ConfigurationManager.AppSettings["EndPoint"], connectionString = ConfigurationManager.AppSettings["connectionString"];
                    public void SpeakerRegistration(string data, string name)//Speaker Registration is happening
                    {
                        try
                        {
                            if (data == "")
                                Error = "No Voice Data Found";
                            else if (name == "")
                                Error = "Name is Empty";
                            else
                            {
                                byte[] VoiceBytes = Convert.FromBase64String(data); //converting voice data to Voice B
                                string profileId = CreateProfileId();//creating profile id
                                if (profileId == "")
                                    Error = "Profile Id Not Generated";
                                else
                                {
                                    if(EnrollProfile(profileId, VoiceBytes))//Enrolling Profile
                                    {

                                        Thread.Sleep(3000);
                                        string status = EnrolledStatus(profileId);// checking Enrollment status
                                        if (status!="")
                                        {
                                            if (AddProfileId(profileId, name))// inserting profile id and name in database
                                                Result = status;
                                            else
                                                Error = "Name Enrollment in Database Failed";
                                        }
                                        else
                                           Result = "Enrollment Failed";
                                    }
                                    else
                                        Error = "Enrollment Problem";
                                }
                            }
                        }
                        catch (Exception e)// handling runtime errors and returning error as Json
                        {
                            Error = e.Message;
                        }
                    }


                    public void SpeakerIdentification(string data) //Speaker identification
                    {
                        try
                        {
                            if (data == "")
                                Error = "No Voice Data Found";
                            else
                            {
                                byte[] VoiceBytes = Convert.FromBase64String(data);//converting voice data to voice bytes
                                string ids = GetAllEnrolledId();// Getting all Enrolled profile id
                                if (ids == "")
                                    Result = "No Registered Ids Found";
                                else
                                {
                                    string OperationLoc = Identification(ids, VoiceBytes); // speaker identification happening

                                    if (OperationLoc == "")
                                        Error = "Voice Identification Failed";
                                    else
                                    {
                                        Thread.Sleep(3000);
                                        string ProfileId = GetProfileId(OperationLoc); // getting user's voice profile id
                                        if(ProfileId=="")
                                            Error = "Voice Identification Id Getting Problem";
                                        else if (ProfileId == "notstarted"|| ProfileId == "running" || ProfileId == "failed")
                                        {
                                            Error = "The operation is " + ProfileId;
                                        }
                                        else
                                        {
                                            string ProfileName = GetPersonName(ProfileId);// getting name from database
                                            if (ProfileName == "")
                                                Result = "Unauthorized Speaker";
                                            else
                                                Result = "Identified Speaker is " + ProfileName;
                                        }

                                    }

                                }
                                
                            }

                        }
                        catch (Exception e)// handling runtime errors and returning error as Json
                        {
                            Error = e.Message;
                        }
                    }




                    private string CreateProfileId()// creating profile
                    {
                        try
                        {

                            var client = new RestClient(VoiceIDEndpoint + "/spid/v1.0/identificationProfiles");
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
                            request.AddParameter("undefined", "{\"locale\":\"en-us\"}", ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);

                            dynamic PersonData = JObject.Parse(response.Content);

                            foreach (JProperty prop in PersonData.Properties())
                                if (prop.Name == "error")
                                    return "";

                            return PersonData.identificationProfileId;
                        }
                        catch (Exception)
                        {
                            return "";
                        }
                    }


                    private bool EnrollProfile(string profileId,byte[] voiceBytes)//Enrolling Profile
                    {
                        try
                        {

                            var client = new RestClient(VoiceIDEndpoint +"/spid/v1.0/identificationProfiles/"+ profileId +"/enroll?shortAudio=true");
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
                            request.AddHeader("content-type", "application/octet-stream");
                            request.AddParameter("undefined", voiceBytes, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);

                            if (response.Content.Length == 0)
                                return true;
                            else
                                return false;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }



                    private string EnrolledStatus(string profileId)// Checking Enrollment status
                    {
                        try
                        {
                            var client = new RestClient(VoiceIDEndpoint + "/spid/v1.0/identificationProfiles/"+ profileId);
                            var request = new RestRequest(Method.GET);
                            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
                            IRestResponse response = client.Execute(request);
                            dynamic PersonData = JObject.Parse(response.Content);

                            foreach (JProperty prop in PersonData.Properties())
                                if (prop.Name == "error")
                                    return "";

                            if (PersonData.enrollmentStatus == "Training" || PersonData.enrollmentStatus == "Enrolling")
                                return "Profile is currently " + PersonData.enrollmentStatus + " and is not ready for verification";

                            else if (PersonData.enrollmentStatus == "Enrolled")
                                return "Profile is enrolled and is ready for verification";
                            else
                                return "";
                        }
                        catch (Exception)
                        {
                            return "";
                        }
                    }




                    private string GetAllEnrolledId()// Getting all Enrolled profile id
                    {
                        try
                        {
                            var client = new RestClient(VoiceIDEndpoint +"/spid/v1.0/identificationProfiles");
                            var request = new RestRequest(Method.GET);
                            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
                            IRestResponse response = client.Execute(request);

                            JArray PersonArray = JArray.Parse(response.Content);

                            string Ids = "";
                            int j = 0;
                            for (int i = 0; i < PersonArray.Count; i++)
                            {
                                dynamic PersonData = JObject.Parse(PersonArray[i].ToString());
                                if (PersonData.enrollmentStatus == "Enrolled" && Ids == "")
                                {
                                    Ids += PersonData.identificationProfileId;
                                    j++;

                                }

                                else if (PersonData.enrollmentStatus == "Enrolled")
                                {
                                    Ids += "," + PersonData.identificationProfileId;
                                    j++;
                                }

                                if (j == 10)
                                    break;
                            }
                            return Ids;
                        }
                        catch (Exception)
                        {
                            return "";
                        }
                    }


                    private string Identification(string profileIds,byte[] voiceByte)// speaker identification 
                    {
                        try
                        {
                            var client = new RestClient(VoiceIDEndpoint + "/spid/v1.0/identify?identificationProfileIds=" + profileIds + "&shortAudio=true");
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
                            request.AddHeader("content-type", "application/octet-stream");
                            request.AddParameter("undefined", voiceByte, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            if (response.Content.Length == 0)
                            {
                                string Location = response.Headers.ToList().Find(x => x.Name == "Operation-Location").Value.ToString();
                                return Location;
                            }
                            else
                                return "";
                        }
                        catch (Exception)
                        {
                            return "";
                        }
                    }




                    private string GetProfileId(string operationUrl)// getting profile id
                    {
                        try
                        {

                            var client = new RestClient(operationUrl);
                            var request = new RestRequest(Method.GET);
                            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
                            IRestResponse response = client.Execute(request);

                            dynamic PersonData = JObject.Parse(response.Content);
                            //string Resultresponse = PersonData.processingResult.ToString();
                            if (PersonData.status == "succeeded")
                            {
                                dynamic processingResult = JObject.Parse(PersonData.processingResult.ToString());
                                return processingResult.identifiedProfileId.ToString();
                            }
                            else if (PersonData.status == "notstarted" || PersonData.status == "running" || PersonData.status == "failed")
                                return PersonData.status;
                            else
                                return "";

                        }
                        catch (Exception)
                        {
                            return "";
                        }
                    }

                    public bool AddProfileId(string Id,string name)// inserting id and name in database
                    {
                        try
                        {
                            // Initialization
                            SqlConnection conn;
                            SqlCommand cmd;

                            using (conn = new SqlConnection(connectionString))
                            {
                                // Selecting all the rows in the image validation 
                                cmd = new SqlCommand("insert into voice(voiceid,name) values('" + Id + "','" + name + "') ", conn);
                                conn.Open();
                                var temp = cmd.ExecuteNonQuery();
                                //connection close
                                conn.Close();
                                if (temp != 0)
                                    return true;
                                return false;
                                
                            }
                        }
                        catch (Exception)
                        {
                         return false;
                        }
                    }



                    public string GetPersonName(string ProfileId)// getting person name by profile id
                    {
                       try
                        {
                            // Initialization
                            SqlConnection conn;
                            SqlDataReader rdr;
                            SqlCommand cmd;

                            using (conn = new SqlConnection(connectionString))
                            {
                                // Selecting all the rows in the voice Table 
                                conn.Open();
                                cmd = new SqlCommand("SELECT name FROM voice where voiceid ='" + ProfileId + "'", conn);
                                
                                rdr = cmd.ExecuteReader();
                                string Name = "";
                                //string ID = "";
                                while (rdr.Read())
                                {
                                    Name = rdr["name"].ToString();
                                    //ID= rdr["voiceid"].ToString();
                                }
                                conn.Close();
                                return Name;

                            }
                        }
                        catch (Exception)
                        {
                            return "";
                        }
                    }
                }
            }
        }
    }
}