export interface CompanyRegistrationDto {
  arabicName: string;
  englishName: string;
  email: string;
  phoneNumber?: string;
  websiteUrl?: string;
  logo?: File;
}

export interface OtpValidationDto {
  email: string;
  otpCode: string;
}

export interface SetPasswordDto {
  email: string;
  newPassword: string;
  confirmPassword: string;
}

export interface LoginDto {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  companyName: string;
  companyLogo: string;
  expiresAt: Date;
}

export interface CompanyProfile {
  id: number;
  arabicName: string;
  englishName: string;
  email: string;
  phoneNumber?: string;
  websiteUrl?: string;
  logoUrl?: string;
  createdAt: Date;
}

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data?: T;
  errors?: string[];
}
