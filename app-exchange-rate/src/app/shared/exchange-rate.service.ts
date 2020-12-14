import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Currency } from '../models/currency';
import { ExchangeRate } from '../models/exchangerate';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class ExchangeRateService {

  constructor(private http: HttpClient) {

  }

  getCurrencies() {
      return this.http.get<Currency[]>(`${environment.apiUrl}/api/ExchangeRate/currency`);
  }   
  
  getExchangeRate(request : ExchangeRate){
    return this.http.post<ExchangeRate>(`${environment.apiUrl}/api/ExchangeRate/calculate`,request);
  }
}
