using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using RewardServices.Models.Dtos;
using RewardsService.Models;
using RewardsService.Services;
using System.Text;

namespace RewardsService.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _topicName;
        private readonly string _subscription;
        private readonly ServiceBusProcessor _rewardsProcessor;
        private readonly RewardsServices _rewardService;


        public AzureServiceBusConsumer(IConfiguration configuration, RewardsServices reward)
        {
            _configuration = configuration;
            _rewardService = reward;
            _connectionString = _configuration.GetValue<string>("AzureConnectionString");
            _topicName= _configuration.GetValue<string>("QueueAndTopics:bookingTopic");
            _subscription = _configuration.GetValue<string>("QueueAndTopics:bookingSubscription");

            var client = new ServiceBusClient(_connectionString);
            _rewardsProcessor = client.CreateProcessor(_topicName,_subscription);
           
        }
        public async Task Start()
        {
            

            _rewardsProcessor.ProcessMessageAsync += OnBooking;
            _rewardsProcessor.ProcessErrorAsync += ErrorHandler;
            await _rewardsProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _rewardsProcessor.StopProcessingAsync();
            await _rewardsProcessor.DisposeAsync();
        }

        private  async Task OnBooking(ProcessMessageEventArgs arg)
        {

            var message = arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);//read  as String
            var reward = JsonConvert.DeserializeObject<RewardDto>(body);//string to UserMessageDto

            try
            {
                //insert  to Database
                var rwd = new Reward()
                {
                   OrderId=reward.OrderId,
                   ProductTotal=reward.ProductTotal,
                   Email=reward.Email,
                   Name=reward.Name,
                   Points=reward.ProductTotal/1000

                };

             await _rewardService.AddReward(rwd);
             await arg.CompleteMessageAsync(arg.Message);//we are done delete the message from the queue 
            }
            catch (Exception ex)
            {
                throw;
                //send an Email to Admin
            }
        }

      

        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {

            //send Email to Admin 
             return Task.CompletedTask;
        }

           
    }
}
