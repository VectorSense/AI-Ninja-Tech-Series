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

// Booking bot
bot.dialog('/Intent1', [
    function (session, args, next) {
            console.log("Intent1 encountered...");
            var Entity1 = builder.EntityRecognizer.findEntity(args.intent.entities, 'Entity1');
            var Entity2 = builder.EntityRecognizer.findEntity(args.intent.entities, 'Entity2');
            session.userData.IsIntent1StillActive = true;
console.log(Entity1);
console.log(Entity2);


            get_entities(session, Entity1, Entity2);
            check_entities(session);
    }
]).triggerAction({
    matches: 'Intent1'
});

// Booking bot
bot.dialog('/Deviate', [
    function (session, args, next) {
        session.send("Deviate Intent encountered...");
        session.endDialog();
    }
]).triggerAction({
    matches: 'Deviate'
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

bot.dialog('/TakeAPause1', [
    function (session, args, next) {
        //session.send("Out of domain encountered");
        session.send("Hmmmm...Alright! Come back later...");
        session.endDialog();
    }
]).triggerAction({
    matches: 'TakeAPause1'
});
function get_entities (session, Entity1, Entity2) {
    
    if(Entity1) {
        
                //console.log("inside get entiries");
                session.userData.Entity1=Entity1.entity;

            }
    if(Entity2) {
        
                session.userData.Entity2=Entity2.entity;

            }

}



bot.dialog('/NoEntity1', [
    function(session) {
        builder.Prompts.text(session, "Enter Entity1");
    },
    function(session, results) {
        session.userData.Entity1 = results.response;
        check_entities(session);
    }
]);




bot.dialog('/NoEntity2', [
    function(session) {
        builder.Prompts.text(session, "Enter Entity2");
    },
    function(session, results) {
        session.userData.Entity2 = results.response;
        check_entities(session);
    }
]);



bot.dialog('/DisplayEntities', [
    function(session) {
        session.send("Thanks for entering all entities");
        session.send("Entity1---"+ session.userData.Entity1);
        session.send("Entity2---"+ session.userData.Entity2);
	

        session.userData.Entity1=null;
        session.userData.Entity2=null;

        session.userData.IsIntent1StillActive=false;
        session.endDialog;  

            }
]);


function check_entities (session) {
    console.log(session.userData.Entity1);
    if (session.userData.Entity1 == null || session.userData.Entity1 == undefined || session.userData.Entity1 == "") {
        session.beginDialog('/NoEntity1');
    } else if (session.userData.Entity2 == null || session.userData.Entity2 == undefined || session.userData.Entity2 == "") {
        session.beginDialog('/NoEntity2');
 
    }
   else {
        session.beginDialog('/DisplayEntities');
    }
}

