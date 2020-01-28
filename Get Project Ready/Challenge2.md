<h1>AI Series HOL</h1>
<h2>Challenge 2 – Gesture Management using Azure Custom Vision</h2>
<p>In Challenge 2 we are going to explore how the Gesture Management works using Azure Custom Vision API.</p>
<h2>Getting Started</h2>
<h3>Prerequisites</h3>
<li>Kindly ensure that the application works fine so far.</li>
<li>We are going to build the Gesture Management in the same application</li>
<h3>Code Summary</h3>
<p>For the Gesture Management,Azure Custom Vision API will be used. For creating Custom Vision solution, we have used five gestures to verify the users. In the Admin portal, we can view all the gestures. Admin has the authority to Enable or Disable gestures.Also, he can add new gestures for gesture management.</p>
<p>In the Verification page, gesture will be used to verify a user. In the User's page we will diplay a gesture which will be randomly fetched from the database. The user should show the same gesture to verify the user name in Azure API</p>
<p>This application has the GestureHandler.cs file which is used to implement all the functionalities. The StorageHandler.cs file deals with all the database. The Facade.cs file is used as an intermediator between HomeController and GestureHandler files.</p>&nbsp;
<h3>Invoke the Gesture Management API</h3>
<ol>
  <strong>
      <li>To start with, have to update the API Prediction Key, Endpoint, Project ID and Iteration name in Web.Config</li>
      <li>Train your model in 'customvision.ai'</li>
      <li>After training your model publish it</li>
      <img src="http://139.59.61.161/PartnerTechSeries2019/custom/custom1.jpg" alt="image" style="max-width: 100%;">
      <img src="http://139.59.61.161/PartnerTechSeries2019/custom/custom2.jpg" alt="image" style="max-width: 100%;">
      <li>Click on Prediction URL</li>
      <img src="http://139.59.61.161/PartnerTechSeries2019/custom/custom3.jpg" alt="image" style="max-width: 100%;">
      <li>Grab your endpoint, project-id and iteration name. [Example endpoint : 'https://southeastasia.api.cognitive.microsoft.com']</li>
      <img src="http://139.59.61.161/PartnerTechSeries2019/custom/custom6.jpg" alt="image" style="max-width: 100%;">
      <li>Follow the picture shown below to navigate to the Web.Config file</li>&nbsp;
      <img src="http://139.59.61.161/PartnerTechSeries2019/Class/Web_config.PNG" alt="image" style="max-width:100%;">&nbsp;
      <li>Copy and paste the Endpoint in 'GestureAPICall', Prediction Key in 'GestureKey' for invoking Gesture Management API.</li>&nbsp;
      <img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/12.PNG" alt="image" style="max-width: 100%;">
      <li>Copy your Project ID</li>&nbsp;
      <img src="http://139.59.61.161/PartnerTechSeries2019/custom/custom7.jpg" alt="image" style="max-width:100%;">&nbsp;
      <img src="http://139.59.61.161/PartnerTechSeries2019/custom/custom8.jpg" alt="image" style="max-width:100%;">&nbsp;
      <li>Now paste the values for project-id in GestureProjectID and iteration in GestureIteration [example iteration name is 'Iteration1']</li>
      <img src="http://139.59.61.161/PartnerTechSeries2019/custom/new.PNG" alt="image" style="max-width:100%;">&nbsp;
    </strong>
