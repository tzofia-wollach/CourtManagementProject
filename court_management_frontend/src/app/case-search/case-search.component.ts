import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { Case } from '../../models/case.model';

@Component({
  selector: 'app-case-search',
  standalone: true,
  imports: [CommonModule, FormsModule],
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
    },
    {
      caseNumber: '109/2024',
      title: 'תביעה כספית',
      openingDate: new Date('2024-07-10'),
      status: 'סגור',
      judge: 'משה משה',
    },
    // Add more hardcoded cases
  ];
    
  filteredCases: Case[] = [...this.cases];

  applyFilterAndSort() {
    this.filteredCases = this.cases
      .filter(c => {
        const matchesSearch = this.searchQuery === '' || c.title.includes(this.searchQuery);
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
