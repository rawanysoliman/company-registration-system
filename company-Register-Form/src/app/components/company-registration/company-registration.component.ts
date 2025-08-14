import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CompanyService } from '../../services/company.service';
import { CompanyRegistrationDto } from '../../models/company-registration.model';

@Component({
  selector: 'app-company-registration',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './company-registration.component.html',
  styleUrl: './company-registration.component.css'
})
export class CompanyRegistrationComponent implements OnInit {
  registrationForm!: FormGroup;
  isLoading = false;
  logoPreview: string | null = null;
  logoError = false;
  logoErrorMessage = '';
  successMessage = '';
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
    this.registrationForm = this.fb.group({
      arabicName: ['', [Validators.required, Validators.minLength(2)]],
      englishName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.pattern(/^(\+?[0-9\s\-\(\)]{10,})$/)]],
      websiteUrl: ['', [Validators.pattern(/^https?:\/\/.+/)]]
    });
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      // Validate file type
      if (!file.type.startsWith('image/')) {
        this.logoError = true;
        this.logoErrorMessage = 'Please select a valid image file';
        return;
      }

      // Validate file size (5MB limit)
      if (file.size > 5 * 1024 * 1024) {
        this.logoError = true;
        this.logoErrorMessage = 'File size must be less than 5MB';
        return;
      }

      this.logoError = false;
      this.logoErrorMessage = '';

      // Create preview
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.logoPreview = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }

  removeLogo(): void {
    this.logoPreview = null;
    this.logoError = false;
    this.logoErrorMessage = '';
    const fileInput = document.getElementById('logo') as HTMLInputElement;
    if (fileInput) {
      fileInput.value = '';
    }
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.registrationForm.get(fieldName);
    return field ? field.invalid && field.touched : false;
  }

  getErrorMessage(fieldName: string): string {
    const field = this.registrationForm.get(fieldName);
    if (!field) return '';

    if (field.hasError('required')) {
      return `${this.getFieldDisplayName(fieldName)} is required`;
    }
    if (field.hasError('email')) {
      return 'Please enter a valid email address';
    }
    if (field.hasError('minlength')) {
      return `${this.getFieldDisplayName(fieldName)} must be at least ${field.errors?.['minlength'].requiredLength} characters`;
    }
    if (field.hasError('pattern')) {
      if (fieldName === 'phoneNumber') {
        return 'Please enter a valid phone number';
      }
      if (fieldName === 'websiteUrl') {
        return 'Please enter a valid URL starting with http:// or https://';
      }
    }

    return 'Invalid input';
  }

  private getFieldDisplayName(fieldName: string): string {
    const displayNames: { [key: string]: string } = {
      arabicName: 'Company Arabic Name',
      englishName: 'Company English Name',
      email: 'Email Address',
      phoneNumber: 'Phone Number',
      websiteUrl: 'Website URL'
    };
    return displayNames[fieldName] || fieldName;
  }

  onSubmit(): void {
    if (this.registrationForm.invalid) {
      this.markFormGroupTouched();
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    const formData = new FormData();
    formData.append('arabicName', this.registrationForm.get('arabicName')?.value);
    formData.append('englishName', this.registrationForm.get('englishName')?.value);
    formData.append('email', this.registrationForm.get('email')?.value);
    
    if (this.registrationForm.get('phoneNumber')?.value) {
      formData.append('phoneNumber', this.registrationForm.get('phoneNumber')?.value);
    }
    
    if (this.registrationForm.get('websiteUrl')?.value) {
      formData.append('websiteUrl', this.registrationForm.get('websiteUrl')?.value);
    }

    // Add logo if selected
    const fileInput = document.getElementById('logo') as HTMLInputElement;
    if (fileInput?.files?.[0]) {
      formData.append('logo', fileInput.files[0]);
    }

    this.companyService.registerCompany(formData).subscribe({
      next: (response) => {
        if (response.success) {
          this.successMessage = response.message;
          // Store email for OTP validation
          localStorage.setItem('pendingEmail', this.registrationForm.get('email')?.value);
          // Navigate to OTP validation after a short delay
          setTimeout(() => {
            this.router.navigate(['/otp-validation']);
          }, 2000);
        } else {
          this.errorMessage = response.message || 'Registration failed';
        }
      },
      error: (error) => {
        console.error('Registration error:', error);
        this.errorMessage = error.error?.message || 'An error occurred during registration';
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  private markFormGroupTouched(): void {
    Object.keys(this.registrationForm.controls).forEach(key => {
      const control = this.registrationForm.get(key);
      control?.markAsTouched();
    });
  }
}
