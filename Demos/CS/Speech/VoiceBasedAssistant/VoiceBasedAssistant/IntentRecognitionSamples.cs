using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Intent;
using Newtonsoft.Json.Linq;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace Speech
            {
                class IntentRecognitionSamples
                {

                    public static async Task VoiceBasedAssistant()
                    {
                        // <intentContinuousRecognitionWithFile>
                        // Creates an instance of a speech config with specified subscription key
                        // and service region. Note that in contrast to other services supported by
                        // the Cognitive Services Speech SDK, the Language Understanding service
                        // requires a specific subscription key from https://www.luis.ai/.
                        // The Language Understanding service calls the required key 'endpoint key'.
                        // Once you've obtained it, replace with below with your own Language Understanding subscription key
                        // and service region (e.g., "westus").
                        var Voiceconfig = SpeechConfig.FromSubscription("", "westus");
                        var language = "en-IN";
                        Voiceconfig.SpeechRecognitionLanguage = language;
                        // Creates a speech synthesizer using the default speaker as audio output.
                        using (var synthesizer = new SpeechSynthesizer(Voiceconfig))
                        {
                            var config = SpeechConfig.FromSubscription("", "westus");
                            config.SpeechRecognitionLanguage = language;
                            using (var recognizer = new IntentRecognizer(config))
                            {
                                // Creates a Language Understanding model using the app id, and adds specific intents from your model
                                var model = LanguageUnderstandingModel.FromAppId("");
                                recognizer.AddIntent(model, "DiscoverDealsForTheDay", "id1");
                                recognizer.AddIntent(model, "DiscoverLocation", "id2");
                                recognizer.AddIntent(model, "DiscoverPersonalizedDeals", "id3");
                                recognizer.AddIntent(model, "GoodBye", "id4");
                                recognizer.AddIntent(model, "Greeting", "id5");
                                recognizer.AddIntent(model, "HowToGoToTrialRoom", "id6");
                                recognizer.AddIntent(model, "DiscoverPaymentQueueLength", "id7");

                                // Subscribes to events.
                                recognizer.Recognizing += (s, e) =>
                                {
                                    //Console.WriteLine($"RECOGNIZING: Text={e.Result.Text}");
                                };

                                recognizer.Recognized += (s, e) =>
                                {
                                    if (e.Result.Reason == ResultReason.RecognizedIntent)
                                    {
                                        Console.WriteLine($"\nText={e.Result.Text}");
                                        //Console.WriteLine($"    Intent Id: {e.Result.IntentId}.");
                                        string Response = e.Result.Properties.GetProperty(PropertyId.LanguageUnderstandingServiceResponse_JsonResult);
                                        //Console.Write(Response);
                                        dynamic IntentResponse = JObject.Parse(Response);
                                        dynamic IntentResult = JObject.Parse(IntentResponse.topScoringIntent.ToString());
                                        string Intent = IntentResult.intent.ToString();
                                        Console.WriteLine("Intent: "+Intent);
                                        String TextResult = "";
                                        if (Intent == "Greeting")
                                            TextResult = "Hello Suneetha, Welcome back to Shopper's Heaven. Last time you have visited us on Jan 1st ,2019 for some new year offers. How can I help you today ?";
                                        else if (Intent == "DiscoverDealsForTheDay")
                                            TextResult = "Sure...Give me 2 mins.. We have got great offers for you today. Women and Kids Casual Wears having 40 % discount offers. Women Foot wear section had just launched 60 % clearance sale offer. And cosmetics section is having personalized offer for you";
                                        else if (Intent == "DiscoverLocation")
                                            TextResult = "Please go till Gate 2 and take the elevator to second floor. You can find Woman's casual wear section on your righthand side";
                                        else if (Intent == "HowToGoToTrialRoom")
                                            TextResult = "Trail room is just 200 metres away from you on your left hand side. But looks like all trial rooms are busy now. Please try after 10 mins";
                                        else if (Intent == "DiscoverPersonalizedDeals")
                                            TextResult = "Please proceed to cosmetic sections at the entrance of second floor. Swith to scan mode in your app and just scan the product that you are interested to buy. You will get Virtual vouchers for that product which you can redeem when you are buying it";
                                        else if (Intent == "DiscoverPaymentQueueLength")
                                            TextResult = "Its looks good Suneetha. You will be 3rd person in the queue. Please proceed";
                                        else if (Intent == "GoodBye")
                                            TextResult = "Thanks Suneetha for visiting us again. Hope you enjoyed shopping with me today. See you soon. Last but not least, please do not forget to give feedback about your experience at Shopper Heaven today at the link..";
                                        else
                                            TextResult = "No Intent Found";
                                        Console.WriteLine(TextResult + "\n");
                                        synthesizer.SpeakTextAsync(TextResult).Wait();
                                       
                                    }
                                    else if (e.Result.Reason == ResultReason.RecognizedSpeech)
                                    {
                                        //Console.WriteLine($"RECOGNIZED: Text={e.Result.Text}");
                                        //Console.WriteLine($"    Intent not recognized.");
                                    }
                                    else if (e.Result.Reason == ResultReason.NoMatch)
                                    {
                                        //Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                                    }
                                };

                                recognizer.Canceled += (s, e) =>
                                {
                                    Console.WriteLine($"\nCANCELED: Reason={e.Reason}");

                                    if (e.Reason == CancellationReason.Error)
                                    {
                                        Console.WriteLine($"CANCELED: ErrorCode={e.ErrorCode}");
                                        Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
                                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                                    }
                                };

                                recognizer.SessionStarted += (s, e) =>
                                {
                                    //Console.WriteLine("\nSession started");
                                };

                                recognizer.SessionStopped += (s, e) =>
                                {
                                    //Console.WriteLine("\nSession stopped");
                                    //Console.WriteLine("\nStop recognition.");
                                };


                                // Starts continuous recognition. Uses StopContinuousRecognitionAsync() to stop recognition.
                                Console.WriteLine("Say something...");
                                await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                                // Waits for completion.
                                // Use Task.WaitAny to keep the task rooted.

                                do
                                {
                                    Console.WriteLine("Press Enter to stop");
                                } while (Console.ReadKey().Key != ConsoleKey.Enter);

                                // Stops recognition.
                                await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                            }
                        }
                        // </intentContinuousRecognitionWithFile>
                    }
                }
            }
        }
    }
}

