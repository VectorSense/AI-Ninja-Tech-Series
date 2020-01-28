<h1>AI Series HOL</h1>
<h2>Challenge 5 – Automatic Extraction of Key Phrases using Azure OCR and LUIS</h2>
<p>In Challenge 5 we are going to explore about how the Legal Document Verification works using Azure Computer Vision API and Microsoft Luis.</p>
<h2>Getting Started</h2>
<h3>Prerequisites</h3>
<li>Kindly ensure that the application works fine so far.</li>
<li>We are going to build the Legal Document Verification in the same application</li>
<h3>Code Summary</h3>
<p>Legal Document Verification has two parts. The first part is OCR (Optical Character Recognizer), its an Azure Computer Vision API. It is used to extract the characters from a given document or an image. Using OCR, we can get the text from the input(document/image)</p>
<p>After getting all the text from the OCR, we need to do the Automatic Keyword Extraction using Microsoft LUIS.</p>
<p>This application has the DocumentVerificationHandler.cs file, which is used to implement all the functionalities.The Facade.cs file is used as an intermediator between HomeController and DocumentVerificationHandler files.</p>&nbsp;
<h3>Invoking the Legal Document Verification API</h3>
<h3>Creating Cognitive service API Key</h3>
<ol>
  <strong>
    <li>Sign-in to Azure Portal by typing "portal.azure.com" in browser, enter your username</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/computervisionAPI_create/portal_1.jpg" alt="image" style="max-width: 100%;">&nbsp;
    <li>Enter your Password</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/computervisionAPI_create/portal_2.jpg" alt="image" style="max-width: 100%;">&nbsp;
    <li>Click on create a resource</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/computervisionAPI_create/portal_3.jpg" alt="image" style="max-width: 100%;">&nbsp;
    <li>In the search box type 'cognitive service'</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/computervisionAPI_create/portal_4.jpg" alt="image" style="max-width: 100%;">&nbsp;
    <li>Click on create</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/computervisionAPI_create/portal_5.jpg" alt="image" style="max-width: 100%;">&nbsp;
    <li>Enter name and select location, pricing tier and resource group</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/computervisionAPI_create/portal_6.jpg" alt="image" style="max-width: 100%;">&nbsp;
    <li>Click on Overview tab</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/computervisionAPI_create/portal_7.jpg" alt="image" style="max-width: 100%;">&nbsp;
    <li>Copy the endpoint</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/computervisionAPI_create/portal_8.jpg" alt="image" style="max-width: 100%;">&nbsp;
    <li>Click on Keys tab</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/computervisionAPI_create/portal_9.jpg" alt="image" style="max-width: 100%;">
    <li>Copy the Keys</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/computervisionAPI_create/portal_10.jpg" alt="image" style="max-width: 100%;">&nbsp;
  </strong></ol>
  <h3>Updating the Web.config</h3>
<ol>
  <strong>
      <li>To start with, update the API Key and Endpoint in Web.Config</li>
      <li>Follow the picture shown below to navigate the Web.Config file</li>&nbsp;
      <img src="http://139.59.61.161/PartnerTechSeries2019/Class/Web_config.PNG" alt="image" style="max-width:100%;">&nbsp;
      <li>Paste the Endpoint in 'OCREndpoint' and Key in 'OCRSubscriptionKey'</li>&nbsp;
      <img src="http://139.59.61.161/PartnerTechSeries2019/OCR/4.PNG" alt="image" style="max-width: 100%;"></strong>
