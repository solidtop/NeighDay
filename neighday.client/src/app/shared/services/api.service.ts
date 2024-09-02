import { Injectable, inject } from '@angular/core';
import { enviroment } from '../../../enviroment';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  protected readonly http = inject(HttpClient);
  protected readonly apiUrl = enviroment.apiUrl;
  protected readonly headers = new HttpHeaders({
    'Content-type': 'application/json',
  });
}
