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
                public class GestureHandler
                {
                    //Assigning Subscription Key and Face Endpoint from web.config file
                    private static string GestureAPIKey = ConfigurationManager.AppSettings["GestureKey"], GestureAPIEndpoint = ConfigurationManager.AppSettings["GestureAPICall"], GestureProjectID = ConfigurationManager.AppSettings["GestureProjectID"], GestureIteration = ConfigurationManager.AppSettings["GestureIteration"];
                    public string error = "";

                    //DB check
                    AuditLoggerTable alt = new AuditLoggerTable();

                    public bool Validate(string url, byte[] imageBytes, string gesture)
                    {
                        string tagname = "";
                        try
                        {
                            //Gesture API Call
                            var client1 = new RestClient(GestureAPIEndpoint + "/customvision/v3.0/Prediction/"+ GestureProjectID + "/detect/iterations/"+ GestureIteration +"/image");
                            var request1 = new RestRequest(Method.POST);
                            request1.AddHeader("cache-control", "no-cache");
                            request1.AddHeader("Content-Type", "application/octet-stream");
                            request1.AddHeader("Prediction-Key", GestureAPIKey);
                            request1.AddParameter("data", imageBytes, ParameterType.RequestBody);
                            IRestResponse response1 = client1.Execute(request1);

                            var result = response1.Content;

                            dynamic objjsn = JObject.Parse(result);
                            var jsnar = objjsn.predictions.ToString();
                            JArray ar = JArray.Parse(jsnar);
                            int max = 0;
                            for (int i = 0; i < ar.Count; i++)
                            {
                                dynamic pred = JObject.Parse(ar[i].ToString());
                                int prob = pred.probability * 100;

                                if (prob > max)
                                {
                                    max = prob;
                                    tagname = pred.tagName;
                                }

                            }

                            if (tagname == gesture)
                            {
                                alt.Add("Random Gesture", "Pass", url);
                                return true;
                            }

                            alt.Add("Random Gesture", "Fail", url);
                            return false;
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return false;
                        }
                    }

                    public bool GenerateDefaultGesture(string url, byte[] imageBytes)
                    {
                        try
                        {
                            //Smile API Call
                            var response = ImageValidationHandler.FaceAPICall(imageBytes);

                            if (response.Length > 2)
                            {
                                JArray items = JArray.Parse(response);

                                for (int i = 0; i < items.Count; i++)
                                {
                                    var item = (JObject)items[i];
                                    var smile = item["faceAttributes"]["smile"];

                                    if ((double)smile > 0.5)
                                    {
                                        alt.Add("Default Gesture - Smile", "Pass", url);
                                        return true;
                                    }
                                }
                            }
                            alt.Add("Default Gesture - Smile", "Fail", url);
                            return false;
                        }
                        catch (Exception e)
                        {
                            error = e.Message;
                            return false;
                        }
                    }


                }
            }
        }
    } 

}