</ol>
<h3>Getting started with coding - here we will implement the GestureHandler.cs file</h3>
<li>Follow the screenshot shown below to navigate to the GestureHandler.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Class/GestureHandler.PNG" alt="image" style="max-width:100%;">&nbsp;
<ol>
<strong>
<li>Copy and Paste the code given below in 'GestureHandler.cs', (i.e) below the comment 'Paste the GestureHandler Class code here...'</li>
<li>Follow the picture shown below to navigate to the GestureHandler.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/13-New.PNG" alt="image" style="max-width:100%;">&nbsp;
<blockquote>
<pre>
<code>
public class GestureHandler
{
    //Assigning Subscription Key and Endpoint from web.config file
    private static string GestureAPIKey = ConfigurationManager.AppSettings["GestureKey"], GestureAPIEndpoint = ConfigurationManager.AppSettings["GestureAPICall"], GestureProjectID = ConfigurationManager.AppSettings["GestureProjectID"], GestureIteration = ConfigurationManager.AppSettings["GestureIteration"];
    public string error = "";
&nbsp;
    //DB check
    AuditLoggerTable alt = new AuditLoggerTable();
&nbsp;
    public bool Validate(string url, byte[] imageBytes, string gesture)
    {
        string tagname = "";
        try
        {
            //Gesture API Call
            var client1 = new RestClient(GestureAPIEndpoint + "/customvision/v3.0/Prediction/"+ GestureProjectID + "/detect/iterations/"+ GestureIteration +"/image");
            var request1 = new RestRequest(Method.POST);
            request1.AddHeader("cache-control", "no-cache");
            request1.AddHeader("Content-Type", "application/octet-stream");
            request1.AddHeader("Prediction-Key", GestureAPIKey);
            request1.AddParameter("data", imageBytes, ParameterType.RequestBody);
            IRestResponse response1 = client1.Execute(request1);
&nbsp;
            var result = response1.Content;
&nbsp;
            dynamic objjsn = JObject.Parse(result);
            var jsnar = objjsn.predictions.ToString();
            JArray ar = JArray.Parse(jsnar);
            int max = 0;
            for (int i = 0; i < ar.Count; i++)
            {
                dynamic pred = JObject.Parse(ar[i].ToString());
                int prob = pred.probability * 100;
&nbsp;
                if (prob > max)
                {
                    max = prob;
                    tagname = pred.tagName;
                }
            }
&nbsp;
            if (tagname == gesture)
            {
                alt.Add("Random Gesture", "Pass", url);
                return true;
            }
&nbsp;
            alt.Add("Random Gesture", "Fail", url);
            return false;
        }
        catch (Exception e)
        {
            error = e.Message;
            return false;
        }
    }
&nbsp;
    public bool GenerateDefaultGesture(string url, byte[] imageBytes)
    {
        try
        {
            //Smile API Call
            var response = ImageValidationHandler.FaceAPICall(imageBytes);
&nbsp;
            if (response.Length > 2)
            {
                JArray items = JArray.Parse(response);
&nbsp;
                for (int i = 0; i < items.Count; i++)
                {
                    var item = (JObject)items[i];
                    var smile = item["faceAttributes"]["smile"];
&nbsp;
                    if ((double)smile > 0.5)
                    {
                        alt.Add("Default Gesture - Smile", "Pass", url);
                        return true;
                    }
                }
            }
            alt.Add("Default Gesture - Smile", "Fail", url);
            return false;
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
<li>Also add the below namespace in 'GestureHandler.cs' file</li>
<blockquote>
<pre>
 <code>using RestSharp;</code>
</pre>
</blockquote>
</strong>
</ol>
<h2>Azure SQL Server Table Updates</h2>
<h3>Code snippet demonstrating the implementation of StorageHandler.cs file</h3>
<li>Follow the screenshot shown below to navigate to the StorageHandler.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Class/StorageHandler.PNG" alt="image" style="max-width:100%;">&nbsp;
<ol>
<strong>
<li>Copy and Paste the code given below in 'StorageHandler.cs', (i.e) below the comment 'Paste the gesture_management Class code here...'</li>
<li>Follow the screenshot shown below to navigate to the gesture_management Class in the StorageHandler.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/1.png" alt="image" style="max-width:100%;">&nbsp;
<blockquote>
<pre>
<code>
public class gesture_management
{
&nbsp;
    public int id { get; set; }
&nbsp;
    public string gesture_name { get; set; }
&nbsp;
    public string thumbnail_url { get; set; }
&nbsp;
    public string gesture_message { get; set; }
&nbsp;
    public int isactive { get; set; }
}
</code>
</pre>
</blockquote>
</strong>
</ol>
<ol>
<strong>
<li>Copy and Paste the code given below in 'StorageHandler.cs', (i.e) below the comment 'Paste the GestureTable Class code here...'</li>
<li>Follow the picture shown below to navigate to the GestureTable Class in the StorageHandler.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/2.png" alt="image" style="max-width:100%;">&nbsp;
<blockquote>
<pre>
<code>
class GestureTable
{
    //Connection String
    private static string connectionString = ConfigurationManager.AppSettings["AzureSqlConnectionString"];
    public string error = "";
&nbsp;
    // Select function
    public List&lt;gesture_management&gt; List()
    {
        // Gesture Management Model Initialization 
        var gesturemanagement_list = new List&lt;gesture_management&gt;();
&nbsp;
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Selecting all the rows in the gesture table
                SqlCommand cmd = new SqlCommand("SELECT * FROM gesture", conn);
                //connection open
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    // Creating Gesture management Object file
                    var gesturemanagement_obj = new gesture_management();
                    gesturemanagement_obj.id = (int)rdr["id"];
                    gesturemanagement_obj.gesture_name = rdr["gesture_name"].ToString();
                    gesturemanagement_obj.thumbnail_url = rdr["thumbnail_url"].ToString();
                    gesturemanagement_obj.gesture_message = rdr["gesture_message"].ToString();
                    gesturemanagement_obj.isactive = (int)rdr["isactive"];
                    // Adding the Object to model file 
                    gesturemanagement_list.Add(gesturemanagement_obj);
                }
                //connection close
                conn.Close();
            }
            // Returning the model file
            return gesturemanagement_list;
        }
        catch (Exception e)
        {
            error = e.Message;
            return gesturemanagement_list;
        }
    }
&nbsp;
    // Edit function
    public gesture_management Edit(string data)
    {
        // Image Validation object creation
        var gesturemanagement_obj = new gesture_management();
        try
        {
            // Initialization
            SqlConnection conn;
            SqlDataReader rdr;
            SqlCommand cmd;
&nbsp;
            var id = Convert.ToInt32(data);
&nbsp;
            using (conn = new SqlConnection(connectionString))
            {
                // Selecting all the rows in the image validation 
                cmd = new SqlCommand("SELECT * FROM gesture where id ='" + id + "'", conn);
                conn.Open();
&nbsp;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    gesturemanagement_obj.id = (int)rdr["id"];
                    gesturemanagement_obj.gesture_name = rdr["gesture_name"].ToString();
                    gesturemanagement_obj.thumbnail_url = rdr["thumbnail_url"].ToString();
                    gesturemanagement_obj.gesture_message = rdr["gesture_message"].ToString();
                    gesturemanagement_obj.isactive = (int)rdr["isactive"];
                }
                conn.Close();
            }
            // Returning object
            return gesturemanagement_obj;
        }
&nbsp;                        
        catch (Exception e)
        {
            error = e.Message;
            // Returning the result
            return gesturemanagement_obj;
        }
    }