</ol>
<h3>Importing the LUIS Model</h3>
<ol>
    <strong>
        <li>Download the LUIS Json file from <a href="https://github.com/VectorSense/AI-Ninja-Tech-Series/blob/master/Challenges/LegalDocumentLUIS.json">GitHub</a></li>
        <li>Login to Luis.ai</li>&nbsp;
        <img src="http://139.59.61.161/PartnerTechSeries2019/LuisImages/1.jpg" alt="image" style="max-width: 100%;">&nbsp;
        <li>Enter your username</li>&nbsp;
        <img src="http://139.59.61.161/PartnerTechSeries2019/LuisImages/2.jpg" alt="image" style="max-width: 100%;">&nbsp;
        <li>Enter your password</li>&nbsp;
        <img src="http://139.59.61.161/PartnerTechSeries2019/LuisImages/3.jpg" alt="image" style="max-width: 100%;">&nbsp;
        <li>Click on Import app</li>&nbsp;
        <img src="http://139.59.61.161/PartnerTechSeries2019/LuisImages/4.jpg" alt="image" style="max-width: 100%;">&nbsp;
        <li>Select the Json file</li>&nbsp;
        <img src="http://139.59.61.161/PartnerTechSeries2019/LuisImages/5.jpg" alt="image" style="max-width: 100%;">&nbsp;
        <img src="http://139.59.61.161/Hackathon/MSWorkshop2019/LuisImages/6_hackathon.jpg" alt="image" style="max-width: 100%;">&nbsp;
        <li>Enter name</li>&nbsp;
        <img src="http://139.59.61.161/Hackathon/MSWorkshop2019/LuisImages/7_hackathon.jpg" alt="image" style="max-width: 100%;">&nbsp;
        <li>Click on Train button</li>&nbsp;
        <img src="http://139.59.61.161/PartnerTechSeries2019/custom/LUIS2.jpg" alt="image" style="max-width: 100%;">&nbsp;
        <li>After training is completed, click on Publish button</li>&nbsp;
        <img src="http://139.59.61.161/PartnerTechSeries2019/custom/LUIS3.jpg" alt="image" style="max-width: 100%;">
        <img src="http://139.59.61.161/PartnerTechSeries2019/custom/LUIS1.JPG" alt="image" style="max-width: 100%;">&nbsp;
        <li>Click on Manage</li>&nbsp;
        <img src="http://139.59.61.161/PartnerTechSeries2019/LuisImages/8.jpg" alt="image" style="max-width: 100%;">&nbsp;
        <li>Copy the Application ID</li>&nbsp;
        <img src="http://139.59.61.161/Hackathon/MSWorkshop2019/LuisImages/9_hackathon.jpg" alt="image" style="max-width: 100%;">&nbsp;
        <li>Copy Key and Endpoint</li>&nbsp;
        <img src="http://139.59.61.161/PartnerTechSeries2019/LuisImages/10_Updated.jpg" alt="image" style="max-width: 100%;">&nbsp;
        <li>Paste the Endpoint in 'LUIS_EndPoint', Application ID in 'LUIS_AppID' and Key 'LUIS_Key' in Web.Config [NOTE : Paste the endpoint only till 'v2.0/apps/' from example query, don't copy the endpoint url (example endpoint : "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/")]</li>
    </strong>
</ol>
<h3>Getting started with coding - here we will implement the DocumentVerificationHandler.cs file</h3>
<li>Follow the screenshot shown below to navigate to the DocumentVerificationHandler.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Class/DocumentVerificationHandler.PNG" alt="image" style="max-width:100%;">&nbsp;
<ol>
<strong>
<li>Copy and Paste the code given below in 'DocumentVerificationHandler.cs', (i.e) below the comment 'Paste the DocumentVerificationHandler Class code here...'</li>
<blockquote>
<pre>
<code>
public class DocumentVerificationHandler
{
    private string subscriptionKey = ConfigurationManager.AppSettings["OCRSubscriptionKey"], Endpoint = ConfigurationManager.AppSettings["OCREndpoint"];
&nbsp;
    //For printed text
    private const TextRecognitionMode textRecognitionMode = TextRecognitionMode.Printed;
    private const int numberOfCharsInOperationId = 36;
    //Variable to append the OCR Results from SDK
    public string Error = "";
    public List&lt;string&gt; OCRList = new List&lt;string&gt;();
    public string OcrResult = "";
    public string ContractDate = "", VendorName = "", ClientName = "", Services = "", ContractValue = "", EndDate = "", PenaltyValue = "", JurisdictionPlace = "", VendorEmail = "", VendorPhone = "", ClientEmail = "", ClientPhone = "", FinalResult = "";
&nbsp;
    public async Task ExtractText(string data)
    {
        ComputerVisionClient computerVision = new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey), new System.Net.Http.DelegatingHandler[] { });
        //Endpoint
        computerVision.Endpoint = Endpoint;
&nbsp;
        //Image data to Byte Array
        byte[] imageBytes = Convert.FromBase64String(data);
&nbsp;
        //Byte Array To Stream
        Stream stream = new MemoryStream(imageBytes);
&nbsp;
        try
        {
            //Starting the async process to recognize the text
            BatchReadFileInStreamHeaders textHeaders = await computerVision.BatchReadFileInStreamAsync(stream, textRecognitionMode);
&nbsp;
            await GetTextAsync(computerVision, textHeaders.OperationLocation);
&nbsp;
        }
        catch (Exception e)
        {
            Error = e.Message;
        }
    }
