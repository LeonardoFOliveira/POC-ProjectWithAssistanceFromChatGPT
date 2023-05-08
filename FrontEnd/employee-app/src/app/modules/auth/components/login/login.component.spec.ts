import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { LoginComponent } from './login.component';
import { AuthService } from '../../services/auth.service';
import { of, throwError } from 'rxjs';
import { LoginRequestDto } from '../../models/login-request.dto';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: AuthService;
  let httpTestingController: HttpTestingController;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        FormsModule
      ],
      declarations: [ LoginComponent ],
      providers: [ AuthService ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    authService = TestBed.inject(AuthService);
    httpTestingController = TestBed.inject(HttpTestingController);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should log in successfully and navigate to the home page', () => {
    const loginRequest: LoginRequestDto = { Cpf: '12345678900', Password: 'password' };
    const token = 'GeneratedJwtToken';
    spyOn(authService, 'login').and.returnValue(of(token));
    spyOn(authService, 'setToken');
    spyOn(router, 'navigate');

    component.loginRequest = loginRequest;
    component.onSubmit();

    expect(authService.login).toHaveBeenCalledWith(loginRequest);
    expect(authService.setToken).toHaveBeenCalledWith(token);
    expect(router.navigate).toHaveBeenCalledWith(['/']);
  });

  it('should display an error message when login fails', () => {
    const loginRequest: LoginRequestDto = { Cpf: '12345678900', Password: 'wrong_password' };
    spyOn(authService, 'login').and.returnValue(throwError({ error: { errorMessage: 'Error' } }));

    component.loginRequest = loginRequest;
    component.onSubmit();

    expect(component.errorMessage).toBe('Error');
  });

  afterEach(() => {
    httpTestingController.verify();
  });
});