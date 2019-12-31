using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace FileLoggerBot
{
    public class Middleware:IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext context, NextDelegate next, CancellationToken cancellationToken = default(CancellationToken))
        {

            await next(cancellationToken);


            if ((context.Activity.From.Id != "") && (context.Activity.Recipient.Id != "") && (context.Activity.Text.Length != 0))
            {
                try
                {
                    //string docPath = Environment.GetFolderPath()
                    //Pass the filepath and filename to the StreamWriter Constructor
                    StreamWriter sw = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "LogData.txt"));

                    //Write a line of text
                    sw.WriteLine("From:" + context.Activity.From.Id + " - To:" + context.Activity.Recipient.Id + " - Message:" + context.Activity.Text);



                    //Close the file
                    sw.Close();
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine("Exception: " + e.Message);
                }
                finally
                {
                    Debug.WriteLine("Executing finally block.");
                }


               // Debug.WriteLine("From:" + context.Activity.From.Id + " - To:" + context.Activity.Recipient.Id + " - Message:" + context.Activity.Text);

            }


        }





    }
}
