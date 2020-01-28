using System;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace VoiceAPI
            {
                public class VoiceVerification
                {
                    public string Error = "", Result = "",Phrase="",EnrollmentId="";
                    //Assigning Subscription Key and Voice Endpoint from web.config file
                    private string subscriptionKey = ConfigurationManager.AppSettings["VoiceKey"], VoiceIDEndpoint = ConfigurationManager.AppSettings["EndPoint"], connectionString = ConfigurationManager.AppSettings["connectionString"];
                    public void SpeakerRegistration(string data, string profileId) //Speaker Registration is happening
                    {
                        try
                        {
                            if (data == "")
                                Error = "No Voice Data Found";
                            else
                            {
                                if (profileId == "")
                                    profileId = CreateProfileId(); //creating profile id

                                if (profileId == "")
                                    Error = "Profile Id Not Generated";
                                else
                                {
                                    EnrollmentId = profileId;
                                    byte[] VoiceBytes = Convert.FromBase64String(data); //converting base64 voice data to Bytes
                                    List<string> EnrollStatus = EnrollProfile(profileId, VoiceBytes); //Enrolling Profile
                                    if(EnrollStatus.Count==1)//assigning api error message
                                        Error = EnrollStatus[0];
                                    else if(EnrollStatus.Count == 0) //assigning Enrollment Failed if any runtime error occur
                                        Error = "Enrollment Failed";
                                    else//assigning Status and Phrase
                                    {
                                        Result = EnrollStatus[0];
                                        Phrase = EnrollStatus[1];

                                    }
                                }
                            }
                        }
                        catch (Exception e)// handling runtime errors and returning error as Json
                        {
                            Error = e.Message;
                        }
                    }


                    public void SpeakerVerification(string data) //Speaker Verification happening
                    {
                        try
                        {
                            if (data == "")
                                Error = "No Voice Data Found";
                            else
                            {
                                byte[] VoiceBytes = Convert.FromBase64String(data); //converting base64 voice data to Voice Bytes
                                List<string> ids = GetAllEnrolledId(); //Getting all Enrolled Profile Id
                                if (ids.Count == 0) // passing error if no id found
                                    Error = "No Registered Ids Found";
                                else
                                {
                                    List<string> response = Identification(ids, VoiceBytes); //Speaker Verification happening
                                    if (response.Count == 0) // returning Identification Failed if any run time error occur
                                        Error = "Identification Failed";
                                    else if (response.Count == 1)// assigning api error messages
                                        Error = response[0];
                                    else if (response.Count == 2) //assinging  Rejected message
                                        Result = response[0];
                                    else// assigning Accepted message, phrase and Enrollment id
                                    {
                                        Result = response[0];
                                        Phrase = response[1];
                                        EnrollmentId= response[2];
                                    }

                                }

                            }
                        }
                        catch (Exception e)// handling runtime errors and returning error as Json
                        {
                            Error = e.Message;
                        }
                    }




                    private string CreateProfileId() // Creating Profile
                    {
                        try
                        {
                            var client = new RestClient(VoiceIDEndpoint + "/spid/v1.0/verificationProfiles");
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
                            request.AddParameter("undefined", "{\"locale\":\"en-us\"}", ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);

                            dynamic PersonData = JObject.Parse(response.Content);

                            foreach (JProperty prop in PersonData.Properties())
                                if (prop.Name == "error")
                                    return "";

                            return PersonData.verificationProfileId;
                        }
                        catch (Exception)
                        {
                            return "";
                        }
                    }


                    private List<string> EnrollProfile(string profileId,byte[] voiceBytes)// Enrolling Profile
                    {
                        List<string> Response = new List<string>();
                        try
                        {
                            var client = new RestClient(VoiceIDEndpoint + "/spid/v1.0/verificationProfiles/" + profileId + "/enroll");
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
                            request.AddHeader("content-type", "application/octet-stream");
                            request.AddParameter("undefined", voiceBytes, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);

                            dynamic PersonData = JObject.Parse(response.Content);

                            foreach (JProperty prop in PersonData.Properties())
                            {
                                if (prop.Name == "error")
                                {
                                    dynamic ErrorData = JObject.Parse(PersonData.error.ToString());
                                    Response.Add(ErrorData.message.ToString());
                                    return Response;
                                }
                            }
                            Response.Add(PersonData.enrollmentStatus.ToString());
                            Response.Add(PersonData.phrase.ToString());
                            return Response;
                        }
                        catch (Exception)
                        {
                            return Response;
                        }
                    }



                    private string EnrolledStatus(string profileId)// Checking Enrollment Status
                    {
                        try
                        {
                            var client = new RestClient(VoiceIDEndpoint + "/spid/v1.0/verificationProfiles/" + profileId);
                            var request = new RestRequest(Method.GET);
                            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
                            IRestResponse response = client.Execute(request);
                            dynamic PersonData = JObject.Parse(response.Content);

                            foreach (JProperty prop in PersonData.Properties())
                                if (prop.Name == "error")
                                    return "";

                            return PersonData.enrollmentStatus;
                        }
                        catch (Exception)
                        {
                            return "";
                        }
                    }




                    private List<string> GetAllEnrolledId()// Getting All Enrolled id
                    {
                        List<string> Response = new List<string>();
                        try
                        {
                            var client = new RestClient(VoiceIDEndpoint + "/spid/v1.0/verificationProfiles");
                            var request = new RestRequest(Method.GET);
                            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
                            IRestResponse response = client.Execute(request);

                            JArray PersonArray = JArray.Parse(response.Content);

                            
                            for (int i = 0; i < PersonArray.Count; i++)
                            {
                                dynamic PersonData = JObject.Parse(PersonArray[i].ToString());
                                if (PersonData.enrollmentStatus == "Enrolled")
                                {
                                    Response.Add(PersonData.verificationProfileId.ToString());
                                }
                            }
                            return Response;
                        }
                        catch (Exception)
                        {
                            return Response;
                        }
                    }


                    private List<string> Identification(IList<string> profileIds,byte[] voiceByte)// Speaker Verification
                    {
                        List<string> Response = new List<string>();
                        try
                        {
                            for (int i = 0; i < profileIds.Count; i++)
                            {
                                var client = new RestClient(VoiceIDEndpoint + "/spid/v1.0/verify?verificationProfileId=" + profileIds[i]);
                                var request = new RestRequest(Method.POST);
                                request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
                                request.AddHeader("content-type", "application/octet-stream");
                                request.AddParameter("undefined", voiceByte, ParameterType.RequestBody);
                                IRestResponse response = client.Execute(request);
                                dynamic PersonData = JObject.Parse(response.Content);

                                foreach (JProperty prop in PersonData.Properties()) {
                                    if (prop.Name == "error")
                                    {
                                        dynamic ErrorData = JObject.Parse(PersonData.error.ToString());
                                        Response.Add(ErrorData.message.ToString());
                                        return Response;
                                    }
                                }

                                if (PersonData.result == "Accept")
                                {
                                    Response.Add("Accepted");
                                    Response.Add(PersonData.phrase.ToString());
                                    Response.Add(profileIds[i].ToString());
                                    return Response;
                                }
                            }
                            Response.Add("Rejected");
                            Response.Add("");
                            return Response;
                        }
                        catch (Exception)
                        {
                            return Response;
                        }
                    }
                }
            }
        }
    }
}