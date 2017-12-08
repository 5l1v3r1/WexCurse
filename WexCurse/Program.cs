using System;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;

namespace WexCurse
{
    // Developers : Falweek
    internal class Program
    {
        public static decimal currencyLast = 1;

        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Для получения данных о валюте, нажмите Enter, для выхода любую другую клавишу");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    var webClient = new WebClient();
                    var url = $"https://wex.nz/api/3/ticker/btc_usd-btc_btc?ignore_invalid=1";

                    var response = webClient.DownloadString(url);
                    var currencyInfo = JObject.Parse(response)["btc_usd"].ToArray();

                    Console.Clear();

                    try
                    {
                        foreach (var realtimeinfo in currencyInfo)
                        {
                            var currencybuy = realtimeinfo.Parent["buy"].ToString();
                            var currenceSell = realtimeinfo.Parent["sell"].ToString();
                            var currenceHigh = realtimeinfo.Parent["high"].ToString();
                            var currenceMin = realtimeinfo.Parent["low"].ToString();
                            var currenceAvg = realtimeinfo.Parent["avg"].ToString();
                            Console.WriteLine("Текущая цена покупки : {0} ", currencybuy);
                            Console.WriteLine("Текущая цена продажи : {0}", currenceSell);
                            Console.WriteLine("Максимальная цена за сутки : {0}", currenceHigh);
                            Console.WriteLine("Минимальная цена за сутки : {0}", currenceMin);
                            Console.WriteLine("Средняя цена за сутки : {0}", currenceAvg);
                            Console.WriteLine("Текущая цена продажи : {0}", currenceSell);
                            var realTimeSell = Convert.ToDecimal(currenceSell);
                            var sum = (realTimeSell - currencyLast) / (realTimeSell / 100);
                            currencyLast = realTimeSell;
                            if (sum > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Процент прибыли : {0}", sum);
                            }
                            else if (sum < 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Процент прибыли : {0}", sum);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.WriteLine("Процент прибыли : {0}", sum);
                            }
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("------------------------------------------");
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Что то пошло не так");
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}