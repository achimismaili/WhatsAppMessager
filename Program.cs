// https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/advanced-messaging/whatsapp/connect-whatsapp-business-account
// See https://aka.ms/new-console-template for more information
using Azure;
using Azure.Communication.Messages;
using Azure.Communication.Messages.Models.Channels;

// Retrieve connection string from environment variable
string connectionString =
    Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING");

// Instantiate the client
var notificationMessagesClient = new NotificationMessagesClient(connectionString);

var channelRegistrationId = new Guid("37e06ce7-6ba1-42b0-8e0f-43e816332a1e");

var recipientList = new List<string> { "+4915168401150" };

// Assemble the template content
string templateName = "muell";
string templateLanguage = "de";

var binColor = new MessageTemplateText(name: "binColor", text: "Gelbe");
var binDate = new MessageTemplateText(name: "binDate", text: "Montag, 01.01.2020");
var addAction = new MessageTemplateText(name: "addAction", text: ". Beide!");

WhatsAppMessageTemplateBindings bindings = new();
bindings.Header.Add(new(binColor.Name));
bindings.Body.Add(new(binColor.Name));
bindings.Body.Add(new(binDate.Name));
bindings.Body.Add(new(addAction.Name));

var messageTemplate = new MessageTemplate(templateName, templateLanguage);
messageTemplate.Bindings = bindings;
messageTemplate.Values.Add(binColor);
messageTemplate.Values.Add(binDate);
messageTemplate.Values.Add(addAction);


// Assemble template message
var templateContent =
    new TemplateNotificationContent(channelRegistrationId, recipientList, messageTemplate);

// Send template message
Response<SendMessageResult> sendTemplateMessageResult =
    await notificationMessagesClient.SendAsync(templateContent);

// after initiation
//// Assemble text message
//var textContent =
//    new TextNotificationContent(channelRegistrationId, recipientList, "Thanks for your feedback.\n From Notification Messaging SDK");

//// Send text message
//Response<SendMessageResult> sendTextMessageResult =
//    await notificationMessagesClient.SendAsync(textContent);