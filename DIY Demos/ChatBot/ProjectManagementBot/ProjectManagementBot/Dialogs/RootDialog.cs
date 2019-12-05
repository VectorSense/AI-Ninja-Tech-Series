using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;


using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System.Web.Configuration;



using System.Web.Hosting;
using AdaptiveCards;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using Microsoft.Bot.Builder.ConnectorEx;
using System.Data.SqlClient;
using System.Text;


namespace ProjectManagementBot.Dialogs
{
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        string dataInput;
        int Employeeid;
        string Status;
        string EmployeeName;
        dynamic DateFromLuis;
        static bool Namepresent=false;
        static bool Datepresent = false;
        int PDIEngineerID;

        public RootDialog() : base(GetNewService())
        {

        }

        private static ILuisService[] GetNewService()
        {
            var modelId = ConfigurationManager.AppSettings.Get("LuisModelId");
            var subscriptionKey = ConfigurationManager.AppSettings.Get("LuisSubscriptionKey");

            var luisModel = new LuisModelAttribute(modelId, subscriptionKey);
            return new ILuisService[] { new LuisService(luisModel) };
        }


        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"I didn't get it.I am still learning.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ReportStatus")]
        public async Task Reportstatus(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            //await context.PostAsync($"I am in report Status intent."); 
            string filePath = HostingEnvironment.MapPath($"/Adaptive_Cards/SubmitWorkStatus.json");
            var adaptiveCardJson = File.ReadAllText(filePath);

            var returnMessage = context.MakeMessage();
            var results = AdaptiveCard.FromJson(adaptiveCardJson);
            var card = results.Card;
            returnMessage.Attachments.Add(new Attachment()
            {
                Content = card,
                ContentType = "application/vnd.microsoft.card.adaptive",
                Name = "Card",
            });
            await context.PostAsync(returnMessage);
            context.Wait(MessageReceivedAsync);
            //await SuggestiveActions(context, activity);

            //context.Wait(this.MessageReceived);

        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> item)
        {
            var message = await item;

            dynamic value = message.Value;
            string tex = value["id"];
            if (value != null)
            {
                if (tex == "submit_WorkStatus")
                {
                   var  dataInput = value["data_WorkStatus"];
                    var EmployeeID = value["id_WorkStatus"];
                    if ((dataInput == "")&&(EmployeeID ==""))
                    {
                        await context.PostAsync($"Please fill all fields above.");
                    }
                    else
                    {
                        DateTime now = DateTime.Now;
                        Status = dataInput;
                        Employeeid = EmployeeID;
                       

                        try
                        {
                            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                            builder.DataSource = WebConfigurationManager.AppSettings["DataSource"];
                            builder.UserID = WebConfigurationManager.AppSettings["UserID"];
                            builder.Password = WebConfigurationManager.AppSettings["Password"];
                            builder.InitialCatalog = WebConfigurationManager.AppSettings["InitialCatalog"];

                            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                            {

                                
                                connection.Open();
                                StringBuilder sb = new StringBuilder();
                                sb.Append("Insert into task(pdi_engineer_id,task,task_datetime) values('" + Employeeid + "','" + Status + "','" + now + "')");

                                String sql = sb.ToString();

                                using (SqlCommand command = new SqlCommand(sql, connection))
                                {
                                    command.ExecuteNonQuery();
                                    Employeeid =0;
                                    Status = "";

                                    await context.PostAsync($"Your work status is updated successfully.\n\nHow can I help you ?");
                                   
                                    
                                
                                    
                                    context.Wait(this.MessageReceived);

                                }
                            }
                        }
                        catch (SqlException e)
                        {
                            Console.WriteLine(e.ToString());
                        }

                    }

                }
                else if (tex == "cancel_Workstatus")
                {
                    await context.PostAsync($"How can I help you ?");
                    
                    context.Wait(this.MessageReceived);

                }
            }

        }

      


        [LuisIntent("Greetings")]
        public async Task GreetingMessage(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Welcome to Project Management Bot.\nHow can I help you ?");
           

        }


        [LuisIntent("Get_Status")]
        public async Task GetMeStatus(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
       
            
            for (var i = 0; i < result.Entities.Count;i++) {

                //await context.PostAsync(result.Entities[i].Entity);
                //await context.PostAsync(result.Entities[i].Type);

                if (result.Entities[i].Type == "Name")
                {
                    EmployeeName = result.Entities[i].Entity;
                    Namepresent = true;


                }else if (result.Entities[i].Type == "builtin.datetimeV2.date") {

                    Datepresent = true;
                    DateFromLuis= result.Entities[i].Entity; 

                }

            }

            CheckEntityPresent(context, activity);
           

        }


        private async Task CheckEntityPresent(IDialogContext context, IAwaitable<object> activity)
        {

            
            if (Namepresent == false)
            {
                await GetName(context, activity);


            }
            else if (Datepresent == false){
                   await GetDate(context, activity);

            }else if((Namepresent == true) && (Datepresent == true))
            {

                //get the id and fetch task from db
                // var p = DateFromLuis;
                //var j = EmployeeName;
                //context.PostAsync("hello i am here");
                GetIDforPDI(context, activity);



            }


        }



        private async Task GetIDforPDI(IDialogContext context, IAwaitable<object> result)
        {
            var resultFromNewOrder = await result;

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = WebConfigurationManager.AppSettings["DataSource"];
                builder.UserID = WebConfigurationManager.AppSettings["UserID"];
                builder.Password = WebConfigurationManager.AppSettings["Password"];
                builder.InitialCatalog = WebConfigurationManager.AppSettings["InitialCatalog"];

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {


                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("select id from pdi_engineer_details where name='" + EmployeeName + "'");

                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                PDIEngineerID = reader.GetInt32(0);

                                GetTaskforPDI(context, result);


                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }


        }


        private async Task GetTaskforPDI(IDialogContext context, IAwaitable<object> result)
        {
            var resultavail = await result;

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = WebConfigurationManager.AppSettings["DataSource"];
            builder.UserID = WebConfigurationManager.AppSettings["UserID"];
            builder.Password = WebConfigurationManager.AppSettings["Password"];
            builder.InitialCatalog = WebConfigurationManager.AppSettings["InitialCatalog"];

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {


                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("select task from task where convert(varchar(10),task_datetime,103)='" +DateFromLuis+ "' and pdi_engineer_id=" + PDIEngineerID + "");
                String sql = sb.ToString();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows!=false)
                        {
                            while (reader.Read())
                            {


                               context.PostAsync(reader.GetString(0));

                               
        
                            }
                            DateFromLuis = null;
                            PDIEngineerID = 0;
                                             
                            

                        }
                        else {
                            DateFromLuis = null;
                            PDIEngineerID = 0;
                           
                             context.PostAsync($"Data is not available.");
                             context.PostAsync($"How can I help you ?");

                            context.Wait(this.MessageReceived);


                        }
                        

                    }
                }
                
            }

            
            context.Wait(this.MessageReceived);


        }















        private async Task GetDate(IDialogContext context, IAwaitable<object> result)
        {

            PromptDialog.Text(
             context: context,
             resume: GetDatevalue,
             prompt: "Please enter Date for which you would like to check status.(e.g :DD/MM/YYYY)."
         );


        }

        private async Task GetDatevalue(IDialogContext context, IAwaitable<object> result)
        {
            var response = await result;
            DateFromLuis = response;
            Datepresent = true;

            await CheckEntityPresent(context, result);

        }

        public async Task GetName(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Text(
           context: context,
           resume: GetNamevalue,
           prompt: "Please enter Name of PDI Engineer."
          
           );



        }

        public async Task GetNamevalue(IDialogContext context, IAwaitable<string> result)
        {
            var response = await result;
            EmployeeName = response;
            Namepresent = true;

           await CheckEntityPresent(context, result);


        }



    }
}