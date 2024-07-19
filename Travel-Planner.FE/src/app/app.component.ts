import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Travel Planner';
  
  constructor(public router: Router) {}

  isNotLandingPage(): boolean {
    return this.router.url !== '/welcome';
  }
}
