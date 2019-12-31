/*-----------------------------------------------------------------------------
A Luis based bot for the Microsoft Bot Framework. 
-----------------------------------------------------------------------------*/

var builder = require('botbuilder');
var restify = require('restify');
var botbuilder_azure = require("botbuilder-azure");

// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log('%s listening to %s', server.name, server.url);
});

// Create chat connector for communicating with the Bot Framework Service
var connector = new builder.ChatConnector({
    appId: process.env.MicrosoftAppId,              //Type MicrosoftAppId here
    appPassword: process.env.MicrosoftAppPassword,  //Type MicrosoftAppPassword here
});

server.post('/api/messages', connector.listen());

var bot = new builder.UniversalBot(connector, function (session) {
    session.beginDialog('/outofdomain');
});

// You can provide your own model by specifing the 'LUIS_MODEL_URL' environment variable
// This Url can be obtained by uploading or creating your model from the LUIS portal: https://www.luis.ai/
var recognizer = new builder.LuisRecognizer('https://eastus.api.cognitive.microsoft.com/luis/v2.0/apps/55e06bb7-b4d4-49ad-a4df-bc26cc2abfa2?verbose=true&timezoneOffset=-360&subscription-key=92fd43f0976445429bbe09ed1a672866&q=');
bot.recognizer(recognizer);


bot.dialog('Greeting', [
    function (session, args, next) {
            session.send("Greeting Intent Encountered...");
            session.endDialog();
    }
]).triggerAction({
    matches: 'Greeting'
});

bot.dialog('Discover', [
    function (session, args, next) {
            session.send("Discover Intent encountered...");
            session.endDialog();
    }
]).triggerAction({
    matches: 'Discover'
});

// Thank you bot
bot.dialog('/Intent1', [
    function (session, args, next) {
        session.send("Intent 1 Intent encountered....");
        session.endDialog();
    }
]).triggerAction({
    matches: 'Intent1'
});

// Thank you bot
bot.dialog('/outofdomain', [
    function (session, args, next) {
        //session.send("Out of domain encountered");
        session.send("I do not understand what you are asking...");
        session.endDialog();
    }
]).triggerAction({
    matches: 'outofdomain'
});

bot.customAction({
    matches: 'None',
    onSelectAction: (session, args, next) => {
        session.beginDialog('/outofdomain');
    }
    
}
);


