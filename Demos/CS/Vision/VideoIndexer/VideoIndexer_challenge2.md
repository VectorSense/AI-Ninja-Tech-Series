<h1>AI Series HOL</h1>
<h2>Challenge 2 – Creating new Video Indexer</h2>
<p>In Challenge 2 we are going to explore about how the Video Indexer works using Azure API.</p>
<h2>Getting Started</h2>
<p>Just Clone the Video Indexer Starter Kit from the <a href="https://github.com/jumpstartninjatech/AI-TechSeries/tree/master/Demos/CS/Vision/VideoIndexer/VideoIndexer_StarterKit">Git Repository</a> Kinldy ignore, if you done this step in previous</p>
<h3>Prerequisites</h3>
<li>Kindly ensure that your Visual Studio working fine</li>
<li>Have minimum two photos for all the person in your video file, it will be usefull when the training is heppen</li>
<li>Open the AI Series Starter Kit application in the folder</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/32.jpg" alt="image" style="max-width:100%;">
<li>In the solution explorer [View -> Solution Explorer]</li>&nbsp; 
<img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/2.PNG" alt="image" style="max-width:100%;">
<li>Right click on the solution name and select Build</li>&nbsp;
<img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/3.PNG" alt="image" style="max-width:100%;">
<li>Make sure that no error is thrown after building your application</li>
<li>Now click on the Run button to run and see the application's output in the browser</li>&nbsp;
<img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/4.PNG" alt="image" style="max-width:100%;">
<p>Following are the <b>Output Screens</b> of the application. Initially all the screens having the data fields will be empty.</p>
<li>Index page, just click 'See it in Action' to navigate the videoinsights.cshtml page</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/29.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Video Insights page, just click any one video in the Gallery</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/single1.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Empty result page</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/error.JPG" alt="image" style="max-width:100%;">&nbsp;
<h3>Azure API Connectivity through Azure Portal</h3>
<p>Now we are going to create new Video Indexer in Azure portal</p>
<p>You're ready to get started with Video Indexer. For more information to connect with Azure, see the following articles:</p>
<li>The picture shown below used to navigate the Azure Video Indexer page <a href="https://vi.microsoft.com/en-us/">Click here</a>  to Sign-in to the Azure portal</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/1.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Select the account type which is comportable to you</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/2.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Enter your email id</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/3.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Enter your password</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/4.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Click the <b>Content Model Customization</b> icon to start the process</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/5.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Click the <b>People Model Menu</b> to add the person</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/6.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Click <b>Add person</b> to add a person</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/7.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>You can avail the sample video and images from <a href = "https://github.com/jumpstartninjatech/AI-TechSeries/tree/master/Demos/CS/Vision/VideoIndexer/Resources">here</a></li>
<li>By clicking <b>choose files</b> you can able to select the images in your folder</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/8.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Select the relevant photo then click open</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/9.jpg" alt="image" style="max-width:100%;">&nbsp;
<p>Kindly upload minimum two photos for one person</p>
<li>Rename the person name, just click <b>Rename</b> to modify the person name</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/10.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>After provided the name just click tick mark to confirm the name</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/11.jpg" alt="image" style="max-width:100%;">&nbsp;
<p>Would you like to add more images for a same person just click <b>Add images</b></p>
<li>Click <b>Close</b> button</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/12.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>After completing all the changes you can able to see the person photos with the name</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/13.jpg" alt="image" style="max-width:100%;">&nbsp;
<p>Repeat the same process for adding more persons</p>
<li>Just click upload button to upload a video</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/14.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Drop your video file by clicking <by>browse for a file</b> or enter the URL by clicking <b>file URL</b></li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/15.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Select the video file then click <b>Open</b></li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/16.jpg" alt="image" style="max-width:100%;">&nbsp;
<p>After selecting your video file just make sure that video file details are correct, would you like to change video name just change the video name in the relavant box and also select the language which is comportable to you in <b>video source language</b></p>&nbsp;
<li>Change privacy mode to <b>public</b> by clicking <b>Privacy</b></li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/17.jpg" alt="image" style="max-width:100%;">&nbsp;
<img src="http://139.59.61.161/VideoIndexer/18.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>click <b>upload</b> button to upload your video file</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/19.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>You can able to see the video loading</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/20.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>After uploading Video, it will make the <b>Video Indexing</b></li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/21.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Completed the Video Indexing</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/22.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Just click <b>Embed</b> to get the Iframe source</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/23.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Select widgets as <b>insights</b> then copy the src in <copy embed code></li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/24.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Paste the src in Widget.config file , (i.e) below the comment 'Player 1 details...' in the value variable</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/web1.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>After that, select the widgets as <b>player</b> then copy the src in <copy embed code></li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/26.jpg" alt="image" style="max-width:100%;">&nbsp;
<img src="http://139.59.61.161/VideoIndexer/27.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Paste the src in Widget.config file , (i.e) below the comment 'Player 1 details...' in the value variable</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/web0.jpg" alt="image" style="max-width:100%;">&nbsp;
<h3>Script Changes</h3>
<li>Navigate the videoinsights.cshtml file</li>
<img src="http://139.59.61.161/VideoIndexer/videoinsights.jpg" alt="image" style="max-width:100%;">&nbsp;
<ol>
  <strong>
    <li>Paste the code given below in 'videoinsights.cs', (i.e) below the comment 'Paste the Script code here...'</li>&nbsp;
    <img src="http://139.59.61.161/VideoIndexer/script.JPG" alt="image" style="max-width:100%;">&nbsp;
    <blockquote>
        <pre>
           <code>
