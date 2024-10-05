using HtmlAgilityPack;
using System;
using System.IO;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Parsing;

Console.OutputEncoding = Encoding.UTF8;

    await CurrencyConverter.GetUsdRate();

    Console.WriteLine("Введіть суму в гривнях для конвертації в долари США:");

    string input = Console.ReadLine();
    if (decimal.TryParse(input, out decimal grnAmount))
    {
        decimal usdAmount = CurrencyConverter.ConvertToUsd(grnAmount);

        if (usdAmount != 0)
        {
            Console.WriteLine($"Сума в доларах США: {usdAmount:F2} USD");
        }
    }
    else
    {
        Console.WriteLine("Помилка: введене значення не є числом. Спробуйте ще раз.");
    }
