// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.3.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Connector;
using System;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;




namespace ImageAnalyticsBot.Bots
{
    public class EchoBot : ActivityHandler
    {

        private readonly IConfiguration _Configuration;
        public static string subscriptionKey;
        public static string endpoint;

        //private readonly IHttpClientFactory _httpClientFactory;

        public EchoBot(IConfiguration configuration)
        {
            _Configuration = configuration;
            subscriptionKey = _Configuration["ComputerVisionKey"];
            endpoint = _Configuration["endpoint"];

        }




      // _Configuration["ComputerVisionKey"];
            


        private static readonly List<VisualFeatureTypes> features =
                 new List<VisualFeatureTypes>()
             {
                            VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                            VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                            VisualFeatureTypes.Tags
             };


        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {

            if (turnContext.Activity.Type == ActivityTypes.Message) {

                try
                {
                    var connector = new ConnectorClient(new Uri(turnContext.Activity.ServiceUrl));
                    var message=await GetCaptionAsync(turnContext, connector);
                    message = "Description : " + message;
                    await turnContext.SendActivityAsync(MessageFactory.Text(message), cancellationToken);
                   

                }
                catch (ArgumentException e)
                {
                   var message = "Did you upload an image? I'm more of a visual person. " +
                        "Try sending me an image or an image URL";
                    await turnContext.SendActivityAsync(MessageFactory.Text(message), cancellationToken);

                   
                }



            }

           
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hi! I am ImageAnalytics Bot.\n Please upload an Image as attachment to Check Results."), cancellationToken);
                }
            }
        }


        private static async Task<string> GetCaptionAsync(ITurnContext turnContext, ConnectorClient connector)
        {



            
        //var imageAttachment = activity.Attachments?.FirstOrDefault(a => a.ContentType.Contains("image"));
        var imageAttachment=turnContext.Activity.Attachments?.FirstOrDefault(a => a.ContentType.Contains("image"));
            if (imageAttachment != null)
            {
                using (var stream = await GetImageStream(connector, imageAttachment))
                {

                    ComputerVisionClient computerVision = new ComputerVisionClient(
                   new ApiKeyServiceClientCredentials(subscriptionKey),
                   new System.Net.Http.DelegatingHandler[] { });
                    computerVision.Endpoint =endpoint;

                    ImageAnalysis analysis = await computerVision.AnalyzeImageInStreamAsync(
                            stream, features);

                    string tags = "";
                    string describe = "";
                    string categories = "";
                    string comma = ",";

                    if (analysis.Description.Captions.Count != 0)
                    {
                        describe = analysis.Description.Captions[0].Text;
                    }

                    for (int i = 0; i < analysis.Tags.Count; i++)
                    {
                        tags = tags + analysis.Tags[i].Name + comma;
                        if (i == analysis.Tags.Count - 2)
                        {
                            comma = "";
                        }
                    }
                    if (analysis.Categories.Count != 0)
                    {
                        categories = analysis.Categories[0].Name;
                    }



                    string message = " " + describe + "\n" + "Tags : " + tags + "\n" + "Categories : " + categories;


                    return string.IsNullOrEmpty(message) ?
                                "Couldn't find a Details for this one" :
                                "I think it's " + message;


                }
            }
            throw new ArgumentException("The activity doesn't contain a valid image attachment or an image URL.");
        }

        private static async Task<Stream> GetImageStream(ConnectorClient connector, Attachment imageAttachment) {

            using (var httpClient = new HttpClient())
            {

                var uri = new Uri(imageAttachment.ContentUrl);
                if (uri.Host.EndsWith("skype.com") && uri.Scheme == "https")
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync(connector));
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
                }

                return await httpClient.GetStreamAsync(uri);
            }

        }


        private static async Task<string> GetTokenAsync(ConnectorClient connector)
        {
            var credentials = connector.Credentials as MicrosoftAppCredentials;
            if (credentials != null)
            {
                return await credentials.GetTokenAsync();
            }

            return null;
        }
    }
}
