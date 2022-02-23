using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExchageRates;
using ExchageRates.Models;
using ExchageRates.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ExchageRates.Views.ViewerModels;

namespace ExchageRates.Controllers
{
    public class ExchangesController : Controller
    {
        string URLString = "https://openexchangerates.org/api/";
        string Key = "?app_id=d75e9e0dd5c74225be5f724120a90185";

        double MoyExchangeGBP = 0;
        double MoyExchangeEUR = 0;
        double MoyExchangeKursGBP = 0;
        double MoyExchangeKursUSD = 0;
        double MoyExchangeKursEUR = 0;
        private readonly IExhangeRepository<Exchange> _exhangeRepository;

        public ExchangesController(IExhangeRepository<Exchange> exhangeRepository)
        {
            _exhangeRepository = exhangeRepository;
        }

        // GET: Exchanges
        public IActionResult Index()
        {
            Exchange exchangeGBP = new Exchange();
            Exchange exchangeEuro = new Exchange();
            ExchangeView exchangeView = new ExchangeView();
            try
            {
                string URLCreate = URLString + "latest.json" + Key;

                using (var webClient = new System.Net.WebClient())
                {
                    var exhanges = _exhangeRepository.GetAll();
                    var json = webClient.DownloadString(URLCreate);
                    dynamic Test = JObject.Parse(json);
                    var timestamp = Test.timestamp.Value;
                    DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
                    var isExiste = exhanges.Where(d => d.DateExchange == dt).FirstOrDefault();
                    if (isExiste == null)
                    {
                        exchangeGBP.ExchangeRates = Test.rates.EUR;
                        exchangeEuro.ExchangeRates = Test.rates.GBP;

                        exchangeGBP.DateExchange = dt;
                        exchangeEuro.DateExchange = dt;
                        exchangeEuro.ExchangeName = "EUR";
                        exchangeGBP.ExchangeName = "GBP";
                        _exhangeRepository.Insert(exchangeGBP);
                        _exhangeRepository.Insert(exchangeEuro);
                        exhanges = _exhangeRepository.GetAll();
                    }
                    SaveSevenDayExchange();
                    SaveSevenDayExchangeKurs();
                    exchangeView.MoyExchangeEUR = MoyExchangeEUR;
                    exchangeView.MoyExchangeGBP = MoyExchangeGBP;
                    exchangeView.MoyExchangeKursGBP = MoyExchangeKursGBP;
                    exchangeView.MoyExchangeKursUSD = MoyExchangeKursUSD;
                    exchangeView.MoyExchangeKursEUR = MoyExchangeKursEUR;
                    exchangeView.Exchanges = exhanges.ToList();


                    return View(exchangeView);
                }
            }
            catch (Exception)
            {
                return NotFound();
            }

        }


        public void SaveSevenDayExchangeKurs()
        {
            DateTime dateTime = DateTime.Now;
            double rateUSd = 0;
            double rateGBP = 0;
            double rateEUR = 0;
            var formatdate = dateTime.ToString("yyyy-MM-dd");
            string URLKursUSD = "https://kurs.resenje.org/api/v1/currencies/usd/rates/" + formatdate + "/count/7";
            string URLKursGBP = "https://kurs.resenje.org/api/v1/currencies/gbp/rates/" + formatdate + "/count/7";
            string URLKursEUR = "https://kurs.resenje.org/api/v1/currencies/eur/rates/" + formatdate + "/count/7";
            using (var webClient = new System.Net.WebClient())
            {
                var jsonURLKursUSD = webClient.DownloadString(URLKursUSD);
                var jsonURLKursGBP = webClient.DownloadString(URLKursGBP);
                var jsonURLKursEUR = webClient.DownloadString(URLKursEUR);
                dynamic ResultUSD = JObject.Parse(jsonURLKursUSD);
                dynamic ResultGBP = JObject.Parse(jsonURLKursGBP);
                dynamic ResultEUR = JObject.Parse(jsonURLKursEUR);
                var ratesUSD = ResultUSD.rates;
                var ratesGBP = ResultGBP.rates;
                var ratesEUR = ResultEUR.rates;
                for (int i = 0; i < 7; i++)
                {
                    rateUSd += ratesUSD[i].exchange_buy.Value;
                    rateGBP += ratesGBP[i].exchange_buy.Value;
                    rateEUR += ratesEUR[i].exchange_buy.Value;

                }
            }
            MoyExchangeKursUSD = rateUSd / 7;
            MoyExchangeKursGBP = rateGBP / 7;
            MoyExchangeKursEUR = rateEUR / 7;

        }
        public void SaveSevenDayExchange()
        {
            DateTime dateTime = DateTime.Now;
            string URLCreatehistorical = URLString + "historical/";
            double ExchangeEUR = 0;
            double ExchangeGBP = 0;

            using (var webClient = new System.Net.WebClient())
            {
                for (int i = 0; i < 7; i++)
                {
                    var date = dateTime.AddDays(-1);
                    var formatdate = date.ToString("yyyy-MM-dd") + ".json";
                    string urlToHistory = URLCreatehistorical + formatdate + Key;
                    var json = webClient.DownloadString(urlToHistory);
                    dynamic ResultDay = JObject.Parse(json);
                    ExchangeEUR += ResultDay.rates.EUR.Value;
                    ExchangeGBP += ResultDay.rates.GBP.Value;
                }
            }
            MoyExchangeGBP = ExchangeGBP / 7;
            MoyExchangeEUR = ExchangeEUR / 7;

        }

        private bool ExchangeExists(int id)
        {
            return _exhangeRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
