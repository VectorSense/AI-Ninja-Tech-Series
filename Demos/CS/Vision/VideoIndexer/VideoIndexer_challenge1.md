<h1>AI Series HOL</h1>
<h2>Challenge 1 – Existing Video Indexer</h2>
<p>In Challenge 1 we are going to explore about how the Video Indexer works using Azure API.</p>
<h2>Getting Started</h2>
<h3>Prerequisites</h3>
<li>Kindly ensure that the Visual Studio works fine</li>
<h3>Code Summary</h3>
<p>This application has two cshtml files. The two files are playing the major role in this application.The index.cshtml file is used to navigate to the videoinsights.cshtml file.</p>
<p>The videoinsights.cshtml file is having all the Azure functionalities.</p>
<h3>Invoking the Video Indexer</h3>
<p>Just Clone the Video Idexer HOL Completed Kit from the <a href="https://github.com/jumpstartninjatech/AI-TechSeries/tree/master/Demos/CS/Vision/VideoIndexer/VideoIndexer_CompletedKit">Git Repository.</a></p>
<h3>Things that you need to do in Completed Kit</h3>
<li>Open the Video Idexer Completed Kit application in the folder</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/32.jpg" alt="image" style="max-width:100%;">
<li>Follow the picture given below to navigate to the Web.config file</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/Webconfig.png" alt="image" style="max-width:100%;">&nbsp; 
<ol>
  <strong>
  	<li>Paste the code given below in 'Web.config', (i.e) below the comment 'Player 1 details...'</li>&nbsp; 
    <img src="http://139.59.61.161/VideoIndexer/palyer1.png" alt="image" style="max-width:100%;">&nbsp; 
  	<blockquote>
        <pre>
           <code>
&lt;add key ="VideoPlayer1" value="https://www.videoindexer.ai/embed/player/de293372-7e31-4e9d-a7cd-4b3ea2de924b/7dcff184e1/"/&gt;
&lt;add key ="VideoInsights1" value="https://www.videoindexer.ai/embed/insights/de293372-7e31-4e9d-a7cd-4b3ea2de924b/7dcff184e1/"/&gt;
            </code>
        </pre>
   </blockquote>
   	<li>Paste the code given below in 'Web.config', (i.e) below the comment 'Player 2 details...'</li>&nbsp; 
    <img src="http://139.59.61.161/VideoIndexer/palyer2.png" alt="image" style="max-width:100%;">&nbsp; 
  	<blockquote>
        <pre>
           <code>
&lt;add key ="VideoPlayer2" value="https://www.videoindexer.ai/embed/player/de293372-7e31-4e9d-a7cd-4b3ea2de924b/67e196b1ac/"/&gt;
&lt;add key ="VideoInsights2" value="https://www.videoindexer.ai/embed/insights/de293372-7e31-4e9d-a7cd-4b3ea2de924b/67e196b1ac/"/&gt;
            </code>
        </pre>
   </blockquote>
  </strong>
</ol>   
<h2>Outputs</h2>
<li>In the solution explorer [View -> Solution Explorer]</li>&nbsp; 
<img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/2.PNG" alt="image" style="max-width:100%;">&nbsp; 
<li>Right click on the solution name and select Build</li>&nbsp;
<img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/3.PNG" alt="image" style="max-width:100%;">&nbsp; 
<li>Make sure that no error is thrown after building your application</li>
<li>Now click on the <b>Run</b> button to run and see the application's output in the browser</li>&nbsp;
<img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/4.PNG" alt="image" style="max-width:100%;">
<h3>Following are the output screens of the application.</h3>
<li>Index page, just click <b>See it in Action</b> to navigate the videoinsights.cshtml page</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/29.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Video Insights page, just click any one video in the Gallery</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/30.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>The final result page, here you can do all the activities in Video Indexer</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/31.jpg" alt="image" style="max-width:100%;">&nbsp;
<h3>Congratulations! You have successfully completed Challenge 1</h3>
<h3>Next steps</h3>
<li><a href="https://github.com/jumpstartninjatech/AI-TechSeries/blob/master/Demos/CS/Vision/VideoIndexer/VideoIndexer_challenge2.md">Challenge 2: Creating new Video Indexer</a> or <a href="">Back to ReadMe</a></li>