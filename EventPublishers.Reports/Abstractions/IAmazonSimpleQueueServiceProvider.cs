namespace EventPublishers.Reports.Abstractions;

public interface IAmazonSimpleQueueServiceProvider
{
    Task Publish<TMessage>(
        TMessage message,
        string messageType,
        int messageVersion,
        string correlationId,
        long? evenBy = null);
}
