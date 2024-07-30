using System.Text.Json;
using CourtManagementBackend.Models;

public class EmailService
{
    private readonly HttpClient _httpClient;

    public EmailService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendEmailNotificationAsync(Case newCase)
    {
        var emailData = new
        {
            CaseId = newCase.Id,
            Subject = $"New Case Created: {newCase.Title}",
            Body = $"A new case has been created with the ID: {newCase.Id}"
        };

        var content = new StringContent(JsonSerializer.Serialize(emailData), System.Text.Encoding.UTF8, "application/json");

        try
        {
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
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }
}