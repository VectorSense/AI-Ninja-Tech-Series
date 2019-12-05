using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;



namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace Speech
            {
                class SpeechRecognitionSamples
                {
                   
                    public static async Task SpeechContinuousRecognitionUsingCustomizedModelAsync()
                    {
                        // Creates an instance of a speech config with specified subscription key and service region.
                        // Replace with your own subscription key and service region (e.g., "westus").
                        var config = SpeechConfig.FromSubscription("", "westus");
                        var language = "en-IN";
                        config.SpeechRecognitionLanguage = language;
                        using (var recognizer = new SpeechRecognizer(config))
                        {
                            // Subscribes to events.
                            recognizer.Recognizing += (s, e) =>
                            {
                                //Console.WriteLine($"\nRECOGNIZING: Text={e.Result.Text}");
                            };

                            recognizer.Recognized += (s, e) =>
                            {
                                var result = e.Result;
                                //Console.WriteLine($"\nReason: {result.Reason.ToString()}");
                                if (result.Reason == ResultReason.RecognizedSpeech)
                                {
                                    Console.WriteLine($"\nText: {result.Text}.");
                                }
                            };

                            recognizer.Canceled += (s, e) =>
                            {
                                Console.WriteLine($"\nRecognition Canceled. Reason: {e.Reason.ToString()}, CanceledReason: {e.Reason}");
                            };

                            recognizer.SessionStarted += (s, e) =>
                            {
                                Console.WriteLine("\nSession started event.");
                            };

                            recognizer.SessionStopped += (s, e) =>
                            {
                                Console.WriteLine("\nSession stopped event.");
                            };

                            // Starts continuous recognition. Uses StopContinuousRecognitionAsync() to stop recognition.
                            await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                            do
                            {
                                Console.WriteLine("Press Enter to stop");
                            } while (Console.ReadKey().Key != ConsoleKey.Enter);

                            // Stops recognition.
                            await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                        }
                    }
                }
            }
        }
    }
}
