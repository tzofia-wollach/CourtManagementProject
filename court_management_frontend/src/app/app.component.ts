import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CaseSearchComponent } from './case-search/case-search.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, CaseSearchComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'court-management-frontend';
}
