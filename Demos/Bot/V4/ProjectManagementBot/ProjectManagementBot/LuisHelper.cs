// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.3.0

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ProjectManagementBot
{
    public static class LuisHelper
    {
        public static async Task<ProjectData> ExecuteLuisQuery(IConfiguration configuration, ILogger logger, ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var ProjectData = new ProjectData();

            try
            {
                // Create the LUIS settings from configuration.
                var luisApplication = new LuisApplication(
                    configuration["LuisAppId"],
                    configuration["LuisAPIKey"],
                    configuration["LuisAPIHostName"]
                );

                var recognizer = new LuisRecognizer(luisApplication);

                // The actual call to LUIS
                var recognizerResult = await recognizer.RecognizeAsync(turnContext, cancellationToken);

                var (intent, score) = recognizerResult.GetTopScoringIntent();
                ProjectData.intentIdenified = intent;
                if (intent == "Get_Status")
                {
                   
                    ProjectData.EmployeeName = recognizerResult.Entities["Name"]?.FirstOrDefault()?.ToString();
                    ProjectData.Date = recognizerResult.Entities["datetime"]?.FirstOrDefault()?["timex"]?.FirstOrDefault()?.ToString().Split('T')[0];
                    var DateText = recognizerResult.Entities["$instance"]["datetime"]?.FirstOrDefault()?["text"].ToString();
                   
                    if ((ProjectData.Date != null) && (ProjectData.Date.Contains("XXXX") != true)&&((DateText!="today")&&(DateText!="yesterday")&&(DateText!="tommorow")))
                    {
                      
                        ProjectData.Date = DateText;
                    }
                    if ((ProjectData.Date != null) && (ProjectData.Date.Contains("XXXX") != true) && ((DateText == "today") || (DateText == "yesterday") || (DateText == "tommorow")))
                    {
                        var replace2 = ProjectData.Date.Replace("-", "/");
                        string[] sa = replace2.Split('/');
                        string strNew = sa[2] + "/" + sa[1] + "/" + sa[0];
                        ProjectData.Date = strNew;
                    }
                    if (ProjectData.Date.Contains("XXXX") == true)
                    {
                        var replace1 = ProjectData.Date.Replace("XXXX", "2019");
                        ProjectData.Date = replace1;
                        var replace2 = ProjectData.Date.Replace("-", "/");
                        ProjectData.Date = replace2;
                        string Dateused = Convert.ToDateTime(ProjectData.Date).ToString("dd/MM/yyyy");
                        ProjectData.Date = Dateused;

                    }

                }
                else if (intent == "ReportStatus")
                {

                    
                    ProjectData.EmployeeName = recognizerResult.Entities["Name"]?.FirstOrDefault()?.ToString();
                    ProjectData.Date = recognizerResult.Entities["datetime"]?.FirstOrDefault()?["timex"]?.FirstOrDefault()?.ToString().Split('T')[0];
                    var DateText = recognizerResult.Entities["$instance"]["datetime"]?.FirstOrDefault()?["text"].ToString();
                    
                    if ((ProjectData.Date != null) && (ProjectData.Date.Contains("XXXX") != true) && ((DateText != "today")&& (DateText != "yesterday") && (DateText != "tommorow")))
                    {
                        
                        ProjectData.Date = DateText;
                    }
                    if ((ProjectData.Date != null) && (ProjectData.Date.Contains("XXXX") != true) && ((DateText == "today") || (DateText == "yesterday") || (DateText == "tommorow")))
                    {
                        var replace2 = ProjectData.Date.Replace("-", "/");
                        string[] sa = replace2.Split('/');
                        string strNew = sa[2] + "/" + sa[1] + "/" + sa[0];
                        ProjectData.Date = strNew;
                    }
                    if (ProjectData.Date.Contains("XXXX") == true)
                    {
                        var replace1 = ProjectData.Date.Replace("XXXX", "2019");
                        ProjectData.Date = replace1;
                        var replace2 = ProjectData.Date.Replace("-", "/");
                        ProjectData.Date = replace2;
                        string Dateused = Convert.ToDateTime(ProjectData.Date).ToString("dd/MM/yyyy");
                        ProjectData.Date = Dateused;

                    }

                } else if (intent=="ReportStatusWithInput") {

                    ProjectData.Workstatus= recognizerResult.Entities["WorkStatus"]?.FirstOrDefault()?.ToString();
                    ProjectData.Date = recognizerResult.Entities["datetime"]?.FirstOrDefault()?["timex"]?.FirstOrDefault()?.ToString().Split('T')[0];
                    var DateText = recognizerResult.Entities["$instance"]["datetime"]?.FirstOrDefault()?["text"].ToString();
                    
                    if ((ProjectData.Date != null) && (ProjectData.Date.Contains("XXXX") != true) && ((DateText != "today") && (DateText != "yesterday") && (DateText != "tommorow")))
                    {
                       
                        ProjectData.Date = DateText;
                    }
                    if ((ProjectData.Date != null) && (ProjectData.Date.Contains("XXXX") != true) && ((DateText == "today") || (DateText == "yesterday") || (DateText == "tommorow")))
                    {
                        var replace2 = ProjectData.Date.Replace("-", "/");
                        string[] sa = replace2.Split('/');
                        string strNew = sa[2] + "/" + sa[1] + "/" + sa[0];
                        ProjectData.Date = strNew;
                    }
                    if (ProjectData.Date.Contains("XXXX") == true)
                    {
                        var replace1 = ProjectData.Date.Replace("XXXX", "2019");
                        ProjectData.Date = replace1;
                        var replace2 = ProjectData.Date.Replace("-", "/");
                        ProjectData.Date = replace2;
                        string Dateused = Convert.ToDateTime(ProjectData.Date).ToString("dd/MM/yyyy");
                        ProjectData.Date = Dateused;

                    }

                }
            }
            catch (Exception e)
            {
                logger.LogWarning($"LUIS Exception: {e.Message} Check your LUIS configuration.");
            }

            return ProjectData;
        }
    }
}
