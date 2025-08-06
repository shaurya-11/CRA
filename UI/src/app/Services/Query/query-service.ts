import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AdminPatchQueryDto } from '../../Models/admin-patch-query-dto';
import { PatchNotificationQueryDto } from '../../Models/patch-notification-query';

@Injectable({
  providedIn: 'root'
})
export class QueryService {
  private apiUrl = 'http://localhost:5260/api/query'; // ✅ Update this if your URL is different

  constructor(private http: HttpClient) {}

  // Example: Get details for one customer
  getCustomerProductDetails(customerId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${customerId}/patch-details`);
  }
  getAdminPatchDetails(): Observable<AdminPatchQueryDto[]> {
    return this.http.get<AdminPatchQueryDto[]>(`${this.apiUrl}/Admin/patches`);
  }
  getPendingPatches(customerId: number): Observable<PatchNotificationQueryDto[]> {
    return this.http.get<PatchNotificationQueryDto[]>(`${this.apiUrl}/notification/${customerId}`);
  }
}