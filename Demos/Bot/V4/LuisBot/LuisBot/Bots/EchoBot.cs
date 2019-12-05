// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.3.0

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Extensions.Configuration;


namespace LuisBot.Bots
{
    public class EchoBot : ActivityHandler
    {
        private readonly IConfiguration _configuration;


        public EchoBot(IConfiguration configuration)
        {
            _configuration = configuration;
               
        }





        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            
            var luisApplication = new LuisApplication(
           _configuration["LuisApplicationID"],
           _configuration["LuisEndPointKey"],
           _configuration["LuisEndPoint"]);

            var recognizer = new LuisRecognizer(luisApplication);
            // The actual call to LUIS
            var recognizerResult = await recognizer.RecognizeAsync(turnContext, cancellationToken);

            var topIntent = recognizerResult.GetTopScoringIntent();
            // Next, we call the dispatcher with the top intent.
            await DispatchToTopIntentAsync(turnContext, topIntent.intent, recognizerResult, cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Welcome to LUIS Bot."), cancellationToken);
                }
            }
        }

        private async Task DispatchToTopIntentAsync(ITurnContext<IMessageActivity> turnContext, string intent, RecognizerResult recognizerResult, CancellationToken cancellationToken)
        {

            switch (intent)
            {
              
              
                case "Intent1":
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Intent 1 is encountered."), cancellationToken);
                    break;
                case "Intent2":
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Intent 2 is encountered."), cancellationToken);
                    break;
                case "Intent3":
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Intent 3 is encountered. "), cancellationToken);
                    break;

                default:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Default is encountered. "), cancellationToken);
                    break;
            }



        }






    }
}
