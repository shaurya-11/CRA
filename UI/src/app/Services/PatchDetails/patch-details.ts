// src/app/Services/PatchDetails/patch-details.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatchDetailsService {
  private apiUrl = 'http://localhost:5260/api/patch';

  constructor(private http: HttpClient) {}

  getPatches(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  addPatch(patch: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, patch);
  }
}
