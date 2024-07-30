using System;

namespace CourtManagementBackend.Models
{
    public class Case
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public CaseStage Stage { get; set; }
    }
}
