using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace DemoChess.GameScripts.SNS
{
    public class EmailUser
    {
        private AmazonSimpleNotificationServiceClient client;
        private String email = "s3477928@student.rmit.edu.au";
        private String topicArn;

        public EmailUser()
        {
            client = new AmazonSimpleNotificationServiceClient(RegionEndpoint.APSoutheast2);

        }

        public void Subscribe(string topic)
        {
            topicArn = client.CreateTopic(new CreateTopicRequest
            {
                Name = topic
            }).CreateTopicResult.TopicArn;

            // Set display name to a friendly value
            client.SetTopicAttributes(new SetTopicAttributesRequest
            {
                TopicArn = topicArn,
                AttributeName = "DisplayName",
                AttributeValue = "Notification Chess"
            });

            // Subscribe an endpoint - in this case, an email address
            client.Subscribe(new SubscribeRequest
            {
                TopicArn = topicArn,
                Protocol = "email",
                Endpoint = "wcappyz@gmail.com"
            });

        }


        public void MessageUser(string message)
        { 

            client.Publish(new PublishRequest
            {
                Subject = "Turn change!",
                Message = message,
                TopicArn = topicArn
            });

        }

        public void DeleteTopic()
        {
            client.DeleteTopic(new DeleteTopicRequest
            {
                TopicArn = topicArn
            });
        }



    }
}