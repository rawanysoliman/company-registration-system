import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CompanyService } from '../../services/company.service';

@Component({
  selector: 'app-set-password',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './set-password.component.html',
  styleUrl: './set-password.component.css'
})
export class SetPasswordComponent implements OnInit {
  passwordForm!: FormGroup;
  isLoading = false;
  showPassword = false;
  showConfirmPassword = false;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private companyService: CompanyService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.initForm();
  }

  private initForm(): void {
    this.passwordForm = this.fb.group({
      newPassword: ['', [
        Validators.required,
        Validators.minLength(6),
        this.passwordComplexityValidator
      ]],
      confirmPassword: ['', [Validators.required]]
    }, { validators: this.passwordMatchValidator });
  }

  private passwordComplexityValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.value;
    if (!password) return null;

    const hasCapital = /[A-Z]/.test(password);
    const hasNumber = /\d/.test(password);
    const hasSpecialChar = /[@$!%*?&]/.test(password);

    if (!hasCapital || !hasNumber || !hasSpecialChar) {
      return { complexity: true };
    }

    return null;
  }

  private passwordMatchValidator(group: AbstractControl): ValidationErrors | null {
    const newPassword = group.get('newPassword');
    const confirmPassword = group.get('confirmPassword');

    if (newPassword && confirmPassword && newPassword.value !== confirmPassword.value) {
      return { passwordMismatch: true };
    }

    return null;
  }

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  toggleConfirmPassword(): void {
    this.showConfirmPassword = !this.showConfirmPassword;
  }

  hasMinLength(): boolean {
    const password = this.passwordForm.get('newPassword')?.value;
    return password && password.length >= 6;
  }

  hasCapital(): boolean {
    const password = this.passwordForm.get('newPassword')?.value;
    return password && /[A-Z]/.test(password);
  }

  hasNumber(): boolean {
    const password = this.passwordForm.get('newPassword')?.value;
    return password && /\d/.test(password);
  }

  hasSpecialChar(): boolean {
    const password = this.passwordForm.get('newPassword')?.value;
    return password && /[@$!%*?&]/.test(password);
  }

  onSubmit(): void {
    if (this.passwordForm.invalid) {
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

    const passwordData = {
      email: email,
      newPassword: this.passwordForm.get('newPassword')?.value,
      confirmPassword: this.passwordForm.get('confirmPassword')?.value
    };

    this.companyService.setPassword(passwordData).subscribe({
      next: (response) => {
        if (response.success) {
          // Clear pending email
          localStorage.removeItem('pendingEmail');
          // Navigate to login
          this.router.navigate(['/login']);
        } else {
          this.errorMessage = response.message || 'Failed to set password';
        }
      },
      error: (error) => {
        console.error('Set password error:', error);
        this.errorMessage = error.error?.message || 'Failed to set password';
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  private markFormGroupTouched(): void {
    Object.keys(this.passwordForm.controls).forEach(key => {
      const control = this.passwordForm.get(key);
      control?.markAsTouched();
    });
  }
}
