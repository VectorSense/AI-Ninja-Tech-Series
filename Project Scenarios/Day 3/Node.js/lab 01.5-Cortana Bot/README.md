# Cortana Bot 

In this lab you will build a voice enabled bot which interacts through the voice and collects the information such as name,city,company from the user and display the summary to the user.

### Prerequisites

The following software environment is needed for running this bot :

```
1.Node.js
2.Microsoft BotFramework Emulator.
3.SubLimeText Editor.
```

### Collecting the keys

Over the course of this lab, we will collect various keys. It is recommended that you save all of them in a text file, so you can easily access them throughout the workshop.Keys are :
```
-Bot Framework App ID.
-Bot Framework App password.
```
Note:Please refer following link to understand how to register bot and generate Bot Framework APP ID and App password.  link :[Bot Registration](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-quickstart-registration?view=azure-bot-service-3.0).

Note:Please enable Cortana Channel after registartion.To understand how to enable cortana channel please refer following link :[Cortana Registration](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-channel-connect-cortana?view=azure-bot-service-3.0).


### Implementation

To check implementation of this lab please refer to the following file in this path Chat-Bot-AI-Virtual-Trail-Blazer-Series\Node.js\Day 1\lab 01.5-Cortana Bot:

```
                             cortanabot.js
```

###Check implementation through following steps


1. Open cortanabot.js file in SubLimeText Editor and provide Bot Framework App ID and Bot Framework App password in this section of code. (note : If you are working on local then specify Bot Framework App ID and Bot Framework App password  ).

![adaptivecardbot_0](https://user-images.githubusercontent.com/31923904/39426232-aeab6f22-4c9c-11e8-9255-b1ab41b1f3f6.jpg)


2.Open command prompt (cmd) then set path to lab 01.5-Cortana Bot folder then run cortanabot.js file using command below:

                               node cortanabot.js

3.Start the Bot Framework Emulator and connect your bot and type http://localhost:3978/api/messages into the address bar.(This is the default end point that your bot listens to when hosted locally).Click on “Connect” button.(note : If you are working on local then no need to specify Microsoft App ID and Microsoft App Password ).  

![simple](https://user-images.githubusercontent.com/31923904/40777287-1a271f60-64eb-11e8-9862-17e50503e330.png)


  -The following screenshot shows the results of this chatbot running in the Bot Framework Channel Emulator.

![cortana](https://user-images.githubusercontent.com/31923904/40777221-e1106dc6-64ea-11e8-94c4-5930222a6141.png)