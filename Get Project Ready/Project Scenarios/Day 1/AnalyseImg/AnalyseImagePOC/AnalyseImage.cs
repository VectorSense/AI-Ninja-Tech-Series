using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                    public object[] Objectarray = null, Brandarray = null, Tagarray = null;
                    public string Erorr = "";
                    //Assigning Subscription Key and Face Endpoint from web.config file
                    private string subscriptionKey = ConfigurationManager.AppSettings["CVSubscriptionKey"], CVEndpoint = ConfigurationManager.AppSettings["CVEndpoint"];
                    //Setting Needed Feature Types to extract features from image
                    private static readonly List<VisualFeatureTypes> features = new List<VisualFeatureTypes>() { VisualFeatureTypes.Categories, VisualFeatureTypes.Description, VisualFeatureTypes.Tags, VisualFeatureTypes.Brands, VisualFeatureTypes.Objects };
                    public async Task ImageAnalyse(string data, bool flag)
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
                                else // Analysing Features in Image and return as Json response
                                    GetImageAttributes(await computerVision.AnalyzeImageInStreamAsync(new MemoryStream(Convert.FromBase64String(data)), features));
                            }
                            else// Processing Url if the flag is false
                            {
                                if (!Uri.IsWellFormedUriString(data, UriKind.Absolute))// sending error message if the url is invalid
                                    Erorr = "Invalid Image Url:\n{" + data + "}";
                                else // Analysing Features in Image and return as Json response
                                    GetImageAttributes(await computerVision.AnalyzeImageAsync(data, features));
                            }
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

                        Tagarray = new object[analysis.Tags.Count];//Object array for storing Tags at run time
                        for (int j = 0; j < analysis.Tags.Count; j++)// Iterating Tag list one by one
                        {
                            // storing each Tag in Tag Object array
                            Tagarray.SetValue(new { Name = analysis.Tags[j].Name }, j);
                        }

                        Objectarray = new object[analysis.Objects.Count];//Object array for storing Object's information at run time
                        for (int j = 0; j < analysis.Objects.Count; j++) // Iterating Object list one by one
                        {
                            // storing each Object's attribute in Object array
                            Objectarray.SetValue(new { Name = analysis.Objects[j].ObjectProperty, Confidence = analysis.Objects[j].Confidence, Rect = new { Left = analysis.Objects[j].Rectangle.X, Top = analysis.Objects[j].Rectangle.Y, Width = analysis.Objects[j].Rectangle.W, Height = analysis.Objects[j].Rectangle.H } }, j);
                        }
                    }
                }
            }
        }
    }
}