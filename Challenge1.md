<h1>AI Series HOL</h1>
<h1>Challenge 1 – Image Validation using Azure Computer Vision</h1>
<p>In Challenge 1, we are going to explore how to use Azure's Face API to validate the given image (the image will be taken from the live stream), connect with Azure SQL Server Database and register a person's face to be used later during the face identification.</p>
<h2>Getting Started</h2>
<p>Download the AI Series HOL Starter Kit from the <a href="https://github.com/VectorSense/Azure-AI-Ninja-Tech-Series/tree/master/DIY%20Demos/AI_Series_Starter_Kit">Git Repo</a></p>
<h3>Prerequisites</h3>
    <li>Kindly ensure that your Visual Studio and SQL Server Management Studio are working fine.</li>
    <li>Open the AI Series Starter Kit application.</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Invoke_StarterKit/1.PNG" alt="image" style="max-width:100%;">
    <li>In the solution explorer [View -> Solution Explorer]</li>&nbsp; 
    <img src="http://139.59.61.161/PartnerTechSeries2019/Invoke_StarterKit/2.PNG" alt="image" style="max-width:100%;">
    <li>Right click on the solution name and select Build</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Invoke_StarterKit/3.PNG" alt="image" style="max-width:100%;">
    <li>Make sure there are no errors once the build is complete.</li>
    <li>Now click on the Run button and see the application's output in the browser</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Invoke_StarterKit/4.PNG" alt="image" style="max-width:100%;">
    <li>Following are the output screens of the application. Since the database doesn't contain any entries, all the screen will be empty.</li>
    <li>Home page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Admin/main.PNG" alt="image" style="max-width:100%;">&nbsp;
    <li>Navigate to Admin page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Admin/admin_1.PNG" alt="image" style="max-width:100%;">&nbsp;
    <li>Admin Index page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Admin/admin_index.PNG" alt="image" style="max-width:100%;">&nbsp;
    <li>Navigate to Image Validation page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Admin/admin_index_1.png" alt="image" style="max-width:100%;">&nbsp;
    <li>Empty Image Validation page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Admin/image_validation.PNG" alt="image" style="max-width:100%;">&nbsp;
    <li>Navigate to Gesture Management page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Admin/admin_index_2.png" alt="image" style="max-width:100%;">&nbsp;
    <li>Empty Gesture Management page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Admin/gesture_management.PNG" alt="image" style="max-width:100%;">&nbsp;
    <li>Navigate to Audit Log page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Admin/admin_index_3.png" alt="image" style="max-width:100%;">&nbsp;
    <li>Empty Audit Log page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Admin/audit_log.PNG" alt="image" style="max-width:100%;">&nbsp;
    <li>Navigate to User page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/User/user_1.png" alt="image" style="max-width:100%;">&nbsp;
    <li>User Index page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/User/User_Index.PNG" alt="image" style="max-width:100%;">&nbsp;
    <li>Navigate to Register page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/User/User_Index_1.png" alt="image" style="max-width:100%;">&nbsp;
    <li>Empty Register page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/User/Register_Page.PNG" alt="image" style="max-width:100%;">&nbsp;
    <li>Navigate to Verify page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/User/User_Index_2.png" alt="image" style="max-width:100%;">&nbsp;
    <li>Empty Verify page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/User/Verify_Page.PNG" alt="image" style="max-width:100%;">&nbsp;
    <li>Navigate to Legal Document Verification page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Document_Verification/doc.png" alt="image" style="max-width:100%;">&nbsp;
    <li>Empty Legal Document Verification page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Document_Verification/doc_1.PNG" alt="image" style="max-width:100%;">&nbsp;
    <li>Navigate to Quality Control Check page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/QCC_1.png" alt="image" style="max-width:100%;">&nbsp;
    <li>Empty Quality Control Check page</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Quality_Check/QCC_2.PNG" alt="image" style="max-width:100%;">&nbsp;
    <h2>Install Nuget Package</h2>
    <p>The Nuget package installed in this project is 'Microsoft.Azure.CognitiveServices.Vision.ComputerVision'</p>
    <p>Below is the installation procedure to install 'RestSharp' package</p>
    <li>Install the 'RestSharp' Nuget Package</li>
    <li>Click on Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Invoke_StarterKit/5.PNG" alt="image" style="max-width:100%;">
    <li>Click on the Browse tab, type 'RestSharp' and press Enter. From the search result set, select the specified package & project and click on Install</li>&nbsp;
    <img src="http://139.59.61.161/PartnerTechSeries2019/Invoke_StarterKit/6.PNG" alt="image" style="max-width:100%;">&nbsp;
