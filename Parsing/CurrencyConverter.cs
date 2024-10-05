using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parsing
{
    internal class CurrencyConverter
    {
        private static decimal usdRate = 0;

        public static async Task GetUsdRate()
        {
            string url = "https://bank.gov.ua/ua/markets/exchangerates";

            HttpClient client = new HttpClient();
            string html = await client.GetStringAsync(url);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var usdNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"exchangeRates\"]/tbody/tr[8]/td[5]");

            if (usdNode != null)
            {
                string usdRateText = usdNode.InnerText.Trim().Replace('.', ',');

                if (decimal.TryParse(usdRateText, out decimal parsedRate))
                {
                    usdRate = parsedRate;
                    Console.WriteLine($"Курс USD: {usdRate}");
                }
                else
                {
                    Console.WriteLine("Помилка перетворення курсу USD.");
                }
            }
            else
            {
                Console.WriteLine("Не вдалося знайти інформацію про курс USD на сайті.");
            }
        }

        public static decimal ConvertToUsd(decimal grn)
        {
            if (usdRate == 0)
            {
                Console.WriteLine("Курс USD не встановлено. Неможливо здійснити конвертацію.");
                return 0;
            }
            return grn / usdRate;
        }
    }
}
