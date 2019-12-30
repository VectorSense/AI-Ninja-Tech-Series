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

                public class ImageDepthHandler
                {
                    public string error = "";
                    private string RealFakeUrl = ConfigurationManager.AppSettings["RealFakeUrl"];

                    //DB check
                    AuditLoggerTable alt = new AuditLoggerTable();

                    public bool Validate(byte[] imagebyte, string url)
                    {
                        try
                        {
                            string data = Convert.ToBase64String(imagebyte);
                            var client = new RestClient(RealFakeUrl);
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            request.AddParameter("undefined", "{\"Binary\":\"" + data + "\"} ", ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);

                            dynamic obj = JObject.Parse(response.Content);
                            var result_string = obj.Result.ToString();

                            JArray a = JArray.Parse(result_string);

                            var value = a[0].ToString();
                            dynamic obj1 = JObject.Parse(value);

                            if (obj1.TagName == "Real")
                            {
                                alt.Add("Real/Fake", "Pass", url);
                                return true;
                            }
                            alt.Add("Real/Fake", "Fail", url);
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
