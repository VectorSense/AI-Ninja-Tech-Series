using System;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace FaceAPI
            {
                public class OCR
                {
                    private string subscriptionKey = ConfigurationManager.AppSettings["OCRSubscriptionKey"],Endpoint= ConfigurationManager.AppSettings["OCREndpoint"];

                    //For printed text
                    private const TextRecognitionMode textRecognitionMode = TextRecognitionMode.Printed;
                    private const int numberOfCharsInOperationId = 36;
                    //Variable to append the OCR Results from SDK
                    public string Error="";
                    public List<string> OCRList = new List<string>();
                    public string OcrResult = "";

                    public async Task ExtractText(string data)
                    {
                            ComputerVisionClient computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey), new System.Net.Http.DelegatingHandler[] { });
                            //Endpoint
                            computerVision.Endpoint = Endpoint;

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

                    //Retriving the recognized text
                    private async Task GetTextAsync(ComputerVisionClient computerVision, string operationLocation)
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
                    }
                }
            }
        }
    }
}