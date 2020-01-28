using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Configuration;

namespace AI_SeriesHOL
{
    namespace PartnerTechSeries
    {
        namespace AI
        {
            namespace HOL
            {
                namespace FaceAPI
                {
                    public class QualityControlChecker
                    {
                        //Assigning Subscription Key and Face Endpoint from web.config file
                        private static string QualityPredictionKey_model1 = ConfigurationManager.AppSettings["QualityPredictionKey_model1"], QualityAPIEndpoint_model1 = ConfigurationManager.AppSettings["QualityEndPoint_model1"], QualityProjectID_model1 = ConfigurationManager.AppSettings["QualityProjectID_model1"], QualityIterationID_model1 = ConfigurationManager.AppSettings["QualityIterationID_model1"];
                        private static string QualityPredictionKey_model2 = ConfigurationManager.AppSettings["QualityPredictionKey_model2"], QualityAPIEndpoint_model2 = ConfigurationManager.AppSettings["QualityEndPoint_model2"], QualityProjectID_model2 = ConfigurationManager.AppSettings["QualityProjectID_model2"], QualityIterationID_model2 = ConfigurationManager.AppSettings["QualityIterationID_model2"];

                        public string error = "";
                        public string TagName = "";

                        public void Quality_Check(string data, bool flag, string check)
                        {

                            try
                            {
                                if(check == "QualityCheck_Model1")
                                {
                                    try
                                    {
                                        var result = "";
                                        //checking the flag is true then execute the URL Image
                                        if (flag)
                                        {
                                            var client = new RestClient(QualityAPIEndpoint_model1 + "/customvision/v3.0/Prediction/" + QualityProjectID_model1 + "/detect/iterations/" + QualityIterationID_model1 + "/url");
                                            var request = new RestRequest(Method.POST);
                                            request.AddHeader("Content-Type", "application/json");
                                            request.AddHeader("Prediction-Key", QualityPredictionKey_model1);
                                            request.AddParameter("undefined", "{\"Url\": \"" + data + "\"}", ParameterType.RequestBody);
                                            IRestResponse response = client.Execute(request);
                                            result = response.Content;

                                        }
                                        // Executing the normal images
                                        else
                                        {
                                            var imagebytes = Convert.FromBase64String(data);

                                            var client = new RestClient(QualityAPIEndpoint_model1 + "/customvision/v3.0/Prediction/" + QualityProjectID_model1 + "/detect/iterations/" + QualityIterationID_model1 + "/image");
                                            var request = new RestRequest(Method.POST);
                                            request.AddHeader("Content-Type", "application/octet-stream");
                                            request.AddHeader("Prediction-Key", QualityPredictionKey_model1);
                                            request.AddParameter("data", imagebytes, ParameterType.RequestBody);
                                            IRestResponse response = client.Execute(request);
                                            result = response.Content;
                                        }
                                        //creating the jObject 
                                        dynamic res_obj = JObject.Parse(result);
                                        var res_prediction = res_obj.predictions.ToString();
                                        //creating the JArray
                                        JArray res_array = JArray.Parse(res_prediction);

                                        if (res_array.Count != 0)
                                        {
                                            TagName = "Pass";

                                            for (int i = 0; i < res_array.Count; i++)
                                            {
                                                dynamic pred = JObject.Parse(res_array[i].ToString());
                                                int probability = pred.probability * 100;
                                                //checking the probability 
                                                if (pred.tagName == "Fail" && probability >= 90)
                                                {
                                                    TagName = "Fail";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            TagName = "Cound not find any object";
                                        }
                                        
                                    }

                                    catch (Exception e)
                                    {
                                        error = e.Message;
                                    }
                                }

                                if (check == "QualityCheck_Model2")
                                {
                                    try
                                    {
                                        var result = "";
                                        //checking the flag is true then execute the URL Image
                                        if (flag)
                                        {
                                            var client = new RestClient(QualityAPIEndpoint_model2+ "/customvision/v3.0/Prediction/" + QualityProjectID_model2 + "/classify/iterations/" + QualityIterationID_model2 + "/url");
                                            var request = new RestRequest(Method.POST);
                                            request.AddHeader("Content-Type", "application/json");
                                            request.AddHeader("Prediction-Key", QualityPredictionKey_model2);
                                            request.AddParameter("undefined", "{\"Url\": \"" + data + "\"}", ParameterType.RequestBody);
                                            IRestResponse response = client.Execute(request);
                                            result = response.Content;

                                        }
                                        // Executing the normal images
                                        else
                                        {
                                            var imagebytes = Convert.FromBase64String(data);

                                            var client = new RestClient(QualityAPIEndpoint_model2 + "/customvision/v3.0/Prediction/" + QualityProjectID_model2 + "/classify/iterations/" + QualityIterationID_model2 + "/image");
                                            var request = new RestRequest(Method.POST);
                                            request.AddHeader("Content-Type", "application/octet-stream");
                                            request.AddHeader("Prediction-Key", QualityPredictionKey_model2);
                                            request.AddParameter("data", imagebytes, ParameterType.RequestBody);
                                            IRestResponse response = client.Execute(request);
                                            result = response.Content;
                                        }
                                        //creating the jObject 
                                        dynamic res_obj = JObject.Parse(result);
                                        var res_prediction = res_obj.predictions.ToString();
                                        //creating the JArray
                                        JArray res_array = JArray.Parse(res_prediction);

                                        if (res_array.Count != 0)
                                        {
                                            dynamic pred = JObject.Parse(res_array[0].ToString());
                                            //int probability = pred.probability * 100;
                                            
                                            //checking the probability 
                                            if (pred.tagName == "Accurate-Space")
                                            {
                                                TagName = "Pass ";
                                            }
                                            else
                                            {
                                                TagName = "Fail ";
                                            }
                                        }
                                        else
                                        {
                                            TagName = "Cound not find any object";
                                        }
                                    }

                                    catch (Exception e)
                                    {
                                        error = e.Message;
                                    }
                                }
                            }
                            catch(Exception e)
                            {
                                error = e.Message;
                            }
                        }
                    }
                }
            }
        }
    }
}