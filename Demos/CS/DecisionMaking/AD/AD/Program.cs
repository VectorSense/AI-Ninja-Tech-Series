using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AD
{
    class Program
    {
        // Replace the subscriptionKey string value with your valid subscription key.
        const string subscriptionKey = "";
        // Replace the endpoint URL with the correct one for your subscription. 
        // Your endpoint can be found in the Azure portal. For example: https://westus2.api.cognitive.microsoft.com
        const string endpoint = "https://westus2.api.cognitive.microsoft.com/anomalydetector";
        // Replace the dataPath string with a path to the JSON formatted time series data.
        const string dataPath = @"F:\Ms WorkShop\AD\timeseries_data.txt";
        const string latestPointDetectionUrl = "/anomalydetector/v1.0/timeseries/last/detect";
        const string batchDetectionUrl = "/anomalydetector/v1.0/timeseries/entire/detect";

        static void Main(string[] args)
        {
            var requestData = File.ReadAllText(dataPath);

            detectAnomaliesBatch(requestData);
            detectAnomaliesLatest(requestData);


            System.Console.ReadKey();

        }


        static void detectAnomaliesBatch(string requestData)
        {
            System.Console.WriteLine("Detecting anomalies as a batch");

            var result = Request(endpoint, batchDetectionUrl, subscriptionKey, requestData).Result;

            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
            System.Console.WriteLine(jsonObj);

            if (jsonObj["code"] != null)
            {
                System.Console.WriteLine($"Detection failed. ErrorCode:{jsonObj["code"]}, ErrorMessage:{jsonObj["message"]}");
            }
            else
            {
                bool[] anomalies = jsonObj["isAnomaly"].ToObject<bool[]>();
                System.Console.WriteLine("\nAnomalies detected in the following data positions:");
                for (var i = 0; i < anomalies.Length; i++)
                {
                    if (anomalies[i])
                    {
                        System.Console.Write(i + ", ");
                    }
                }
            }
        }

        static void detectAnomaliesLatest(string requestData)
        {
            System.Console.WriteLine("\n\nDetermining if latest data point is an anomaly");
            var result = Request(
                endpoint,
                latestPointDetectionUrl,
                subscriptionKey,
                requestData).Result;

            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
            System.Console.WriteLine(jsonObj);
        }

        static async Task<string> Request(string apiAddress, string endpoint, string subscriptionKey, string requestData)
        {
            using (HttpClient client = new HttpClient { BaseAddress = new Uri(apiAddress) })
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                var content = new StringContent(requestData, Encoding.UTF8, "application/json");
                var res = await client.PostAsync(endpoint, content);
                return await res.Content.ReadAsStringAsync();
            }
        }
    }

}
