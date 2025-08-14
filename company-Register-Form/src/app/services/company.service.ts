import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { 
  CompanyRegistrationDto, 
  LoginDto, 
  OtpValidationDto, 
  SetPasswordDto, 
  LoginResponse, 
  ApiResponse, 
  CompanyProfile 
} from '../models/company-registration.model';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private baseUrl = 'http://localhost:5133/api/company';

  constructor(private http: HttpClient) { }

  registerCompany(registrationData: FormData): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(`${this.baseUrl}/register`, registrationData);
  }

  validateOtp(otpData: OtpValidationDto): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(`${this.baseUrl}/validate-otp`, otpData);
  }

  resendOtp(email: string): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(`${this.baseUrl}/resend-otp`, { email });
  }

  setPassword(passwordData: SetPasswordDto): Observable<ApiResponse<string>> {
    return this.http.post<ApiResponse<string>>(`${this.baseUrl}/set-password`, passwordData);
  }

  login(loginData: LoginDto): Observable<ApiResponse<LoginResponse>> {
    return this.http.post<ApiResponse<LoginResponse>>(`${this.baseUrl}/login`, loginData);
  }

  getProfile(): Observable<ApiResponse<CompanyProfile>> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    
    return this.http.get<ApiResponse<CompanyProfile>>(`${this.baseUrl}/profile`, { headers });
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    if (!token) return false;
    
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.exp * 1000 > Date.now();
    } catch {
      return false;
    }
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('companyName');
    localStorage.removeItem('companyLogo');
  }
}