&nbsp;
    //Retriving the recognized text
    public async Task GetTextAsync(ComputerVisionClient computerVision, string operationLocation)
    {
        //Retrieve the URI where the recognized text will be stored from the Operation-Location header
        string operationId = operationLocation.Substring(operationLocation.Length - numberOfCharsInOperationId);
&nbsp;
        //Calling GetReadOperationResultAsync
        ReadOperationResult result = await computerVision.GetReadOperationResultAsync(operationId);
&nbsp;
        //Waiting for the operation to complete
        int i = 0;
        int maxRetries = 10;
        while ((result.Status == TextOperationStatusCodes.Running ||
                result.Status == TextOperationStatusCodes.NotStarted) && i++ < maxRetries)
        {
            await Task.Delay(1000);
&nbsp;
            result = await computerVision.GetReadOperationResultAsync(operationId);
        }
&nbsp;
        //Displaying the results
        var recResults = result.RecognitionResults;
&nbsp;
        foreach (TextRecognitionResult recResult in recResults)
        {
            foreach (Line line in recResult.Lines)
            {
                OCRList.Add(line.Text);
                OcrResult += line.Text + " <br> ";
            }
        }
&nbsp;                                            
        //Loop
        for (int j = 0; j< OCRList.Count;j++)
        {
            //Calling LUIS
            var client = new RestClient(ConfigurationManager.AppSettings["LUIS_EndPoint"] + ConfigurationManager.AppSettings["LUIS_AppID"] + "?verbose=true&timezoneOffset=-360&subscription-key=" + ConfigurationManager.AppSettings["LUIS_Key"] + "&q=" + OCRList[j]);
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            dynamic jObject = JObject.Parse(response.Content);
&nbsp;
            JArray luislenobj = JArray.Parse(jObject.entities.ToString());
&nbsp;
            if (luislenobj.Count > 0)
            {
                for (int k = 0; k < luislenobj.Count; k++)
                {
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Contract Details" && jObject["entities"][k]["type"].ToString() == "Contract Date")
                    {
                        ContractDate = jObject["entities"][k]["entity"].ToString();
                        break;
                    } 
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Contract Details" && jObject["entities"][k]["type"].ToString() == "Client Name")
                    {
                        ClientName = jObject["entities"][k]["entity"].ToString();
                        break;
                    }
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Contract Details" && jObject["entities"][k]["type"].ToString() == "Vendor Name")
                    {
                        VendorName += jObject["entities"][k]["entity"].ToString();
                    }
&nbsp;
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Services" && jObject["entities"][k]["type"].ToString() == "Services")
                    {
                        Services += " " + jObject["entities"][k]["entity"].ToString();
                    }
&nbsp;
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Contract Value" && jObject["entities"][k]["type"].ToString() == "Contract Value")
                    {
                        ContractValue = jObject["entities"][k]["entity"].ToString();
                        break;
                    }
&nbsp;
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Contract End Date" && jObject["entities"][k]["type"].ToString() == "End Date")
                    {
                        EndDate = jObject["entities"][k]["entity"].ToString();
                        break;
                    }
&nbsp;
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Penalty" && jObject["entities"][k]["type"].ToString() == "builtin.percentage")
                    {
                        PenaltyValue = jObject["entities"][k]["entity"].ToString();
                        break;
                    }
&nbsp;
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Jurisdiction" && jObject["entities"][k]["type"].ToString() == "Jurisdiction Place")
                    {
                        JurisdictionPlace = jObject["entities"][k]["entity"].ToString();
                        break;
                    }
&nbsp;
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Vendor Details" && jObject["entities"][k]["type"].ToString() == "builtin.email")
                    {
                        VendorEmail= jObject["entities"][k]["entity"].ToString();
                        break;
                    }
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Vendor Details" && jObject["entities"][k]["type"].ToString() == "builtin.phonenumber")
                    {
                        VendorPhone = jObject["entities"][k]["entity"].ToString();
                        break;
                    }
&nbsp;
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Client Details" && jObject["entities"][k]["type"].ToString() == "builtin.email")
                    {
                        ClientEmail = jObject["entities"][k]["entity"].ToString();
                        break;
                    }
                    if (jObject["topScoringIntent"]["intent"].ToString() == "Discover Client Details" && jObject["entities"][k]["type"].ToString() == "builtin.phonenumber")
                    {
                        ClientPhone = jObject["entities"][k]["entity"].ToString();
                        break;
                    }
                }
            }
            Thread.Sleep(210);
        }
&nbsp;                           
        FinalResult = "<br>" + "Contract Date : " + ContractDate + "<br>" + "Vendor Name : " + VendorName + "<br>" + "Client Name : " + ClientName + "<br>" + "Service Description : " + Services + "<br>" + "Contract Value : " + ContractValue + "<br>" + "End Date : " + EndDate + "<br>" + "Penalty Value : " + PenaltyValue + "<br>" + "Jurisdiction Place : " + JurisdictionPlace + "<br>" + "Vendor Email : " + VendorEmail + "<br>" + "Vendor Phone : " + VendorPhone + "<br>" + "Client Email : " + ClientEmail + "<br>" + "Client Phone : " + ClientPhone + "<br>";
&nbsp;
    }
