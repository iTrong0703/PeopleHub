import { Component, inject, input, ViewEncapsulation } from '@angular/core';
import { Colleague } from '../../../core/models/colleague.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-colleague-card',
  standalone: true,
  imports: [],
  templateUrl: './colleague-card.component.html',
  styleUrl: './colleague-card.component.css',
  encapsulation: ViewEncapsulation.None
})
export class ColleagueCardComponent {
  private router = inject(Router);
  colleague = input.required<Colleague>();
}
