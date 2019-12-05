<h1>SPEECH</h1>
<h3>Creating Speech Subscription Key</h3>
<ol>
	<strong>
		<li>Sign-in to Azure Portal by typing "portal.azure.com" in browser, enter your username</li>&nbsp;
	    <img src="http://139.59.61.161/MSWorkshop2019/face_computer_portal/faceAPI_create/portal_1.jpg" alt="image" style="max-width: 100%;">&nbsp;
	    <li>Enter your Password</li>&nbsp;
	    <img src="http://139.59.61.161/MSWorkshop2019/face_computer_portal/faceAPI_create/portal_2.jpg" alt="image" style="max-width: 100%;">&nbsp;
	    <li>Click on create a resource</li>&nbsp;
	    <img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/speech1.jpg" alt="image" style="max-width: 100%;">&nbsp;
	    <li>In the search box type 'speech'</li>&nbsp;
	    <img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/speech2.jpg" alt="image" style="max-width: 100%;">&nbsp;
	    <li>Click on Create button</li>&nbsp;
	    <img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/speech3.jpg" alt="image" style="max-width: 100%;">&nbsp;
	    <li>Enter name and select location, pricing tier and resource group</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/speech4.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Click on Keys tab</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/speech5.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Copy the Keys</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/speech6.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Navigate to Overview tab and copy the endpoint</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/speech7.jpg" alt="image" style="max-width: 100%;">&nbsp;
	</strong>