&nbsp;
}
</code>
</pre>
</blockquote>
<li>Also add the below namespace in 'DocumentVerificationHandler.cs' file</li>
<blockquote>
<pre>
 <code>using RestSharp;
 using System.Threading;
</code>
</pre>
</blockquote>
</strong>
</ol>
<li>Follow the screenshot shown below to navigate to the Facade.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Class/Facade.PNG" alt="image" style="max-width:100%;">&nbsp;
<ol>
	<strong>
		<li>Copy and Paste the code given below in the  'Facade.cs', (i.e) below the comment 'Paste the DocumentVerificationHandler Function Code here...'</li>
		<blockquote>
			<pre>
				<code>
public async Task&lt;DocumentVerificationHandler&gt; DocumentVerificationHandler(string base64)
{
    DocumentVerificationHandler dvh_obj = new DocumentVerificationHandler();
    await dvh_obj.ExtractText(base64);
    return (dvh_obj);
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
		<li>Copy and Paste the code given below in the 'HomeController.cs', (i.e) below the comment 'Paste the DocumentVerification Function code here...'</li>
		<blockquote>
			<pre>
				<code>
public async Task&lt;JsonResult&gt; DocumentVerification(string data)
{
    DocumentVerificationHandler dvh_obj = new DocumentVerificationHandler();
&nbsp;
    await dvh_obj.ExtractText(data);
&nbsp;
    if (dvh_obj.Error == "")
    {
        return Json(new { Contract_Date = dvh_obj.ContractDate, Vendor_Name = dvh_obj.VendorName, Client_Name = dvh_obj.ClientName, Service_Description = dvh_obj.Services, Contract_Value = dvh_obj.ContractValue, End_Date = dvh_obj.EndDate, Penalty_Value = dvh_obj.PenaltyValue, Jurisdiction_Place = dvh_obj.JurisdictionPlace, Vendor_Email = dvh_obj.VendorEmail, Vendor_Phone = dvh_obj.VendorPhone, Client_Email = dvh_obj.ClientEmail, Client_Phone = dvh_obj.ClientPhone, Error = "" });
    }
    return Json(new { Summary = "", Error = dvh_obj.Error });
}
				</code>
			</pre>
		</blockquote>
	</strong>
</ol>
<li>Follow the screenshot shown below to Run the HomeController.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Class/HomeController.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Click on the Run button to run the solution and get the output</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Run.PNG" alt="image" style="max-width:100%;">&nbsp;
<strong>
<p>STEP 1 : Now, lets test the image using Azure API. </p>
<p>STEP 2 : If you choose to take the picture by live streaming, make sure you use the correct image. If the Image does not contain any text, it will throw the 'Fail' response.</p>
<p>STEP 3 : If you click the Browse Button for selecting an image, make sure you select the relevant image.</p>
<p>STEP 4 : If you enter the URL for selecting an image, make sure you give the right path for the image.</p>
</strong>
<h2>Sample Outputs [Navigate to 'Legal Document Verification' Page]</h2>
<h3>Browse Button for selecting images from the Local Machine</h3>
<li>Turn off the Live Streamming and select the Browse Button</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_2.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Open the Right image from the folder</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/OCR/2.png" alt="image" style="max-width:100%;">&nbsp;
<li>Below is the screenshot of the Right image</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/OCR/3.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Select the wrong file type</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_7.PNG" alt="image" style="max-width:100%;">&nbsp;
<li>Error page if you select a wrong file type</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/Model1_output/output_8.PNG" alt="image" style="max-width:100%;">&nbsp;
<h3>Congratulations! You have successfully completed Challenge 5</h3>
<h3>The next session is <a href="https://github.com/VectorSense/AI-Ninja-Tech-Series/blob/master/Challenge6.md">Challenge 6</a></h3>