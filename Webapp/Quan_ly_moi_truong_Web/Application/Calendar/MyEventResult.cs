namespace Application.Calendar
{
    public record MyEventResult(MyEvent myEvent);
    public record MyAddedEventResult(MyAddedEvent myAddedEvent);
    public record MyUpdatedEventResult(MyUpdatedEvent myUpdatedEvent);
    public record MyUpdatedJobStatusResult(bool myUpdatedJobStatus);
    public record MyDeletedEventResult(bool myDeletedEvent);
}