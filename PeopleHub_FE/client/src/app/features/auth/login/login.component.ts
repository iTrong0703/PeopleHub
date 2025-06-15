import { Component, inject } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  authService = inject(AuthService);
  private router = inject(Router);
  private toastr = inject(ToastrService);
  loginData = {
    username: '',
    password: ''
  };
  onLogin() {
    this.authService.login(this.loginData).subscribe({
      next: _ => {
        this.router.navigate(['/colleagues']);
        // this.router.navigate(['/dashboard']);
        this.toastr.success("Welcome!!!", "Login successfully")
      },
      error: err => this.toastr.error("Check lại đi cưng", "Lỗi nè hihi")
    })
  }

}
