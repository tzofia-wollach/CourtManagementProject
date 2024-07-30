public class Discussion
{
    public Guid Id { get; set; }
    public string Topic { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }
    public Guid CaseId { get; set; }
}
