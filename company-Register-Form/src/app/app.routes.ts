import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: '/register', pathMatch: 'full' },
  { path: 'register', loadComponent: () => import('./components/company-registration/company-registration.component').then(m => m.CompanyRegistrationComponent) },
  { path: 'otp-validation', loadComponent: () => import('./components/otp-validation/otp-validation.component').then(m => m.OtpValidationComponent) },
  { path: 'set-password', loadComponent: () => import('./components/set-password/set-password.component').then(m => m.SetPasswordComponent) },
  { path: 'login', loadComponent: () => import('./components/login/login.component').then(m => m.LoginComponent) },
  { path: 'profile', loadComponent: () => import('./components/company-profile/company-profile.component').then(m => m.CompanyProfileComponent) }
];
