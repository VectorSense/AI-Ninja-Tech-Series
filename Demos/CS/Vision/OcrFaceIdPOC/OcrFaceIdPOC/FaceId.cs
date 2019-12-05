using System;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace FaceAPI
            {
                public class FaceId
                {
                    public string Error = "",Result="";
                    //Assigning Subscription Key and Face Endpoint from web.config file
                    private string subscriptionKey = ConfigurationManager.AppSettings["FaceIDSubscriptionKey"], FaceIDEndpoint = ConfigurationManager.AppSettings["FaceIDEndpoint"], PersonGroupId = ConfigurationManager.AppSettings["PersonGroupId"];
                    public void FaceRegistration(string data, string name)
                    {
                        try
                        {
                            if (data == "")
                               Error = "Image is Empty";
                            else if (name == "")
                                Error = "Name is Empty";
                            else
                            {
                                byte[] imageBytes = Convert.FromBase64String(data);
                                string PersonId = GetPersonId(name);
                                if (PersonId == "")
                                    Error = "Person Id Not Generated";
                                else
                                {
                                    string PersistedFace = AddFace(imageBytes, PersonId);
                                    if (PersistedFace == "")
                                        Error = "Person's Face Not Found";
                                    else
                                        if (TrainPersonGroup())
                                            Result = "Registered Successfully";
                                        else
                                            Error = "Register Failed";
                                }
                            }
                        }
                        catch (Exception e)// handling runtime errors and returning error as Json
                        {
                            Error = e.Message;
                        }
                    }






                    public void FaceIdentification(string data)
                    {
                        try
                        {
                            if (data == "")
                                Error = "Image is Empty";
                            else
                            {
                                byte[] imageBytes = Convert.FromBase64String(data);
                                string response = DetectFace(imageBytes);
                                if (response == "Detect Face Error" || response == "Identify Face Error" || response == "Getting Person Information Error")
                                    Error = response;
                                else
                                    Result = response;
                            }

                        }
                        catch (Exception e)// handling runtime errors and returning error as Json
                        {
                            Error = e.Message;
                        }
                    }




                    private string GetPersonId(string Name)
                    {
                        try
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
                        catch (Exception)
                        {
                            return "";
                        }
                    }



                    private string AddFace(byte[] imageBytes, string PersonID)
                    {
                        try
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
                        catch (Exception)
                        {
                            return "";
                        }

                    }



                    private bool TrainPersonGroup()
                    {
                        try
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
                        catch (Exception)
                        {
                            return false;
                        }

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

                            string Name = "Face Not Found";
                            for (int i = 0; i < PersonArray.Count; i++)
                            {
                                dynamic PersonData = JObject.Parse(PersonArray[i].ToString());
                                string faceId = PersonData.faceId;
                                Name = IdentifyFace(faceId);
                                i = PersonArray.Count;
                            }
                            return Name;
                        }
                        catch (Exception)
                        {
                            return "Detect Face Error";
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

                            string Name = "Unauthorized Person";
                            for (int i = 0; i < ResponseArray.Count; i++)
                            {
                                dynamic PersonData = JObject.Parse(ResponseArray[i].ToString());
                                JArray PersonArray = JArray.Parse(PersonData.candidates.ToString());
                                for (int j = 0; j < PersonArray.Count; j++)
                                {
                                    dynamic Person = JObject.Parse(PersonArray[j].ToString());
                                    string PersonID = Person.personId;
                                    Name = GetPersonInfo(PersonID);
                                    j = PersonArray.Count;
                                }
                                i = ResponseArray.Count;
                            }
                            return Name;
                        }
                        catch (Exception)
                        {
                            return "Identify Face Error";
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
                            return "Welcome " + PersonData.name;
                        }
                        catch (Exception)
                        {
                            return "Getting Person Information Error";
                        }

                    }


                }
            }
        }
    }
}