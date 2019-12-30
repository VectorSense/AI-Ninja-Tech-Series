using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace Speech
            {
                class TextToSpeechSample
                {
                    // Speech synthesis to the default speaker.
                    public static async Task SynthesisToSpeakerAsync()
                    {
                        // Creates an instance of a speech config with specified subscription key and service region.
                        // Replace with your own subscription key and service region (e.g., "westus").
                        // The default language is "en-us".
                        var config = SpeechConfig.FromSubscription("", "westus");
                        var language = "en-IN";
                        config.SpeechRecognitionLanguage = language;
                        // Creates a speech synthesizer using the default speaker as audio output.
                        using (var synthesizer = new SpeechSynthesizer(config))
                        {
                            while (true) { 
                                // Receives a text from console input and synthesize it to speaker.
                                Console.WriteLine("\nType some text that you want to speak...");
                                Console.Write("> ");
                                string text = Console.ReadLine();

                                using (var result = await synthesizer.SpeakTextAsync(text))
                                {
                                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                                    {
                                        //Console.WriteLine($"text: {text}");
                                    }
                                    else if (result.Reason == ResultReason.Canceled)
                                    {
                                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                                        if (cancellation.Reason == CancellationReason.Error)
                                        {
                                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                                            Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                                        }

                                    }
                                }
                                Console.WriteLine("\nPress 0 to stop");
                                if (Console.ReadKey().Key == ConsoleKey.D0)
                                    break;

                            }

                        }
                    }

                }
            }
        }
    }
}