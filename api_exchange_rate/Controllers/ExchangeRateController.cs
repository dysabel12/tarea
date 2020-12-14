using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_exchange_rate.Database;
using api_exchange_rate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_exchange_rate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private ExchangeRateDBContext _context;

        public ExchangeRateController(ExchangeRateDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [Route("currency")]
        public IActionResult GetCurrencies()
        {
            List<Currency> lst = _context.Currencies.ToList();
            return Ok(lst);
        }

        [HttpPost]
        [Authorize]
        [Route("calculate")]
        public IActionResult GetExchangeRate(CalculateExchangeRate request)
        {
            if (request.amount_input == 0
                || request.currency_input == 0
                || request.currency_output == 0)
            {
                return BadRequest("No ha ingresado todos los campos obligatorios");
            }
            ExchangeRate value1 = null, value2 = null;
            value1 = _context.ExchangeRates.Where(x => x.currency_input == request.currency_input && x.currency_output == request.currency_output).FirstOrDefault();
            if (value1 == null)
            {
                value2 = _context.ExchangeRates.Where(x => x.currency_input == request.currency_output && x.currency_output == request.currency_input).FirstOrDefault();
            }
            if (value1 == null && value2 == null)
            {
                return BadRequest("No existe configuración para el tipo de cambio solicitado");
            }

            if (value1 != null)
            {
                request.exchange_rate = value1.exchange_rate;
                request.amount_output = Math.Round(value1.exchange_rate * request.amount_input,2);
            }
            if (value2 != null)
            {
                request.exchange_rate = 1 / value2.exchange_rate;
                request.amount_output = Math.Round(request.exchange_rate * request.amount_input,2);
            }

            return Ok(request);
        }


        [HttpPost]
        [Authorize]
        [Route("update")]
        public IActionResult UpdateExchangeRate(ExchangeRate request)
        {
            if (request.exchange_rate == 0
                || request.currency_input == 0
                || request.currency_output == 0)
            {
                return BadRequest("No ha ingresado todos los campos obligatorios");
            }
            ExchangeRate result = null, result2 = null;
            result = (from p in _context.ExchangeRates
                                   where p.currency_input == request.currency_input && p.currency_output == request.currency_output
                                   select p).SingleOrDefault();
            if(result == null)
            {
                result2 = (from p in _context.ExchangeRates
                                       where p.currency_input == request.currency_output && p.currency_output == request.currency_input
                                       select p).SingleOrDefault();
            }

            if (result != null)
            {
                result.exchange_rate = Math.Round(request.exchange_rate, 7);
            }


            if (result2 != null)
            {
                result2.exchange_rate = Math.Round(1 / request.exchange_rate, 7);
            }

            _context.SaveChanges();

            return Ok(request);
        }
    }
}
