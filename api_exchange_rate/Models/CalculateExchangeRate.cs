using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_exchange_rate.Models
{
    public class CalculateExchangeRate
    {
        public int currency_input { get; set; }
        public int currency_output { get; set; }
        public Decimal exchange_rate { get; set; }
        public Decimal amount_input { get; set; }
        public Decimal amount_output { get; set; }

    }
}
