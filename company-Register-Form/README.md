# Company Registration System - Frontend

This is the Angular frontend for the Company Registration System, providing a modern, responsive user interface for company registration, OTP validation, password setup, and login functionality.

## 🚀 Features

- **Company Registration**: Multi-step registration form with logo upload
- **OTP Validation**: Secure email verification with resend functionality
- **Password Setup**: Strong password requirements with real-time validation
- **User Login**: Secure authentication with JWT tokens
- **Company Profile**: Dashboard displaying company information
- **Responsive Design**: Mobile-first approach with Bootstrap 5
- **Modern UI**: Beautiful gradients and smooth animations

## 🛠️ Technology Stack

- **Angular 17**: Latest Angular framework with standalone components
- **TypeScript**: Type-safe JavaScript development
- **Bootstrap 5**: Modern CSS framework for responsive design
- **Bootstrap Icons**: Beautiful icon library
- **RxJS**: Reactive programming for async operations
- **Angular Forms**: Reactive forms with validation

## 📁 Project Structure

```
src/
├── app/
│   ├── components/
│   │   ├── company-registration/     # Company signup form
│   │   ├── login/                    # User login form
│   │   ├── otp-validation/          # OTP verification
│   │   ├── set-password/            # Password setup
│   │   └── company-profile/         # Company dashboard
│   ├── models/                       # TypeScript interfaces
│   ├── services/                     # HTTP services
│   ├── app.component.ts              # Main app component
│   ├── app.routes.ts                 # Routing configuration
│   └── app.config.ts                 # App configuration
├── assets/                           # Static assets
├── styles.css                        # Global styles
├── main.ts                           # Application entry point
└── index.html                        # Main HTML file
```

## 🚀 Getting Started

### Prerequisites

- Node.js (v18 or higher)
- npm (v9 or higher)
- Angular CLI (v17)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd company-Register-Form
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Start development server**
   ```bash
   npm start
   ```

4. **Open your browser**
   Navigate to `http://localhost:4200`

### Build for Production

```bash
npm run build
```