<h2>Code Summary</h2>
<p>In this application we have seven C# classes and each class is meant to handle a specific module and functionality.The Facade Class is used as the intermediate class between these seven classes and the HomeController. The HomeController manages all the views and the C# classes. </p>
<h2>Getting Started with the coding part - Following are the guidelines to work on the Computer Vision API</h2>
<h3>Converting an Image into Base64 Data</h3>
<p>The StorageHandler Class consists of many functions which are used to handle all the database functionalities,. It also consists of a function called 'SaveToFile' which is used to store the base64 image data into a byte array. The below code snippet converts the Base64 image data into a Byte Array. </p>
<ol>
  <strong>
    <li>Paste the code given below in 'StorageHandler.cs', (i.e) below the comment 'Paste the StorageHandler Class code here...'</li>
    <blockquote>
        <pre>
           <code>
public class StorageHandler
{
  public static byte[] SaveToFile(string base64data)
  {
    return Convert.FromBase64String(base64data);
  }
}
            </code>
        </pre>
   </blockquote></strong>
   <img src="http://139.59.61.161/PartnerTechSeries2019/Invoke_StarterKit/8.PNG" alt="image" style="max-width:100%;">
   &nbsp;
<p>The below code snippet invokes the SaveToFile function of StorageHandler Class from Facade Class.</p><strong>
   <li>Paste the code given below in 'Facade.cs', (i.e) below the comment 'Paste the storetoserver Function Code here...'</li>
    <blockquote>
    <pre>
      <code>
public static byte[] storetoserver(string base64data)
{
  return StorageHandler.SaveToFile(base64data);
}
      </code>
    </pre>
</blockquote>
</strong>
</ol>
  <img src="http://139.59.61.161/PartnerTechSeries2019/Invoke_StarterKit/9.PNG" alt="image" style="max-width:100%;">&nbsp;
<p>Lets move on to the Face API</p>
<h3>Creating Face API Key</h3>
<ol>
  <strong>
    <li>Sign-in to Azure Portal by typing "portal.azure.com" in browser, enter your username</li>
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_1.jpg" alt="image" style="max-width: 100%;">
    <li>Enter your Password</li>
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_2.jpg" alt="image" style="max-width: 100%;">
    <li>Click on create a resource</li>
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_3.jpg" alt="image" style="max-width: 100%;">
    <li>In the search box type 'face'</li>
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_4.jpg" alt="image" style="max-width: 100%;">
    <li>Click on create</li>
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_5.jpg" alt="image" style="max-width: 100%;">
    <li>Enter name and select location, pricing tier and resource group</li>
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_6.jpg" alt="image" style="max-width: 100%;">
    <li>Click on Overview tab</li>
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_7.jpg" alt="image" style="max-width: 100%;">
    <li>Copy the endpoint</li>
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_8.jpg" alt="image" style="max-width: 100%;">
    <li>Click on Keys tab</li>
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_9.jpg" alt="image" style="max-width: 100%;">
    <li>Copy the Keys</li>
    <img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_10.jpg" alt="image" style="max-width: 100%;">
  </strong></ol>
<h3>Invoking the Face API</h3>
<ol>
  <strong>
      <li>To start with, update the API Key and Endpoint in Web.Config</li>
      <li>Navigate to Web.Config</li>
      <li>Paste the Endpoint in 'FaceAPIEndPoint' and Key in 'FaceAPIKey'</li>
      <li>NOTE : Paste the endpoint only till '.com', example : 'https://southeastasia.api.cognitive.microsoft.com'</li>
      &nbsp;
        <img src="http://139.59.61.161/PartnerTechSeries2019/Invoke_StarterKit/webconfig.PNG" alt="image" style="max-width: 100%;"></strong>
        &nbsp;
<p>The below code snippet calls the Face API and checks for 4 attributes - Face availability, Multiple Face check, Sunglasses check and allowed Emotions.</p>
<strong>
    <li>Paste the code given below in 'ImageValidationHandler.cs', (i.e) below the comment 'Paste the ImageValidationHandler Class code here...'</li>
    <blockquote>
