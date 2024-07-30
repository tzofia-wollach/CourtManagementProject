public class Decision
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DecidedAt { get; set; }
    public Guid CaseId { get; set; }
}
