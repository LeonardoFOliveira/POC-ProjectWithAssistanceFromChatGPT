import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginRequestDto } from '../../models/login-request.dto';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  loginRequest: LoginRequestDto = { Cpf: '', Password: '' };
  errorMessage: string | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {}

  onSubmit(): void {
    this.authService.login(this.loginRequest).subscribe(
      (token: string) => {
        this.authService.setToken(token);
        this.router.navigate(['/']);
      },
      (error) => {
        this.errorMessage = error.error.errorMessage;
      }
    );
  }
}