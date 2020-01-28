using PartnerTechSeries.AI.Demo.TextAnalytics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TextAnalyticsPOC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //Text Analytics Functions
        [HttpPost]
        public async Task<JsonResult> AnalyseText(string data)
        {
            try
            {

                //Assigning Subscription Key and Face Endpoint from web.config file
                string SubscriptionKey = ConfigurationManager.AppSettings["TextAnalyticsSubscriptionKey"], Endpoint = ConfigurationManager.AppSettings["TextAnalyticsEndpoint"];
                SentimentAnalysisSample sa = new SentimentAnalysisSample();
                LanguageDetectionSample ld = new LanguageDetectionSample();
                RecognizeEntitiesSample re = new RecognizeEntitiesSample();
                KeyPhraseExtractionSample ke = new KeyPhraseExtractionSample();
                await sa.RunAsync(Endpoint, SubscriptionKey, data);
                await ld.RunAsync(Endpoint, SubscriptionKey, data);
                await re.RunAsync(Endpoint, SubscriptionKey, data);
                await ke.RunAsync(Endpoint, SubscriptionKey, data);
                return Json(new { Sentiment = sa.Sentiment, Language = ld.Language, Entity = re.Entity, Phrase = ke.Phrase });
            }
            catch (Exception e)// handling runtime errors and returning error as Json
            {
                return Json(new { Erorr = e.Message });
            }
        }
    }
}
