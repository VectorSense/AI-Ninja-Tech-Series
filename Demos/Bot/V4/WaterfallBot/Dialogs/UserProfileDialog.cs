// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;

namespace Microsoft.BotBuilderSamples
{
    public class UserProfileDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<UserProfile> _userProfileAccessor;

        public UserProfileDialog(UserState userState)
            : base(nameof(UserProfileDialog))
        {
            _userProfileAccessor = userState.CreateProperty<UserProfile>("UserProfile");

            // This array defines how the Waterfall will execute.
            var waterfallSteps = new WaterfallStep[]
            {
                askNameAsync,
                AskCompanyNameAsync,
                AskPhoneAsync,
                DisplayAsync

            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new NumberPrompt<long>(nameof(NumberPrompt<long>)));
        

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private static async Task<DialogTurnResult> askNameAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is a Prompt Dialog.
            // Running a prompt here mean be run when the users response is received.
            return await stepContext.PromptAsync(nameof(TextPrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please enter your Name."),

                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> AskCompanyNameAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["name"] = (string)stepContext.Result;

            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Please enter your Company Name.") }, cancellationToken);
        }

        private async Task<DialogTurnResult> AskPhoneAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["companyname"] = (string)stepContext.Result;
            // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is a Prompt Dialog.
            return await stepContext.PromptAsync(nameof(NumberPrompt<long>), new PromptOptions { Prompt = MessageFactory.Text("Please enter your phone number.") }, cancellationToken);
        }

        private async Task<DialogTurnResult> DisplayAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileAccessor.GetAsync(stepContext.Context, () => new UserProfile(), cancellationToken);
            stepContext.Values["phonenumber"] = (long)stepContext.Result;

            // Get the current profile object from user state.
            userProfile.companyname = (string)stepContext.Values["companyname"];
            userProfile.Name = (string)stepContext.Values["name"];
            userProfile.phoneno = (long)stepContext.Values["phonenumber"];

            var msg = $"Information Received :\n Name :{userProfile.Name}\n Company Name :{userProfile.companyname}\n Phone Number :{userProfile.phoneno}";
            await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);


        }

    }  

     
}