<pre>
<code>
public class ImageValidationHandler
{
    //Assigning Subscription Key and Face Endpoint from web.config file
    private static string FaceAPIKey = ConfigurationManager.AppSettings["FaceAPIKey"], FaceAPIEndpoint = ConfigurationManager.AppSettings["FaceAPIEndPoint"];
    &nbsp;
    public string error = "";
    &nbsp;
    public static string FaceAPICall(byte[] imageBytes)
    {
        var client = new RestClient(FaceAPIEndpoint + "/face/v1.0/detect?returnFaceLandmarks=false& returnFaceId =true&returnFaceAttributes=age%2Csmile%2Cgender%2Cglasses%2CheadPose%2CfacialHair%2Cemotion%2Cmakeup&%20returnFaceId%20=true");
        var request = new RestRequest(Method.POST);
        request.AddHeader("Postman-Token", "9a9a2c14-f11f-446d-b73f-8a224159b377");
        request.AddHeader("cache-control", "no-cache");
        request.AddHeader("ocp-apim-subscription-key", FaceAPIKey);
        request.AddHeader("Content-Type", "application/octet-stream");
        request.AddParameter("undefined", imageBytes, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }
    &nbsp;
    public string Validate(string url, byte[] imagebytes)
    {
        try
        {
            //API CAll
            var apiresponse = FaceAPICall(imagebytes);
&nbsp;
            bool isface = true, ismultipleface = true, issunglasses = true, isemotions = true;
&nbsp;
            if (apiresponse.Length == 2)
            {
                isface = false;
            }
&nbsp;
            if (apiresponse.Length > 2)
            {
                JArray items = JArray.Parse(apiresponse);
&nbsp;
                for (int i = 0; i < items.Count; i++)
                {
                    var item = (JObject)items[i];
                    var itemres = item["faceAttributes"]["glasses"];
&nbsp;
                    if (itemres.ToString() == "Sunglasses")
                    {
                        issunglasses = false;
                    }
                }
            }
&nbsp;
            if (apiresponse.Length > 2)
            {
                JArray items = JArray.Parse(apiresponse);
                int length = items.Count;
&nbsp;
                if (length > 1)
                {
                    ismultipleface = false;
                }
            }
&nbsp;
            if (apiresponse.Length > 2)
            {
                JArray items = JArray.Parse(apiresponse);
&nbsp;
                for (int i = 0; i < items.Count; i++)
                {
                    var item = (JObject)items[i];
                    var anger = item["faceAttributes"]["emotion"]["anger"];
                    var sadness = item["faceAttributes"]["emotion"]["sadness"];
                    var surprise = item["faceAttributes"]["emotion"]["surprise"];
&nbsp;
                    if ((float)anger > 0.1 || (float)sadness > 0.1 || (float)surprise > 0.1)
                    {
                        isemotions = false;
                    }
                }
            }
&nbsp;
            //Check with API Call Result
            if (!isface)
            {
                return "Face Not Found";
            }
&nbsp;
            //Check with API Call Result
            if (!ismultipleface)
            {
                return "Multiple Faces are detected";
            }                          
&nbsp;
            //Check with API Call Result
            if (!issunglasses)
            {
                return "Please remove the sunglasses";
            }                          
&nbsp;
            //Check with API Call Result
            if (!isemotions)
            {
                return "Your expression must be Neutral";
            }                           
&nbsp;
            //Success Enum
            return "0";
        }
        catch (Exception e)
        {
            error = e.Message;
            return "";
        }
    }
}
</code>
</pre>
</blockquote>
<li>Also add the below namespace in 'ImageValidationHandler.cs' file</li>
<blockquote>
<pre>
 <code>using RestSharp;</code>
</pre>
</blockquote>
</strong>
 <img src="http://139.59.61.161/PartnerTechSeries2019/Invoke_StarterKit/10.PNG" alt="image" style="max-width:100%;">
<h3>Invoke the Validate() function of ImageValidationHandler Class from Facade</h3>
   <strong>
   <li>Paste the code given below in 'Facade.cs', (i.e) below the comment 'Paste the User Image Validation Function code here...'</li>
   <blockquote>
     <pre>
       <code>
         public static List&lt;List&lt;string&gt;&gt; User_ImageValidation(byte[] imagebyte, string url)
         {
            List&lt;List&lt;string&gt;&gt; err = new List&lt;List&lt;string&gt;&gt;();
            err.Add(new List&lt;string&gt;());
&nbsp;
            ImageValidationHandler ivhobj = new ImageValidationHandler();
&nbsp;
            string result = ivhobj.Validate(url, imagebyte);
&nbsp;
            if (result == "0")
            {
                err[0].Add("Success");
                err[0].Add("");
                return err;
            }
            else
            {
                err[0].Add(result);
                err[0].Add("");
                return err;
            }
        }
       </code>
     </pre>
   </blockquote></strong>
   <h3>Invoke the User_ImageValidation() function of Facade Class from HomeController</h3><strong>
   <li>Paste the code given below in 'HomeController.cs', (i.e) below the comment 'Paste the ImageValidationAPI code here...'</li>
   <blockquote>
     <pre>
       <code>
        public async Task&lt;JsonResult&gt; ImageValidationAPI(string data)
        {
            string imgefile = "Img" + $@"{System.DateTime.Now.Ticks}.jpg";
            string Url = Server.MapPath(@"~\Images\" + imgefile);
            System.IO.File.WriteAllBytes(Url, Convert.FromBase64String(data));
            var imagebyte = Facade.storetoserver(data);
&nbsp;
            List&lt;List&lt;string&gt;&gt; result = Facade.User_ImageValidation(imagebyte, imgefile);
&nbsp;
            if (result[0][1] == "")
            {
                return Json(new { Result = result[0][0] });
            }
&nbsp;
            return Json(new { Result = "Failed" });
        }
      </code>
   </pre>
 </blockquote>
</strong>
  <img src="http://139.59.61.161/PartnerTechSeries2019/Invoke_StarterKit/11.PNG" alt="image" style="max-width:100%;">
</ol>
  <h3>Till this you can run the solution and get the output</h3>
    <p><b>STEP 1 :</b> Make sure you take the picture with a face to pass the face availability. Also take a picture without showing the face in the camera to get the error message 'Face not found'</p>
    <p><b>STEP 2 :</b> Make sure you take the picture with a single person to pass the multiple face check. Also take a picture with more than one person to get the error message 'Multiple Faces are not allowed'</p>
    <p><b>STEP 3 :</b> Make sure you take the picture without wearing sunglasses to pass the sunglasses check. Also take a picture sunglasses to get the error message 'Please remove the sunglasses'. [Note : Reading glasses are allowed]</p>
    <p><b>STEP 4 :</b> Make sure you do not show emotions such as anger, sadness and surprise while taking the picture to pass the allowed emotions check. Also take a picture with the above specified emotions to get the error message 'Your expression must be Neutral'</p>
  <h2>Sample Outputs</h2>
  <li>Face availability test case</li>
  <img src="http://139.59.61.161/PartnerTechSeries2019/Emotions/1.PNG" alt="image" style="max-width: 100%;">
  <li>Multiple face test case</li>
  <img src="http://139.59.61.161/PartnerTechSeries2019/Emotions/2.PNG" alt="image" style="max-width: 100%;">
  <li>Sunglasses test case</li>
  <img src="http://139.59.61.161/PartnerTechSeries2019/Emotions/3.PNG" alt="image" style="max-width: 100%;">
  <li>Reading glasses are allowed</li>
  <img src="http://139.59.61.161/PartnerTechSeries2019/Emotions/4.PNG" alt="image" style="max-width: 100%;">
  <li>Allowed Emotions test case - Anger, Sad, Surprised emotions are not allowed</li>
  <img src="http://139.59.61.161/PartnerTechSeries2019/Emotions/5.PNG" alt="image" style="max-width: 100%;">
  <img src="http://139.59.61.161/PartnerTechSeries2019/Emotions/6.PNG" alt="image" style="max-width: 100%;">
  <img src="http://139.59.61.161/PartnerTechSeries2019/Emotions/7.PNG" alt="image" style="max-width: 100%;">&nbsp;
<h3>Azure SQL Server Database Connectivity</h3>
<li>Open SQL Server Management Studio</li>
<li>To connect with the Azure SQL Server, you have to provide Server name, Login and Password details.</li>
<img src="http://139.59.61.161/PartnerTechSeries2019/DB_Creation/1.jpg" style="max-width:100%;">
<p>Download the script file from the GitHub and run the <a href="https://github.com/VectorSense/Azure-AI-Ninja-Tech-Series/tree/master/DIY%20Demos/HOL_Script.sql">Script File</a> </p> 
<h2>Screens to demonstrate how to run the script file</h2>
<li>Open the script file from the path where you have saved the downloaded GIT script file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/DB_Creation/2.jpg" style="max-width:100%;">&nbsp;
<li>Select the appropriate script file, and click on open</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/DB_Creation/3.jpg" style="max-width:100%;">&nbsp;
<li>Run the script file to create all the tables(usertable, imagevalidation, gesture, auditlog, verifytime)</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/DB_Creation/4.jpg" style="max-width:100%;">&nbsp;
<li>Click the Execute button to run the script</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/DB_Creation/5.jpg" style="max-width:100%;">&nbsp;
<li>Output message</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/DB_Creation/6.jpg" style="max-width:100%;">&nbsp;
<li>Till now we have created all the tables, just click the database to visualize the tables </li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/DB_Creation/7.jpg" style="max-width:100%;">&nbsp;
<li>Click your Database</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/DB_Creation/8.jpg" style="max-width:100%;">&nbsp;
<li>Click tables</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/DB_Creation/9.jpg" style="max-width:100%;">&nbsp;
<li>List of all the tables</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/DB_Creation/10.jpg" style="max-width:100%;">&nbsp;
<h3>Azure SQL Server Connectivity through Azure Portal</h3>
<li>Sign-in to Azure Portal by typing "portal.azure.com" in browser, enter your username</li>
<img src="http://139.59.61.161/PartnerTechSeries2019/sql/sql0c.JPG" alt="image" style="max-width: 100%;">
<li>Enter your Password</li>
<img src="http://139.59.61.161/PartnerTechSeries2019/sql/sql0.1c.JPG" alt="image" style="max-width: 100%;">
<li>Click on SQL databases tab in the left pane</li>
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql1.jpg" alt="image" style="max-width: 100%;">
<li>Select your particular database</li>
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql2.jpg" alt="image" style="max-width: 100%;">
<li>Click on Query editor</li>
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql3.jpg" alt="image" style="max-width: 100%;">
<li>Specify your database password</li>
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql5.jpg" alt="image" style="max-width: 100%;">
<li>Copy all the queries from <a href="https://github.com/VectorSense/Azure-AI-Ninja-Tech-Series/tree/master/DIY%20Demos/HOL_Script.sql">sql script file</a></li>
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql4.jpg" alt="image" style="max-width: 100%;">   
<li>Paste all the queries in the editor pane and click on Run button</li>
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql6_hackathon.jpg" alt="image" style="max-width: 100%;">
<li>Now click on Connection strings tab in the left pane</li>
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql7_hackathon.jpg" alt="image" style="max-width: 100%;">
<li>Copy your Connection string</li>
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql8_hackathon.jpg" alt="image" style="max-width: 100%;">
<h4>Paste your SQL server connection string in Web.config (i.e) for the key 'AzureSqlConnectionString', in the connection string specify your database user name and password, make 'MultipleActiveResultSets' as True</h4>
<p>Sample Connectionstring : "Server=tcp:jsn.database.windows.net,1433;Initial Catalog=dbname;Persist Security Info=False;User ID=demouser; Password=demo@pass123;MultipleActiveResultSets=True;Encrypt=True; TrustServerCertificate=False;Connection Timeout=30;"</p>
<p>Lets move on to the actual scenario where the Admin part is also included.</p>
<p>The below code snippet contains the image_validation class properties. The ImageValidationTable Class has separate functions for each database query.</p>
<ol>
  <strong>
    <li>Paste the code given below in 'StorageHandler.cs', (i.e) below the comment 'Paste the image_validation Class code here...'</li>
    <blockquote>
      <pre>
        <code>
//Image validation Class - initialization
public class image_validation
{
    public int id { get; set; }
    public string validation_type { get; set; }
    public string validation_message { get; set; }
    public int isactive { get; set; }
}</code></pre></blockquote>
<li>Paste the code given below in 'StorageHandler.cs', (i.e) below the comment 'Paste the ImageValidationTable Class code here...'</li>
<blockquote><pre><code>
// Image validation - table operations 
public class ImageValidationTable
{
      //Connection String
      private static string connectionString = ConfigurationManager.AppSettings["AzureSqlConnectionString"];
      public string error = "";
      &nbsp;
      // Select function
      public List&lt;image_validation&gt;AdminList()
      {
           // Image Validation List creation
            var imagevalidation_list = new List&lt;image_validation&gt;();
            &nbsp;
            try
            {
               using (SqlConnection conn = new SqlConnection(connectionString))
               {
                  // Selecting all rows in image validation table
                  SqlCommand cmd = new SqlCommand("SELECT * FROM imagevalidation", conn);
                  //Connection Open 
                  conn.Open();
                  SqlDataReader rdr = cmd.ExecuteReader();
                  while (rdr.Read())
                  {
                     //Creating Image Validation Object
                    var imagevalidation_obj = new image_validation();
                    imagevalidation_obj.id = (int)rdr["id"];
                    imagevalidation_obj.validation_type = rdr["validation_type"].ToString();
                    imagevalidation_obj.validation_message = rdr["validation_message"].ToString();
                    imagevalidation_obj.isactive = (int)rdr["isactive"];
                    &nbsp;
                    // Adding object file to Model file
                    imagevalidation_list.Add(imagevalidation_obj);
                  }
                  //Connection Close
                  conn.Close();
                }
              // returning the List
              return imagevalidation_list;
            }
            catch (Exception e)
            {
                error = e.Message;
                return imagevalidation_list;
            }
      }
      &nbsp;
      // Select function
      public List&lt;bool&gt; UserList()
      {
          // Image Validation List creation
          var imagevalidation_list = new List&lt;bool&gt;();
          &nbsp;
          try
          {
              using (SqlConnection conn = new SqlConnection(connectionString))
              {
                  // Selecting all rows in image validation table
                  SqlCommand cmd = new SqlCommand("SELECT * FROM imagevalidation", conn);
                  //Connection Open 
                  conn.Open();
                  SqlDataReader rdr = cmd.ExecuteReader();
                  while (rdr.Read())
                  {                                   
                      // Adding object file to Model file
                      if ((int)rdr["isactive"]==0)
                          imagevalidation_list.Add(true);
                      else
                          imagevalidation_list.Add(false);
                  }
                  //Connection Close
                  conn.Close();
              }
              // returning the List
              return imagevalidation_list;
          }
          catch (Exception e)
          {
              error = e.Message;
              return imagevalidation_list;
          }
      }
      &nbsp;
      // Select function by ID
      public image_validation AdminListById(string data)
      {
          // Image Validation object creation
          var imagevalidation_obj = new image_validation();
          &nbsp;
          try
          {
              // Initialization
              SqlConnection conn;
              SqlDataReader rdr;
              SqlCommand cmd;
              &nbsp;
              var id = Convert.ToInt32(data);
              using (conn = new SqlConnection(connectionString))
              {
                  // Selecting all the rows in the image validation 
                  cmd = new SqlCommand("SELECT * FROM imagevalidation where id ='" + id + "'", conn);
                  conn.Open();
                  rdr = cmd.ExecuteReader();
                  while (rdr.Read())
                  {
                      imagevalidation_obj.id = (int)rdr["id"];
                      imagevalidation_obj.validation_type = rdr["validation_type"].ToString();
                      imagevalidation_obj.validation_message = rdr["validation_message"].ToString();
                      imagevalidation_obj.isactive = (int)rdr["isactive"];
                  }
                  conn.Close();
              }
              // Returning object
              return imagevalidation_obj;
          }
          catch (Exception e)
          {
              error = e.Message;
              return imagevalidation_obj;
          }
      }
      &nbsp;
      // Update function 
      public bool Modify(string data, string isactive)
      {
          try
          {
              // Initialization 
              SqlConnection conn;
              SqlCommand cmd;
              var id = Convert.ToInt32(data);
              &nbsp;
              using (conn = new SqlConnection(connectionString))
              {
                  // Selecting the perticular row in the table and updating that using particular ID 
                  cmd = new SqlCommand("update imagevalidation set isactive ='" + isactive + "' where id = '" + id + "'", conn);
                  //connection open
                  conn.Open();
                  var temp = cmd.ExecuteNonQuery();
                  //connection close
                  conn.Close();
                  if (temp != 0)
                      return true;
                  return false;
              }
          }
          catch (Exception e)
          {
              error = e.Message;
              return false;
          }
      }
}
</code>
      </pre>
    </blockquote>
  </strong>
  <li>Paste the code given below in 'StorageHandler.cs', (i.e) below the comment 'Paste the 'audit_log' Class code here...'</li>
  <strong>
    <blockquote>
      <pre>
        <code>
// Audit Log class - initialization
public class audit_log
{
    public int id { get; set; }
&nbsp;
    public string layer { get; set; }
&nbsp;
    public string result_type { get; set; }
&nbsp;
    public string device_type { get; set; }
&nbsp;
    public string userimage { get; set; }
}
        </code>
      </pre>
    </blockquote>
  </strong>
<li>Paste the code given below in 'StorageHandler.cs', (i.e) below the comment 'Paste the 'AuditLoggerTable' Class code here...'</li>
    <strong>
    <blockquote>
      <pre>
        <code>
class AuditLoggerTable
{
    //Connection string
    private static string connectionString = ConfigurationManager.AppSettings["AzureSqlConnectionString"];
    public string error = "";
&nbsp;
    // User Insert
    public bool Add(string layer, string result_type, string imageurl, string device_type = "Web")
    {
        try
        {
            // Initialization 
            SqlConnection conn;
            SqlCommand cmd;
&nbsp;
            using (conn = new SqlConnection(connectionString))
            {
                // Selecting the perticular row in the table and updating that using particular ID 
                cmd = new SqlCommand("INSERT INTO auditlog(layer, result_type, device_type, userimage) VALUES('" + layer + "', '" + result_type + "', '" + device_type + "', '" + imageurl + "')", conn);
                //connection open
                conn.Open();
                var temp = cmd.ExecuteNonQuery();
                //connection close
                conn.Close();
                if (temp != 0)
                    return true;
                return false;
            }
        }
        catch (Exception e)
        {
            error = e.Message;
            // Returning the result
            return false;
        }
    }
&nbsp;
    public List&lt;audit_log&gt; List()
    {
        // Audit log list creation
        var auditlog_list = new List&lt;audit_log&gt;();
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Selecting the all the rows in gthe Audit Log table
                SqlCommand cmd = new SqlCommand("SELECT * FROM auditlog order by id desc", conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    // Creating audit log object file
                    var auditlog_obj = new audit_log();
                    auditlog_obj.id = (int)rdr["id"];
                    auditlog_obj.layer = rdr["layer"].ToString();
                    auditlog_obj.result_type = rdr["result_type"].ToString();
                    auditlog_obj.device_type = rdr["device_type"].ToString();
                    auditlog_obj.userimage = rdr["userimage"].ToString();
                    // Adding the audit log object file into list file
                    auditlog_list.Add(auditlog_obj);
                }
                //connection close
                conn.Close();
            }
            // returning the list file
            return auditlog_list;
        }
        catch (Exception e)
        {
            error = e.Message;
            return auditlog_list;
        }
    }
}
        </code>
      </pre>
    </blockquote>
  </strong>
