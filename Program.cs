using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace speech_services_azure
{
    class Program
    {

public static async Task SynthesisToSpeakerAsync()
        {
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            // The default language is "en-us".
            var config = SpeechConfig.FromSubscription("ca14dda5909c4c4bb82eb4c93fc831f6","canadacentral");

            // Creates a speech synthesizer using the default speaker as audio output.
            using (var synthesizer = new SpeechSynthesizer(config))
            {
                // Receive a text from console input and synthesize it to speaker.
                Console.WriteLine("Type some text that you want to speak...");
                Console.Write("> ");
                string text = Console.ReadLine();

                using (var result = await synthesizer.SpeakTextAsync(text))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
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

                // This is to give some time for the speaker to finish playing back the audio
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
        public static async Task RecognizeSpeechAsycn()
        {
            var config = SpeechConfig.FromSubscription("ca14dda5909c4c4bb82eb4c93fc831f6","canadacentral");
           //var audio =  AudioConfig.FromWavFileInput("");
            using (var recogniger = new SpeechRecognizer(config))
            {
                var result = await recogniger.RecognizeOnceAsync();

                switch (result.Reason)
                {
                    case ResultReason.Canceled:
                    {
                        var cancellation = CancellationDetails.FromResult(result);
                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELLED : Error Code ={cancellation.ErrorCode}");
                             Console.WriteLine($"CANCELLED : Error Details ={cancellation.ErrorDetails}");
                        }
                    }
                    break;
                    case ResultReason.RecognizedSpeech:
                    {
                        Console.WriteLine($"We recognized : {result.Text} within {result.Duration}");
                    }
                    break;
                    case ResultReason.NoMatch:
                    {
                         Console.WriteLine($"NOMATCH : Speech chould not be recognized.");
                    }
                    break;
                }
            }
        }
  public static async Task RecognizeSpeechCustomAsycn()
  {
      var config = SpeechConfig.FromSubscription("83038629aa20401dad98e2d5d9d6ade9","canadacentral");
      config.EndpointId = "751121ac-d119-4734-a387-3168e1dff423";
      

            using (var recogniger = new SpeechRecognizer(config))
            {
                var result = await recogniger.RecognizeOnceAsync();

                switch (result.Reason)
                {
                    case ResultReason.Canceled:
                    {
                        var cancellation = CancellationDetails.FromResult(result);
                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELLED : Error Code ={cancellation.ErrorCode}");
                             Console.WriteLine($"CANCELLED : Error Details ={cancellation.ErrorDetails}");
                        }
                    }
                    break;
                    case ResultReason.RecognizedSpeech:
                    {
                        Console.WriteLine($"We recognized : {result.Text} within {result.Duration}");
                    }
                    break;
                    case ResultReason.NoMatch:
                    {
                         Console.WriteLine($"NOMATCH : Speech chould not be recognized.");
                    }
                    break;
                }
            }
  }
        static void Main(string[] args)
        {
             // SynthesisToSpeakerAsync().Wait();

            RecognizeSpeechCustomAsycn().Wait();
          
            Console.WriteLine("Please press a key to continue");
            Console.ReadLine();
        }
    }
}
