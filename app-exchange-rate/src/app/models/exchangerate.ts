export class ExchangeRate {
    currency_input!: number;
    currency_output!: number;
    exchange_rate!: number;
    amount_input!: number;
    amount_output!: number;

    constructor(currency_input: number, currency_output: number, amount_input:number) {
        this.currency_input = currency_input;
        this.currency_output = currency_output;
        this.amount_input = amount_input;
      }
}