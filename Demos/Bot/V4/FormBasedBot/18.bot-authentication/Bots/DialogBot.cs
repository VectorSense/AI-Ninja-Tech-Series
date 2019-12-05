// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Bot.Builder.AI.Luis;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace Microsoft.BotBuilderSamples
{
    // This IBot implementation can run any type of Dialog. The use of type parameterization is to allows multiple different bots
    // to be run at different endpoints within the same project. This can be achieved by defining distinct Controller types
    // each with dependency on distinct IBot types, this way ASP Dependency Injection can glue everything together without ambiguity.
    // The ConversationState is used by the Dialog system. The UserState isn't, however, it might have been used in a Dialog implementation,
    // and the requirement is that all BotState objects are saved at the end of a turn.
    public class DialogBot<T> : ActivityHandler where T : Dialog
    {
        protected readonly BotState ConversationState;
        protected readonly Dialog Dialog;
        protected readonly ILogger Logger;
        protected readonly BotState UserState;
        private readonly IStatePropertyAccessor<StoredDetails> _TokenAccessor;
        private readonly IConfiguration _configuration;

        public DialogBot(ConversationState conversationState, UserState userState, T dialog, ILogger<DialogBot<T>> logger, IConfiguration configuration)
        {
            ConversationState = conversationState;
            UserState = userState;
            Dialog = dialog;
            Logger = logger;
            _TokenAccessor = userState.CreateProperty<StoredDetails>("StoredDetails");
            _configuration = configuration;
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occured during the turn.
            await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
           // await Dialog.Run(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);

        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Running dialog with Message Activity.");
            var Details = await _TokenAccessor.GetAsync(turnContext, () => new StoredDetails(), cancellationToken);
            var p = Details.TokenResponse;

            // Run the Dialog with the new message Activity.
            if ((p != null)&&(turnContext.Activity.Text!="logout"))
            {

                if (turnContext.Activity.Type == ActivityTypes.Message)
                {


                    if (string.IsNullOrEmpty(turnContext.Activity.Text))
                    {
                        dynamic value = turnContext.Activity.Value;
                        string tex = value["id"];// The property will be named after your text input's ID
                        if (value != null)
                        {
                            if (tex == "submit_others")
                            {
                                string data = value["data_others"];
                                if (data == "")
                                {
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"Please fill all fields above."), cancellationToken);
                                }
                                else
                                {
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"Your Complaint is Registered with us successfully.\nPlease note down Reference Number :HG1278 for further Reference."), cancellationToken);
                                    await SendSuggestedActionsAsync(turnContext, cancellationToken);
                                }
                            }
                            else if (tex == "submit_others_cancel")
                            {

                                await SendSuggestedActionsAsync(turnContext, cancellationToken);

                            }
                            else if (tex == "submit_forgotpassword")
                            {

                                string dataInput1 = value["FP_userid"];
                                string dataInput2 = value["FP_mobilenumber"];
                                string dataInput3 = value["FP_emailid"];
                                string dataInput4 = value["FP_choiceset"];
                                if ((dataInput1 == "") || (dataInput2 == "") || (dataInput3 == "") || (dataInput4 == ""))
                                {
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"Please fill all fields above."), cancellationToken);
                                }
                                else
                                {
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"Request for Physical Password has been registered successfully.\nIt will reach your registered Address within 8 working days."), cancellationToken);
                                    await SendSuggestedActionsAsync(turnContext, cancellationToken);
                                }

                            }
                            else if (tex == "submit_forgotpassword_cancel")
                            {
                                await SendSuggestedActionsAsync(turnContext, cancellationToken);

                            }
                            else if (tex == "submit_otp")
                            {
                                string data = value["otp_data"];
                                if (data == "")
                                {
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"Please fill all fields above."), cancellationToken);
                                }
                                else
                                {
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"Your Mobile Number for OTP is registered successfully.Please note down Reference Number :HG1278 for further Reference."), cancellationToken);
                                    await SendSuggestedActionsAsync(turnContext, cancellationToken);
                                }

                            }
                            else if (tex == "submit_otp_cancel")
                            {
                                await SendSuggestedActionsAsync(turnContext, cancellationToken);
                            }
                            else if (tex == "submit_checkbook")
                            {
                                string data = value["data_checkbook"];
                                if (data == "")
                                {
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"Please fill all fields above."), cancellationToken);
                                }
                                else
                                {
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"Your Request has been successfully Registered.Please note down Reference Number :HG1278 for further Reference.\nCheck Book will be delivered to Registered Address within 8 working days."), cancellationToken);
                                    await SendSuggestedActionsAsync(turnContext, cancellationToken);
                                }

                            }
                            else if (tex == "submit_checkbook_cancel")
                            {
                                await SendSuggestedActionsAsync(turnContext, cancellationToken);
                            }
                            else if (tex == "submit_updatemobileno")
                            {
                                string data = value["data_updatemobileno"];
                                if (data == "")
                                {
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"Please fill all fields above."), cancellationToken);
                                }
                                else
                                {
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"Your Mobile Number has been updated successfully.Please note down Reference Number :HG1278 for further Reference."), cancellationToken);
                                    await SendSuggestedActionsAsync(turnContext, cancellationToken);
                                }

                            }
                            else if (tex == "submit_updatemobileno_cancel")
                            {
                                await SendSuggestedActionsAsync(turnContext, cancellationToken);

                            }

                        }

                    }
                    else
                    {

                        var luisApplication = new LuisApplication(
                   "bdc48cb4-0945-40e8-8e89-99fec5924d03",
                   "76e8996cfd994ef3a4b79b938c0c2b04",
                   $"https://westus.api.cognitive.microsoft.com");

                        var recognizer = new LuisRecognizer(luisApplication);
                        // The actual call to LUIS
                        var recognizerResult = await recognizer.RecognizeAsync(turnContext, cancellationToken);

                        var topIntent = recognizerResult.GetTopScoringIntent();
                        // Next, we call the dispatcher with the top intent.
                        await DispatchToTopIntentAsync(turnContext, topIntent.intent, recognizerResult, cancellationToken);
                        //await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {turnContext.Activity.Text}"), cancellationToken);

                    }







                }


                //await turnContext.SendActivityAsync(MessageFactory.Text($"I am in on message"), cancellationToken);
            }
            else {

                await Dialog.Run(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
            }


           
        }


        private async Task DispatchToTopIntentAsync(ITurnContext<IMessageActivity> turnContext, string intent, RecognizerResult recognizerResult, CancellationToken cancellationToken)
        {

            switch (intent)
            {
                case "Account_Balance":
                     await SendAccountBalanceAsync(turnContext, cancellationToken);
                    break;
                case "CardBlock_Complaint":
                    await SendCardBlockAsync(turnContext, cancellationToken);
                    break;
                case "ForgotPassword_Complaint":
                    await SendForgotPasswordAsync(turnContext, cancellationToken);
                    break;
                case "ForgotPIN_Complaint":
                    await SendForgotPinAsync(turnContext, cancellationToken);
                    break;
                case "Greeting":
                    //await turnContext.SendActivityAsync(MessageFactory.Text($"I am in greeting."), cancellationToken);
                    break;
                case "Nearest_ATM":
                   await SendNearestATMAsync(turnContext, cancellationToken);
                    break;
                case "Nearest_Branch":
                    await SendNearestBranchAsync(turnContext, cancellationToken);
                    break;
                case "None":
                    await turnContext.SendActivityAsync(MessageFactory.Text($"I didn't get it.I am still learning"), cancellationToken);
                    break;
                case "Offers":
                   await SendOffersAsync(turnContext, cancellationToken);
                    break;
                case "Others_Complaint":
                    await SendOtherComplaintAsync(turnContext, cancellationToken);
                    break;
                case "OTP_RequestToBank":
                    await SendOtpRequestAsync(turnContext, cancellationToken);
                    break;
                case "Recent_Transactions":
                    await SendRecentTransectionsAsync(turnContext, cancellationToken);
                    break;
                case "UpdateMobileNo_RequestToBank":
                    await SendUpdateMobileNumberAsync(turnContext, cancellationToken);
                    // await turnContext.SendActivityAsync(MessageFactory.Text($"Please fill all fields above."), cancellationToken);
                    break;
                case "Complaint":
                     await SendComplaintAsync(turnContext, cancellationToken);
                    break;
                case "Request_to_Bank":
                     await SendRequestToBankAsync(turnContext, cancellationToken);
                    break;
                case "CheckBook_RequestToBank":
                    await SendCheckBookRequestAsync(turnContext, cancellationToken);
                    break;
                case "Exit":
                    await Dialog.Run(turnContext, ConversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
                    break;


                default:
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Please fill all fields above."), cancellationToken);
                    break;
            }

        }


        private static async Task SendAccountBalanceAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {

            await turnContext.SendActivityAsync(MessageFactory.Text($"Customer Name : Mr. Srikar Kumar \n Account No :7893564521 \n Account Balance :50000000 Rs."), cancellationToken);
            await SendSuggestedActionsAsync(turnContext, cancellationToken);
        }


        private static async Task SendRecentTransectionsAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {

            string filePath = Path.Combine(".", "AdaptiveCards", "RecentTransection.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };


            // Create adaptive card and attach it to the message 
            var cardAttachment = adaptiveCardAttachment;
            var reply = turnContext.Activity.CreateReply();
            reply.Attachments = new List<Attachment>() { cardAttachment };
            await turnContext.SendActivityAsync(MessageFactory.Attachment(adaptiveCardAttachment), cancellationToken);
            await SendSuggestedActionsAsync(turnContext, cancellationToken);
        }


        private static async Task SendNearestBranchAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var activity = MessageFactory.Carousel(
          new Attachment[]
          {
                   new HeroCard(
                       title : "1 Km from Current Location.",
                       subtitle : "No.150, Velachery Tambaram Rd, Medavakkam, Chennai, Tamil Nadu 600100",
                       images : new CardImage[] { new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg") },
                       buttons : new CardAction[]
                       {
                           new CardAction(title: "View Map", type: ActionTypes.OpenUrl, value: "https://www.google.com/maps/dir//State+Bank+of+India,+No.150,+Velachery+Tambaram+Rd,+V.G.P+Pushpa+Nagar,+Medavakkam,+United+Colony,+V.G.P+Pushpa+Nagar,+Medavakkam,+Chennai,+Tamil+Nadu+600100/@12.9137104,80.1818837,15z/data=!4m16!1m6!3m5!1s0x3a525c1fffa58599:0x2901962e5eeaf17!2sState+Bank+of+India!8m2!3d12.9193671!4d80.1875752!4m8!1m0!1m5!1m1!1s0x3a525c1fffa58599:0x2901962e5eeaf17!2m2!1d80.1875752!2d12.9193671!3e3")
                       })
                   .ToAttachment(),
                   new HeroCard(
                       title: "700 metre from Current Location.",
                       subtitle : "3/153, 11/329, Perumbakkam Main Rd, Opp ARUN Hospital, Medavakkam, Chennai, Tamil Nadu 600100",
                       images: new CardImage[] { new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg") },
                       buttons: new CardAction[]
                       {
                           new CardAction(title: "View Map", type: ActionTypes.OpenUrl, value: "https://www.google.com/maps/dir//3%2F153,+State+Bank+Of+India,+11%2F329,+Perumbakkam+Main+Rd,+Opp+ARUN+Hospital,+Medavakkam,+Chennai,+Tamil+Nadu+600100/@12.9137101,80.1818408,15z/data=!4m16!1m6!3m5!1s0x3a525c1f1e55d089:0x9c58792bc66efa35!2sState+Bank+Of+India!8m2!3d12.9150086!4d80.1944557!4m8!1m0!1m5!1m1!1s0x3a525c1f1e55d089:0x9c58792bc66efa35!2m2!1d80.1944557!2d12.9150086!3e3")
                       })
                   .ToAttachment(),
                   new HeroCard(
                       title: "2 Km from Current Location",
                       subtitle : "Plot No.75, Rajakilapakkam, Madambakkam Main Rd, Gokul Nagar, Rajakilpakkam, Madambakkam, Chennai, Tamil Nadu 600073",
                       images: new CardImage[] { new CardImage(url:"https://www.mantri.in/serenity/images/serenity-map.jpg") },
                       buttons: new CardAction[]
                       {
                           new CardAction(title: "View Map", type: ActionTypes.OpenUrl, value: "https://www.google.com/maps/dir//State+Bank+of+India,+Plot+No.75,+Rajakilapakkam,+Madambakkam+Main+Rd,+Gokul+Nagar,+Rajakilpakkam,+Madambakkam,+Chennai,+Tamil+Nadu+600073/@12.9162504,80.14359,15z/data=!4m16!1m6!3m5!1s0x3a525edd173ed3b5:0x8d34c93d459d1f72!2sState+Bank+of+India!8m2!3d12.9162504!4d80.1523447!4m8!1m0!1m5!1m1!1s0x3a525edd173ed3b5:0x8d34c93d459d1f72!2m2!1d80.1523447!2d12.9162504!3e3")
                       })
                   .ToAttachment(),
                   new HeroCard(
                       title: "100 Metre from Current Location",
                       subtitle : "Natwest Vijay Block IV,, Velachery Main Rd, Pallikaranai, Chennai, Tamil Nadu 600100",

                       images: new CardImage[] { new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg") },
                       buttons: new CardAction[]
                       {
                           new CardAction(title: "View Map", type: ActionTypes.OpenUrl, value:"https://www.google.com/maps/dir//State+Bank+of+India,+Natwest+Vijay+Block+IV,,+Velachery+Main+Rd,+Pallikaranai,+Chennai,+Tamil+Nadu+600100/@12.9292956,80.1945426,15z/data=!4m16!1m6!3m5!1s0x3a525c3a37c5a1a7:0x3f7aa4f76c57c487!2sState+Bank+of+India!8m2!3d12.9292956!4d80.2032973!4m8!1m0!1m5!1m1!1s0x3a525c3a37c5a1a7:0x3f7aa4f76c57c487!2m2!1d80.2032973!2d12.9292956!3e3")
                       })
                   .ToAttachment()
          });
            await turnContext.SendActivityAsync(MessageFactory.Text($"I found 4 Nearest Branch."), cancellationToken);
            // Send the activity as a reply to the user.
            await turnContext.SendActivityAsync(activity, cancellationToken: cancellationToken);
            await SendSuggestedActionsAsync(turnContext, cancellationToken);



        }


        private static async Task SendNearestATMAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var activity = MessageFactory.Carousel(
            new Attachment[]
            {
                   new HeroCard(
                       title : "1 Km from Current Location.",
                       subtitle : " No.150, Velachery Tambaram Rd, V.G.P Pushpa Nagar, Medavakkam, Chennai, Tamil Nadu 600100",
                       images : new CardImage[] { new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg") },
                       buttons : new CardAction[]
                       {
                           new CardAction(title: "View Map", type: ActionTypes.OpenUrl, value: "https://www.google.com/maps/dir/12.9137106,80.1819266/State+Bank+of+India,+No.150,+Velachery+Tambaram+Rd,+V.G.P+Pushpa+Nagar,+Medavakkam,+Chennai,+Tamil+Nadu+600100/@12.9172299,80.1789818,16z/data=!3m1!4b1!4m9!4m8!1m1!4e1!1m5!1m1!1s0x3a525c1fffa58599:0x2901962e5eeaf17!2m2!1d80.1875752!2d12.9193671")
                       })
                   .ToAttachment(),
                   new HeroCard(
                       title: "700 metre from Current Location.",
                       subtitle : "3/153, 11/329, Perumbakkam Main Rd, Opp ARUN Hospital, Medavakkam, Chennai, Tamil Nadu 600100",
                       images: new CardImage[] { new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg") },
                       buttons: new CardAction[]
                       {
                           new CardAction(title: "View Map", type: ActionTypes.OpenUrl, value: "https://www.google.com/maps/dir//nearest+sbi+atm/data=!4m6!4m5!1m1!4e2!1m2!1m1!1s0x3a525c1f1e55d089:0x9c58792bc66efa35?sa=X&ved=2ahUKEwi5w_v10v7hAhXm7HMBHb34ABUQ9RcwAHoECAEQCQ")
                       })
                   .ToAttachment(),
                   new HeroCard(
                       title: "2 Km from Current Location",
                       subtitle : "Shop No. 94, Velachery Main Rd, Pallikaranai, Chennai, Tamil Nadu 600100",
                       images: new CardImage[] { new CardImage(url:"https://www.mantri.in/serenity/images/serenity-map.jpg") },
                       buttons: new CardAction[]
                       {
                           new CardAction(title: "View Map", type: ActionTypes.OpenUrl, value: "https://www.google.com/maps/dir//nearest+sbi+atm/data=!4m6!4m5!1m1!4e2!1m2!1m1!1s0x3a525c1878a5398b:0x422c12ef418ce4d9?sa=X&ved=2ahUKEwiQyP7A0_7hAhXzjuYKHcp5CrkQ9RcwAHoECAEQCA")
                       })
                   .ToAttachment(),
                   new HeroCard(
                       title: "100 Metre from Current Location",
                       subtitle : "Munusamy Nagar, Vimala Nagar, Medavakkam, Chennai, Tamil Nadu 600100",

                       images: new CardImage[] { new CardImage(url: "https://www.mantri.in/serenity/images/serenity-map.jpg") },
                       buttons: new CardAction[]
                       {
                           new CardAction(title: "button", type: ActionTypes.OpenUrl, value:"https://www.google.com/maps/dir//nearest+sbi+atm/data=!4m6!4m5!1m1!4e2!1m2!1m1!1s0x3a525c1fab940a2b:0x289cd69a4db77f3?sa=X&ved=2ahUKEwiu18y_0_7hAhV1muYKHULKDLsQ9RcwAHoECAEQCA")
                       })
                   .ToAttachment()
            });
            await turnContext.SendActivityAsync(MessageFactory.Text($"I found 4 Nearest ATM."), cancellationToken);
            // Send the activity as a reply to the user.
            await turnContext.SendActivityAsync(activity, cancellationToken: cancellationToken);
            await SendSuggestedActionsAsync(turnContext, cancellationToken);
        }


        private static async Task SendOffersAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var activity = MessageFactory.Carousel(
           new Attachment[]
           {
                   new HeroCard(
                       title : "Vijay Sales",
                       subtitle : "5% Cashback at vijay Sales.",
                       images : new CardImage[] { new CardImage(url: "https://www.sbicard.com/sbi-card-en/assets/media/images/personal/offers/categories/shopping/vijaysales/thumb-vijay-sales.jpg") },
                       buttons : new CardAction[]
                       {
                           new CardAction(title: "View Offer", type: ActionTypes.OpenUrl, value: "https://www.sbicard.com/en/personal/offer/vijaysales.page")
                       })
                   .ToAttachment(),
                   new HeroCard(
                       title: "spencers",
                       subtitle : "Get 5% Cashback .",
                       images: new CardImage[] { new CardImage(url: "https://www.sbicard.com/sbi-card-en/assets/media/images/personal/offers/spencers/thumb-spencer.jpg") },
                       buttons: new CardAction[]
                       {
                           new CardAction(title: "View Offer", type: ActionTypes.OpenUrl, value: "https://www.sbicard.com/en/personal/offer/spencers.page")
                       })
                   .ToAttachment(),
                   new HeroCard(
                       title: "Healthians",
                       subtitle : "Flat 50% off on Healthians SAVE HEALTH PACKAGE ",
                       images: new CardImage[] { new CardImage(url: "https://www.sbicard.com/sbi-card-en/assets/media/images/personal/offers/categories/lifestyle/healthians/thumb-healthians.jpg") },
                       buttons: new CardAction[]
                       {
                           new CardAction(title: "View Offer", type: ActionTypes.OpenUrl, value: "https://www.sbicard.com/en/personal/offer/healthians.page")
                       })
                   .ToAttachment(),
                   new HeroCard(
                       title: "Amazon",
                       subtitle : "10% Instant Discount at Amazon.in ",

                       images: new CardImage[] { new CardImage(url: "https://www.sbicard.com/sbi-card-en/assets/media/images/personal/offers/categories/shopping/amazon-summer-sale/thumb-amazon-summer-sale.jpg") },
                       buttons: new CardAction[]
                       {
                           new CardAction(title: "View Offer", type: ActionTypes.OpenUrl, value:"https://www.sbicard.com/en/personal/offer/amazon-summer-sale.page")
                       })
                   .ToAttachment()
           });
            await turnContext.SendActivityAsync(MessageFactory.Text($"I found 4 Offers."), cancellationToken);

            await turnContext.SendActivityAsync(activity, cancellationToken: cancellationToken);
            await SendSuggestedActionsAsync(turnContext, cancellationToken);

        }


        private static async Task SendComplaintAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var reply = turnContext.Activity.CreateReply("Please kindly register your Complaint.");
            reply.SuggestedActions = new SuggestedActions
            {
                Actions = new List<CardAction>()
                    {
                         new CardAction() { Title = "Card Block", Type = ActionTypes.ImBack, Value = "Card Block" },
                         new CardAction() { Title = "Forgot PIN", Type = ActionTypes.ImBack, Value = "Forgot PIN" },
                         new CardAction() { Title = "Forgot Password", Type = ActionTypes.ImBack, Value = "Forgot Password" },
                         new CardAction() { Title = "Others", Type = ActionTypes.ImBack, Value = "Others" },
                    }
            };
            await turnContext.SendActivityAsync(reply, cancellationToken);

            //await turnContext.SendActivityAsync(MessageFactory.Text($"I am Send Complaint"), cancellationToken);
        }


        private static async Task SendOtherComplaintAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {

            string filePath = Path.Combine(".", "AdaptiveCards", "OthersComplaint.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };


            // Create adaptive card and attach it to the message 
            var cardAttachment = adaptiveCardAttachment;
            var reply = turnContext.Activity.CreateReply();
            reply.Attachments = new List<Attachment>() { cardAttachment };
            await turnContext.SendActivityAsync(MessageFactory.Attachment(adaptiveCardAttachment), cancellationToken);
            //await turnContext.SendActivityAsync(reply, cancellationToken);
            //await SendSuggestedActionsAsyncSecond(turnContext, cancellationToken);
           // await SendSuggestedActionsAsync(turnContext, cancellationToken);

        }


        private static async Task SendForgotPinAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            await turnContext.SendActivityAsync(MessageFactory.Text($"Please Check the Registered E-mail ID for a link sent by us in email."), cancellationToken);
            await SendSuggestedActionsAsync(turnContext, cancellationToken);
        }



        private static async Task SendForgotPasswordAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            //await turnContext.SendActivityAsync(MessageFactory.Text($"Please Check the Registered E-mail ID for a link sent by us in email."), cancellationToken);
            string filePath = Path.Combine(".", "AdaptiveCards", "ForgotPassword.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };


            // Create adaptive card and attach it to the message 
            var cardAttachment = adaptiveCardAttachment;
            var reply = turnContext.Activity.CreateReply();
            reply.Attachments = new List<Attachment>() { cardAttachment };
            await turnContext.SendActivityAsync(MessageFactory.Attachment(adaptiveCardAttachment), cancellationToken);
            //await turnContext.SendActivityAsync(reply, cancellationToken);
           // await SendSuggestedActionsAsync(turnContext, cancellationToken);

        }


        private static async Task SendCardBlockAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            await turnContext.SendActivityAsync(MessageFactory.Text($"Your Request has been generated for Blocking Card No:XXXXX XXXXXXXXXXX 152.\nPlease note down Complaint Reference Number :435G34 for further Reference."), cancellationToken);
            await SendSuggestedActionsAsync(turnContext, cancellationToken);
        }










        private static async Task SendRequestToBankAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            //await turnContext.SendActivityAsync(MessageFactory.Text($"I am in send request"), cancellationToken);
            var reply = turnContext.Activity.CreateReply("Please register your Request to Bank.");
            reply.SuggestedActions = new SuggestedActions
            {
                Actions = new List<CardAction>()
                    {
                         new CardAction() { Title = "OTP", Type = ActionTypes.ImBack, Value = "OTP" },
                         new CardAction() { Title = "Check Book", Type = ActionTypes.ImBack, Value = "Check Book" },
                         new CardAction() { Title = "Update Mobile Number", Type = ActionTypes.ImBack, Value = "Update Mobile Number" },
                    }

            };
            await turnContext.SendActivityAsync(reply, cancellationToken);
        }



        private static async Task SendOtpRequestAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            //await turnContext.SendActivityAsync(MessageFactory.Text($"I am in send otp request"), cancellationToken);

            string filePath = Path.Combine(".", "AdaptiveCards", "OtpRequest.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };


            // Create adaptive card and attach it to the message 
            var cardAttachment = adaptiveCardAttachment;
            var reply = turnContext.Activity.CreateReply();
            reply.Attachments = new List<Attachment>() { cardAttachment };
            await turnContext.SendActivityAsync(MessageFactory.Attachment(adaptiveCardAttachment), cancellationToken);
            //await turnContext.SendActivityAsync(reply, cancellationToken);
            //await SendSuggestedActionsAsync(turnContext, cancellationToken);
        }



        private static async Task SendCheckBookRequestAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            //await turnContext.SendActivityAsync(MessageFactory.Text($"I am in send check book request"), cancellationToken);
            string filePath = Path.Combine(".", "AdaptiveCards", "CheckBookRequest.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };


            // Create adaptive card and attach it to the message 
            var cardAttachment = adaptiveCardAttachment;
            var reply = turnContext.Activity.CreateReply();
            reply.Attachments = new List<Attachment>() { cardAttachment };
            await turnContext.SendActivityAsync(MessageFactory.Attachment(adaptiveCardAttachment), cancellationToken);
            //await turnContext.SendActivityAsync(reply, cancellationToken);
            //await SendSuggestedActionsAsync(turnContext, cancellationToken);

        }


        private static async Task SendUpdateMobileNumberAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            //await turnContext.SendActivityAsync(MessageFactory.Text($"I am in send update mobile Number."), cancellationToken);
            string filePath = Path.Combine(".", "AdaptiveCards", "UpdateMobileNumber.json");
            var adaptiveCardJson = File.ReadAllText(filePath);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(adaptiveCardJson),
            };


            // Create adaptive card and attach it to the message 
            var cardAttachment = adaptiveCardAttachment;
            var reply = turnContext.Activity.CreateReply();
            reply.Attachments = new List<Attachment>() { cardAttachment };
            await turnContext.SendActivityAsync(MessageFactory.Attachment(adaptiveCardAttachment), cancellationToken);
            //await turnContext.SendActivityAsync(reply, cancellationToken);
            //await SendSuggestedActionsAsync(turnContext, cancellationToken);

        }






        private static async Task SendSuggestedActionsAsync(ITurnContext turnContext, CancellationToken cancellationToken)
            {
                var reply = turnContext.Activity.CreateReply("How can I help you?");
                reply.SuggestedActions = new SuggestedActions()
                {
                    Actions = new List<CardAction>()
                    {
                         new CardAction() { Title = "Account Balance", Type = ActionTypes.ImBack, Value = "Account Balance" },
                         new CardAction() { Title = "Recent Transactions", Type = ActionTypes.ImBack, Value = "Recent Transactions" },
                         new CardAction() { Title = "Nearest Branch", Type = ActionTypes.ImBack, Value = "Nearest Branch" },
                         new CardAction() { Title = "Nearest ATM", Type = ActionTypes.ImBack, Value = "Nearest ATM" },
                         new CardAction() { Title = "Offers", Type = ActionTypes.ImBack, Value = "Offers" },
                         new CardAction() { Title = "Complaint", Type = ActionTypes.ImBack, Value = "Complaint" },
                         new CardAction() { Title = "Request to Bank", Type = ActionTypes.ImBack, Value = "Request to Bank" },
                         new CardAction() { Title = "Exit", Type = ActionTypes.ImBack, Value = "Exit" },
                     },
                };
                await turnContext.SendActivityAsync(reply, cancellationToken);


            }

        









    }
}