&nbsp;
    //Update function
    public bool Update(string data, string isactive)
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
                cmd = new SqlCommand("update gesture set isactive ='" + isactive + "' where id = '" + id + "'", conn);
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
&nbsp;
        //Insert function
        public bool Add(string name, string url, string message, string active)
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
                    cmd = new SqlCommand("insert into gesture(gesture_name,thumbnail_url,gesture_message,isactive) values('" + name + "','" + url + "','" + message + "','" + active + "') ", conn);
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
    //Random Gestures selection function
    public List&lt;string&gt; GenerateRandomGesture()
    {
        var gesturemanagement_list = new List&lt;string&gt;();
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Selecting the all the rows in the Audit Log table
                SqlCommand cmd = new SqlCommand("SELECT top 10 percent id, gesture_name, thumbnail_url from gesture where isactive = 0 order by newid()", conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    // Adding the Object to model file 
                    gesturemanagement_list.Add(rdr["gesture_name"].ToString());
                    gesturemanagement_list.Add(rdr["thumbnail_url"].ToString());
                }
                //connection close
                conn.Close();
                return gesturemanagement_list;
            }
        }
        catch (Exception e)
        {
            error = e.Message;
            return gesturemanagement_list;
        }
    }
}
</code>
</pre>
</blockquote>
</strong>
</ol>
<h3>Below is the code snippet of Intermediator -'Facade.cs' Class which is used to connect to the C# classes [GestureHandler.cs, StorageHandler.cs & HomeController.cs]</h3>
<ol>
<strong>
<li>Follow the screenshot shown below to navigate to the RandomGestureShow Function in the Facade.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/3.png" alt="image" style="max-width:100%;">&nbsp;
<li>Copy and Paste the code given below in 'Facade.cs', (i.e) below the comment 'Paste the RandomGestureShow Function Code here...'</li>
<blockquote>
<pre>
<code>
public static List&lt;List&lt;string&gt;&gt; RandomGestureShow()
{
    GestureTable gtobj = new GestureTable();
    List&lt;List&lt;string&gt;&gt; err = new List&lt;List&lt;string&gt;&gt;();
    err.Add(new List&lt;string&gt;());
    List&lt;string&gt; gsres = gtobj.GenerateRandomGesture();
&nbsp;
    if (gtobj.error != "")
    {
        err[0].Add("");
        err[0].Add(gtobj.error);
        return err;
    }
    err[0].Add(gsres[0]);
    err[0].Add(gsres[1]);
    return err;
}
</code>
</pre>
</blockquote>
</strong>
</ol>
<ol>
<strong>
<li>Copy and Paste the code given below in 'Facade.cs', (i.e) below the comment 'Paste the Admin_GestureShow Function Code here...'</li>
<li>Follow the screenshot shown below to navigate to the Admin_GestureShow Function in the Facade.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/4.png" alt="image" style="max-width:100%;">&nbsp;
<blockquote>
<pre>
<code>
public static List&lt;gesture_management&gt; Admin_GestureShow()
{
    GestureTable gstobj = new GestureTable();
&nbsp;
    return gstobj.List();
}
</code>
</pre>
</blockquote>
</strong>
</ol>
<ol>
<strong>
<li>Copy and Paste the code given below in 'Facade.cs', (i.e) below the comment 'Paste the Admin_GestureEdit Function Code here...'</li>
<li>Follow the screenshot shown below to navigate to the Admin_GestureEdit Function in the Facade.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/5.png" alt="image" style="max-width:100%;">&nbsp;
<blockquote>
<pre>
<code>
public static gesture_management Admin_GestureEdit(string id)
{
&nbsp;
    GestureTable gt = new GestureTable();
    return gt.Edit(id);
}
</code>
</pre>
</blockquote>
</strong>
</ol>
<ol>
<strong>
<li>Copy and Paste the code given below in 'Facade.cs', (i.e) below the comment 'Paste the Admin_GestureUpdate Function Code here...'</li>
<li>Follow the screenshot shown below to navigate to the Admin_GestureUpdate Function in the Facade.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/6.png" alt="image" style="max-width:100%;">&nbsp;
<blockquote>
<pre>
<code>
public static bool Admin_GestureUpdate(string id, String isactive)
{
&nbsp;
    GestureTable gt = new GestureTable();
    return gt.Update(id,isactive);
}
	</code>
