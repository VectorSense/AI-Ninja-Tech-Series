
var restify = require('restify');
var builder = require('botbuilder');


// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
   console.log('%s listening to %s', server.name, server.url); 
});

//Create chat connector for communicating with the Bot Framework Service
var connector = new builder.ChatConnector({
    appId: process.env.appId,
    appPassword:process.env.appPassword
    
});



// Listen for messages from users 
server.post('/api/messages', connector.listen());



// Create your bot with a function to receive messages from the user
var bot = new builder.UniversalBot(connector);


//Root Dialog execute when user gives any input
bot.dialog("/",[
	function(session)
	{
		
		builder.Prompts.text(session, 'Please enter your name.', {            
           speak: 'Please enter your name ?',
           retrySpeak: 'Please enter your name ?',
           inputHint: builder.InputHint.expectingInput
        });
	},
	function(session,results)
	{
		session.userData.name=results.response;
		builder.Prompts.text(session, 'Please enter city.', {            
               speak: 'Please enter city ?',
               retrySpeak: 'Please enter city ?',
               inputHint: builder.InputHint.expectingInput
        });
	},
	function(session,results)
	{
		session.userData.city=results.response;
		builder.Prompts.text(session, 'Please enter company name.', {            
               speak: 'Please enter company name.?',
               retrySpeak: 'Please enter company name.?',
               inputHint: builder.InputHint.expectingInput
        });

	},
	function(session,results)
	{
		session.userData.company=results.response;
	    session.send("Information entered by you:\n\n Name : "+session.userData.name+"\nCity : "+session.userData.city+"\ncompany : "+session.userData.company);
	}
	]);

