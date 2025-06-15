import { Component, inject, output } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { AuthService } from '../../../core/services/auth.service';
import { RouterLink } from '@angular/router';
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
  passwordMismatch = false;
  registerData = {
    username: '',
    password: '',
    confirmPassword: ''
  };

  onRegister(form: NgForm) {
    this.passwordMismatch = this.registerData.password !== this.registerData.confirmPassword
    if (form.valid && !this.passwordMismatch) {
      const { username, password } = this.registerData;
      this.authService.register({ username, password }).subscribe({
        next: response => {
          this.toastr.success("Please login!!!", "Register successfully")
        },
        error: err => this.toastr.error("Check lại đi cưng", "Lỗi nè hihi")
      })
    } else {
      this.toastr.error('Passwords do not match.');
    }
  }
  onFocus() {
    this.passwordMismatch = false;
  }
}
