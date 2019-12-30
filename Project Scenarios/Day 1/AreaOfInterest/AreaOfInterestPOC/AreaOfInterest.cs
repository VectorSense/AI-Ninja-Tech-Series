using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace FaceAPI
            {
                public class AreaOfInterest
                {
                    //Assigning Subscription Key and Face Endpoint from web.config file
                    private static string AOI_APIKey  = ConfigurationManager.AppSettings["AOI_SubcriptionKey"], AOI_APIEndpoint = ConfigurationManager.AppSettings["AOI_EndPointURL"];
                    public string Error = "";
                    public string Top = "";
                    public string Left = "";
                    public string Width = "";
                    public string Height = "";

                    public void GetAreaofInterest(string data, bool flag)
                    {
                        try
                        {
                            var result = "";
                            if (flag)// Processing image if the flag is true  
                            {

                                var client = new RestClient(AOI_APIEndpoint+ "/vision/v2.0/areaOfInterest");
                                var request = new RestRequest(Method.POST);
                                request.AddHeader("Content-Type", "application/json");
                                request.AddHeader("Ocp-Apim-Subscription-Key", AOI_APIKey);
                                request.AddParameter("undefined", "{\"url\":\" " + data + "\"}", ParameterType.RequestBody);
                                IRestResponse response = client.Execute(request);
                                result = response.Content;
                                
                            }
                            else// Processing Url if the flag is false
                            {
                                var imagebytes = Convert.FromBase64String(data);

                                var client = new RestClient(AOI_APIEndpoint+ "/vision/v2.0/areaOfInterest");
                                var request = new RestRequest(Method.POST);
                                request.AddHeader("Content-Type", "application/octet-stream");
                                request.AddHeader("Ocp-Apim-Subscription-Key", AOI_APIKey);
                                request.AddParameter("data", imagebytes, ParameterType.RequestBody);
                                IRestResponse response = client.Execute(request);
                                result = response.Content;
                            }
                            dynamic res_obj = JObject.Parse(result);
                            var res_prediction = res_obj.areaOfInterest.ToString();
                            dynamic obj = JObject.Parse(res_prediction);

                            if (obj == null)
                            {
                                Error = "Cound not find any values";
                            }
                            else
                            {
                                //Assigning the quardinates to variables
                                Top = obj.y;
                                Left = obj.x;
                                Width = obj.w;
                                Height = obj.h;
                            }
                        }
                        catch (Exception e)// handling runtime errors and returning error as Json
                        {
                            Error = e.Message;
                        }
                    }
                }
            }
        }
    }
}