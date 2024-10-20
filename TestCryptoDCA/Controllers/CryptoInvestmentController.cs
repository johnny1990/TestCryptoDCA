using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestCryptoDCA.Models;
using TestCryptoDCA.Services;

namespace TestCryptoDCA.Controllers
{
    public class CryptoInvestmentController : Controller
    {
        private readonly CryptoDCAContext _context;
        private readonly CryptoDCAService _cryptoService;

        public CryptoInvestmentController(CryptoDCAService cryptoService, CryptoDCAContext context)
        {
            _cryptoService = cryptoService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Calculator()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> InvestmentResult()
        {
            try
            {
                return View(await _context.DCAResults.ToListAsync());
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CalculateDCA(CryptoInvestment investment, CryptoDCAResult result)
        {
            var currentPrice = await _cryptoService.GetCurrentPrice(investment.CryptoSymbol);

            for (var date = investment.StartDate; date <= DateTime.Today; date = date.AddMonths(1))
            {
                var investedAmount = investment.MonthlyAmount;
                var priceAtDate = await _cryptoService.GetPriceAtGivenDate(investment.CryptoSymbol, date);
                var cryptoAmount = investedAmount / priceAtDate;

                var valueToday = cryptoAmount * currentPrice;
                var roi = (valueToday - investedAmount) / investedAmount * 100;


                result.Date = date;
                result.InvestedAmount = investedAmount;
                result.CryptoAmount = cryptoAmount;
                result.ValueToday = valueToday;
                result.ROI = roi;

                //Insert into db
                _context.Add(investment);
                await _context.SaveChangesAsync();

                _context.Add(result);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction("InvestmentResult");

        }
    }
}
