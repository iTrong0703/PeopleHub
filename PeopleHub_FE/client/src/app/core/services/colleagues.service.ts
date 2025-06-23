import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Colleague } from '../models/colleague.model';
import { AuthService } from './auth.service';
import { PaginatedResult } from '../models/paginatedResult.model';

@Injectable({
  providedIn: 'root'
})
export class ColleaguesService {
  private http = inject(HttpClient);
  private authService = inject(AuthService);
  baseUrl = environment.apiUrl;

  getColleagues() {
    return this.http.get<PaginatedResult<Colleague>>(this.baseUrl + 'colleagues', this.getHttpOptions());
  }

  getColleaguesById(id: number) {
    return this.http.get<Colleague>(this.baseUrl + 'colleagues/' + id, this.getHttpOptions());
  }

  getHttpOptions() {
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.authService.currentUser()?.token}`
      })
    }
  }
}
