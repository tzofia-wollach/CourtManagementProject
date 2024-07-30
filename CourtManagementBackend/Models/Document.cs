public class Document
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public Guid CaseId { get; set; }
    public DateTime UploadedAt { get; set; }
}
