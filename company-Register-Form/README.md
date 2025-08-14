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

The build artifacts will be stored in the `dist/` directory.

## 🔧 Configuration

### Backend API URL

Update the backend URL in `src/app/services/company.service.ts`:

```typescript
private baseUrl = 'http://localhost:5133/api/company';
```

### Environment Variables

Create environment files for different configurations:

- `src/environments/environment.ts` (development)
- `src/environments/environment.prod.ts` (production)

## 📱 Component Details

### Company Registration Component
- Form validation for company details
- Logo upload with preview
- File type and size validation
- Responsive grid layout

### OTP Validation Component
- 6-digit OTP input
- Auto-submit on completion
- Resend functionality with countdown
- Error handling

### Set Password Component
- Password complexity validation
- Real-time requirement checking
- Password confirmation matching
- Visual feedback

### Login Component
- Email and password validation
- JWT token storage
- Error handling
- Navigation to profile

### Company Profile Component
- Protected route
- Company information display
- Logo display
- Logout functionality

## 🎨 Styling

- **CSS Variables**: Consistent color scheme
- **Gradients**: Modern gradient backgrounds
- **Animations**: Smooth transitions and hover effects
- **Responsive**: Mobile-first responsive design
- **Bootstrap**: Utility classes and components

## 🔒 Security Features

- **JWT Authentication**: Secure token-based auth
- **Form Validation**: Client-side validation
- **Route Guards**: Protected routes
- **Input Sanitization**: XSS prevention
- **HTTPS Ready**: Secure communication

## 🧪 Testing

```bash
# Run unit tests
npm test

# Run end-to-end tests
npm run e2e
```

## 📦 Build & Deployment

### Development Build
```bash
npm run build
```

### Production Build
```bash
npm run build --configuration production
```

### Docker Deployment
```dockerfile
FROM nginx:alpine
COPY dist/company-register-form /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

## 🌐 Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)
- Mobile browsers

## 📝 API Integration

The frontend integrates with the following backend endpoints:

- `POST /api/company/register` - Company registration
- `POST /api/company/validate-otp` - OTP validation
- `POST /api/company/resend-otp` - Resend OTP
- `POST /api/company/set-password` - Set password
- `POST /api/company/login` - User login
- `GET /api/company/profile` - Get company profile

## 🐛 Troubleshooting

### Common Issues

1. **Port already in use**
   ```bash
   # Kill process using port 4200
   npx kill-port 4200
   ```

2. **Dependencies not found**
   ```bash
   # Clear npm cache and reinstall
   npm cache clean --force
   rm -rf node_modules package-lock.json
   npm install
   ```

3. **Build errors**
   ```bash
   # Clear Angular cache
   ng cache clean
   ```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License.

## 📞 Support

For support and questions:
- Create an issue in the repository
- Contact the development team
- Check the documentation

---

**Happy Coding! 🎉**
