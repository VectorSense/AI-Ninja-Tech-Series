using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace TextAnalytics
            {
                public class LanguageDetectionSample
                {
                    public List<string> Language = new List<string>();
                    public async Task RunAsync(string endpoint, string key, string text)
                    {
                        var credentials = new ApiKeyServiceClientCredentials(key);
                        var client = new TextAnalyticsClient(credentials)
                        {
                            Endpoint = endpoint
                        };

                        // The documents to be submitted for language detection. The ID can be any value.
                        var inputDocuments = new LanguageBatchInput(
                                new List<LanguageInput>
                                    {
                                        new LanguageInput(id: "1", text: text)
                                    });

                        var langResults = await client.DetectLanguageAsync(false, inputDocuments);

                        // Printing detected languages
                          foreach (var document in langResults.Documents)
                        {
                            Language.Add($"{document.DetectedLanguages[0].Name}");
                            //Console.WriteLine($"Document ID: {document.Id} , Language: {document.DetectedLanguages[0].Name}");
                        }
                    }
                }
            }
        }
    }
}
