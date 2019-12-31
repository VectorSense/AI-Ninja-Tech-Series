// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Data.SqlClient;
using System.Text;


namespace ProjectManagementBot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        protected readonly IConfiguration Configuration;
        protected readonly ILogger Logger;


        private readonly IStatePropertyAccessor<ProjectData> _ProjectDataAccessor;
        public MainDialog(IConfiguration configuration, ILogger<MainDialog> logger,UserState userState)
            : base(nameof(MainDialog))
        {
            _ProjectDataAccessor = userState.CreateProperty<ProjectData>("projectData");
            Configuration = configuration;
            Logger = logger;

            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new Getstatus(Configuration));
            AddDialog(new SubmitStatusReqInput(Configuration));
            AddDialog(new SubmitStatusNLP(Configuration));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                IntroStepAsync,
                ActStepAsync,
                FinalStepAsync,
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> IntroStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(Configuration["LuisAppId"]) || string.IsNullOrEmpty(Configuration["LuisAPIKey"]) || string.IsNullOrEmpty(Configuration["LuisAPIHostName"]))
            {
                await stepContext.Context.SendActivityAsync(
                    MessageFactory.Text("NOTE: LUIS is not configured. To enable all capabilities, add 'LuisAppId', 'LuisAPIKey' and 'LuisAPIHostName' to the appsettings.json file."), cancellationToken);

                return await stepContext.NextAsync(null, cancellationToken);
            }
            else
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("How can I help you ?") }, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> ActStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Call LUIS and gather any potential booking details. (Note the TurnContext has the response to the prompt.)
            var projectData = stepContext.Result != null
                    ?
                await LuisHelper.ExecuteLuisQuery(Configuration, Logger, stepContext.Context, cancellationToken)
                    :
                new ProjectData();

            var temp = projectData.intentIdenified;
           
            var nameofstatus ="";
            if (temp == "Get_Status")
            {
                nameofstatus = nameof(Getstatus);
            }
            else if (temp == "ReportStatus")
            {

                nameofstatus = nameof(SubmitStatusReqInput);
            }
            else if (temp == "ReportStatusWithInput") {

                nameofstatus = nameof(SubmitStatusNLP);
            }

           return await stepContext.BeginDialogAsync(nameofstatus, projectData, cancellationToken);
            
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var projectdata = (ProjectData)stepContext.Options;
          
            if (stepContext.Result != null)
            {
               
                if (stepContext.Result.ToString() == "Getstatus") {
                   
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("How can I help you ?"), cancellationToken);











                }

            }
            else
            {
                await stepContext.Context.SendActivityAsync(MessageFactory.Text("Thank you."), cancellationToken);
            }
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }
}
