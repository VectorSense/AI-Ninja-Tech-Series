using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MiddlewareBot
{
    public class Middleware: IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext context, NextDelegate next, CancellationToken cancellationToken = default(CancellationToken))
        {
         

            await next(cancellationToken);

            
            if ((context.Activity.From.Id != "") && (context.Activity.Recipient.Id != "") && (context.Activity.Text.Length != 0))
            {

                Debug.WriteLine("From:" + context.Activity.From.Id + " - To:" + context.Activity.Recipient.Id + " - Message:" + context.Activity.Text);

            }

           
        }




    }
}
