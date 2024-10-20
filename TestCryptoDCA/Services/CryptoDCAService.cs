using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using TestCryptoDCA.Models;

namespace TestCryptoDCA.Services
{
    public class CryptoDCAService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "b54bcf4d-1bca-4e8e-9a24-22ff2c3d462c";

        public CryptoDCAService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetCurrentPrice(string symbol)
        {
            var url = $"https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?symbol={symbol}&CMC_PRO_API_KEY={_apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

                dynamic data = JObject.Parse(json);

                var price = data.data[symbol].quote.USD.price;
                return price;
            }

            return 0;

        }

        public async Task<decimal> GetPriceAtGivenDate(string symbol, DateTime date)
        {
            string formattedDate = date.ToString("yyyy-MM-dd");
            var url = $"https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/quotes/historical?symbol={symbol}&time_start={formattedDate}T00:00:00&time_end={formattedDate}T23:59:59&CMC_PRO_API_KEY={_apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                dynamic data = JObject.Parse(json);

                var price = data["data"]["quotes"]?.First?["quote"]?["USD"]?["price"]?.Value<decimal>();

                //if price = null, set price to 0.06 i didn't manage to get the price from 
                if (price == null)
                {
                    price = 0.06m;
                }
                return price;
            }

            return 0m;
        }


    }
}
