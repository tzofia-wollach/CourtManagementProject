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

        public CaseManagementService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

            // Step 2: Send email notification asynchronously
            await SendEmailNotificationAsync(newCase);

            return newCase;
        }

        private void SaveCaseToDatabase(Case newCase)
        {
            // Simulate saving the case to a database
            Console.WriteLine($"Case {newCase.Id} saved to the database.");
        }

        private async Task SendEmailNotificationAsync(Case newCase)
        {
            var emailData = new
            {
                CaseId = newCase.Id,
                Subject = $"New Case Created: {newCase.Title}",
                Body = $"A new case has been created with the ID: {newCase.Id}"
            };

            var content = new StringContent(JsonSerializer.Serialize(emailData), System.Text.Encoding.UTF8, "application/json");

            // Simulate calling an Email Service
            var response = await _httpClient.PostAsync("http://emailservice/send", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Email notification sent successfully.");
            }
            else
            {
                Console.WriteLine("Failed to send email notification.");
            }
        }
    }
}
