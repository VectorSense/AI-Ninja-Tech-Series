using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Configuration;
namespace PartnerTechSeries
{
    namespace AI
    {
        namespace HOL
        {
            namespace FaceAPI
            {

                public class FaceRegistrationHandler
                {
                    public string error = "";
                    //Assigning Subscription Key and Face Endpoint from web.config file
                    private string subscriptionKey = ConfigurationManager.AppSettings["FaceAPIKey"], FaceIDEndpoint = ConfigurationManager.AppSettings["FaceAPIEndPoint"], PersonGroupId = ConfigurationManager.AppSettings["PersonGroupId"];


                    public string RegisterFace(byte[] imageBytes, string name)
                    {
                        try
                        {
                            string PersonId = GetPersonId(name);
                            if (PersonId == "")
                                return "";
                            else
                            {
                                string PersistedFace = AddFace(imageBytes, PersonId);
                                if (PersistedFace == "")
                                    return "";
                                else
                                    if (TrainPersonGroup())
                                    return PersonId;
                                return "";
                            }
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return "";
                        }
                    }

                    public string VerifyFace(byte[] imageBytes,bool CheckIn)
                    {
                        try
                        {
                            string faceid = DetectFace(imageBytes);
                            if (faceid == "")
                                return "Face Not Found";
                            else
                            {
                                string personid = IdentifyFace(faceid);
                                if (personid == "")
                                    return "Unauthorized Person";
                                else
                                {
                                    DateTime dt = DateTime.Now;
                                    string date = dt.ToString("dd/MM/yyyy"); // Will give you smth like 25/05/2011
                                    string time = dt.ToString("hh:mm tt"); //Output: 11:00 PM
                                    VerifyTimeTable vtt = new VerifyTimeTable();
                                    string name = GetPersonInfo(personid);
                                    if (CheckIn)
                                    {
                                         if (vtt.CheckIn(personid, date, time))
                                            return "Welcome " + name + ", You Checked-In at " + time;
                                        else
                                            return "Sorry " + name + ", You are Already Checked-In";
                                    }
                                    else
                                    {
                                        if (vtt.CheckOut(personid, date, time))
                                            return "Good Bye " + name + ", You Checked-Out at " + time;
                                        else
                                            return "Please Check-In " + name;
                                    }
                                }
                            }

                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return "";
                        }
                    }


                    private string GetPersonId(string Name)
                    {
                        var client = new RestClient(FaceIDEndpoint + "/face/v1.0/persongroups/" + PersonGroupId + "/persons");
                        var request = new RestRequest(Method.POST);

                        request.AddHeader("ocp-apim-subscription-key", subscriptionKey);
                        request.AddHeader("content-type", "application/json");
                        request.AddParameter("application/json", "{\r\n    \"name\": \"" + Name + "\"\r\n}", ParameterType.RequestBody);

                        IRestResponse response = client.Execute(request);
                        dynamic PersonData = JObject.Parse(response.Content);

                        foreach (JProperty prop in PersonData.Properties())
                            if (prop.Name == "error")
                                return "";
                        return PersonData.personId;
                    }



                    private string AddFace(byte[] imageBytes, string PersonID)
                    {
                        var client = new RestClient(FaceIDEndpoint + "/face/v1.0/persongroups/" + PersonGroupId + "/persons/" + PersonID + "/persistedFaces");
                        var request = new RestRequest(Method.POST);

                        request.AddHeader("ocp-apim-subscription-key", subscriptionKey);
                        request.AddHeader("content-type", "application/octet-stream");
                        request.AddParameter("undefined", imageBytes, ParameterType.RequestBody);

                        IRestResponse response = client.Execute(request);

                        dynamic FaceData = JObject.Parse(response.Content);

                        foreach (JProperty prop in FaceData.Properties())
                            if (prop.Name == "error")
                                return "";

                        return FaceData.persistedFaceId;
                    }



                    private bool TrainPersonGroup()
                    {
                        var client = new RestClient(FaceIDEndpoint + "/face/v1.0/persongroups/" + PersonGroupId + "/train");
                        var request = new RestRequest(Method.POST);

                        request.AddHeader("ocp-apim-subscription-key", subscriptionKey);
                        request.AddHeader("content-type", "application/json");

                        IRestResponse response = client.Execute(request);
                        if (response.Content.Length == 0)
                            return true;
                        else
                            return false;
                    }



                    private string DetectFace(byte[] imageBytes)
                    {
                        try
                        {

                            var client = new RestClient(FaceIDEndpoint + "/face/v1.0/detect?returnFaceId=true&returnFaceLandmarks=false&recognitionModel=recognition_01&returnRecognitionModel=false");
                            var request = new RestRequest(Method.POST);

                            request.AddParameter("undefined", imageBytes, ParameterType.RequestBody);
                            request.AddHeader("ocp-apim-subscription-key", subscriptionKey);
                            request.AddHeader("content-type", "application/octet-stream");

                            IRestResponse response = client.Execute(request);
                            JArray PersonArray = JArray.Parse(response.Content);

                            string faceId = "";
                            for (int i = 0; i < PersonArray.Count; i++)
                            {
                                dynamic PersonData = JObject.Parse(PersonArray[i].ToString());
                                faceId = PersonData.faceId;
                                i = PersonArray.Count;
                            }
                            return faceId;
                        }
                        catch (Exception)
                        {
                            return "";
                        }
                    }



                    private string IdentifyFace(string FaceID)
                    {
                        try
                        {
                            var client = new RestClient(FaceIDEndpoint + "/face/v1.0/identify");
                            var request = new RestRequest(Method.POST);

                            request.AddHeader("ocp-apim-subscription-key", subscriptionKey);
                            request.AddHeader("content-type", "application/json");
                            request.AddParameter("application/json", "{\r\n    \"personGroupId\": \"" + PersonGroupId + "\",\r\n    \"faceIds\": [\r\n        \"" + FaceID + "\"\r\n    ],\r\n    \"maxNumOfCandidatesReturned\": 1,\r\n    \"confidenceThreshold\": 0.5\r\n}", ParameterType.RequestBody);

                            IRestResponse response = client.Execute(request);
                            JArray ResponseArray = JArray.Parse(response.Content);

                            string personid = "";
                            for (int i = 0; i < ResponseArray.Count; i++)
                            {
                                dynamic PersonData = JObject.Parse(ResponseArray[i].ToString());
                                JArray PersonArray = JArray.Parse(PersonData.candidates.ToString());
                                for (int j = 0; j < PersonArray.Count; j++)
                                {
                                    dynamic Person = JObject.Parse(PersonArray[j].ToString());
                                    personid = Person.personId;
                                    j = PersonArray.Count;
                                }
                                i = ResponseArray.Count;
                            }
                            return personid;
                        }
                        catch (Exception)
                        {
                            return "";
                        }
                    }

                    private string GetPersonInfo(string personId)
                    {
                        try
                        {
                            var client = new RestClient(FaceIDEndpoint + "/face/v1.0/persongroups/" + PersonGroupId + "/persons/" + personId);
                            var request = new RestRequest(Method.GET);

                            request.AddHeader("ocp-apim-subscription-key", subscriptionKey);
                            request.AddHeader("content-type", "application/json");
                            IRestResponse response = client.Execute(request);

                            dynamic PersonData = JObject.Parse(response.Content);
                            return PersonData.name;
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