# Company Registration System

A full-stack web application for company registration with email verification, OTP validation, and user authentication.

## 🏗️ Architecture

- **Backend**: ASP.NET Core 8.0 Web API with Entity Framework Core
- **Frontend**: Angular 17 with Bootstrap 5
- **Database**: PostgreSQL
- **Authentication**: JWT Tokens
- **Email Service**: Mailtrap (development) / SMTP (production)

## 📁 Project Structure

```
Task/
├── CompanyRegisterationForm/          # Backend API
│   ├── Controllers/                   # API endpoints
│   ├── Data/                         # Entity Framework
│   ├── Models/                       # DTOs, ViewModels, Validation
│   ├── Repository/                   # Data access layer
│   ├── Services/                     # Business logic
│   └── wwwroot/uploads/logos/        # File storage
└── company-Register-Form/            # Frontend Angular app
    └── src/app/
        ├── components/               # Angular components
        ├── services/                 # HTTP services
        └── models/                   # TypeScript interfaces
```

## 🚀 Features

### Backend Features
- ✅ Company registration with logo upload
- ✅ Email verification via OTP
- ✅ Password complexity validation
- ✅ JWT authentication
- ✅ File upload handling
- ✅ Email notifications (OTP, Welcome)
- ✅ PostgreSQL database integration
- ✅ Repository pattern implementation

### Frontend Features
- ✅ Responsive Bootstrap UI
- ✅ Multi-step registration flow
- ✅ Real-time form validation
- ✅ File upload with preview
- ✅ OTP input with auto-focus
- ✅ Password strength indicator
- ✅ Protected routes with JWT
- ✅ Error handling and user feedback

## 🛠️ Prerequisites

- .NET 8.0 SDK
- Node.js 18+ and npm
- PostgreSQL database
- Mailtrap account (for email testing)

## 📦 Installation

### Backend Setup

1. **Clone and navigate to backend:**
```bash
cd CompanyRegisterationForm
```

2. **Install dependencies:**
```bash
dotnet restore
```

3. **Configure database:**
   - Update connection string in `appsettings.json`
   - Run migrations:
```bash
dotnet ef database update
```

4. **Configure email settings:**
   - Update Mailtrap credentials in `appsettings.json`
   - For production, replace with your SMTP settings

5. **Run the application:**
```bash
dotnet run
```

The API will be available at `http://localhost:5133`

### Frontend Setup

1. **Navigate to frontend:**
```bash
cd company-Register-Form
```

2. **Install dependencies:**
```bash
npm install
```

3. **Run the application:**
```bash
npm start
```

The app will be available at `http://localhost:4200`

## 🔧 Configuration

### Backend Configuration (`appsettings.json`)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=CompanyRegistrationDB;Username=postgres;Password=your_password"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyHereThatShouldBeAtLeast32CharactersLong",
    "Issuer": "CompanyRegistrationAPI",
    "Audience": "CompanyRegistrationClient"
  },
  "Mailtrap": {
    "Host": "sandbox.smtp.mailtrap.io",
    "Port": 587,
    "Username": "your_username",
    "Password": "your_password",
    "EnableSsl": true
  },
  "BaseUrl": "http://localhost:5133"
}
```

### Frontend Configuration

Update the API base URL in `src/app/services/company.service.ts`:
```typescript
private baseUrl = 'http://localhost:5133/api/company';
```

## 📋 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/company/register` | Register new company |
| POST | `/api/company/validate-otp` | Validate OTP code |
| POST | `/api/company/set-password` | Set company password |
| POST | `/api/company/resend-otp` | Resend OTP email |
| POST | `/api/company/login` | Company login |
| GET | `/api/company/profile` | Get company profile (protected) |

## 🔐 Security Features

- **Password Requirements:**
  - Minimum 6 characters
  - At least one capital letter
  - At least one number
  - At least one special character (@$!%*?&)

- **OTP Security:**
  - 6-digit numeric codes
  - 10-minute expiration
  - In-memory storage (cleared on app restart)
  - Rate limiting on resend requests

- **JWT Authentication:**
  - 24-hour token expiration
  - Company ID claims
  - Secure token validation

## 📧 Email Flow

1. **Registration**: OTP sent to company email
2. **Password Setup**: Welcome email sent after successful registration
3. **Resend**: New OTP can be requested with 30-second cooldown

## 🗄️ Database Schema

### Company Table
- `Id` (Primary Key)
- `ArabicName`, `EnglishName`
- `Email`, `PhoneNumber`, `WebsiteUrl`
- `LogoPath`, `PasswordHash`
- `OtpCode`, `OtpExpiry`
- `IsEmailVerified`
- `CreatedAt`

## 🧪 Testing

### Backend Testing
```bash
cd CompanyRegisterationForm
dotnet test
```

### Frontend Testing
```bash
cd company-Register-Form
ng test
```

## 🚀 Deployment

### Backend Deployment
1. Build the application:
```bash
dotnet publish -c Release
```

2. Deploy to your hosting platform (Azure, AWS, etc.)

### Frontend Deployment
1. Build for production:
```bash
ng build --configuration production
```

2. Deploy the `dist/` folder to your hosting platform

## 🔍 Troubleshooting

### Common Issues

**OTP Validation Fails:**
- Check if OTP has expired (10 minutes)
- Ensure backend is running
- Verify email configuration

**File Upload Issues:**
- Check `wwwroot/uploads/logos/` directory exists
- Verify file size limits (5MB max)
- Check file type restrictions

**Database Connection:**
- Verify PostgreSQL is running
- Check connection string in `appsettings.json`
- Run `dotnet ef database update` if needed

**CORS Issues:**
- Backend CORS is configured for `http://localhost:4200`
- Update CORS policy for production domains

## 📝 Development Notes

- **OTP Storage**: Currently in-memory (not persistent across restarts)
- **File Storage**: Local file system (consider cloud storage for production)
- **Email**: Uses Mailtrap for development (replace with production SMTP)
- **Logging**: Console logging (implement proper logging for production)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 👥 Support

For support and questions:
- Check the troubleshooting section
- Review the API documentation
- Open an issue on GitHub