</ol>
<h3>Custom Speech</h3>
<ol>
	<strong>
		<li>Open CustomSpeech project and navigate to SpeechRecognitionSamples.cs</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/customspeech1.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Enter your subscription key, region [instead of 'westus' give your region which was specified in your endpoint] and endpoint id [your trained model's custom speech id] </li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/customspeech2.jpg" alt="image" style="max-width: 100%;">&nbsp;
	</strong>
</ol>
<h3>Language Translation</h3>
<ol>
	<strong>
		<li>Open LanguageTranslation project and navigate to TranslationSamples.cs</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/language1.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Enter your subscription key, region [instead of 'westus' give your region which was specified in your endpoint] </li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/language2.jpg" alt="image" style="max-width: 100%;">&nbsp;
	</strong>
</ol>
<h3>Speech To Text</h3>
<ol>
	<strong>
		<li>Open SpeechToText project and navigate to SpeechRecognitionSamples.cs</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/speechtotext1.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Enter your subscription key, region [instead of 'westus' give your region which was specified in your endpoint] </li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/speechtotext2.jpg" alt="image" style="max-width: 100%;">&nbsp;
	</strong>
</ol>
<h3>Text To Speech</h3>
<ol>
	<strong>
		<li>Open TextToSpeech project and navigate to SpeechSynthesisSamples.cs</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/texttospeech1.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Enter your subscription key, region [instead of 'westus' give your region which was specified in your endpoint] </li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/texttospeech2.jpg" alt="image" style="max-width: 100%;">&nbsp;
	</strong>
</ol>
<h3>Text Based Assistance</h3>
<ol>
	<strong>
		<li>Open TextBasedAssistance project and navigate to IntentRecognitionSamples.cs</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/textbased1.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Enter your subscription key, region [instead of 'westus' give your region which was specified in your endpoint] </li>&nbsp;
		<li>Enter your trained LUIS model application id</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/textbased2.jpg" alt="image" style="max-width: 100%;">&nbsp;
	</strong>
</ol>
<h3>Voice Based Assistance</h3>
<ol>
	<strong>
		<li>Open VoiceBasedAssistance project and navigate to IntentRecognitionSamples.cs</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/voicebased1.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Enter your subscription key, region [instead of 'westus' give your region which was specified in your endpoint] </li>&nbsp;
		<li>Enter your LUIS subscription key </li>&nbsp;
		<li>Enter your LUIS model application id</li>&nbsp;
		<li>NOTE : Both the subscription key and LUIS subscription key can be of same, but LUIS model id should be trained using LUIS subscription key only</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speech/voicebased2.jpg" alt="image" style="max-width: 100%;">&nbsp;
	</strong>
</ol>
<h1>SPEAKER RECOGNITION</h1>
<h3>Creating Speaker Recognition Subscription Key</h3>
<ol>
	<strong>
		<li>Sign-in to Azure Portal by typing "portal.azure.com" in browser, enter your username</li>&nbsp;
	    <img src="http://139.59.61.161/MSWorkshop2019/face_computer_portal/faceAPI_create/portal_1.jpg" alt="image" style="max-width: 100%;">&nbsp;
	    <li>Enter your Password</li>&nbsp;
	    <img src="http://139.59.61.161/MSWorkshop2019/face_computer_portal/faceAPI_create/portal_2.jpg" alt="image" style="max-width: 100%;">&nbsp;
	    <li>Click on create a resource</li>&nbsp;
	    <img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speaker/speaker1.jpg" alt="image" style="max-width: 100%;">&nbsp;
	    <li>In the search box type 'speaker recognition'</li>&nbsp;
	    <img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speaker/speaker2.jpg" alt="image" style="max-width: 100%;">&nbsp;
	    <li>Click on Create button</li>&nbsp;
	    <img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speaker/speaker3.jpg" alt="image" style="max-width: 100%;">&nbsp;
	    <li>Enter name and select location, pricing tier and resource group, click create button</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speaker/speaker4.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Click on Keys tab</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speaker/speaker5.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Copy the Keys</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speaker/speaker6.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Navigate to Overview tab and copy the endpoint</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speaker/speaker7.jpg" alt="image" style="max-width: 100%;">&nbsp;
	</strong>
</ol>
<h3>Speaker Identification</h3>
<ol>
	<strong>
		<li>Open SpeakerIdentification project and navigate to Web.config</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speaker/identification1.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Enter your EndPoint, VoiceKey and connectionstring [NOTE : Paste the endpoint only till '.com', example : 'https://westus.api.cognitive.microsoft.com']</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speaker/identification2.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>For getting connectionstring follow the below procedure</li>
		<h3>Azure SQL Server Connectivity through Azure Portal</h3>
		<li>Sign-in to Azure Portal by typing "portal.azure.com" in browser, enter your username</li>
		<img src="http://139.59.61.161/MSWorkshop2019/sql/sql0.JPG" alt="image" style="max-width: 100%;">
		<li>Enter your Password [W!pro!Azure1]</li>
		<img src="http://139.59.61.161/MSWorkshop2019/sql/sql0.1.JPG" alt="image" style="max-width: 100%;">
		<li>Click on SQL databases tab in the left pane</li>
		<img src="http://139.59.61.161/MSWorkshop2019/sql/sql1.jpg" alt="image" style="max-width: 100%;">
		<li>Select your particular database</li>
		<img src="http://139.59.61.161/MSWorkshop2019/sql/sql2.jpg" alt="image" style="max-width: 100%;">
		<li>Click on Query editor</li>
		<img src="http://139.59.61.161/MSWorkshop2019/sql/sql3.jpg" alt="image" style="max-width: 100%;">
		<li>Specify your database password</li>
		<img src="http://139.59.61.161/MSWorkshop2019/sql/sql5.jpg" alt="image" style="max-width: 100%;">
		<li>Copy the below query</li>
		<blockquote>
			<pre>
				<code>
CREATE TABLE voice(id int NOT NULL IDENTITY PRIMARY KEY, voiceid varchar(100), name varchar(150));
				</code>
			</pre>
		</blockquote>
		<li>Paste the query in the editor pane and click on Run button</li>
		<img src="http://139.59.61.161/MSWorkshop2019/sql/sql6.jpg" alt="image" style="max-width: 100%;">
		<li>Now click on Connection strings tab in the left pane</li>
		<img src="http://139.59.61.161/MSWorkshop2019/sql/sql7.jpg" alt="image" style="max-width: 100%;">
		<li>Copy your Connection string</li>
		<img src="http://139.59.61.161/MSWorkshop2019/sql/sql8.jpg" alt="image" style="max-width: 100%;">
		<h4>Paste your SQL server connection string in Web.config (i.e) for the key 'connectionstring', in the connection string specify your database user name and password, make 'MultipleActiveResultSets' as True</h4>
		<li>Make sure your changing the database name to your database name [(i.e) Initial Catalog= your_db_name]</li>
		<li>Sample Connectionstring : "Server=tcp:jsn.database.windows.net,1433;Initial Catalog=dbname;Persist Security Info=False;User ID=demouser; Password=demo@pass123;MultipleActiveResultSets=True;Encrypt=True; TrustServerCertificate=False;Connection Timeout=30;"</li>
	</strong>
</ol>
<h3>Speaker Verification</h3>
<ol>
	<strong>
		<li>Open SpeakerIdentification project and navigate to Web.config</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speaker/verification1.jpg" alt="image" style="max-width: 100%;">&nbsp;
		<li>Enter your EndPoint, VoiceKey [NOTE : Paste the endpoint only till '.com', example : 'https://westus.api.cognitive.microsoft.com']</li>&nbsp;
		<img src="http://139.59.61.161/MSWorkshop2019/SpeechDemo/speaker/verification2.jpg" alt="image" style="max-width: 100%;">&nbsp;
	</strong>
</ol>
