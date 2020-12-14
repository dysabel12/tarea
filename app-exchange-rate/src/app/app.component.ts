import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Currency } from './models/currency';
import { ExchangeRate } from './models/exchangerate';
import { AuthService } from './shared/auth.service';
import { ExchangeRateService } from './shared/exchange-rate.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit  {
  title = 'app-exchange-rate';
  form: FormGroup = new FormGroup({
    currencyInput: new FormControl('', [Validators.required]),
    amountInput: new FormControl('', Validators.required),
    currencyOutput: new FormControl('', Validators.required),
    amountOutput: new FormControl('')
  });
  currencyList: Currency[] = [];
  request!: ExchangeRate;
  
  constructor(private authService: AuthService, private exchangeRateService: ExchangeRateService){

  }
  
  
  ngOnInit(): void {
    this.authService.login();
    this.exchangeRateService.getCurrencies().subscribe(currencies => {
      this.currencyList = currencies;
    });
  }

  submit(){
    this.request = new ExchangeRate(parseFloat(this.form.value.currencyInput),
      parseFloat(this.form.value.currencyOutput),
      parseFloat(this.form.value.amountInput)
    );

    this.exchangeRateService.getExchangeRate(this.request).subscribe(exchangeRate => {
      this.form.controls["amountOutput"].setValue(exchangeRate.amount_output);
    });
    
  }
}
