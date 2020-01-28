// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
namespace Microsoft.BotBuilderSamples
{
    public class AuthBot<T> : DialogBot<T> where T : Dialog
    {
        
        public AuthBot(ConversationState conversationState, UserState userState, T dialog, ILogger<DialogBot<T>> logger, IConfiguration configuration)
            : base(conversationState, userState, dialog, logger,configuration)
        {
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {

                    await SendWelcomeMessageAsync(turnContext, cancellationToken);

                    //await turnContext.SendActivityAsync(MessageFactory.Text("Welcome to AuthenticationBot.\nType anything to get logged in."), cancellationToken);
                    await Dialog.Run(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
                }
            }
        }

        protected override async Task OnTokenResponseEventAsync(ITurnContext<IEventActivity> turnContext, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Running dialog with Token Response Event Activity.");

            // Run the Dialog with the new Token Response Event Activity.
            await Dialog.Run(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
        }



        private static async Task SendWelcomeMessageAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    var reply = turnContext.Activity.CreateReply();
                    reply.Attachments = new List<Attachment>();
                    var heroCard = new HeroCard
                    {
                        Title = "State Bank Of India",
                        Subtitle = "Welcome to SBI Bank.",
                        Text = "This Platform will help you to check Account Balance,Nearest Branch,Nearest ATM and daily Banking needs.",
                        Images = new List<CardImage> { new CardImage("https://www.wordzz.com/wp-content/uploads/2016/10/sbi.jpg") },

                    };
                    reply.Attachments.Add(heroCard.ToAttachment());
                    await turnContext.SendActivityAsync(reply, cancellationToken);
                    
                }
            }
        }









    }
}
