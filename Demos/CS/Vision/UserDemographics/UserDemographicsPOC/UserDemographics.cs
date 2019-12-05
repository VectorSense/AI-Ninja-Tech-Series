using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace FaceAPI
            {
                public class UserDemographics
                {
                    //Assigning Subscription Key and Face Endpoint from web.config file
                    private string subscriptionKey = ConfigurationManager.AppSettings["FaceSubscriptionKey"], faceEndpoint = ConfigurationManager.AppSettings["FaceEndpoint"];
                    //Setting Needed Face Attributes
                    private static readonly FaceAttributeType[] faceAttributes = { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Emotion };
                    public object[] Jarray = null;
                    public Int32 MCount = 0, FCount = 0;// To storing Male count and Female count
                    public string Erorr = "";

                    public async Task DetectFaceAttribute(string data, bool flag)
                    {
                        try
                        {
                            //Creating object for Face Client Class 
                            FaceClient faceClient = new FaceClient(new ApiKeyServiceClientCredentials(subscriptionKey), new System.Net.Http.DelegatingHandler[] { });

                            //Assingning Endpoint url in Face Client's object 
                            faceClient.Endpoint = faceEndpoint;

                            if (flag) // Processing image if the flag is true  
                            {
                                if (data == "") // sending error message if the image is empty
                                    Erorr = "Not found image data";
                                else// Finding face attributes and return as Json response
                                    GetFaceAttributes(await faceClient.Face.DetectWithStreamAsync(new MemoryStream(Convert.FromBase64String(data)), true, true, faceAttributes));
                            }
                            else // Processing Url if the flag is false
                            {
                                if (!Uri.IsWellFormedUriString(data, UriKind.Absolute)) // sending error message if the url is invalid
                                    Erorr = "Invalid Image Url:\n{"+ data + "}";                                // Finding face attributes and return as Json response
                                else GetFaceAttributes(await faceClient.Face.DetectWithUrlAsync(data, true, true, faceAttributes));
                            }
                        }
                        catch (APIErrorException e) //handling runtime api error and returning error as Json
                        {
                            Erorr = e.Message;
                        }
                        catch (Exception e)// handling runtime errors and returning error as Json
                        {
                            Erorr = e.Message;
                        }

                    }
                    //Extracting Json Results getting from Face API and creating new Json for result of interest
                    private void GetFaceAttributes(IList<DetectedFace> faceList)
                    {
                        Jarray = new object[faceList.Count]; //Object array for storing result at run time
                        Int32 i = 0; //used for object array indexing
                        
                        foreach (DetectedFace face in faceList) // Iterating all face list one by one
                        {
                            string gender = face.FaceAttributes.Gender.ToString(); //getting Gender from induvidual face
                            if (face.FaceAttributes.Gender.ToString() == "Male") // Increment Male count by one if the Gender is male
                                MCount += 1;
                            else// Increment Female count by one if the Gender is female
                                FCount += 1;


                            // getting emotion from induvidual face if the emotion have above 65 % 
                            string emotion = "Neutral";
                            if (face.FaceAttributes.Emotion.Happiness > 0.65)
                                emotion = "Happy";
                            else if (face.FaceAttributes.Emotion.Sadness > 0.65)
                                emotion = "Sad";
                            else if (face.FaceAttributes.Emotion.Anger > 0.65)
                                emotion = "Angry";
                            else if (face.FaceAttributes.Emotion.Surprise > 0.65)
                                emotion = "Surprise";
                            else if (face.FaceAttributes.Emotion.Neutral > 0.65)
                                emotion = "Neutral";
                            else if (face.FaceAttributes.Emotion.Contempt > 0.65)
                                emotion = "Contempt";
                            else if (face.FaceAttributes.Emotion.Disgust > 0.65)
                                emotion = "Disgust";
                            else if (face.FaceAttributes.Emotion.Fear > 0.65)
                                emotion = "Fear";

                            // storing each face's attribute in Object array
                            Jarray.SetValue(new { Gender = gender, Age = (double)face.FaceAttributes.Age, Emotion = emotion, Rect = new { Left = face.FaceRectangle.Left, Top = face.FaceRectangle.Top, Width = face.FaceRectangle.Width, Height = face.FaceRectangle.Height } }, i++);
                        }
                    }
                 }
            }
        }
    }
}