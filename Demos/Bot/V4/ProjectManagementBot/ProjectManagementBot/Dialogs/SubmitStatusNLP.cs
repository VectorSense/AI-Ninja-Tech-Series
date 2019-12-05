using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Recognizers.Text.DataTypes.TimexExpression;
using System.Data.SqlClient;
using System;
using System.Net.Http;
using System.Text;
using System.Globalization;
using System;
using Microsoft.Extensions.Configuration;

namespace ProjectManagementBot.Dialogs
{
    public class SubmitStatusNLP : ComponentDialog
    {
        protected readonly IConfiguration Configuration;
        public SubmitStatusNLP(IConfiguration configuration)
         : base(nameof(SubmitStatusNLP))
        {
            Configuration = configuration;
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new NumberPrompt<int>(nameof(NumberPrompt<int>)));

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                EmployeeIDStepAsync,
                dateStepAsync,
                FinalAsync

            }));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> EmployeeIDStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var projectData = (ProjectData)stepContext.Options;

            if (projectData.EmployeeID == 0)
            {
                return await stepContext.PromptAsync(nameof(NumberPrompt<int>), new PromptOptions { Prompt = MessageFactory.Text("Please enter Employee ID of PDI Engineer ?") }, cancellationToken);
            }
            else
            {
                return await stepContext.NextAsync(projectData.EmployeeID, cancellationToken);
            }


        }

        private async Task<DialogTurnResult> dateStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var projectData = (ProjectData)stepContext.Options;

            projectData.EmployeeID = (int)stepContext.Result;

            if (projectData.EmployeeID != 0)
            {

                int a = 0;
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
                        StringBuilder sb = new StringBuilder();
                        sb.Append("SELECT count(name) from pdi_engineer_details where id='" + projectData.EmployeeID + "'");

                        String sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    //Console.WriteLine(reader.GetInt32(0));
                                    a = reader.GetInt32(0);
                                }
                            }
                        }
                        connection.Close();
                        if (a != 0)
                        {
                            Console.WriteLine("Employee ID is correct");

                        }
                        else
                        {

                            projectData.EmployeeID = 0;
                            await stepContext.Context.SendActivityAsync(MessageFactory.Text("Employee ID is Incorrect."), cancellationToken);
                            return await stepContext.BeginDialogAsync(nameof(SubmitStatusNLP), projectData, cancellationToken);


                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }



            }

            if (projectData.Date == null)
            {
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Please enter Date on which your work has been done.(e.g :DD/MM/YYYY)") }, cancellationToken);

            }
            else
            {
                return await stepContext.NextAsync(projectData.Date, cancellationToken);
            }


        }

        private async Task<DialogTurnResult> FinalAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var projectData = (ProjectData)stepContext.Options;

            projectData.Date = (string)stepContext.Result;

            try
            {

                string StatusSuccess = "0";

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = Configuration["DataSource"];
                builder.UserID = Configuration["UserID"];
                builder.Password = Configuration["Password"];
                builder.InitialCatalog = Configuration["InitialCatalog"];
              
            
                string dateString = projectData.Date;
                string format = "dd/mm/yyyy";
                DateTime dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
                string strNewDate = dateTime.ToString("yyyy-mm-dd");


                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {


                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Insert into task(pdi_engineer_id,task,task_datetime) values('" + projectData.EmployeeID + "','" + projectData.Workstatus + "','" + strNewDate + "')");

                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.RecordsAffected == 1)
                            {

                                StatusSuccess = "1";
                            }
                            else
                            {

                                StatusSuccess = "0";
                            }

                        }
                    }
                    connection.Close();
                    if (StatusSuccess == "1")
                    {
                        projectData.EmployeeName = null;
                        projectData.EmployeeID = 0;
                        projectData.Date = null;
                        projectData.Workstatus = null;
                        StatusSuccess = "0";
                        projectData.intentIdenified = null;
                        await stepContext.Context.SendActivityAsync(MessageFactory.Text("Work Status is submitted successfully."), cancellationToken);
                        return await stepContext.BeginDialogAsync(nameof(MainDialog), projectData, cancellationToken);
                    }
                    else
                    {
                        projectData.EmployeeName = null;
                        projectData.EmployeeID = 0;
                        projectData.Date = null;
                        projectData.Workstatus = null;
                        StatusSuccess = "0";
                        projectData.intentIdenified = null;
                        await stepContext.Context.SendActivityAsync(MessageFactory.Text("Work Status is not updated.\nPlease try again!"), cancellationToken);
                        return await stepContext.BeginDialogAsync(nameof(MainDialog), projectData, cancellationToken);
                    }


                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }



            var confirm = "SubmitStatusNLP";
           
            return await stepContext.EndDialogAsync(confirm, cancellationToken);





        }




    }
}
