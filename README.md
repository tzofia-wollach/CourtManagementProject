# Court Management System

## Project Overview

The Court Management System is designed to manage court cases, including involved parties, documents, decisions, hearings, interim requests, and more. The system's lifecycle includes case creation by clients, review by court clerks, hearings, decisions, and notifications to relevant parties.

## Architecture

The system is based on a microservices architecture, where each service is responsible for a specific part of the system. The primary services include:

- **User Management Service**: Manages user registration, authentication, and details.
- **Case Management Service**: Handles case creation, retrieval, updates, and deletions.
- **Document Management Service**: Manages the upload, retrieval, and deletion of case-related documents.
- **Discussion Management Service**: Schedules and manages discussions related to cases.
- **Decision Management Service**: Manages decisions made in relation to cases.
- **Email Service**: Sends notifications and updates to relevant parties.

Each service interacts with others using synchronous and asynchronous communication as needed.

## Microservices

### 1. User Management Service
Handles user-related functionalities, including registration and authentication.

### 2. Case Management Service
Responsible for all case-related operations.

#### Main Function Signatures
- `Task<Case> CreateCaseAsync(CaseData caseData);`
- `Task<Case> GetCaseAsync(Guid caseId);`
- `Task<List<Case>> GetCasesAsync();`
- `Task UpdateCaseAsync(Guid caseId, CaseUpdateData updateData);`
- `Task DeleteCaseAsync(Guid caseId);`

### 3. Document Management Service
Manages all document-related operations.

#### Main Function Signatures
- `Task<Document> UploadDocumentAsync(DocumentData documentData);`
- `Task<Document> GetDocumentAsync(Guid documentId);`
- `Task<List<Document>> GetDocumentsAsync(Guid caseId);`
- `Task DeleteDocumentAsync(Guid documentId);`

### 4. Discussion Management Service
Handles scheduling and management of case discussions.

#### Main Function Signatures
- `Task<Discussion> ScheduleDiscussionAsync(DiscussionData discussionData);`
- `Task<Discussion> GetDiscussionAsync(Guid discussionId);`
- `Task<List<Discussion>> GetDiscussionsAsync(Guid caseId);`
- `Task UpdateDiscussionAsync(Guid discussionId, DiscussionUpdateData updateData);`
- `Task DeleteDiscussionAsync(Guid discussionId);`

### 5. Decision Management Service
Manages decisions related to cases.

#### Main Function Signatures
- `Task<Decision> AddDecisionAsync(DecisionData decisionData);`
- `Task<Decision> GetDecisionAsync(Guid decisionId);`
- `Task<List<Decision>> GetDecisionsAsync(Guid caseId);`
- `Task UpdateDecisionAsync(Guid decisionId, DecisionUpdateData updateData);`
- `Task DeleteDecisionAsync(Guid decisionId);`

### 6. Email Service
Responsible for sending email notifications.

#### Main Function Signatures
- `Task SendEmailAsync(EmailData emailData);`

## Case Management Service Example

### Implementation Details

- **Case Creation**: When a new case is created, an email notification is sent to relevant authorities. This is done asynchronously using the Email Service.
- **Save and Notify**: The service saves the new case information and triggers the email notification.

```csharp
public async Task<Case> CreateCaseAsync(CaseData caseData)
{
    var newCase = new Case
    {
        Id = Guid.NewGuid(),
        Title = caseData.Title,
        Description = caseData.Description,
        CreatedAt = DateTime.UtcNow,
        Stage = CaseStage.Created
    };

    SaveCaseToDatabase(newCase);
    await _emailService.SendEmailAsync(new EmailData
    {
        CaseId = newCase.Id,
        Subject = $"New Case Created: {newCase.Title}",
        Body = $"A new case has been created with the ID: {newCase.Id}"
    });

    return newCase;
}
```

## Frontend

The frontend is developed using Angular. The primary functionalities include case searching, filtering, and sorting. The case search screen allows users to:

- Sort cases by opening date or case number.
- Filter cases by "all," "my cases," "active cases," or "closed cases."
- Display a list of cases based on the criteria.

### Key Components

#### Case Search Component

Handles the display and functionality of the case search screen.

```typescript
@Component({
  selector: 'app-case-search',
  templateUrl: './case-search.component.html',
  styleUrls: ['./case-search.component.css']
})
export class CaseSearchComponent {
  searchQuery = '';
  filter = 'all';
  sort = 'openingDate';
  cases: Case[] = [
    {
      caseNumber: '108/2024',
      title: 'ערעור תביעה משפטית',
      openingDate: new Date('2024-07-15'),
      status: 'פתוח',
      judge: 'ישראל ישראלי',
      sideName: 'יוסי כהן'
    },
    // Additional hardcoded cases
  ];

  filteredCases: Case[] = [...this.cases];

  applyFilterAndSort() {
    this.filteredCases = this.cases
      .filter(c => {
        const matchesSearch = this.searchQuery === '' ||
          c.caseNumber.includes(this.searchQuery) ||
          c.sideName.includes(this.searchQuery);
        const matchesFilter =
          this.filter === 'all' ||
          (this.filter === 'myCases' && c.judge === 'ישראל ישראלי') ||
          (this.filter === 'active' && c.status === 'פתוח') ||
          (this.filter === 'closed' && c.status === 'סגור');
        return matchesSearch && matchesFilter;
      })
      .sort((a, b) => {
        if (this.sort === 'openingDate') {
          return a.openingDate.getTime() - b.openingDate.getTime();
        } else if (this.sort === 'caseNumber') {
          return a.caseNumber.localeCompare(b.caseNumber);
        }
        return 0;
      });
  }

  onFilterChange(filter: string) {
    this.filter = filter;
    this.applyFilterAndSort();
  }

  onSortChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    const value = target.value;
    this.sort = value;
    this.applyFilterAndSort();
  }

  onSearch() {
    this.applyFilterAndSort();
  }
}
```

## Running the Project

### Backend

1. **Navigate** to the `CourtManagementBackend` directory.
2. **Build and run** the project using your preferred .NET environment or IDE.

### Frontend

1. **Navigate** to the `court_management_frontend` directory.
2. **Install dependencies** using `npm install`.
3. **Run the application** using `ng serve`.

## Future Improvements

- **Database Integration**: Implement a database to store and manage data.
- **Authentication**: Add user authentication and authorization.
- **Advanced Filtering**: Implement more complex filtering options and search capabilities.

## License

This project is licensed under the MIT License.

---

This README provides an overview of the system, its architecture, main components, and instructions on how to run the project. Further documentation and details should be added as the project evolves.
