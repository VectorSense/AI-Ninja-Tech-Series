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
                        config.EndpointId = "";
                        // Creates a speech recognizer from microphone.
                        var language = "en-IN";
                        config.SpeechRecognitionLanguage = language;
                        using (var recognizer = new SpeechRecognizer(config))
                        {
                            // Subscribes to events.
                            recognizer.Recognizing += (s, e) =>
                            {
                                Console.WriteLine($"\nRECOGNIZING: Text={e.Result.Text}");
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

                    // Speech recognition using a customized model.
                    public static async Task RecognitionUsingCustomizedModelAsync()
                    {
                        // <recognitionCustomized>
                        // Creates an instance of a speech config with specified subscription key and service region.
                        // Replace with your own subscription key and service region (e.g., "westus").
                        var config = SpeechConfig.FromSubscription("", "westus");
                        // Replace with the CRIS endpoint id of your customized model.
                        config.EndpointId = "";
                        //var language = "en-IN";
                        //config.SpeechRecognitionLanguage = language;
                        // Creates a speech recognizer using microphone as audio input.
                        using (var recognizer = new SpeechRecognizer(config))
                        {
                            // Starts speech recognition, and returns after a single utterance is recognized. The end of a
                            // single utterance is determined by listening for silence at the end or until a maximum of 15
                            // seconds of audio is processed.  The task returns the recognition text as result. 
                            // Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
                            // shot recognition like command or query. 
                            // For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
                            while (true)
                            {
                                Console.WriteLine("Say something...");
                                var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

                                // Checks results.
                                if (result.Reason == ResultReason.RecognizedSpeech)
                                {
                                    Console.WriteLine($"RECOGNIZED: Text={result.Text}");
                                }
                                else if (result.Reason == ResultReason.NoMatch)
                                {
                                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                                }
                                else if (result.Reason == ResultReason.Canceled)
                                {
                                    var cancellation = CancellationDetails.FromResult(result);
                                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                                    if (cancellation.Reason == CancellationReason.Error)
                                    {
                                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                                        Console.WriteLine($"CANCELED: Did you update the subscription info?");
                                    }
                                }
                                Console.WriteLine("\nPress 0 to stop");
                                if (Console.ReadKey().Key == ConsoleKey.D0)
                                    break;
                            }
                        }
                        // </recognitionCustomized>
                    }



                }
            }
        }
    }
}
