using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CourtManagementBackend.Models;

namespace CourtManagementBackend.Services
{
    public class CaseManagementService
    {
        private readonly HttpClient _httpClient;
        private readonly EmailService _emailService;


    public CaseManagementService(HttpClient httpClient, EmailService emailService)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

        public async Task<Case> CreateCaseAsync(CaseData caseData)
        {
            // Step 1: Create a new case
            var newCase = new Case
            {
                Id = Guid.NewGuid(),
                Title = caseData.Title,
                Description = caseData.Description,
                CreatedAt = DateTime.UtcNow,
                Stage = CaseStage.Created
            };

            // Simulate saving the case to a database
            SaveCaseToDatabase(newCase);

            // Log case creation
            Console.WriteLine($"Case {newCase.Id} created successfully.");

            // Step 2: Send email notification asynchronously
            await _emailService.SendEmailNotificationAsync(newCase);

            return newCase;
        }

        private void SaveCaseToDatabase(Case newCase)
        {
            // Simulate saving the case to a database
            Console.WriteLine($"Case {newCase.Id} saved to the database.");
        }

    }
}
