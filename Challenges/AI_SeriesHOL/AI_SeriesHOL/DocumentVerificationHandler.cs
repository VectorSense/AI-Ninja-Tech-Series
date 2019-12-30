using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

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
                    public class DocumentVerificationHandler
                    {
                        private string subscriptionKey = ConfigurationManager.AppSettings["OCRSubscriptionKey"], Endpoint = ConfigurationManager.AppSettings["OCREndpoint"];

                        //For printed text
                        private const TextRecognitionMode textRecognitionMode = TextRecognitionMode.Printed;
                        private const int numberOfCharsInOperationId = 36;
                        //Variable to append the OCR Results from SDK
                        public string Error = "";
                        public List<string> OCRList = new List<string>();
                        public string OcrResult = "";
                        public string ContractDate = "", VendorName = "", ClientName = "", Services = "", ContractValue = "", EndDate = "", PenaltyValue = "", JurisdictionPlace = "", VendorEmail = "", VendorPhone = "", ClientEmail = "", ClientPhone = "", FinalResult = "";

                        public async Task ExtractText(string data, bool flag)
                        {
                            ComputerVisionClient computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey), new System.Net.Http.DelegatingHandler[] { });
                            //Endpoint
                            computerVision.Endpoint = Endpoint;

                            if (flag)
                            {
                                if (!Uri.IsWellFormedUriString(data, UriKind.Absolute))
                                {
                                    Error = "Invalid remoteImageUrl: " + data;
                                }

                                //Starting the async process to read the text
                                BatchReadFileHeaders textHeaders = await computerVision.BatchReadFileAsync(data, textRecognitionMode);

                                await GetTextAsync(computerVision, textHeaders.OperationLocation);
                            }
                            else
                            {
                                //Image data to Byte Array
                                byte[] imageBytes = Convert.FromBase64String(data);

                                //Byte Array To Stream
                                Stream stream = new MemoryStream(imageBytes);

                                try
                                {
                                    //Starting the async process to recognize the text
                                    BatchReadFileInStreamHeaders textHeaders = await computerVision.BatchReadFileInStreamAsync(stream, textRecognitionMode);

                                    await GetTextAsync(computerVision, textHeaders.OperationLocation);

                                }
                                catch (Exception e)
                                {
                                    Error = e.Message;
                                }
                            }
                        }

                        //Retriving the recognized text
                        public async Task GetTextAsync(ComputerVisionClient computerVision, string operationLocation)
                        {
                            //Retrieve the URI where the recognized text will be stored from the Operation-Location header
                            string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);

                            //Calling GetReadOperationResultAsync
                            ReadOperationResult result = await computerVision.GetReadOperationResultAsync(operationId);

                            //Waiting for the operation to complete
                            int i = 0;
                            int maxRetries = 10;
                            while ((result.Status == TextOperationStatusCodes.Running ||
                                    result.Status == TextOperationStatusCodes.NotStarted) && i++ < maxRetries)
                            {
                                await Task.Delay(1000);

                                result = await computerVision.GetReadOperationResultAsync(operationId);
                            }

                            //Displaying the results
                            var recResults = result.RecognitionResults;


                            foreach (TextRecognitionResult recResult in recResults)
                            {
                                foreach (Line line in recResult.Lines)
                                {
                                    OCRList.Add(line.Text);
                                    OcrResult += line.Text + " <br> ";
                                }
                            }

                           
                            //Loop
                            for (int j = 0; j<OCRList.Count;j++)
                            {
                                //Calling LUIS
                                var client = new RestClient(ConfigurationManager.AppSettings["LUIS_EndPoint"] + ConfigurationManager.AppSettings["LUIS_AppID"] + "?verbose=true&timezoneOffset=-360&subscription-key=" + ConfigurationManager.AppSettings["LUIS_Key"] + "&q=" + OCRList[j]);
                                var request = new RestRequest(Method.GET);
                                IRestResponse response = client.Execute(request);
                                dynamic jObject = JObject.Parse(response.Content);

                                JArray luislenobj = JArray.Parse(jObject.entities.ToString());

                                if (luislenobj.Count > 0)
                                {
                                    for (int k = 0; k < luislenobj.Count; k++)
                                    {
                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Contract Details" && jObject["entities"][k]["type"].ToString() == "Contract Date")
                                        {
                                            ContractDate = jObject["entities"][k]["entity"].ToString();
                                            break;
                                        }
                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Contract Details" && jObject["entities"][k]["type"].ToString() == "Client Name")
                                        {
                                            ClientName = jObject["entities"][k]["entity"].ToString();
                                            break;
                                        }
                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Contract Details" && jObject["entities"][k]["type"].ToString() == "Vendor Name")
                                        {
                                            VendorName += jObject["entities"][k]["entity"].ToString();
                                        }


                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Services" && jObject["entities"][k]["type"].ToString() == "Services")
                                        {
                                            Services += " " + jObject["entities"][k]["entity"].ToString();
                                        }

                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Contract Value" && jObject["entities"][k]["type"].ToString() == "Contract Value")
                                        {
                                            ContractValue = jObject["entities"][k]["entity"].ToString();
                                            break;
                                        }

                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Contract End Date" && jObject["entities"][k]["type"].ToString() == "End Date")
                                        {
                                            EndDate = jObject["entities"][k]["entity"].ToString();
                                            break;
                                        }

                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Penalty" && jObject["entities"][k]["type"].ToString() == "builtin.percentage")
                                        {
                                            PenaltyValue = jObject["entities"][k]["entity"].ToString();
                                            break;
                                        }

                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Jurisdiction" && jObject["entities"][k]["type"].ToString() == "Jurisdiction Place")
                                        {
                                            JurisdictionPlace = jObject["entities"][k]["entity"].ToString();
                                            break;
                                        }

                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Vendor Details" && jObject["entities"][k]["type"].ToString() == "builtin.email")
                                        {
                                            VendorEmail= jObject["entities"][k]["entity"].ToString();
                                            break;
                                        }
                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Vendor Details" && jObject["entities"][k]["type"].ToString() == "builtin.phonenumber")
                                        {
                                            VendorPhone = jObject["entities"][k]["entity"].ToString();
                                            break;
                                        }

                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Client Details" && jObject["entities"][k]["type"].ToString() == "builtin.email")
                                        {
                                            ClientEmail = jObject["entities"][k]["entity"].ToString();
                                            break;
                                        }
                                        if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Client Details" && jObject["entities"][k]["type"].ToString() == "builtin.phonenumber")
                                        {
                                            ClientPhone = jObject["entities"][k]["entity"].ToString();
                                            break;
                                        }

                                    }
                                }
                                
                            }

                            FinalResult = "<br>" + "Contract Date : " + ContractDate + "<br>" + "Vendor Name : " + VendorName + "<br>" + "Client Name : " + ClientName + "<br>" + "Service Description : " + Services + "<br>" + "Contract Value : " + ContractValue + "<br>" + "End Date : " + EndDate + "<br>" + "Penalty Value : " + PenaltyValue + "<br>" + "Jurisdiction Place : " + JurisdictionPlace + "<br>" + "Vendor Email : " + VendorEmail + "<br>" + "Vendor Phone : " + VendorPhone + "<br>" + "Client Email : " + ClientEmail + "<br>" + "Client Phone : " + ClientPhone + "<br>";

                        }

                    }
                }
            }
        }
    }
}