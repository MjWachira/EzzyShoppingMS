using Azure.Messaging.ServiceBus;
using MailService.Models;
using MailService.Models.Dtos;
using MailService.Service;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace MailService.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly ServiceBusProcessor _emailProcessor;
        private readonly MailsService _emailService;
        private readonly EmailService _email;


        public AzureServiceBusConsumer(IConfiguration configuration,EmailService service)
        {
            _configuration = configuration;
            _email= service;

            _connectionString = _configuration.GetValue<string>("AzureConnectionString");
            _queueName= _configuration.GetValue<string>("QueueAndTopics:registerQueue");

            var client = new ServiceBusClient(_connectionString);
            _emailProcessor = client.CreateProcessor(_queueName);
            _emailService = new MailsService(configuration);

        }
        public async Task Start()
        {
            _emailProcessor.ProcessMessageAsync += OnRegisterUser;
            _emailProcessor.ProcessErrorAsync += ErrorHandler;
            await _emailProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
           await _emailProcessor.StopProcessingAsync();
           await _emailProcessor.DisposeAsync();
        }

        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            //send Email to Admin 
             return Task.CompletedTask;
        }

        private async Task OnRegisterUser(ProcessMessageEventArgs arg)
        {
           
            var message = arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);//read  as String
            var user = JsonConvert.DeserializeObject<UserMessageDto>(body);//string to UserMessageDto

            try
            {

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://cdn.pixabay.com/photo/2013/07/13/09/51/drink-156144_1280.png\" width=\"800\" height=\"500\">");
                stringBuilder.Append("<h1> Hello " + user.Name + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to Ezzy Shopping");
                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p>A shop on your palms!!</p>");
                await _emailService.sendEmail(user, stringBuilder.ToString());


                //insert  to Database
                var emaiLLogger = new EmailLogger()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Message = stringBuilder.ToString(),
                    DateTime = DateTime.Now,

                };
                await _email.addDatatoDatabase(emaiLLogger);

                await arg.CompleteMessageAsync(arg.Message);//we are done delete the message from the queue 
            }catch (Exception ex)
            {
                throw;
                //send an Email to Admin
            }
        }

       
    }
}
