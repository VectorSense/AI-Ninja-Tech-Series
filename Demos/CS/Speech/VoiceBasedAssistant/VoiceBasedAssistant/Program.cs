using System;
using PartnerTechSeries.AI.Demo.Speech;

namespace VoiceBasedAssistant
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Voivce Based Assistant");
            IntentRecognitionSamples.VoiceBasedAssistant().Wait();
        }
    }
}
