import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './app-header.component.html',
  styleUrl: './app-header.component.css'
})
export class AppHeaderComponent {
  isDarkTheme = false;

  constructor(public router: Router) {}

  toggleTheme() {
    this.isDarkTheme = !this.isDarkTheme;
    console.log("e dark ? ", this.isDarkTheme);
  }

  isLandingPage(): boolean {
    return !this.router.url.includes('login') && !this.router.url.includes('register');
  }
}
