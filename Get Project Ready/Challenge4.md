<h1>AI Series HOL</h1>
<h2>Challenge 4 – Quality Control of PDI Verification using Custom Vision Models</h2>
<p>In Challenge 4, we are going to explore how the Quality Control Checker works using Azure Custom Vision model files.</p>
<h2>Getting Started</h2>
<h3>Prerequisites</h3>
<li>Kindly ensure that the application works fine so far.</li>
<li>We are going to build the Quality Control Checker in the same application.</li>
<h3>Code Summary</h3>
<p>Quality Control Checking has two model files. The model which is created by us uses Azure Custom Vision. Both the model files have different approaches to find the Quality of the products. The first one using images taken from the live stream and the other on by browsing the image from the local machine or using the image URL.</p>
<p>This application has the QualityControlChecker.cs file which is used to implement all the functionalities.The Facade.cs file is used as an intermediator between HomeController and QualityControlChecker files.</p>&nbsp;
<h3>Invoke the Quality Controller Check API</h3>
<ol>
  <strong>
      <li>To start with, update the API Key and Endpoint in Web.Config</li>
      <li>Train your model and publish it in 'customvision.ai' [NOTE : Check Challenge 2 for procedures]</li>
      <li>Follow the screenshot shown below to navigate to the Web.Config file</li>&nbsp;
      <img src="http://139.59.61.161/PartnerTechSeries2019/Class/Web_config.PNG" alt="image" style="max-width:100%;">&nbsp;
      <li>Copy and paste the Endpoint in 'QualityEndPoint', Prediction Key in 'QualityPredictionKey', Project-id in 'QualityProjectID', Iteration name in 'QualityIterationID' in both the model files</li>&nbsp;
      <img src="http://139.59.61.161/PartnerTechSeries2019/custom/webconfig.PNG" alt="image" style="max-width: 100%;">
  </strong>
