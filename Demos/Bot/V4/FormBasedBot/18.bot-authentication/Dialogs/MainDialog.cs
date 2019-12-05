// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Bot.Builder.AI.Luis;

using Microsoft.Bot.Builder.Dialogs.Choices;

namespace Microsoft.BotBuilderSamples
{
    public class MainDialog : LogoutDialog
    {
        protected readonly ILogger Logger;
        private readonly IStatePropertyAccessor<StoredDetails> _TokenAccessor;

        public MainDialog(IConfiguration configuration, ILogger<MainDialog> logger, UserState userState)
            : base(nameof(MainDialog), configuration["ConnectionName"], userState)
        {
            _TokenAccessor = userState.CreateProperty<StoredDetails>("StoredDetails");
            Logger = logger;

            AddDialog(new OAuthPrompt(
                nameof(OAuthPrompt),
                new OAuthPromptSettings
                {
                    ConnectionName = ConnectionName,
                    Text = "Please Sign In",
                    Title = "Sign In",
                    Timeout = 300000, // User has 5 minutes to login (1000 * 60 * 5)
                }));

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            //AddDialog(new LuisHelper());

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                PromptStepAsync,
                LoginStepAsync ,
                AfterLoginAsync
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

      

        private async Task<DialogTurnResult> PromptStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.BeginDialogAsync(nameof(OAuthPrompt), null, cancellationToken);
        }

        private async Task<DialogTurnResult> LoginStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Get the token from the previous step. Note that we could also have gotten the
            // token directly from the prompt itself. There is an example of this in the next method.
            var tokenResponse = (TokenResponse)stepContext.Result;
            var Details = await _TokenAccessor.GetAsync(stepContext.Context, () => new StoredDetails(), cancellationToken);
            Details.TokenResponse = tokenResponse;

            if (tokenResponse != null)
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("You are now logged in."), cancellationToken);
               // await stepContext.Context.SendActivityAsync(MessageFactory.Text("How can I help you ?"), cancellationToken);
             //await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("How can I help you?") }, cancellationToken);
                await SendSuggestedActionsAsync(stepContext.Context, cancellationToken);
               return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            }

            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Login was not successful please try again."), cancellationToken);
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }


        private async Task<DialogTurnResult> AfterLoginAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

           return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
            


        }

        private static async Task SendSuggestedActionsAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = turnContext.Activity.CreateReply("How can I help you?");
            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = new List<CardAction>()
                    {
                         new CardAction() { Title = "Account Balance", Type = ActionTypes.ImBack, Value = "Account Balance" },
                         new CardAction() { Title = "Recent Transactions", Type = ActionTypes.ImBack, Value = "Recent Transactions" },
                         new CardAction() { Title = "Nearest Branch", Type = ActionTypes.ImBack, Value = "Nearest Branch" },
                         new CardAction() { Title = "Nearest ATM", Type = ActionTypes.ImBack, Value = "Nearest ATM" },
                         new CardAction() { Title = "Offers", Type = ActionTypes.ImBack, Value = "Offers" },
                         new CardAction() { Title = "Complaint", Type = ActionTypes.ImBack, Value = "Complaint" },
                         new CardAction() { Title = "Request to Bank", Type = ActionTypes.ImBack, Value = "Request to Bank" },
                         new CardAction() { Title = "Exit", Type = ActionTypes.ImBack, Value = "Exit" },
                     },
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);


        }











    }
}
