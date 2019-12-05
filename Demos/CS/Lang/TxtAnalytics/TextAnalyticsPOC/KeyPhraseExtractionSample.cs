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
                public class KeyPhraseExtractionSample
                {
                    public List<string> Phrase = new List<string>();
                    public async Task RunAsync(string endpoint, string key,string text)
                    {
                        var credentials = new ApiKeyServiceClientCredentials(key);
                        var client = new TextAnalyticsClient(credentials)
                        {
                            Endpoint = endpoint
                        };

                        var inputDocuments = new MultiLanguageBatchInput(
                                    new List<MultiLanguageInput>
                                    {
                                    new MultiLanguageInput("en", "1", text)
                                    });

                        var kpResults = await client.KeyPhrasesAsync(false, inputDocuments);

                        // Printing keyphrases
                        foreach (var document in kpResults.Documents)
                        {
                            //Console.WriteLine($"Document ID: {document.Id} ");

                            //Console.WriteLine("\t Key phrases:");

                            foreach (string keyphrase in document.KeyPhrases)
                            {
                                Phrase.Add($"{keyphrase}");
                                //Console.WriteLine($"\t\t{keyphrase}");
                            }
                        }
                     }
                }
            }
        }
    }
}
