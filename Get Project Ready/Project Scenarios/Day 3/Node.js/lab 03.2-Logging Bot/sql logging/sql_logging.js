var restify = require('restify');
var builder = require('botbuilder');
var Connection = require('tedious').Connection;
var Request = require('tedious').Request;



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


//This function is used for logging conversation on console.
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
		connection(session);
       }, 3000);
	}
	]);


function connection(session)
{

// Create connection to database
var config = 
   {
     userName: 'sample', // update me
     password: 'siddharth_08', // update me
     server: 'dbwe.database.windows.net', // update me
     options: 
        {
           database: 'db_test' //update me
           , encrypt: true
        }
   }
var connection = new Connection(config);

// Attempt to connect and execute queries if connection goes through
connection.on('connect', function(err) 
   {
     if (err) 
       {
          console.log(err)
       }
    else
       {
      

       	 	
       // Read all rows from table
     request = new Request(
          "INSERT INTO userChatLog(fromId, toId, message) VALUES('"+chatDataRecieve[0].id+"','"+chatDatasend[0].id+"','"+chatDatasend[0].message+"')",
             function(err, rowCount, rows) 
                {
                	if(err)
                	{
                     console.log("what is there in errr",err);
                	}else{
                    console.log(rowCount + ' row(s) returned');
                }
                    process.exit();
                }
            );

   
     connection.execSql(request);


       }
   }
 );


   
   


}


