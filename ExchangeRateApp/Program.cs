using Newtonsoft.Json;

namespace ExchangeRateApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Exchange Rate Application");
            Console.Write("Enter the base currency (e.g., USD): ");
            string baseCurrency = Console.ReadLine().ToUpper();

            Console.Write("Enter the target currency (e.g., EUR): ");
            string targetCurrency = Console.ReadLine().ToUpper();

            await GetExchangeRate(baseCurrency, targetCurrency);
        }

        private static async Task GetExchangeRate(string baseCurrency, string targetCurrency)
        {
            string url = $"https://api.exchangerate-api.com/v4/latest/{baseCurrency}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    dynamic exchangeRates = JsonConvert.DeserializeObject(responseBody);
                    var rate = exchangeRates?.rates[targetCurrency];

                    Console.WriteLine($"Exchange rate from {baseCurrency} to {targetCurrency}: {rate}");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
        }
    }
}
