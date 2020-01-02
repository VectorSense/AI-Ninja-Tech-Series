<h1>AI Series HOL</h1>
<h2>Challenge 6 – Integration of NLP based Chat bot for Task Management</h2>
<p>In Challenge 6 we are going to explore about how to Track status and Report status using Bot application</p>
<h2>Getting Started</h2>
<h3>Prerequisites</h3>
<ol>
	<li>Open your ChatBot application [Which will be in the folder PartnerTechSeries-AI -> HOLs -> ChatBot -> ProjectManagementBot]</li>
</ol>
<h3>Getting the LUIS key</h3>
<ol>
<strong>
<li>Login to Luis.ai</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/LuisImages/1.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Enter your username</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/LuisImages/2.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Enter your password</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/LuisImages/3.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Click on Manage</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/LuisImages/8.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Copy the Application ID</li>&nbsp;
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/LuisImages/9_hackathon.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Copy only the Key</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/LUIS1.PNG" alt="image" style="max-width: 100%;"></ol></strong>
<h3>Web Config Changes</h3>
<ol>
<strong>
<li>Navigate to Web.config</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Config1.PNG" alt="image" style="max-width: 100%;">&nbsp;
<li>Paste the LUIS App ID in 'LuisModelId' and Key in 'LuisSubscriptionKey' in Web.Config </li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Config2.png"alt="image" style="max-width: 100%;">&nbsp;
<li>Fill the 'DataSource', 'UserID', 'Password' and 'InitialCatalog' [Grab all these from connectionstring, to get the connectionstring follow the below procedure]</li>
</ol></strong>
<h3>Azure SQL Server Connectivity through Azure Portal</h3>
<ol><strong>
<li>Sign-in to Azure Portal by typing "portal.azure.com" in browser, enter your username</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/sql/sql0c.JPG" alt="image" style="max-width: 100%;">&nbsp;
<li>Enter your Password</li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/sql/sql0.1c.JPG" alt="image" style="max-width: 100%;">&nbsp;
<li>Click on SQL databases tab in the left pane</li>&nbsp;
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql1.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Select your particular database</li>&nbsp;
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql2.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Click on Query editor</li>&nbsp;
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql3.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Specify your database password</li>&nbsp;
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql5.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Copy all the queries from <a href="https://github.com/jumpstartninjatech/PartnerTechSeries-AI/blob/master/HOLs/BotDB_Script.sql">sql script file</a></li>&nbsp;
<img src="http://139.59.61.161/PartnerTechSeries2019/Bot.PNG" alt="image" style="max-width: 100%;">&nbsp;
<li>Paste all the queries in the editor pane and click on Run button</li>&nbsp;
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql6_hackathon.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Now click on Connection strings tab in the left pane</li>&nbsp;
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql7_hackathon.jpg" alt="image" style="max-width: 100%;">&nbsp;
<li>Copy your Connection string</li>&nbsp;
<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/sql/sql8_hackathon.jpg" alt="image" style="max-width: 100%;">&nbsp;
<h4>Paste your SQL server connection string details in Web.config (i.e) for 'DataSource' give 'jsn.database.windows.net', for 'UserID' give 'demouser', for 'Password' give 'demo@pass123' and for 'InitialCatalog' give your database name</h4>&nbsp;
<h4>Before running the bot make sure your 'pdi_engineer_details' table has an entry, (i.e) in the azure editor pane run the following query by specifying the user name</h4>
<blockquote>
	<pre>
		<code>
			INSERT INTO pdi_engineer_details(name) VALUES('Your Name');
		</code>
	</pre>
</blockquote>
<h4>NOTE : In the query, inside the '' give your name</h4>
</ol></strong>
<h3>Publishing the bot application to Azure</h3>
<ol>
	<strong>
		<li>Right click your project name and click on 'Publish'</li>&nbsp;
		<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Publish1.png" alt="image" style="max-width: 100%;">&nbsp;
		<li>Click on Start, click on App Service, click on Create New and click on Publish</li>&nbsp;
		<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Publish2.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Fill all the fields and click on Create button, the application will be published and site URL will be generated copy that for later use</li>&nbsp;
		<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Portal14.PNG" alt="image" style="max-width: 100%;">&nbsp;
	</strong>