</pre>
</blockquote>
</strong>
</ol>
<ol>
<strong>
<li>Copy and Paste the code given below in 'Facade.cs', (i.e) below the comment 'Paste the Admin_GestureAdd Function Code here...'</li>
<li>Follow the picture shown below to navigate to the Admin_GestureAdd Function in the Facade.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/7.png" alt="image" style="max-width:100%;">&nbsp;
<blockquote>
<pre>
<code>
public static bool Admin_GestureAdd(string gesture_name, string thumbnail_url, string gesture_message, string isactive)
{
    GestureTable gstobj = new GestureTable();
&nbsp;
    if (gstobj.Add (gesture_name, thumbnail_url, gesture_message, isactive))
    {
        return true;
    }
&nbsp;
    return false;
}
</code>
</pre>
</blockquote>
</strong>
</ol>
<h3>Below is the code snippet to call the Facade.cs file in 'HomeController.cs' file</h3>
<ol>
<strong>
<li>Copy and Paste the code given below in 'HomeController.cs', (i.e) below the comment 'Paste the RandomGestureAPI Function code here...'</li>
<li>Follow the screenshot shown below to navigate to the RandomGestureAPI Function in the HomeController.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/8.png" alt="image" style="max-width:100%;">&nbsp;
<blockquote>
<pre>
<code>
//Random Gesture API
public JsonResult RandomGestureAPI()
{
    List&lt;List&lt;string&gt;&gt; result = Facade.RandomGestureShow();
&nbsp;
    return Json(new { GestureName = result[0][0], GestureUrl = result[0][1] });
}
</code>
</pre>
</blockquote>
</strong>
</ol>
<ol>
<strong>
<li>Copy and Paste the code given below in 'HomeController.cs', (i.e) below the comment 'Paste the GestureManagement_FetchByID Function code here...'</li>
<li>Follow the screenshot shown below to navigate to the GestureManagement_FetchByID Function in the HomeController.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/9.png" alt="image" style="max-width:100%;">&nbsp;
<blockquote>
<pre>
<code>
public JsonResult GestureManagement_FetchByID(string data)
{
    // Calling the intermediator file and get the object file 
    var gestureManagementID_obj = Facade.Admin_GestureEdit(data);
    //Returing the Json to view
    return Json(new { id = gestureManagementID_obj.id, gesture_name = gestureManagementID_obj.gesture_name, thumbnail_url = gestureManagementID_obj.thumbnail_url, gesture_message = gestureManagementID_obj.gesture_message, isactive = gestureManagementID_obj.isactive });
}
</code>
</pre>
</blockquote>
</strong>
</ol>
<ol>
<strong>
<li>Copy and Paste the code given below in 'HomeController.cs', (i.e) below the comment 'Paste the 'GestureManagement_FetchByIsActive' Function code here...'</li>
<li>Follow the screenshot shown below to navigate to the GestureManagement_FetchByIsActive Function in the HomeController.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/10.png" alt="image" style="max-width:100%;">&nbsp;
<blockquote>
<pre>
<code>
public JsonResult GestureManagement_FetchByIsActive(string data, string value)
{
    // Calling the intermediator file and get the Boolean value 
    var GestureUpdateIsActive = Facade.Admin_GestureUpdate(data, value);
&nbsp;
    //Returing the Json to view
    return Json(new { Result = GestureUpdateIsActive });
}
</code>
</pre>
</blockquote>
</strong>
</ol>
<ol>
<strong>
<li>Copy and Paste the code given below in 'HomeController.cs', (i.e) below the comment 'Paste the 'GestureManagement_InsertByIsID' Function code here...'</li>
<li>Follow the screenshot shown below to navigate to the GestureManagement_InsertByIsID Function in the HomeController.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/11.png" alt="image" style="max-width:100%;">&nbsp;
<blockquote>
<pre>
<code>
public JsonResult GestureManagement_InsertByIsID(string name, string url, string message, string active)
{
    // Calling the intermediator file and get the Boolean value 
    var gesman_obj = Facade.Admin_GestureAdd(name, url, message, active);
&nbsp;
    //Returing the Json to view
    return Json(new { Result = gesman_obj });
}
</code>
</pre>
</blockquote>
</strong>
</ol>
<h3>Code snippet that needs to be replaced in HomeController for Gesture Management</h3>
<p>Replace the following code in gesture_management ActionResult of 'HomeController.cs', (i.e) below the comment 'Admin - Gesture Management Page' [Note : Replace that whole actionresult code]</p>
<li>Follow the screenshot shown below to navigate to the gesture_management ActionResult Function in the HomeController.cs file</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/Class/12.png" alt="image" style="max-width:100%;">&nbsp;
<strong>
<blockquote>
  <pre>
    <code>
