import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CompanyService } from '../../services/company.service';
import { CompanyProfile } from '../../models/company-registration.model';

@Component({
  selector: 'app-company-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './company-profile.component.html',
  styleUrl: './company-profile.component.css'
})
export class CompanyProfileComponent implements OnInit {
  companyProfile: CompanyProfile | null = null;
  isLoading = true;
  errorMessage = '';
  companyLogo = 'assets/images/default-logo.png'; // Default logo path

  constructor(
    private companyService: CompanyService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadProfile();
  }

  private loadProfile(): void {
    if (!this.companyService.isLoggedIn()) {
      this.router.navigate(['/login']);
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.companyService.getProfile().subscribe({
      next: (response) => {
        if (response.success && response.data) {
          this.companyProfile = response.data;
        } else {
          this.errorMessage = response.message || 'Failed to load profile';
        }
      },
      error: (error) => {
        console.error('Profile loading error:', error);
        if (error.status === 401) {
          // Token expired or invalid
          this.companyService.logout();
          this.router.navigate(['/login']);
        } else {
          this.errorMessage = error.error?.message || 'Failed to load profile';
        }
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  logout(): void {
    this.companyService.logout();
    this.router.navigate(['/login']);
  }
}
