import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CompanyService } from '../../services/company.service';
import { interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-otp-validation',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './otp-validation.component.html',
  styleUrl: './otp-validation.component.css'
})
export class OtpValidationComponent implements OnInit, OnDestroy {
  otpForm!: FormGroup;
  isLoading = false;
  countdown = 0;
  errorMessage = '';
  private countdownSubscription?: Subscription;

  constructor(
    private fb: FormBuilder,
    private companyService: CompanyService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.startCountdown();
  }

  ngOnDestroy(): void {
    if (this.countdownSubscription) {
      this.countdownSubscription.unsubscribe();
    }
  }

  private initForm(): void {
    this.otpForm = this.fb.group({
      otpCode: ['', [Validators.required, Validators.pattern(/^\d{6}$/)]]
    });
  }

  onOtpInput(event: any): void {
    const input = event.target;
    const value = input.value.replace(/\D/g, ''); // Remove non-digits
    input.value = value;
    
    // Auto-submit when 6 digits are entered
    if (value.length === 6) {
      this.onSubmit();
    }
  }

  startCountdown(): void {
    this.countdown = 60; // 60 seconds
    this.countdownSubscription = interval(1000).subscribe(() => {
      if (this.countdown > 0) {
        this.countdown--;
      } else {
        this.countdownSubscription?.unsubscribe();
      }
    });
  }

  resendOtp(): void {
    const email = localStorage.getItem('pendingEmail');
    if (!email) {
      this.errorMessage = 'Email not found. Please register again.';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.companyService.resendOtp(email).subscribe({
      next: (response) => {
        if (response.success) {
          this.startCountdown();
        } else {
          this.errorMessage = response.message || 'Failed to resend OTP';
        }
      },
      error: (error) => {
        console.error('Resend OTP error:', error);
        this.errorMessage = error.error?.message || 'Failed to resend OTP';
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.otpForm.invalid) {
      this.markFormGroupTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    const email = localStorage.getItem('pendingEmail');
    if (!email) {
      this.errorMessage = 'Email not found. Please register again.';
      this.isLoading = false;
      return;
    }

    const otpData = {
      email: email,
      otpCode: this.otpForm.get('otpCode')?.value
    };

    this.companyService.validateOtp(otpData).subscribe({
      next: (response) => {
        if (response.success) {
          // Navigate to set password
          this.router.navigate(['/set-password']);
        } else {
          this.errorMessage = response.message || 'Invalid OTP code';
        }
      },
      error: (error) => {
        console.error('OTP validation error:', error);
        this.errorMessage = error.error?.message || 'Failed to validate OTP';
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  private markFormGroupTouched(): void {
    Object.keys(this.otpForm.controls).forEach(key => {
      const control = this.otpForm.get(key);
      control?.markAsTouched();
    });
  }
}