</ol>
<p>Till this you have completed the Admin Image Validation part</p>
<p>Now, lets do some code changes in the ImageValidationHandler.cs file for checking whether the Is_Active field is enabled or disabled</p>
<p>Below code snippet is used to update the changes in ImageValidationHandler.cs file</p>
<strong>
    <li>Replace the code given below in 'ImageValidationHandler.cs', (i.e) Update the whole ImageValidationHandler Class which is already there</li>
    <blockquote>
      <pre>
        <code>
public class ImageValidationHandler
{
    //Assigning Subscription Key and Face Endpoint from web.config file
    private static string FaceAPIKey = ConfigurationManager.AppSettings["FaceAPIKey"], FaceAPIEndpoint = ConfigurationManager.AppSettings["FaceAPIEndPoint"];
&nbsp;
    public string error = "";
&nbsp;
    public string Validate(string url, byte[] imagebytes, bool face, bool multiple_face, bool sunglasses, bool emotions)
    {
        try
        {
            //API CAll
            var apiresponse = FaceAPICall(imagebytes);
&nbsp;
            //DB check
            AuditLoggerTable alt = new AuditLoggerTable();
&nbsp;
            bool isface = true, ismultipleface = true, issunglasses = true, isemotions = true;
&nbsp;
            if (apiresponse.Length == 2)
            {
                isface = false;
            }
&nbsp;
            if (apiresponse.Length > 2)
            {
                JArray items = JArray.Parse(apiresponse);
&nbsp;
                for (int i = 0; i < items.Count; i++)
                {
                    var item = (JObject)items[i];
                    var itemres = item["faceAttributes"]["glasses"];
&nbsp;
                    if (itemres.ToString() == "Sunglasses")
                    {
                        issunglasses = false;
                    }
                }
            }
&nbsp;
            if (apiresponse.Length > 2)
            {
                JArray items = JArray.Parse(apiresponse);
                int length = items.Count;
&nbsp;
                if (length > 1)
                {
                    ismultipleface = false;
                }
            }
&nbsp;
            if (apiresponse.Length > 2)
            {
                JArray items = JArray.Parse(apiresponse);
&nbsp;
                for (int i = 0; i < items.Count; i++)
                {
                    var item = (JObject)items[i];
                    var anger = item["faceAttributes"]["emotion"]["anger"];
                    var sadness = item["faceAttributes"]["emotion"]["sadness"];
                    var surprise = item["faceAttributes"]["emotion"]["surprise"];
&nbsp;
                    if ((float)anger > 0.1 || (float)sadness > 0.1 || (float)surprise > 0.1)
                    {
                        isemotions = false;
                    }
                }
            }
&nbsp;
            //DB Check
            if (face)
            {
                //Check with API Call Result
                if (isface)
                {
&nbsp;
                    alt.Add("Face Availability", "Pass", url);
                }
                else
                {
                    alt.Add("Face Availability", "Fail", url);
                    return "Face Not Found";
                }
            }
&nbsp;
            //DB check
            if (multiple_face)
            {
                //Check with API Call Result
                if (ismultipleface)
                {
                    alt.Add("Multiple Face", "Pass", url);
                }
                else
                {
                    alt.Add("Multiple Face", "Fail", url);
                    return "Multiple Faces are not allowed";
                }
            }
&nbsp;
            //DB check
            if (sunglasses)
            {
                //Check with API Call Result
                if (issunglasses)
                {
                    alt.Add("Sunglasses", "Pass", url);
                }
                else
                {
                    alt.Add("Sunglasses", "Fail", url);
                    return "Please remove the sunglasses";
                }
            }
&nbsp;
            //DB check
            if (emotions)
            {
                //Check with API Call Result
                if (isemotions)
                {
                    alt.Add("Emotions", "Pass", url);
                }
                else
                {
                    alt.Add("Emotions", "Fail", url);
                    return "Your expression must be Neutral";
                }
            }
&nbsp;
            //Success Enum
            return "0";
        }
        catch (Exception e)
        {
            error = e.Message;
            return "";
        }
    }
&nbsp;
    public static string FaceAPICall(byte[] imageBytes)
    {
        var client = new RestClient(FaceAPIEndpoint + "/face/v1.0/detect?returnFaceLandmarks=false& returnFaceId =true&returnFaceAttributes=age%2Csmile%2Cgender%2Cglasses%2CheadPose%2CfacialHair%2Cemotion%2Cmakeup&%20returnFaceId%20=true");
        var request = new RestRequest(Method.POST);
        request.AddHeader("Postman-Token", "9a9a2c14-f11f-446d-b73f-8a224159b377");
        request.AddHeader("cache-control", "no-cache");
        request.AddHeader("ocp-apim-subscription-key", FaceAPIKey);
        request.AddHeader("Content-Type", "application/octet-stream");
        request.AddParameter("undefined", imageBytes, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
&nbsp;
    }
}
        </code>
      </pre>
    </blockquote>
  </strong>
</ol>
<p>Below code is used to update the changes in Facade.cs file</p>
<strong>
    <li>Replace the code given below in 'Facade.cs', (i.e) Update the whole User_ImageValidation Class which is already there</li>
    <blockquote>
      <pre>
        <code>
public static List&lt;List&lt;string&gt;&gt; User_ImageValidation(byte[] imagebyte, string url)
{
    List&lt;List&lt;string&gt;&gt; err = new List&lt;List&lt;string&gt;&gt;();
    err.Add(new List&lt;string&gt;());
&nbsp;
    ImageValidationHandler ivhobj = new ImageValidationHandler();
&nbsp;
    ImageValidationTable ivtobj = new ImageValidationTable();
&nbsp;
    List&lt;bool&gt; flag = ivtobj.UserList();
    if (ivtobj.error != "")
    {
        err[0].Add("");
        err[0].Add(ivtobj.error);
        return err;
    }
&nbsp;
    string result = ivhobj.Validate(url, imagebyte, flag[0], flag[2], flag[1], flag[3]);
&nbsp;
    if (result == "0")
    {
        err[0].Add("Success");
        err[0].Add("");
        return err;
    }
    else
    {
        err[0].Add(result);
        err[0].Add("");
        return err;
    }
}
        </code>
      </pre>
    </blockquote>
<li>Paste the following code in 'Facade.cs', (i.e) below the comment 'Paste the 'Admin_ImageShow' Function Code here...'</li>
<blockquote>
  <pre>
    <code>
public static List&lt;image_validation&gt; Admin_ImageShow()
{
    ImageValidationTable ivtobj = new ImageValidationTable();
    &nbsp;
    return ivtobj.AdminList(); 
}
    </code>
  </pre>
</blockquote>
<li>Paste the following code in 'Facade.cs', (i.e) below the comment 'Paste the 'Admin_ImageEdit' Function Code here...'</li>
<blockquote>
  <pre>
    <code>
public static image_validation Admin_ImageEdit(string id)
{
    ImageValidationTable ivtobj = new ImageValidationTable();
&nbsp;
    return ivtobj.AdminListById(id);
}
    </code>
  </pre>
</blockquote>
<li>Paste the following code in 'Facade.cs', (i.e) below the comment 'Paste the 'Admin_ImageUpdate' Function Code here...'</li>
<blockquote>
  <pre>
    <code>
public static bool Admin_ImageUpdate(string id, string isactive)
{
    ImageValidationTable ivtobj = new ImageValidationTable();
&nbsp;
    return ivtobj.Modify(id, isactive);
}
    </code>
  </pre>
</blockquote>
  </strong>
</ol>
<h3>Code to be added in 'HomeController.cs'</h3>
<p>Paste the following code in 'HomeController.cs', (i.e) below the comment 'Paste the 'ImageValidation_FetchByID' Function code here...'</p>
<strong>
<blockquote>
  <pre>
    <code>
public JsonResult ImageValidation_FetchByID(string data)
{
   // Calling the intermediator file and get the object file 
   var ImgvalID_obj = Facade.Admin_ImageEdit(data);
   //Returing the Json to view
   return Json(new { id = ImgvalID_obj.id, validation_type = ImgvalID_obj.validation_type, validation_message = ImgvalID_obj.validation_message, isactive = ImgvalID_obj.isactive });
}
    </code>
  </pre>
</blockquote>
</strong>
<p>Paste the following code in 'HomeController.cs', (i.e) below the comment 'Paste the 'ImageValidation_FetchByIsActive' Function code here...'</p>
<strong>
<blockquote>
  <pre>
    <code>
public JsonResult ImageValidation_FetchByIsActive(string data, string value)
{
    // Calling the intermediator file and get the object file
    var ImgvalIsActive = Facade.Admin_ImageUpdate(data, value);
    //Returing the Json to view
    return Json(new { Result = ImgvalIsActive });
}
    </code>
  </pre>
</blockquote>
</strong>
<p>Replace the following code in image_validation ActionResult of 'HomeController.cs', (i.e) below the comment 'Admin - Image Validation Page' [Note : Replace that whole actionresult code]</p>
<strong>
<blockquote>
  <pre>
    <code>
public ActionResult image_validation()
{
    // Calling the intermediator file and Returing the List file to the View 
    return View(Facade.Admin_ImageShow());
}
    </code>
  </pre>
</blockquote>
</strong>
<h3>Design Changes</h3>
<p>Lets start un-commenting the code in design screens</p>
<p>Follow the picture shown below to navigate in the image_validation.cshtml file</p>
<ol>
<strong>
<li>Open image_validation.cshtml</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/UnComment_ImageValidation/View_Comment1.PNG" style="max-width:100%;">&nbsp;
<li>Select the code from line number 221</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/UnComment_ImageValidation/View_Comment2.PNG" style="max-width:100%;">&nbsp;
<li>Select the code till line number 270</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/UnComment_ImageValidation/View_Comment3.PNG" style="max-width:100%;">&nbsp;
<li>Click on the uncomment button</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/UnComment_ImageValidation/View_Comment4.png" style="max-width:100%;">&nbsp;
</ol>
<h3>Just click the Run button and see the output in the admin side</h3>&nbsp;
<li>Admin Image Validation with entries</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Admin/image_1.JPG" alt="image" style="max-width: 100%;">&nbsp;
<li>Selecting the Edit Button</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Admin/image_2.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Getting modal box</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Admin/image_3.JPG" alt="image" style="max-width: 100%;">&nbsp;
<li>Selecting Enable button</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Admin/image_4.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Disabled the enable button by clicking</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Admin/image_5.JPG" alt="image" style="max-width: 100%;">&nbsp;
<li>Updating the table</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Admin/image_6.jpg" alt="image" style="max-width: 100%;">&nbsp;
</strong>
<h3>Congratulations! You have successfully completed Challenge 1</h3>
<h3>The next session is<a href="https://github.com/VectorSense/Azure-AI-Ninja-Tech-Series/blob/master/Challenge2.md"> Challenge 2</a></h3>
