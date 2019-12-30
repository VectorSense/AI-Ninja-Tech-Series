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


namespace VoiceBasedInsuranceFormFilling.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> InsuranceFormFill()
        {
            try
            {
                Program p = new Program();
                await p.TranscribeAsync();
                if (p.Error != null)
                    return Json(new { StatusCode = "F", Message = p.Error });
                else
                {                  
                    return Json(new { StatusCode = "S", Name = p.InsName, Number = p.InsuranceNo, DOB = p.DOB, Premium = p.Premium });
                }
            }
            catch(Exception e)
            {
                return Json(new { StatusCode = "F", Message = e.Message });
            }
                
            }
    }

   

        class Program{

            public string InsName = null,Result=null, InsuranceNo = null, DOB = null, Premium = null,Error=null;
            // Replace with your subscription key
            private string SubscriptionKey = ConfigurationManager.AppSettings["SpeechSubscriptionKey"];

            // Update with your service region
            private string Region = ConfigurationManager.AppSettings["Region"];
            private const int Port = 443;

            // recordings and locale
            private const string Locale = "en-US";        
            //private string RecordingsBlobUri = ConfigurationManager.AppSettings["RecordingsBlobUri"];
            private string RecordingsBlobUri = "https://msspeechtext.blob.core.windows.net/msspeechtext/new1.wav?sp=r&st=2019-12-27T13:31:27Z&se=2020-01-30T21:31:27Z&spr=https,http&sv=2019-02-02&sr=b&sig=PN12gJi%2BQnRikMxCeEZWkTiOWFyuXYxttKhAUBxqi3M%3D";

            //name and description
            private const string Name = "Simple transcription";
            private const string Description = "Simple transcription description";

            private const string speechToTextBasePath = "api/speechtotext/v2.0/";

            private void  Luis(string Text)
            {
                var client = new RestClient(ConfigurationManager.AppSettings["LUIS_EndPoint"] + ConfigurationManager.AppSettings["LUIS_AppID"] + "?verbose=true&timezoneOffset=-360&subscription-key=" + ConfigurationManager.AppSettings["LUIS_Key"] + "&q=" + Text);
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                dynamic JsonResult = JsonConvert.DeserializeObject(response.Content);
                for (int k = 0; k < JsonResult.entities.Count; k++)
                {
                    if (JsonResult.entities[k]["type"].ToString() == "PersonName")
                        InsName = JsonResult.entities[k]["entity"].ToString();
                    else if (JsonResult.entities[k]["type"].ToString() == "InsuranceNumber")
                        InsuranceNo = JsonResult.entities[k]["entity"].ToString();
                    else if (JsonResult.entities[k]["type"].ToString() == "builtin.datetimeV2.date")
                        DOB = JsonResult.entities[k]["entity"].ToString();
                    else if (JsonResult.entities[k]["type"].ToString() == "PremiumAmount")
                        Premium = JsonResult.entities[k]["entity"].ToString();
                }
                
            return;
            }

            public async Task TranscribeAsync()
            {   
                // create the client object and authenticate
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(25);
                client.BaseAddress = new UriBuilder(Uri.UriSchemeHttps, $"{Region}.cris.ai", Port).Uri;

                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

                var path = $"{speechToTextBasePath}Transcriptions/";
                var transcriptionDefinition = TranscriptionDefinition.Create(Name, Description, Locale, new Uri(RecordingsBlobUri));

                string res = JsonConvert.SerializeObject(transcriptionDefinition);
                StringContent sc = new StringContent(res);
                sc.Headers.ContentType = JsonMediaTypeFormatter.DefaultMediaType;

                Uri transcriptionLocation = null;

                using (var response = await client.PostAsync(path, sc))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        Error="Error "+response.StatusCode +" starting transcription.";
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
                            Error = "Transcription failed. Status:"+transcription.StatusMessage;
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
            Result = JsonResult.AudioFileResults[0].CombinedResults[0].MaskedITN.ToString();
            Luis(Result);
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