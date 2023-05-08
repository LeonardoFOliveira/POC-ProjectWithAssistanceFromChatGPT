import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequestDto } from '../models/login-request.dto';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl: string = 'https://localhost:7049';

  constructor(private httpClient: HttpClient) { }

  login(loginRequest: LoginRequestDto): Observable<string> {
    return this.httpClient
      .post<{ token: string }>(`${this.apiUrl}/Employee/login`, loginRequest)
      .pipe(map((response) => response.token));
  }

  setToken(token: string): void {
    localStorage.setItem('jwtToken', token);
  }
}