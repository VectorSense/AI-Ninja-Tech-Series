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
                public class RecognizeEntitiesSample
                {
                    public List<string> Entity = new List<string>();
                    public async Task RunAsync(string endpoint, string key, string text)
                    {
                        var credentials = new ApiKeyServiceClientCredentials(key);
                        var client = new TextAnalyticsClient(credentials)
                        {
                            Endpoint = endpoint
                        };

                        // The documents to be submitted for entity recognition. The ID can be any value.
                        var inputDocuments = new MultiLanguageBatchInput(
                            new List<MultiLanguageInput>
                            {
                            new MultiLanguageInput("en", "1", text)
                            });

                        var entitiesResult = await client.EntitiesAsync(false, inputDocuments);

                        // Printing recognized entities
                        foreach (var document in entitiesResult.Documents)
                        {
                            foreach (var entity in document.Entities)
                            {
                                Entity.Add($"{entity.Name}[{entity.Type ?? "N/A"}]");
                                //Console.WriteLine($"\t\tName: {entity.Name},\tType: {entity.Type ?? "N/A"},\tSub-Type: {entity.SubType ?? "N/A"}");
                                //foreach (var match in entity.Matches)
                                //{
                                //    Console.WriteLine($"\t\t\tOffset: {match.Offset},\tLength: {match.Length},\tScore: {match.EntityTypeScore:F3}");
                                //}
                            }
                        }
                    }
                }
            }

        }
    }
}