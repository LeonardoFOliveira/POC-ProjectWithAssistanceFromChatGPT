import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService],
    });

    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call the API with the correct endpoint and credentials and return the token', () => {
    const credentials = { Cpf: '12345678900', Password: 'password' };
    const expectedResponse = { token: 'fakeToken' };

    service.login(credentials).subscribe((token) => {
      expect(token).toEqual(expectedResponse.token);
    });

    const req = httpMock.expectOne(`https://localhost:7049/Employee/login`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(credentials);
    req.flush(expectedResponse);
  });
});
