# Waterfall Bot

In this lab you will build a bot which collects information from user such as name,company,city and later display the summary of information collected.

### Prerequisites
The following software environment is needed for running this bot :

```
1.Node.js
2.Microsoft BotFramework Emulator.
3.SubLimeText Editor.
```

### Collecting the keys

Over the course of this lab, we will collect various keys. It is recommended that you save all of them in a text file, so you can easily access them throughout the workshop.keys:

```
-Bot Framework App ID.
-Bot Framework App password.
```
Note:Please refer following link to understand how to generate Bot Framework APP ID and App password.  link :[Bot Registration](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-quickstart-registration?view=azure-bot-service-3.0).


### Implementation

To check implementation of this lab please refer to the following file in this path Chat-Bot-AI-Virtual-Trail-Blazer-Series\Node.js\Day 1\lab 01.3-Waterfall Bot:

```
                             waterfallbot.js
```

### Check implementation through following steps

1. Open waterfallbot.js file in SubLime Text Editor and provide Bot Framework App ID and Bot Framework App password in this section of code. (note : If you are working on local then there is no need to specify Bot Framework App ID and Bot Framework App password  ).

![adaptivecardbot_0](https://user-images.githubusercontent.com/31923904/39425015-f9250e9c-4c96-11e8-9636-f53ed8cc1a82.jpg)

2.Open command prompt (cmd) then set path to lab 01.3-Waterfall Bot folder then run waterfallbot.js file using command below:
```

                               node waterfallbot.js.
```
3.Start the Bot Framework Emulator and connect your bot and type http://localhost:3978/api/messages into the address bar.(This is the default end point that your bot listens to when hosted locally).Click on “Connect” button.(note : If you are working on local then no need to specify Microsoft App ID and Microsoft App Password ).

![simple](https://user-images.githubusercontent.com/31923904/40782673-b258cea2-64fd-11e8-8033-b61041221302.png)  

  -The following screen shot shows the results of this chat bot running in the Bot Framework Channel Emulator.
![cortana](https://user-images.githubusercontent.com/31923904/40782636-953d8146-64fd-11e8-85ed-db6d5728e1a8.png)
                      
