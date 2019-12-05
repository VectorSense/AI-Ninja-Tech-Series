// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Builder.AI.Luis;

namespace Microsoft.BotBuilderSamples
{
    public class LogoutDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<StoredDetails> _TokenAccessor;

        public LogoutDialog(string id, string connectionName, UserState userState)
            : base(id)
        {
            _TokenAccessor = userState.CreateProperty<StoredDetails>("StoredDetails");
            ConnectionName = connectionName;
        }

        protected string ConnectionName { get; }

        protected override async Task<DialogTurnResult> OnBeginDialogAsync(DialogContext innerDc, object options, CancellationToken cancellationToken = default(CancellationToken))
        {

          


            var result = await InterruptAsync(innerDc, cancellationToken);


            if (result != null)
            {
                return result;
            }
       
                return await base.OnBeginDialogAsync(innerDc, options, cancellationToken);

        }

        protected override async Task<DialogTurnResult> OnContinueDialogAsync(DialogContext innerDc, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await InterruptAsync(innerDc, cancellationToken);
            if (result != null)
            {
                return result;
            }

            return await base.OnContinueDialogAsync(innerDc, cancellationToken);
        }

        private async Task<DialogTurnResult> InterruptAsync(DialogContext innerDc, CancellationToken cancellationToken = default(CancellationToken))
        {

            var Details = await _TokenAccessor.GetAsync(innerDc.Context, () => new StoredDetails(), cancellationToken);

            var p = Details.TokenResponse;
            


            if (innerDc.Context.Activity.Type == ActivityTypes.Message)
            {
                //var text = innerDc.Context.Activity.Text.ToLowerInvariant();

                var luisApplication = new LuisApplication(
             "bdc48cb4-0945-40e8-8e89-99fec5924d03",
             "76e8996cfd994ef3a4b79b938c0c2b04",
             $"https://westus.api.cognitive.microsoft.com");

                var recognizer = new LuisRecognizer(luisApplication);
                // The actual call to LUIS
                var recognizerResult = await recognizer.RecognizeAsync(innerDc.Context, cancellationToken);

                var topIntent = recognizerResult.GetTopScoringIntent();

                if (topIntent.intent == "Exit") {
                    Details.TokenResponse = null;
                    var botAdapter = (BotFrameworkAdapter)innerDc.Context.Adapter;
                    await botAdapter.SignOutUserAsync(innerDc.Context, ConnectionName, null, cancellationToken);
                    await innerDc.Context.SendActivityAsync(MessageFactory.Text("You have been signed out."), cancellationToken);
                    return await innerDc.CancelAllDialogsAsync(cancellationToken);


                }
             

            }
            
            return null;
        }
    }
}
