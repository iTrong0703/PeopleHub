import { Component, inject, output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private authService = inject(AuthService);
  private toastr = inject(ToastrService);
  private router = inject(Router);
  passwordMismatch = false;
  registerData = {
    username: '',
    password: '',
    confirmPassword: '',
    email: '',
    fullName: '',
    dateOfBirth: ''
  };

  onRegister(form: NgForm) {
    this.passwordMismatch = this.registerData.password !== this.registerData.confirmPassword

    if (form.invalid || this.passwordMismatch) {
      return;
    }

    const { username, password, email, fullName, dateOfBirth } = this.registerData;

    this.authService.register({ username, password, email, fullName, dateOfBirth }).subscribe({
      next: response => {
        this.toastr.success("Please login!!!", "Register successfully");
        form.resetForm();
        this.router.navigate(['/login']);
      },
      error: err => this.toastr.error("Check lại đi cưng", "Lỗi nè hihi")
    })

  }
  onFocus() {
    this.passwordMismatch = false;
  }
  hasError(form: NgForm, fieldName: string, errorType: string): boolean {
    const field = form.controls[fieldName];
    return !!(field?.hasError(errorType) && field?.touched);
  }
}
