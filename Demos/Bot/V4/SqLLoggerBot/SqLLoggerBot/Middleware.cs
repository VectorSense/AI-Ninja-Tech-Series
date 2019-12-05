using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SqLLoggerBot
{
    public class Middleware: IMiddleware
    {
        private readonly IConfiguration _configuration;

    

        public Middleware(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }








        public async Task OnTurnAsync(ITurnContext context, NextDelegate next, CancellationToken cancellationToken = default(CancellationToken))
        {

            await next(cancellationToken);


            if ((context.Activity.From.Id != "") && (context.Activity.Recipient.Id != "") && (context.Activity.Text.Length != 0))
            {

               

                string fromid = context.Activity.From.Id;
                string toid = context.Activity.Recipient.Id;
                string message = context.Activity.Text;

                try
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                   

                    builder.DataSource = _configuration["DataSource"];
                    builder.UserID = _configuration["UserID"];
                    builder.Password = _configuration["Password"];
                     builder.InitialCatalog = _configuration["InitialCatalog"];

                    




                    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                    {

                        connection.Open();
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Insert into user_chat_log(from_id,to_id,message)values('" + fromid + "','" + toid + "','" + message + "')");



                        String sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.ExecuteNonQuery();
                            Debug.WriteLine("Insert in Azure SQL DataBase Successfull");


                        }
                    }
                }
                catch (SqlException e)
                {
                    Debug.WriteLine("Error is there :" + e.ToString());
                }








            }


        }


    }
}
