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


var chatDatasend=[];
var chatDataRecieve=[];

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


//This function is used for storing conversation details into array
function logUserConversation (event, type) {
	if (type == "Receive") {
		var chat_history = {
			id:event.address.user.id,
			message: event.text
			
		}
        if(event.text!=''){
		chatDataRecieve.push(chat_history)
	    }
	}

	if (type == "Send") {
		var chat_history1 = {
			id:event.address.user.id,
			message: event.text
		}
     	chatDatasend.push(chat_history1)	
	}
}	

//This dialog will execute when user enter something.
bot.dialog("/",[
	function(session)
	{
		var text=session.message.text;
		var length=text.length; 
		session.send("You sent A message which was "+length +" Characters");
        setTimeout(function(){
		writeinnotepad(session);
       }, 3000);
    
	}
	]);


//This function is used for logging conversation into file
function writeinnotepad(session)
{
	console.log("what is there in the session.message",session.message);

	for(var i=0;i<chatDataRecieve.length;i++)
	{
     
	 fs.writeFile('sample.txt',"From :"+chatDataRecieve[i].id+"-To :"+chatDatasend[i].id+"- Message :"+chatDatasend[i].message , function(err) {

        if (err){
        	console.log("error:",err);
        }else{
        	console.log("Data stored");

        }
    });

	} 
}