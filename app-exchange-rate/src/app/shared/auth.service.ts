import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) {

  }

  login() {
      return this.http.post<User>(`${environment.apiUrl}/api/login`, {"username": "USERAPP"} )
      .subscribe(
        (response) => {
          localStorage.setItem('id_token', response.token);
        });
  }     
}
