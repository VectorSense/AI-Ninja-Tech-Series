using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.IO;
using RestSharp;
using System.Configuration;

//Install-Package Microsoft.AspNet.WebApi.Client


namespace SentimentAnalyse.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> TextAnalytics(string FileName)
        {          
           
            try
            {
                 string RecordingsBlobUri1 = "https://msspeechtext.blob.core.windows.net/msspeechtext/Bad%20Feedback.wav?sp=r&st=2019-12-28T14:21:19Z&se=2020-01-30T22:21:19Z&spr=https,http&sv=2019-02-02&sr=b&sig=06czEOR0nPJ3QUr4MCwINxVu9%2Be6BbTnc9J%2FHqH3UYg%3D";
                 string RecordingsBlobUri2 = "https://msspeechtext.blob.core.windows.net/msspeechtext/Good%20Feedback.wav?sp=r&st=2019-12-28T14:23:07Z&se=2020-01-30T22:23:07Z&spr=https,http&sv=2019-02-02&sr=b&sig=o7vESdz2LwG0P5%2B9qHvUqGkJDQEFKawMdOhLBE9WlHM%3D";

                Program p = new Program();
                if(FileName== "Bad Feedback.wav")
                    await p.TranscribeAsync(RecordingsBlobUri1);
                else
                    await p.TranscribeAsync(RecordingsBlobUri2);
                if (p.Error != null)
                    return Json(new { StatusCode = "F", Message = p.Error });
                else
                {
                    return Json(new { StatusCode = "S", Sentiment = p.Sentiment, Phrases = p.Phrases });
                }
            }
            catch (Exception e)
            {
                return Json(new { StatusCode = "F", Message = e.Message });
            }

        }
    }



    class Program
    {        
        // Replace with your subscription key
        private string TextAnalyticsSubscriptionKey = ConfigurationManager.AppSettings["TextAnalyticsSubscriptionKey"];

        // Update with your service region
        private string TextAnalyticsRegion = ConfigurationManager.AppSettings["TextAnalyticsRegion"];



        public string Sentiment = null, Phrases = null,Error = null;
        // Replace with your subscription key
        private string SubscriptionKey = ConfigurationManager.AppSettings["SpeechSubscriptionKey"];

        // Update with your service region
        private string Region = ConfigurationManager.AppSettings["SpeechRegion"];
        private const int Port = 443;

        // recordings and locale
        private const string Locale = "en-US";
        
        //name and description
        private const string Name = "Simple transcription";
        private const string Description = "Simple transcription description";

        private const string speechToTextBasePath = "api/speechtotext/v2.0/";

        private void SentimentAnalyse(string Text)
        {
            var client = new RestClient("https://"+ TextAnalyticsRegion + ".api.cognitive.microsoft.com/text/analytics/v3.0-preview.1/sentiment");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Ocp-Apim-Subscription-Key", TextAnalyticsSubscriptionKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"documents\": [{\"language\": \"en\",\"id\": \"1\",\"text\": \""+Text+"\"}]}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                dynamic JsonResult = JsonConvert.DeserializeObject(response.Content);
                if (JsonResult.documents.Count > 0)
                    Sentiment = JsonResult.documents[0].sentiment.ToString();
                else
                    Error = "No Document Found in Sentiment Analyse";
            }
            else
                Error = "Sentiment Analyse API Problem";
        }

        private void keyPhrases(string Text)
        {
            var client = new RestClient("https://" + TextAnalyticsRegion + ".api.cognitive.microsoft.com/text/analytics/v3.0-preview.1/keyPhrases");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Ocp-Apim-Subscription-Key", TextAnalyticsSubscriptionKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"documents\": [{\"language\": \"en\",\"id\": \"1\",\"text\": \""+ Text + "\"}]}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if ((int)response.StatusCode == 200)
            {
                dynamic JsonResult = JsonConvert.DeserializeObject(response.Content);
                if (JsonResult.documents.Count > 0)
                    for(int i=0;i< JsonResult.documents[0].keyPhrases.Count;i++)
                        Phrases += JsonResult.documents[0].keyPhrases[i].ToString()+"<br>";
                else
                    Error = "No Document Found in keyPhrases Analyse";
            }
            else
                Error = "keyPhrases Analyse API Problem";
           
        }

        public async Task TranscribeAsync(string Url)
        {
            // create the client object and authenticate
            var client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(25);
            client.BaseAddress = new UriBuilder(Uri.UriSchemeHttps, $"{Region}.cris.ai", Port).Uri;

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var path = $"{speechToTextBasePath}Transcriptions/";
            var transcriptionDefinition = TranscriptionDefinition.Create(Name, Description, Locale, new Uri(Url));
            
            string res = JsonConvert.SerializeObject(transcriptionDefinition);
            StringContent sc = new StringContent(res);
            sc.Headers.ContentType = JsonMediaTypeFormatter.DefaultMediaType;

            Uri transcriptionLocation = null;

            using (var response = await client.PostAsync(path, sc))
            {
                if (!response.IsSuccessStatusCode)
                {
                    Error = "Error " + response.StatusCode + " starting transcription.";
                    return;
                }

                transcriptionLocation = response.Headers.Location;
            }

            bool completed = false;
            Transcription transcription = null;
            string results0 = null;
            // check for the status of our transcriptions periodically
            while (!completed)
            {
                // get all transcriptions for the user
                using (var response = await client.GetAsync(transcriptionLocation.AbsolutePath).ConfigureAwait(false))
                {
                    var contentType = response.Content.Headers.ContentType;

                    if (response.IsSuccessStatusCode && string.Equals(contentType.MediaType, "application/json", StringComparison.OrdinalIgnoreCase))
                    {
                        transcription = await response.Content.ReadAsAsync<Transcription>().ConfigureAwait(false);
                    }
                    else
                    {
                        //Error = "Error with status "+ response.StatusCode + " getting transcription result";
                        continue;
                    }
                }

                // for each transcription in the list we check the status
                switch (transcription.Status)
                {
                    case "Failed":
                        completed = true;
                        Error = "Transcription failed. Status:" + transcription.StatusMessage;
                        break;
                    case "Succeeded":
                        completed = true;
                        var resultsUri0 = transcription.ResultsUrls["channel_0"];
                        WebClient webClient = new WebClient();
                        var filename = Path.GetTempFileName();
                        webClient.DownloadFile(resultsUri0, filename);
                        results0 = File.ReadAllText(filename);
                        break;

                    case "Running":
                        //Error = "Transcription is still running.";
                        break;

                    case "NotStarted":
                        //Error = "Transcription has not started.";
                        break;
                }

                await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
            }
            //var resultObject0 = JsonConvert.DeserializeObject<RootObject>(results0);
            dynamic JsonResult = JsonConvert.DeserializeObject(results0);
            string Result = JsonResult.AudioFileResults[0].CombinedResults[0].Display.ToString();
            SentimentAnalyse(Result);
            keyPhrases(Result);
            //InsName = JsonResult.AudioFileResults[0].SegmentResults[0].NBest[0].Display.ToString();
            //InsuranceNo = JsonResult.AudioFileResults[0].SegmentResults[1].NBest[0].Display.ToString();
            //DOB = JsonResult.AudioFileResults[0].SegmentResults[2].NBest[0].Display.ToString();
            //Premium = JsonResult.AudioFileResults[0].SegmentResults[3].NBest[0].Display.ToString();
            return;
        }
    }

    public sealed class ModelIdentity
    {
        private ModelIdentity(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; private set; }

        public static ModelIdentity Create(Guid Id)
        {
            return new ModelIdentity(Id);
        }
    }

    public sealed class Transcription
    {
        [JsonConstructor]
        private Transcription(Guid id, string name, string description, string locale, DateTime createdDateTime, DateTime lastActionDateTime, string status, Uri recordingsUrl, IReadOnlyDictionary<string, string> resultsUrls)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.CreatedDateTime = createdDateTime;
            this.LastActionDateTime = lastActionDateTime;
            this.Status = status;
            this.Locale = locale;
            this.RecordingsUrl = recordingsUrl;
            this.ResultsUrls = resultsUrls;
        }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public string Locale { get; set; }

        /// <inheritdoc />
        public Uri RecordingsUrl { get; set; }

        /// <inheritdoc />
        public IReadOnlyDictionary<string, string> ResultsUrls { get; set; }

        public Guid Id { get; set; }

        /// <inheritdoc />
        public DateTime CreatedDateTime { get; set; }

        /// <inheritdoc />
        public DateTime LastActionDateTime { get; set; }

        /// <inheritdoc />
        public string Status { get; set; }

        public string StatusMessage { get; set; }
    }

    public sealed class TranscriptionDefinition
    {
        private TranscriptionDefinition(string name, string description, string locale, Uri recordingsUrl, IEnumerable<ModelIdentity> models)
        {
            this.Name = name;
            this.Description = description;
            this.RecordingsUrl = recordingsUrl;
            this.Locale = locale;
            this.Models = models;
            this.properties = new Dictionary<string, string>();
            this.properties.Add("PunctuationMode", "DictatedAndAutomatic");
            this.properties.Add("ProfanityFilterMode", "Masked");
            this.properties.Add("AddWordLevelTimestamps", "True");
        }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public Uri RecordingsUrl { get; set; }

        public string Locale { get; set; }

        public IEnumerable<ModelIdentity> Models { get; set; }

        public IDictionary<string, string> properties { get; set; }

        public static TranscriptionDefinition Create(
            string name,
            string description,
            string locale,
            Uri recordingsUrl)
        {
            return TranscriptionDefinition.Create(name, description, locale, recordingsUrl, new ModelIdentity[0]);
        }

        public static TranscriptionDefinition Create(
            string name,
            string description,
            string locale,
            Uri recordingsUrl,
            IEnumerable<ModelIdentity> models)
        {
            return new TranscriptionDefinition(name, description, locale, recordingsUrl, models);
        }
    }

    public class AudioFileResult
    {
        public string AudioFileName { get; set; }
        public List<SegmentResult> SegmentResults { get; set; }
    }

    public class RootObject
    {
        public List<AudioFileResult> AudioFileResults { get; set; }
    }

    public class NBest
    {
        public double Confidence { get; set; }
        public string Lexical { get; set; }
        public string ITN { get; set; }
        public string MaskedITN { get; set; }
        public string Display { get; set; }
    }

    public class SegmentResult
    {
        public string RecognitionStatus { get; set; }
        public string Offset { get; set; }
        public string Duration { get; set; }
        public List<NBest> NBest { get; set; }
    }


}
