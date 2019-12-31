var restify = require('restify');
var builder = require('botbuilder');
var fs = require('fs');



// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
   console.log('%s listening to %s', server.name, server.url); 
});

// Create chat connector for communicating with the Bot Framework Service
var connector = new builder.ChatConnector({
    appId: process.env.MicrosoftAppId,              
    appPassword: process.env.MicrosoftAppPassword,  
});

// Listen for messages from users 
server.post('/api/messages', connector.listen());


// Create your bot with a function to receive messages from the user
var bot = new builder.UniversalBot(connector);


//This method for intercepting user conversation
bot.use({
	receive: function (event, next) {
		var type = "Receive";
		logUserConversation(event, type);
		next();
	},
	send: function (event, next) {
		var type = "Send";
		logUserConversation(event, type);
		next();
	}
});


//This function is used for logging conversation on console.
function logUserConversation (event, type) {
	if (type == "Receive") {
		console.log("Receive message     ::",event.text);
	}
	if (type == "Send") {
	   console.log("Sent message      ::",event.text);
	}
}	

//This dialog will execute when user enter something.
bot.dialog("/",[
	function(session)
	{
		var text=session.message.text;
		var length=text.length; 
		session.send("You sent A message which was "+length +" Characters");
	}
	]);


