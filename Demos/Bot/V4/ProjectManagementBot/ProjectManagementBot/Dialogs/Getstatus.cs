// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using System.Data.SqlClient;
using System;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace ProjectManagementBot.Dialogs
{
    public class Getstatus :ComponentDialog
    {
        protected readonly IConfiguration Configuration;
        public Getstatus(IConfiguration configuration)
            : base(nameof(Getstatus))
        {
            Configuration = configuration;
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            //AddDialog(new DateTimePrompt(nameof(DateTimePrompt)));
    
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                NameStepAsync,
                DateStepAsync,
                FinalStepAsync,
            
            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var projectData = (ProjectData)stepContext.Options;

            if (projectData.EmployeeName == null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Please enter name of PDI Engineer ?") }, cancellationToken);
            }
            else
            {
                return await stepContext.NextAsync(projectData.EmployeeName, cancellationToken);
            }
        }

        private async Task<DialogTurnResult> DateStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var projectData = (ProjectData)stepContext.Options;

            projectData.EmployeeName = (string)stepContext.Result;

            if (projectData.EmployeeName != null) {

                int a = 0;
                try
                {

                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                    builder.DataSource = Configuration["DataSource"];
                    builder.UserID = Configuration["UserID"];
                    builder.Password =Configuration["Password"];
                    builder.InitialCatalog =Configuration["InitialCatalog"];

                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {


                        connection.Open();
                        StringBuilder sb = new StringBuilder();
                        sb.Append("SELECT id from pdi_engineer_details where name='" + projectData.EmployeeName + "'");

                        String sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                   
                                    a = reader.GetInt32(0);
                                }
                            }
                        }
                        connection.Close();
                        if (a != 0)
                        {
                            projectData.EmployeeID = a; 
                        }
                        else
                        {
                            var temp = projectData.EmployeeName;
                            projectData.EmployeeName = null;
                            projectData.EmployeeID = 0;
                            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Mr/Mrs "+temp+ " is not an employee of company."), cancellationToken);
                            return await stepContext.BeginDialogAsync(nameof(Getstatus), projectData, cancellationToken);

                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }






            }

            if (projectData.Date== null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Please enter Date for which you would like to check status(Example:dd/mm/yyyy)") }, cancellationToken);
                   
            }
            else
            {
                return await stepContext.NextAsync(projectData.Date, cancellationToken);
            }
        }
        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var projectData = (ProjectData)stepContext.Options;

            projectData.Date = (dynamic)stepContext.Result;

            try
            {



                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = Configuration["DataSource"];
                builder.UserID = Configuration["UserID"];
                builder.Password = Configuration["Password"];
                builder.InitialCatalog = Configuration["InitialCatalog"];

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {


                    connection.Open();
                    var statusofEmployee = "";
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select task from task where convert(varchar(10), task_datetime, 103) = '" + projectData.Date + "' and pdi_engineer_id = " + projectData.EmployeeID + "");

                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Console.WriteLine(reader.GetInt32(0));
                                statusofEmployee = statusofEmployee + reader.GetString(0) + "\n";
                            }
                        }
                    }
                    connection.Close();
                    if (statusofEmployee != "")
                    {
                        projectData.EmployeeID = 0;
                        projectData.EmployeeName = null;
                        projectData.Date = null;
                        await stepContext.Context.SendActivityAsync(MessageFactory.Text(statusofEmployee), cancellationToken);
                        //await stepContext.Context.SendActivityAsync(MessageFactory.Text("How can I help you ?"), cancellationToken);
                        return await stepContext.BeginDialogAsync(nameof(MainDialog), projectData, cancellationToken);
                    }
                    else
                    {
                        projectData.EmployeeID = 0;
                        projectData.EmployeeName = null;
                        projectData.Date = null;
                        await stepContext.Context.SendActivityAsync(MessageFactory.Text("Work Status is not Available in Database."), cancellationToken);
                        //await stepContext.Context.SendActivityAsync(MessageFactory.Text("How can I help you ?"), cancellationToken);
                        return await stepContext.BeginDialogAsync(nameof(MainDialog), projectData, cancellationToken);


                    }

                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            var confirm = "Getstatus";
            //projectData.EmployeeName = null;
            //projectData.Date = null;
            //return await stepContext.BeginDialogAsync(nameof(Getstatus), projectData, cancellationToken);
            //return await stepContext.ReplaceDialogAsync(nameof(Getstatus),null,cancellationToken);
            // return await stepContext.BeginDialogAsync(nameof(Getstatus),null, cancellationToken);
            //ReplaceDialogAsync(nameof(Getstatus),cancellationToken);
            return await stepContext.EndDialogAsync(confirm, cancellationToken);
        }       
    }
}
