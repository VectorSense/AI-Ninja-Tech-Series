using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace TextAnalytics
            {
                public class SentimentAnalysisSample
                {
                    public List<string> Sentiment = new List<string>();
                    public async Task RunAsync(string endpoint, string key,string text)
                    {
                        var credentials = new ApiKeyServiceClientCredentials(key);
                        var client = new TextAnalyticsClient(credentials)
                        {
                            Endpoint = endpoint
                        };

                        // The documents to be analyzed. Add the language of the document. The ID can be any value.
                        var inputDocuments = new MultiLanguageBatchInput(
                            new List<MultiLanguageInput>
                            {
                                new MultiLanguageInput("en", "1", text)
                            });

                        var result = await client.SentimentAsync(false, inputDocuments);

                        // Printing sentiment results
                        foreach (var document in result.Documents)
                        {
                            Sentiment.Add($"{document.Score:0.00}");
                            //Console.WriteLine($"Document ID: {document.Id} , Sentiment Score: {document.Score:0.00}");
                        }
                     }
                }
            }
        }
    }
}
