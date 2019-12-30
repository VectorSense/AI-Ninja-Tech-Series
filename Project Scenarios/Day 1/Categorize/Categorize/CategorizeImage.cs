using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace FaceAPI
            {
                public class CategorizeImage
                {
                    public object[] Catearray = null, Descriparray = null;
                    public string Erorr = "";
                    //Assigning Subscription Key and Face Endpoint from web.config file
                    private string subscriptionKey = ConfigurationManager.AppSettings["CVSubscriptionKey"], CVEndpoint = ConfigurationManager.AppSettings["CVEndpoint"];
                    //Setting Needed Feature Types to extract features from image
                    private static readonly List<VisualFeatureTypes> features = new List<VisualFeatureTypes>() { VisualFeatureTypes.Categories, VisualFeatureTypes.Description };

                    public async Task ImageCategorize(string data, bool flag)
                    {
                        try
                        {
                            //Creating object for Computer Vision Client Class 
                            ComputerVisionClient computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey), new System.Net.Http.DelegatingHandler[] { });
                            //Assingning Endpoint url in Computer Vision Client's object
                            computerVision.Endpoint = CVEndpoint;

                            if (flag)// Processing image if the flag is true  
                            {
                                if (data == "")// sending error message if the image is empty
                                    Erorr = "Not found image data";
                                else // Finding Image Category and description in image and return as Json response
                                    GetImageCategorize(await computerVision.AnalyzeImageInStreamAsync(new MemoryStream(Convert.FromBase64String(data)), features));
                            }
                            else// Processing Url if the flag is false
                            {
                                if (!Uri.IsWellFormedUriString(data, UriKind.Absolute))// sending error message if the url is invalid
                                     Erorr = "Invalid Image Url:\n{"+ data + "}";
                                else // Finding Image Category and description in image and return as Json response
                                    GetImageCategorize(await computerVision.AnalyzeImageAsync(data, features));
                            }
                        }
                        catch (Exception e)// handling runtime errors and returning error as Json
                        {
                            Erorr = e.Message;
                        }

                    }

                    //Extracting Json Results getting from Computer Vision API and creating new Json for result of interest
                    private void GetImageCategorize(ImageAnalysis analysis)
                    {
                        Catearray = new object[analysis.Categories.Count];//Object array for storing Category at run time
                        for (int j = 0; j < analysis.Categories.Count; j++)// Iterating category list one by one
                        {
                            Catearray.SetValue(new { Name = analysis.Categories[j].Name }, j);// storing each Category in category Object array
                        }

                        Descriparray = new object[analysis.Description.Captions.Count];//Object array for storing Discription at run time
                        for (int j = 0; j < analysis.Description.Captions.Count; j++)// Iterating discription list one by one
                        {
                            Descriparray.SetValue(new { Name = analysis.Description.Captions[j].Text }, j);// storing each Description in Description Object array
                        }

                     }
                }
            }
        }
    }
}