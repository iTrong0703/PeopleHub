import { ColleagueCardComponent } from './../colleague-card/colleague-card.component';
import { Component, inject, OnInit } from '@angular/core';
import { ColleaguesService } from '../../../core/services/colleagues.service';
import { Colleague } from '../../../core/models/colleague.model';

@Component({
  selector: 'app-colleague-list',
  standalone: true,
  imports: [ColleagueCardComponent],
  templateUrl: './colleague-list.component.html',
  styleUrl: './colleague-list.component.css'
})
export class ColleagueListComponent implements OnInit {
  private colleaguesService = inject(ColleaguesService);
  colleagues: Colleague[] = [];

  ngOnInit(): void {
    this.loadColleagues();
  }
  loadColleagues() {
    this.colleaguesService.getColleagues().subscribe({
      next: response => this.colleagues = response.items
    })
  }
}
