import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import{AuthDto} from '../../Models/auth-dto';

@Injectable({
  providedIn: 'root'
})
@Injectable({ providedIn: 'root' })
export class AuthService {
  private baseUrl = 'http://localhost:5260/api/auth';

  constructor(private http: HttpClient) {}

  AdminLogin(username: string, password: string): Observable<any>{
    return this.http.post<any>(`${this.baseUrl}/login/admin`, { username, password });
  }

  customerLogin(username: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/login/customer`, { username, password });
  }

  registerAdmin(username: string, password: string): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/register/admin`, { username, password });
  }

  registerCustomer(username: string, password: string): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/register/customer`, { username, password });
  }
}
