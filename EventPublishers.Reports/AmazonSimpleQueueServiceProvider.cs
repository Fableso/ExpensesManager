using System.Text.Json;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

using EventPublishers.Reports.Abstractions;

namespace EventPublishers.Reports;

public class AmazonSimpleQueueServiceProvider : IAmazonSimpleQueueServiceProvider
{
    private readonly AmazonSQSClient _amazonSimpleQueueService;
    private readonly string _queueUrl;

    public AmazonSimpleQueueServiceProvider(
        string region,
        string queueUrl)
    {
        var regionEndpoint = RegionEndpoint.GetBySystemName(region);
        _amazonSimpleQueueService = new AmazonSQSClient(regionEndpoint);
        _queueUrl = queueUrl;
    }

    public async Task Publish<TMessage>(
        TMessage message,
        string messageType,
        int messageVersion,
        string correlationId,
        long? evenBy = null)
    {
        var messageContent = JsonSerializer.Serialize(message, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        var messageAttributes = new Dictionary<string, MessageAttributeValue>
        {
            { "MessageType", new MessageAttributeValue { DataType = "String", StringValue = messageType } },
            { "MessageVersion", new MessageAttributeValue { DataType = "Number", StringValue = messageVersion.ToString() } },
            { "CorrelationId", new MessageAttributeValue { DataType = "String", StringValue = correlationId } },
        };

        if (evenBy.HasValue)
        {
            messageAttributes.Add("EvenBy", new MessageAttributeValue { DataType = "Number", StringValue = evenBy.Value.ToString() });
        }
        
        await _amazonSimpleQueueService.SendMessageAsync(new SendMessageRequest
        {
            QueueUrl = _queueUrl,
            MessageBody = messageContent,
            MessageAttributes = messageAttributes,
        });
    }
}