public ActionResult gesture_management()
{
    // Calling the intermediator file and Returing the List file to the View 
    return View(Facade.Admin_GestureShow());
}
    </code>
  </pre>
</blockquote>
</strong>
<h3>Design Changes</h3>
<p>Lets start un-commenting the code in design screens</p>
<p>Follow the screenshot shown below to navigate to the gesture_management.cshtml file</p>
<ol>
<strong>
<li>Open gesture_management.cshtml</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/uncomment/1.png" style="max-width:100%;">&nbsp;
<li>Select the code from line number 291</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/uncomment/2.png" style="max-width:100%;">&nbsp;
<li>Select the code till line number 336</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/uncomment/3.png" style="max-width:100%;">&nbsp;
<li>Click on the uncomment button</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/uncomment/4.png" style="max-width:100%;">&nbsp;
<h3>Click on the Run button to see the output in the admin portal [Navigate to Admin -> Gesture Management page]</h3>&nbsp;
<li>Admin Gesture Management with entries</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/1.JPG" alt="image" style="max-width: 100%;">&nbsp;
<li>Click the button to add the new Gesture </li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/2.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Modal box for Adding a new Gesture</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/3.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Provide the relavant information to add the new Gesture</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/4.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Added a Gesture</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/5.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Click on the edit button to edit a Gesture</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/6.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>while clicking the edit button the relevant details will be displayed in the modal box</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/7.JPG" alt="image" style="max-width: 100%;">&nbsp;
<li>To Disable the Gesture, click on the button shown below</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/8.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Disabled button</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/9.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>If you want to save the changes, click on the Update button to update the Gesture details</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/10.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Changes made are shown below</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Challenge2/output/11.jpg" alt="image" style="max-width: 100%;">&nbsp;
</strong>
<h3>Congratulations! You have successfully completed Challenge 2</h3>
<h3>The next session is<a href="https://github.com/VectorSense/AI-Ninja-Tech-Series/blob/master/Get%20Project%20Ready/Challenge3.md"> Challenge 3</a></h3>