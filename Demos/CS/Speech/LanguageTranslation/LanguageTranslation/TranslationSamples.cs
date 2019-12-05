using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Translation;

namespace PartnerTechSeries
{
    namespace AI
    {
        namespace Demo
        {
            namespace Speech
            {
                class TranslationSamples
                {
                    // Translation from microphone.
                    public static async Task TranslationWithMicrophoneAsync()
                    {
                        // <TranslationWithMicrophoneAsync>
                        // Translation source language.
                        // Replace with a language of your choice.
                        string fromLanguage = "en-US";

                        // Voice name of synthesis output.
                        const string GermanVoice = "Microsoft Server Speech Text to Speech Voice (de-DE, Hedda)";

                        // Creates an instance of a speech translation config with specified subscription key and service region.
                        // Replace with your own subscription key and service region (e.g., "westus").
                        var config = SpeechTranslationConfig.FromSubscription("", "westus");
                        config.SpeechRecognitionLanguage = fromLanguage;
                        config.VoiceName = GermanVoice;

                        // Translation target language(s).
                        // Replace with language(s) of your choice.
                        config.AddTargetLanguage("de");

                        // Creates a translation recognizer using microphone as audio input.
                        using (var recognizer = new TranslationRecognizer(config))
                        {
                            // Subscribes to events.
                            recognizer.Recognizing += (s, e) =>
                            {
                                //Console.WriteLine($"RECOGNIZING in '{fromLanguage}': Text={e.Result.Text}");
                                //foreach (var element in e.Result.Translations)
                                //{
                                //    Console.WriteLine($"    TRANSLATING into '{element.Key}': {element.Value}");
                                //}
                            };

                            recognizer.Recognized += (s, e) =>
                            {
                                if (e.Result.Reason == ResultReason.TranslatedSpeech)
                                {
                                    Console.WriteLine($"\nRECOGNIZED in '{fromLanguage}': Text={e.Result.Text}");
                                    foreach (var element in e.Result.Translations)
                                    {
                                        Console.WriteLine($"TRANSLATED into '{element.Key}': {element.Value}");
                                    }
                                }
                                else if (e.Result.Reason == ResultReason.RecognizedSpeech)
                                {
                                    //Console.WriteLine($"\nText={e.Result.Text}");
                                    //Console.WriteLine($"    Speech not translated.");
                                }
                                else if (e.Result.Reason == ResultReason.NoMatch)
                                {
                                    //Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                                }
                            };

                            recognizer.Synthesizing += (s, e) =>
                            {
                                //var audio = e.Result.GetAudio();
                                //Console.WriteLine(audio.Length != 0
                                //    ? $"AudioSize: {audio.Length}"
                                //    : $"AudioSize: {audio.Length} (end of synthesis data)");

                                //if (audio.Length > 0)
                                //{
                                //    #if NET461
                                //                            using (var m = new MemoryStream(audio))
                                //                            {
                                //                                SoundPlayer simpleSound = new SoundPlayer(m);
                                //                                simpleSound.PlaySync();
                                //                            }
                                //    #endif
                                //}
                            };

                            recognizer.Canceled += (s, e) =>
                            {
                                Console.WriteLine($"CANCELED: Reason={e.Reason}");

                                if (e.Reason == CancellationReason.Error)
                                {
                                    Console.WriteLine($"CANCELED: ErrorCode={e.ErrorCode}");
                                    Console.WriteLine($"CANCELED: ErrorDetails={e.ErrorDetails}");
                                    Console.WriteLine($"CANCELED: Did you update the subscription info?");
                                }
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
                            Console.WriteLine("Say something...");
                            await recognizer.StartContinuousRecognitionAsync().ConfigureAwait(false);

                            do
                            {
                                Console.WriteLine("Press Enter to stop");
                            } while (Console.ReadKey().Key != ConsoleKey.Enter);

                            // Stops continuous recognition.
                            await recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                        }
                        // </TranslationWithMicrophoneAsync>
                    }

                }
            }
        }
    }
}