<h3>Getting started with coding - here we will implement the QualityControlChecker.cs file</h3>
<li>Follow the screenshot shown below to navigate to the QualityControlChecker.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Class/QualityControlChecker.PNG" alt="image" style="max-width:100%;">&nbsp;
<ol>
<strong>
<li>Copy and Paste the code given below in 'QualityControlChecker.cs', (i.e) below the comment 'Paste the QualityControlChecker Class code here...'</li>
<blockquote>
<pre>
<code>
public class QualityControlChecker
{
    //Assigning Subscription Key and Endpoint from web.config file
    private static string QualityPredictionKey_model1 = ConfigurationManager.AppSettings["QualityPredictionKey_model1"], QualityAPIEndpoint_model1 = ConfigurationManager.AppSettings["QualityEndPoint_model1"], QualityProjectID_model1 = ConfigurationManager.AppSettings["QualityProjectID_model1"], QualityIterationID_model1 = ConfigurationManager.AppSettings["QualityIterationID_model1"];
    private static string QualityPredictionKey_model2 = ConfigurationManager.AppSettings["QualityPredictionKey_model2"], QualityAPIEndpoint_model2 = ConfigurationManager.AppSettings["QualityEndPoint_model2"], QualityProjectID_model2 = ConfigurationManager.AppSettings["QualityProjectID_model2"], QualityIterationID_model2 = ConfigurationManager.AppSettings["QualityIterationID_model2"];
&nbsp;
    public string error = "";
    public string TagName = "";
&nbsp;
    public void Quality_Check(string data, bool flag, string check)
    {
&nbsp;
        try
        {
            if(check == "QualityCheck_Model1")
            {
                try
                {
                    var result = "";
                    //checking the flag is true then execute the URL Image
                    if (flag)
                    {
                        var client = new RestClient(QualityAPIEndpoint_model1 + "/customvision/v3.0/Prediction/" + QualityProjectID_model1 + "/detect/iterations/" + QualityIterationID_model1 + "/url");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Prediction-Key", QualityPredictionKey_model1);
                        request.AddParameter("undefined", "{\"Url\": \"" + data + "\"}", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        result = response.Content;
&nbsp;
                    }
                    // Executing the normal images
                    else
                    {
                        var imagebytes = Convert.FromBase64String(data);
&nbsp;
                        var client = new RestClient(QualityAPIEndpoint_model1 + "/customvision/v3.0/Prediction/" + QualityProjectID_model1 + "/detect/iterations/" + QualityIterationID_model1 + "/image");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/octet-stream");
                        request.AddHeader("Prediction-Key", QualityPredictionKey_model1);
                        request.AddParameter("data", imagebytes, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        result = response.Content;
                    }
                    //creating the jObject 
                    dynamic res_obj = JObject.Parse(result);
                    var res_prediction = res_obj.predictions.ToString();
                    //creating the JArray
                    JArray res_array = JArray.Parse(res_prediction);
&nbsp;
                    if (res_array.Count != 0)
                    {
                        TagName = "Pass";
&nbsp;
                        for (int i = 0; i < res_array.Count; i++)
                        {
                            dynamic pred = JObject.Parse(res_array[i].ToString());
                            int probability = pred.probability * 100;
                            //checking the probability 
                            if (pred.tagName == "Fail" && probability >= 90)
                            {
                                TagName = "Fail";
                            }
                        }
                    }
                    else
                    {
                        TagName = "Cound not find any object";
                    }
       &nbsp;             
                }
&nbsp;
                catch (Exception e)
                {
                    error = e.Message;
                }
            }
&nbsp;
            if (check == "QualityCheck_Model2")
            {
                try
                {
                    var result = "";
                    //checking the flag is true then execute the URL Image
                    if (flag)
                    {
                        var client = new RestClient(QualityAPIEndpoint_model2+ "/customvision/v3.0/Prediction/" + QualityProjectID_model2 + "/classify/iterations/" + QualityIterationID_model2 + "/url");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Prediction-Key", QualityPredictionKey_model2);
                        request.AddParameter("undefined", "{\"Url\": \"" + data + "\"}", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        result = response.Content;
&nbsp;
                    }
                    // Executing the normal images
                    else
                    {
                        var imagebytes = Convert.FromBase64String(data);
&nbsp;
                        var client = new RestClient(QualityAPIEndpoint_model2 + "/customvision/v3.0/Prediction/" + QualityProjectID_model2 + "/classify/iterations/" + QualityIterationID_model2 + "/image");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/octet-stream");
                        request.AddHeader("Prediction-Key", QualityPredictionKey_model2);
                        request.AddParameter("data", imagebytes, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        result = response.Content;
                    }
                    //creating the jObject 
                    dynamic res_obj = JObject.Parse(result);
                    var res_prediction = res_obj.predictions.ToString();
                    //creating the JArray
                    JArray res_array = JArray.Parse(res_prediction);
&nbsp;
                    if (res_array.Count != 0)
                    {
                        dynamic pred = JObject.Parse(res_array[0].ToString());
                        //int probability = pred.probability * 100;
        &nbsp;                
                        //checking the probability 
                        if (pred.tagName == "Accurate-Space")
                        {
                            TagName = "Pass ";
                        }
                        else
                        {
                            TagName = "Fail ";
                        }
                    }
                    else
                    {
                        TagName = "Cound not find any object";
                    }
                }
&nbsp;
                catch (Exception e)
                {
                    error = e.Message;
                }
            }
        }
        catch(Exception e)
        {
            error = e.Message;
        }
    }
}
</code>
		</pre>
		</blockquote>
<li>Also add the below namespace in 'QualityControlChecker.cs' file</li>
<blockquote>
<pre>
 <code>using RestSharp;</code>
</pre>
</blockquote>
	</strong>
</ol>
<li>Follow the screenshot shown below to navigate to the Facade.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Class/Facade.PNG" alt="image" style="max-width:100%;">&nbsp;
<ol>
	<strong>
		<li>Copy and Paste the code given below in the 'Facade.cs', (i.e) below the comment 'Paste the QualityControlChecker Function Code here...'</li>
		<blockquote>
			<pre>
				<code>
public static QualityControlChecker QualityControlChecker(string base64, bool flag, string check)
{
	//creating object for Quality control checker 
	QualityControlChecker qcc_obj = new QualityControlChecker();
	//calling the quality check function
	qcc_obj.Quality_Check(base64, flag, check);
	//returning the result
	return (qcc_obj);
}
				</code>
			</pre>
		</blockquote>
	</strong>
</ol>
<li>Follow the screenshot shown below to navigate to the HomeController.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Class/HomeController.PNG" alt="image" style="max-width:100%;">&nbsp;
<ol>
	<strong>
		<li>Copy and Paste the code given below in the 'HomeController.cs', (i.e) below the comment 'Paste the QualityChecking Function code here...'</li>
		<blockquote>
			<pre>
				<code>
public JsonResult QualityChecking(String data, bool flag, string check)
{
    // Calling the function from facade class
    var fa_obj = Facade.QualityControlChecker(data, flag, check);
&nbsp;
    try
    {
        // Checking the error
        if (fa_obj.error == "")
        {
            // returning the result
            return Json(new { TagName = fa_obj.TagName, Error = "" });
        }
        return Json(new { TagName = "", Error = fa_obj.error });
    }
    catch(Exception e)
    {
        return Json(new { TagName = "", Error = e.Message });
    }
}
&nbsp;
				</code>
			</pre>
		</blockquote>
	</strong>
</ol>
<li>Click on the Run button to run the solution and get the output</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Run.PNG" alt="image" style="max-width:100%;">&nbsp;
<strong>
	<p>STEP 1 : Now, let's test the Quality of the product. We have two model files. Select any one of the models(Model 1 / Model 2) by selecting the appropriate Radio Button.</p>
<p>STEP 2 : If you decide to take the picture by live streaming, make sure you use the correct product. If the Image does not contain any related product details, it will throw the 'Fail' response.</p>
<p>STEP 3 : If you click the Browse Button for selecting an image, make sure you select the relevant image.</p>
<p>STEP 4 : If you enter the URL for selecting an image, make sure you give the right path for the image.</p>
</strong>
<h2>Model 1 Sample Outputs</h2>
<h3>Browse Button for selecting images from the Local Machine</h3>
<li>Selecting the Model 1 Check Box</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_1.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Turn off the Live Streamming and select the Browse Button</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_2.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Open the wrong product image from a folder</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_3.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Below is the picture of a wrong product</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_4.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Open the Right product image from the folder</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_5.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Below is the picture of the Right product</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_6.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Select the wrong file type</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_7.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Below is the Error page which you will get after selecting the wrong file type</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_8.PNG" alt="image" style="max-width:100%;">&nbsp;
<h3>URL images</h3>
<li>Error page for Empty URL</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_9.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Giving wrong product path</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_10.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Giving right product path</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_11.PNG" alt="image" style="max-width:100%;">&nbsp;
<h2>Model 2 Sample Outputs</h2>
<li>Select the Model 2 Check Box</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model2_output/output1.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Error page for selecting the No Space image</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model2_output/output2.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Error page for selecting the Long Space image</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model2_output/output3.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Error page for selecting the Accurate image</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model2_output/output4.PNG" alt="image" style="max-width:100%;">&nbsp;
<h3>Congratulations! You have successfully completed Challenge 4</h3>
<h3>The next session is <a href="https://github.com/VectorSense/AI-Ninja-Tech-Series/blob/master/Get%20Project%20Ready/Challenge5.md">Challenge 5</a></h3>