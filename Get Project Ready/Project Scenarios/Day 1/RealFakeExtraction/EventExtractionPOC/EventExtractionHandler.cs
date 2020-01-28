using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Configuration;
namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            public class EventExtractionHandler
            {
                private string ProjectId = ConfigurationManager.AppSettings["ProjectId"], Endpoint = ConfigurationManager.AppSettings["EndPoint"], PredictionKey = ConfigurationManager.AppSettings["PredictionKey"],iteration= ConfigurationManager.AppSettings["iteration"];
                public string error = "";
                public string TagName = "No Problem";
                public object JsonResponse = "";

                public void EventExtraction(string base64data)
                { 
                    try
                    {
                        var imagebytes = Convert.FromBase64String(base64data);
                        //var client = new RestClient(Endpoint + "/customvision/v2.2/Training/projects/"+ ProjectId + "/quicktest/image");
                        var client = new RestClient(Endpoint + "/customvision/v3.0/Prediction/" + ProjectId + "/classify/iterations/" + iteration + "/image");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/octet-stream");
                        //request.AddHeader("training-key", TrainingKey);
                        request.AddHeader("Prediction-Key", PredictionKey);
                        request.AddParameter("data", imagebytes, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);

                        var result = response.Content;
                        JsonResponse = result;

                        dynamic res_obj = JObject.Parse(result);
                        var res_pred = res_obj.predictions.ToString();
                        JArray res_array = JArray.Parse(res_pred);
                        //{ "id":"125d9925-5d15-45b6-ba86-63bd5b8e550f","project":"60e29aa3-4da9-475e-874b-eca3a221b39e","iteration":"9c1fc30e-5eb0-4f71-bfc9-164fa161626e","created":"2019-12-26T13:58:37.597Z","predictions":[{"probability":1.0,"tagId":"8b3a244a-7291-4a56-8300-2f94cc6aad4f","tagName":"Fake"},{"probability":1.53412186E-10,"tagId":"8b2d7078-b698-4ce9-8473-e3d92cb8cbef","tagName":"Real"}]}
                        dynamic Label1 = JObject.Parse(res_array[0].ToString());
                        dynamic Label2 = JObject.Parse(res_array[1].ToString());
                        if((Label1.probability * 100> Label2.probability * 100) && (Label1.tagName=="Real"))
                             TagName = "Real";
                        else
                            TagName = "Fake";
                    }
                    catch (Exception e)
                    {
                        error = e.Message;

                    }

                }
            }
        }
    }

}