&lt;script&gt;
(function () {
var baseInsightsUrl = "https://www.videoindexer.ai/embed/insights/";
var basePlayerInsightsUrl = "https://www.videoindexer.ai/embed/player/";
var videoIdInput = $("#videoId");
var accountIdInput = $("#accountId");
var insightsInputs = $(".insights");
var titleEl = $('input[name="title"]');
var generatedInsightsUrlEl = $("#generatedInsightsUrl");
var generatedPlayerInsightsUrlEl = $("#generatedPlayerUrl");
var insightsWidgetUrlParams = {};
var playerInsightsWidgetUrlParams = { autoplay: false };
var widgetsToRender = [];
var allWidgets = $('input[name="all"]');
var captionsCheckbox = $("#captions");
var autoplayCheckbox = $("#autoplay");
var langSelect = $("#lang");
&nbsp;
// Register listeners
titleEl.on("keyup", modifyInsightsUrl);
insightsInputs.on("change", modifyInsightsUrl);
allWidgets.on("change", toggleUncheckAll);
captionsCheckbox.on("change", modifyPlayerUrl);
autoplayCheckbox.on("change", modifyPlayerUrl);
langSelect.on("change", modifyPlayerUrl);
videoIdInput.on("keyup", function () {
    var id = videoIdInput.val();
    var accountId = accountIdInput.val();
    generateUrlFromParams(insightsWidgetUrlParams, baseInsightsUrl, generatedInsightsUrlEl, accountId, id);
    generateUrlFromParams(playerInsightsWidgetUrlParams, basePlayerInsightsUrl, generatedPlayerInsightsUrlEl, accountId, id);
});
&nbsp;
accountIdInput.on("keyup", function () {
    var id = videoIdInput.val();
    var accountId = accountIdInput.val();
    generateUrlFromParams(insightsWidgetUrlParams, baseInsightsUrl, generatedInsightsUrlEl, accountId, id);
    generateUrlFromParams(playerInsightsWidgetUrlParams, basePlayerInsightsUrl, generatedPlayerInsightsUrlEl, accountId, id);
});
// Init
toggleUncheckAll();
&nbsp;
Utils Functions
function modifyPlayerUrl() {
    var $this = $(this);
    var value = $this.val();
&nbsp;
    if ($this.attr("type") === "checkbox") {
        if ($this.is(":checked")) {
            playerInsightsWidgetUrlParams[$this.val()] = true;
        } else {
            playerInsightsWidgetUrlParams[$this.val()] = false;
        }
    }
    if ($this.prop("id") === "lang") {
        if (value !== "English") {
            playerInsightsWidgetUrlParams.captions = value;
        } else {
            delete playerInsightsWidgetUrlParams.captions;
        }
    }
    console.log(playerInsightsWidgetUrlParams);
    generateUrlFromParams(playerInsightsWidgetUrlParams, basePlayerInsightsUrl, generatedPlayerInsightsUrlEl, accountIdInput.val(), videoIdInput.val());
}
&nbsp;
function modifyInsightsUrl() {
    var $this = $(this);
    var value = $this.val();
    var title = titleEl.val();
    insightsWidgetUrlParams = {};
&nbsp;
    if (title) {
        insightsWidgetUrlParams["title"] = title;
    }
&nbsp;
    if ($this.attr("type") === "checkbox") {
        if ($this.is(":checked")) {
&nbsp;
            widgetsToRender.push(value);
        } else {
            remove(widgetsToRender, value);
        }
    }
&nbsp;
    if (widgetsToRender.length) {
        insightsWidgetUrlParams.widgets = widgetsToRender.join(",");
    }
&nbsp;
    generateUrlFromParams(insightsWidgetUrlParams, baseInsightsUrl, generatedInsightsUrlEl, accountIdInput.val(), videoIdInput.val());
    console.log(insightsWidgetUrlParams);
}
&nbsp;
function toggleUncheckAll() {
&nbsp;
    insightsInputs.each(function () {
        if (allWidgets.is(':checked')) {
            delete insightsWidgetUrlParams.widgets;
            widgetsToRender.length = 0;
            if ($(this).attr("type") === "checkbox") {
                $(this).prop({ "checked": false, "disabled": true });
            }
        } else {
            if ($(this).attr("type") === "checkbox") {
&nbsp;
                $(this).prop({ "checked": false, "disabled": false });
            }
        }
    });
&nbsp;
    generateUrlFromParams(insightsWidgetUrlParams, baseInsightsUrl, generatedInsightsUrlEl, accountIdInput.val(), videoIdInput.val());
}
&nbsp;
function remove(array, element) {
    const index = array.indexOf(element);
&nbsp;
    if (index !== -1) {
        array.splice(index, 1);
    }
}
&nbsp;
function generateUrlFromParams(obj, baseUrl, el, accountId, id) {
    var objKeys = Object.keys(obj);
    var urlParams = objKeys.length ? "?" : "";
    for (key in obj) {
        if (obj.hasOwnProperty(key)) {
            urlParams += key + "=" + obj[key];
            if (key !== objKeys[objKeys.length - 1]) {
                urlParams += "&";
            }
        }
    }
    el.val(baseUrl + accountId + "/" + id + "/" + urlParams);
}
&nbsp;
}())
&lt;/script&gt;
            </code>
        </pre>
   </blockquote>
 </strong>
</ol>
<li>Now click on the Run button to run and see the application's output in the browser</li>&nbsp;
<img src="http://139.59.61.161/MSWorkshop2019/Invoke_StarterKit/4.PNG" alt="image" style="max-width:100%;">
<h3>Sample Outputs</h3>
<li>Index page, just click 'See it in Action' to navigate the videoinsights.cshtml page</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/29.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>Video Insights page, just click any one video in the Gallery</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/single1.jpg" alt="image" style="max-width:100%;">&nbsp;
<li>The final result page, here you can do all the activities in Video Indexer</li>&nbsp;
<img src="http://139.59.61.161/VideoIndexer/single2.jpg" alt="image" style="max-width:100%;">&nbsp;
<h3>Congratulations! You have successfully completed Video Indexer</h3>
<h3>Next steps</h3>
<li><a href="https://github.com/jumpstartninjatech/AI-TechSeries/blob/master/Demos/CS/Vision/VideoIndexer/VideoIndexer_intro.md">Back to ReadMe</a></li>