</ol>
<h3>Getting the Microsoft App ID and Key from Azure</h3>
<ol>
	<strong>
		<li>Sign-in to Azure Portal by typing "portal.azure.com" in browser, enter your username</li>
		<img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_1.jpg" alt="image" style="max-width: 100%;">
		<li>Enter your Password</li>
		<img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_2.jpg" alt="image" style="max-width: 100%;">
		<li>Click on create a resource</li>
		<img src="http://139.59.61.161/PartnerTechSeries2019/face_computer_portal/faceAPI_create/portal_3.jpg" alt="image" style="max-width: 100%;">
		<li>In the search box type 'web app bot'</li>&nbsp;
		<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/BOT/Portal1_hackathon.png" alt="image" style="max-width: 100%;">&nbsp;
		<li>Click on Create button</li>&nbsp;
		<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/BOT/Portal2_hackathon.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Fill out all the details</li>&nbsp;
		<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/BOT/Portal3_hackathon.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Scroll down and click on 'Microsoft App ID and password'</li>&nbsp;
		<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/BOT/Portal4_hackathon.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Click on 'Create New'</li>&nbsp;
		<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/BOT/Portal5_hackathon.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Click on 'Create App ID in the App Registration Portal'</li>&nbsp;
		<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/BOT/Portal6_hackathon.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Sign-in using credentials</li>&nbsp;
		<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/BOT/Portal7.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Select your Account</li>&nbsp;
		<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/BOT/Portal8.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Click on 'Generate an app password to continue'</li>&nbsp;
		<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Portal9.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Copy your Password</li>&nbsp;
		<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Portal10.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Copy your App ID</li>&nbsp;
		<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Portal11.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Paste your app id and password</li>&nbsp;
		<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/BOT/Portal12_hackathon.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Click on Create button</li>&nbsp;
		<img src="http://139.59.61.161/Hackathon/MSWorkshop2019/BOT/Portal13_hackathon.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Open your Web app bot and click on Settings, in the Configuration paste your site URL which you got while publish. [NOTE : Make sure you are adding '/api/messages' in the end of your URL] and click on Save</li>&nbsp;
		<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Portal15.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Click on Channels, click on Edit</li>&nbsp;
		<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Portal16.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Copy the Secret Key</li>&nbsp;
		<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Portal17.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Paste the key in 'YOUR_SECRET_HERE'</li>&nbsp;
		<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Portal18.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<li>Grab the whole Web chat iframe code</li>&nbsp;
		<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Portal19.PNG" alt="image" style="max-width: 100%;">&nbsp;
		<h4>Paste the Microsoft App ID in 'MicrosoftAppId' and Password in 'MicrosoftAppPassword' in Web.config [NOTE : These two fields are not mandatory for running the bot locally but it is mandatory after publishing]</h4>
	</strong>
</ol>
<h3>Design Changes</h3>
<ol>
	<strong>
	<li>Navigate to index.cshtml</li>&nbsp;
	<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Bot4.PNG" alt="image" style="max-width: 100%;">&nbsp;
	<li>Navigate to the code line specified in the below image</li>&nbsp;
	<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Bot3.PNG" alt="image" style="max-width: 100%;">&nbsp;
	<li>Paste the below code below the marked area</li>
		<blockquote>
			<pre>
				<code>
&lt;link rel="stylesheet" type="text/css" href="http://139.59.61.161/PartnerTechSeries2019/chatbot.css"/&gt;
				</code>
			</pre>
		</blockquote>
	<li>Scroll to the code line specified in the below image</li>&nbsp;
	<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Bot51.PNG" alt="image" style="max-width: 100%;">&nbsp;
	<li>Paste the below code below the marked area</li>
		<blockquote>
			<pre>
				<code>
&lt;button class="open-button" id="open_button" onclick="openForm()"&gt;&lt;i class="fa fa-comments" style="font-size: 30px;color:white;"&gt;&lt;/i&gt;&lt;/button&gt;
&nbsp;
&lt;div class="chat-popup" id="myForm" style="z-index:999999;"&gt;
&nbsp;
&lt;form class="form-container"&gt;
&lt;h6 style="color:black;float:left"&gt;Task Management&lt;/h6&gt;
&lt;button type="button" class="btn cancel" onclick="closeForm()" style="background:transparent;padding:0px;margin:0px;float:right;width:10%;"&gt;&lt;i class="fa fa-window-close" style="font-size: 20px;color:black;margin-top:5px;"&gt;&lt;/i&gt;&lt;/button&gt;
&lt;iframe style="width:100%;height:450px;" src="https://webchat.botframework.com/embed/ProjectManagementBot?s=SlFM-pB6rpA.SGjlizPkjluSHUAo1dRDq3yGm6GzZUpBVZ94gRd4yrI" frameborder="0" scrolling="auto" marginheight="0" marginwidth="0"&gt;&lt;/iframe&gt;
&nbsp;
&lt;/form&gt;
&lt;/div&gt;
				</code>
			</pre>
		</blockquote>
    <li>NOTE : Replace the iframe code which you got from Portal</li>
	<li>Scroll to the code line specified in the below image</li>&nbsp;
	<img src="http://139.59.61.161/PartnerTechSeries2019/BOT/Bot6.PNG" alt="image" style="max-width: 100%;">&nbsp;
	<li>Paste the below code below the marked area</li>
		<blockquote>
			<pre>
				<code>
&lt;script&gt;
	function openForm()
	{
	document.getElementById("myForm").style.visibility = "visible";
	}
&nbsp;
	function closeForm()
	{
	document.getElementById("myForm").style.visibility = "hidden";
	}
&lt;/script&gt;
				</code>
			</pre>
		</blockquote>
	</strong>
</ol>
<h4>Now you can run your application and see the output</h4>