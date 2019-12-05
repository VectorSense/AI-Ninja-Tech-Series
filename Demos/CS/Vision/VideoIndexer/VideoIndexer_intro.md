<h1>AI Series HOL</h1>
<h1>Video Indexer - Introduction</h1>
<p>Azure Video Indexer is a cloud application built on Azure Media Analytics, Azure Search, Cognitive Services (such as the Face API, Microsoft Translator, the Computer Vision API, and Custom Speech Service). It enables you to extract the insights from your videos using Video Indexer video and audio models.</p>
<h2>Getting Started</h2>
<h3>Video insights</h3>
<li><b>Face detection:</b> Detects and groups faces appearing in the video.</li>
<li><b>Celebrity identification:</b> Video Indexer automatically identifies over 1 million celebrities – such as world leaders, actors, actresses, athletes, researchers, business, and tech leaders across the globe. The data about these celebrities can also be found on various famous websites, for example, IMDB and Wikipedia.</li>
<li><b>Account-based face identification:</b> Video Indexer trains a model for a specific account. It then recognizes faces in the video based on the trained model.</li>
<li><b>Visual text recognition (OCR):</b> Extracts text that is visually displayed in the video.</li>
<li><b>Visual content moderation:</b> Detects adult and/or racy visuals.</li>
<li><b>Labels identification:</b> Identifies visual objects and actions displayed.</li>
<li><b>Scene segmentation:</b> determines when a scene changes in video based on visual cues. A scene depicts a single event and it is composed by a series of consecutive shots, which are semantically related.</li>
<li><b>Shot detection:</b> determines when a shot changes in video based on visual cues. A shot is a series of frames taken from the same motion-picture camera. </li>
<li><b>Black frame detection:</b> Identifies black frames presented in the video.</li>
<li><b>Keyframe extraction:</b> Detects stable keyframes in a video.</li>
<li><b>Rolling credits:</b> identify the beginning and end of the rolling credits in the end of TV shows and movies.</li>
<h3>Audio insights</h3>
<li><b>Automatic language detection:</b> Automatically identifies the dominant spoken language. Supported languages include English, Spanish, French, German, Italian, Chinese (Simplified), Japanese, Russian, and Brazilian Portuguese Will fallback to English when the language can't be detected.</li>
<li><b>Audio transcription:</b> Converts speech to text in 12 languages and allows extensions. Supported languages include English, Spanish, French, German, Italian, Chinese (Simplified), Japanese, Arabic, Russian, Brazilian Portuguese, Hindi, and Korean.</li>
<li><b>Closed captioning:</b> Creates closed captioning in three formats: VTT, TTML, SRT.</li>
<li><b>Two channel processing:</b> Auto detects, separate transcript and merges to single timeline.</li>
<li><b>Noise reduction:</b> Clears up telephony audio or noisy recordings (based on Skype filters).</li>
<li><b>Transcript customization (CRIS):</b> Trains custom speech to text models to create industry-specific transcripts. </li>
<li><b>Speaker enumeration:</b> Maps and understands which speaker spoke which words and when.</li>
<li><b>Speaker statistics:</b> Provides statistics for speakers speech ratios.</li>
<li><b>Textual content moderation:</b> Detects explicit text in the audio transcript.</li>
<li><b>Audio effects:</b> Identifies audio effects such as hand claps, speech, and silence.</li>
<li><b>Emotion detection:</b> Identifies emotions based on speech (what is being said) and voice tonality (how it is being said). The emotion could be: joy, sadness, anger, or fear.</li>
<li><b>Translation:</b> Creates translations of the audio transcript to 54 different languages.</li>
<h3>Audio and video insights (multi channels)</h3>
<p>When indexing by one channel partial result for those models will be available</p>
<li><b>Keywords extraction:</b> Extracts keywords from speech and visual text.</li>
<li><b>Brands extraction:</b> Extracts brands from speech and visual text.</li>
<li><b>Topic inference:</b> Makes inference of main topics from transcripts. The 1st-level IPTC taxonomy is included.</li>
<li><b>Artifacts:</b> Extracts rich set of "next level of details" artifacts for each of the models.</li>
<li><b>Sentiment analysis:</b> Identifies positive, negative, and neutral sentiments from speech and visual text.</li>
<h3>Scenarios</h3>
<p>Below are a few scenarios where Video Indexer can be useful</p>
<li>Search – Insights extracted from the video can be used to enhance the search experience across a video library. For example, indexing spoken words and faces can enable the search experience of finding moments in a video where a particular person spoke certain words or when two people were seen together. Search based on such insights from videos is applicable to news agencies, educational institutes, broadcasters, entertainment content owners, enterprise LOB apps and in general to any industry that has a video library that users need to search against.</li>
<li>Content creation – insights extracted from videos and help effectively create content such as trailers, social media content, news clips etc. from existing content in the organization archive</li>
<li>Monetization – Video Indexer can help improve the value of videos. As an example, industries that rely on ad revenue (for example, news media, social media, etc.), can deliver more relevant ads by using the extracted insights as additional signals to the ad server (presenting a sports shoe ad is more relevant in the middle of a football match vs. a swimming competition).</li>
<li>User engagement – Video insights can be used to improve user engagement by positioning the relevant video moments to users. As an example, consider an educational video that explains spheres for the first 30 minutes and pyramids in the next 30 minutes. A student reading about pyramids would benefit more if the video is positioned starting from the 30-minute marker.</li>
<h3>Next steps</h3>
<p>You're ready to get started with Video Indexer. For more information, see the following articles:</p>
<li><a href="https://github.com/jumpstartninjatech/AI-TechSeries/blob/master/Demos/CS/Vision/VideoIndexer/VideoIndexer_challenge1.md">Challenge 1: Existing Video Indexer</a></li>
<li><a href="https://github.com/jumpstartninjatech/AI-TechSeries/blob/master/Demos/CS/Vision/VideoIndexer/VideoIndexer_challenge2.md">Challenge 2: Creating new Video Indexer</a></li>
