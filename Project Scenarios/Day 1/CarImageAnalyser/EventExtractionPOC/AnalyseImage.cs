using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Configuration;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace FaceAPI
            {
                public class AnalyseImage
                {
                    public object[] Brandarray = null;
                    public object Color = null;
                    public string Erorr = "";
                    //Assigning Subscription Key and Face Endpoint from web.config file
                    private string subscriptionKey = ConfigurationManager.AppSettings["CVSubscriptionKey"], CVEndpoint = ConfigurationManager.AppSettings["CVEndpoint"];
                    //Setting Needed Feature Types to extract features from image
                    private static readonly List<VisualFeatureTypes> features = new List<VisualFeatureTypes>() { VisualFeatureTypes.Brands, VisualFeatureTypes.Color};
                    public async Task ImageAnalyse(string data)
                    {
                        try
                        {
                            //Creating object for Computer Vision Client Class 
                            ComputerVisionClient computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey), new System.Net.Http.DelegatingHandler[] { });
                            //Assingning Endpoint url in Computer Vision Client's object
                            computerVision.Endpoint = CVEndpoint;                            
                            if (data == "")// sending error message if the image is empty
                                Erorr = "Not found image data";
                            else // Analysing Features in Image and return as Json response
                                GetImageAttributes(await computerVision.AnalyzeImageInStreamAsync(new MemoryStream(Convert.FromBase64String(data)), features));                          
                        }
                        catch (Exception e)// handling runtime errors and returning error as Json
                        {
                            Erorr = e.Message;
                        }
                    }

                    //Extracting Json Results getting from Computer Vision API and creating new Json for result of interest
                    private void GetImageAttributes(ImageAnalysis analysis)
                    {
                        Brandarray = new object[analysis.Brands.Count];//Object array for storing Brand result at run time
                        for (int j = 0; j < analysis.Brands.Count; j++)// Iterating brand list one by one
                        {
                            // storing each Brand in Brand Object array
                            Brandarray.SetValue(new { Name = analysis.Brands[j].Name, Confidence = analysis.Brands[j].Confidence, Rect = new { Left = analysis.Brands[j].Rectangle.X, Top = analysis.Brands[j].Rectangle.Y, Width = analysis.Brands[j].Rectangle.W, Height = analysis.Brands[j].Rectangle.H } }, j);
                        }
                        Color= new { IsBWImg = analysis.Color.IsBWImg, AccentColor = analysis.Color.AccentColor, DominantColorBackground = analysis.Color.DominantColorBackground, DominantColorForeground = analysis.Color.DominantColorForeground, DominantColors = string.Join(",", analysis.Color.DominantColors) };
                    }
                }
            }
        }
    }
}