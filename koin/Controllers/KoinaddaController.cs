

namespace koin.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using koin.Models;
    using koin.Utility;
    using Newtonsoft.Json.Linq;
    using RestSharp;

    public class KoinaddaController : ApiController
    {
        public static IEnumerable<Ticker> tickers = null;

        /// <summary>
        /// The koinex.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet]
        public async Task<IEnumerable<Koinex>> Koinex()
        {
            Init(false);
            var koinTicker = tickers.FirstOrDefault(x => x.id == 1);
            if ((DateTime.UtcNow - koinTicker.time).Minutes <= 5)
            {
                return DataSerializer.JsonDeserializerFromFile<IEnumerable<Koinex>>(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//koinex-ticker.json"));
            }
            var client = new RestClient("https://koinex.in/api/ticker");
            var response = client.Execute(new RestRequest());
            var koinex = DataSerializer.JsonDeserializer<JObject>(response.Content);
            var koinexCustom = new List<Koinex>();
            foreach (var pinr in koinex["prices"]["inr"])
            {
                JToken koin = koinex["stats"]["inr"][pinr.Path.Split('.').LastOrDefault()];
                koinexCustom.Add(koin.ToObject<Koinex>());
            }
            // var tickerContent = DataSerializer.JsonDeserializer<object>(response.Content);
            DataSerializer.JsonSerializerSaveAsFile<IEnumerable<Koinex>>(koinexCustom, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//koinex-ticker.json"));
            koinTicker.time = DateTime.UtcNow;
            DataSerializer.JsonSerializerSaveAsFile<IEnumerable<Ticker>>(tickers, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//tickers.json"));
            return DataSerializer.JsonDeserializerFromFile<IEnumerable<Koinex>>(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//koinex-ticker.json"));
        }

        /// <summary>
        /// The coin delta.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet]
        public async Task<IEnumerable<Coinddelta>> CoinDelta()
        {
            Init(false);
            var koinTicker = tickers.FirstOrDefault(x => x.id == 2);
            if ((DateTime.UtcNow - koinTicker.time).Minutes <= 5)
            {
                return DataSerializer.JsonDeserializerFromFile<IEnumerable<Coinddelta>>(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//coindelta-ticker.json"));
            }
            var client = new RestClient("https://api.coindelta.com/api/v1/public/getticker");
            var response = client.Execute(new RestRequest());
            var tickerContent = DataSerializer.JsonDeserializer<IEnumerable<Coinddelta>>(response.Content).Where(x => x.MarketName.Split('-').LastOrDefault() == "inr");
            // tickerContent.lind = "";
            DataSerializer.JsonSerializerSaveAsFile<IEnumerable<Coinddelta>>(tickerContent, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//coindelta-ticker.json"));
            koinTicker.time = DateTime.UtcNow;
            DataSerializer.JsonSerializerSaveAsFile<IEnumerable<Ticker>>(tickers, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//tickers.json"));
            Init(false);
            return DataSerializer.JsonDeserializerFromFile<IEnumerable<Coinddelta>>(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//coindelta-ticker.json"));
        }

        /// <summary>
        /// The bit bns.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet]
        public async Task<IEnumerable<Bitbns>> BitBns()
        {
            Init(false);
            var koinTicker = tickers.FirstOrDefault(x => x.id == 3);
            if ((DateTime.UtcNow - koinTicker.time).Minutes <= 5)
            {
                return DataSerializer.JsonDeserializerFromFile<IEnumerable<Bitbns>>(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//bitbns-ticker.json"));
            }
            var client = new RestClient("https://bitbns.com/order/getTickerWithVolume/");
            var response = client.Execute(new RestRequest());
            var bitbns = DataSerializer.JsonDeserializer<JObject>(response.Content);
            var bitbnsCustom = new List<Bitbns>();
            foreach (var pinr in bitbns)
            {
                JToken koin = bitbns[pinr.Key];
                var bitObj = koin.ToObject<Bitbns>();
                bitObj.currency_short_form = pinr.Key;
                bitbnsCustom.Add(bitObj);
            }
            // var tickerContent = DataSerializer.JsonDeserializer<object>(response.Content);
            // tickerContent.lind = "";
            DataSerializer.JsonSerializerSaveAsFile<IEnumerable<Bitbns>>(bitbnsCustom, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//bitbns-ticker.json"));
            koinTicker.time = DateTime.UtcNow;
            DataSerializer.JsonSerializerSaveAsFile<IEnumerable<Ticker>>(tickers, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//tickers.json"));
            Init(false);
            return DataSerializer.JsonDeserializerFromFile<IEnumerable<Bitbns>>(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//bitbns-ticker.json"));
        }

        /// <summary>
        /// The wazir x.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet]
        public async Task<IEnumerable<Wazirx>> WazirX()
        {
            Init(false);
            var koinTicker = tickers.FirstOrDefault(x => x.id == 4);
            if ((DateTime.UtcNow - koinTicker.time).Minutes <= 5)
            {
                return DataSerializer.JsonDeserializerFromFile<IEnumerable<Wazirx>>(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//wazirx-ticker.json"));
            }
            var client = new RestClient("https://api.wazirx.com/api/v2/market-status");
            var response = client.Execute(new RestRequest());
            var wazirx = DataSerializer.JsonDeserializer<JObject>(response.Content);
            var wazirxCustom = new List<Wazirx>();
            var allTokes = wazirx["markets"].Children().ToList();
            foreach (var pinr in allTokes)
            {
                var wassest = pinr.ToObject<Wazirx>();
                if (wassest.quoteMarket == "inr")
                {
                    wazirxCustom.Add(wassest);
                }
            }
            // var tickerContent = DataSerializer.JsonDeserializer<object>(response.Content);
            // tickerContent.lind = "";
            DataSerializer.JsonSerializerSaveAsFile<IEnumerable<Wazirx>>(wazirxCustom, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//wazirx-ticker.json"));
            koinTicker.time = DateTime.UtcNow;
            DataSerializer.JsonSerializerSaveAsFile<IEnumerable<Ticker>>(tickers, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//tickers.json"));
            Init(false);
            return DataSerializer.JsonDeserializerFromFile<IEnumerable<Wazirx>>(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//wazirx-ticker.json"));
        }
        /// <summary>
        /// The get all koin.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet]
        public async Task<object> GetAllKoin()
        {
            Init(false);
            try
            {
                var coindelta = await this.CoinDelta();
                var koinex = await this.Koinex();
                var bitbns = await this.BitBns();
                var wazirx = await this.WazirX();
                dynamic exchanges = new ExpandoObject();
                exchanges.coindelta = coindelta;
                exchanges.koinex = koinex;
                exchanges.bitbns = bitbns;
                exchanges.wazirx = wazirx;
               // return CreateCommonData(exchanges);
                return exchanges;
            }
            catch (Exception ex)
            {
                dynamic exchanges = new ExpandoObject();
                exchanges.error = ex;
                return exchanges;
            }

        }

        private IEnumerable<AllInOne> CreateCommonData(dynamic _exchange)
        {
            var allKoins = new List<string>();
            var cd = _exchange.coindelta as IEnumerable<Coinddelta>;
            FindAndAllKoin(cd.Select(x => x.MarketName.Split('-').FirstOrDefault()), allKoins);
            var koinex = _exchange.koinex as IEnumerable<Koinex>;
            FindAndAllKoin(koinex.Select(x => x.currency_short_form), allKoins);
            var wazirx = _exchange.wazirx as IEnumerable<Wazirx>;
            FindAndAllKoin(wazirx.Select(x => x.baseMarket), allKoins);
            var bitbns = _exchange.bitbns as IEnumerable<Bitbns>;
            FindAndAllKoin(bitbns.Select(x => x.currency_short_form), allKoins);

            // prepare data
            var allkoindata = new List<AllInOne>();

            foreach (var koin in allKoins)
            {
                var kd = new AllInOne();
                kd.name = koin;
                kd.fullname = koin;
                if (cd.Count(x => x.MarketName.Split('-').FirstOrDefault().ToUpper() == koin) > 0)
                {
                    var cdData = cd.FirstOrDefault(x => x.MarketName.Split('-').FirstOrDefault().ToUpper() == koin);
                    var exchangeinfo = new TradeCenter
                    {
                        name = "CD",
                        lastprice = cdData.Last,
                        url = $"https://coindelta.com/market/advance/{cdData.MarketName}",
                        
                    };
                    kd.exchages.Add(exchangeinfo);
                }
                else
                {
                    var exchangeinfo = new TradeCenter
                    {
                        name = "CD",
                        lastprice =0,
                        url = "https://coindelta.com",

                    };
                    kd.exchages.Add(exchangeinfo);
                }

                if (koinex.Count(x => x.currency_short_form.ToUpper() == koin) > 0)
                {
                    var kodata = koinex.FirstOrDefault(x => x.currency_short_form.ToUpper() == koin);
                    var exchangeinfo = new TradeCenter
                    {
                        name = "KNX",
                        lastprice = Convert.ToDouble(kodata.last_traded_price),
                        url = $"https://koinex.in/exchange/inr/{kodata.currency_full_form}",

                    };
                    kd.exchages.Add(exchangeinfo);
                }
                else
                {
                    var exchangeinfo = new TradeCenter
                    {
                        name = "KNX",
                        lastprice = 0,
                        url = "https://koinex.in",

                    };
                    kd.exchages.Add(exchangeinfo);
                }

                if (bitbns.Count(x => x.currency_short_form.ToUpper() == koin) > 0)
                {
                    var bitbnsdata = bitbns.FirstOrDefault(x => x.currency_short_form.ToUpper() == koin);
                    var exchangeinfo = new TradeCenter
                    {
                        name = "BITBNS",
                        lastprice = Convert.ToDouble(bitbnsdata.last_traded_price),
                        url = $"https://bitbns.com/trade/#/{koin.ToLower()}",

                    };
                    kd.exchages.Add(exchangeinfo);
                }
                else
                {
                    var exchangeinfo = new TradeCenter
                    {
                        name = "BITBNS",
                        lastprice = 0,
                        url = "https://bitbns.com/trade",

                    };
                    kd.exchages.Add(exchangeinfo);
                }

                if (wazirx.Count(x => x.baseMarket.ToUpper() == koin) > 0)
                {
                    var wazirxdata = wazirx.FirstOrDefault(x => x.baseMarket.ToUpper() == koin);
                    var exchangeinfo = new TradeCenter
                    {
                        name = "WZRX",
                        lastprice = Convert.ToDouble(wazirxdata.last),
                        url = $"https://wazirx.com/exchange/{koin}-INR",

                    };
                    kd.exchages.Add(exchangeinfo);
                }
                else
                {
                    var exchangeinfo = new TradeCenter
                    {
                        name = "WZRX",
                        lastprice = 0,
                        url = "https://wazirx.com/exchange",

                    };
                    kd.exchages.Add(exchangeinfo);
                }

                allkoindata.Add(kd);
            }

            return allkoindata;
        }

        private void FindAndAllKoin(IEnumerable<string> exchangeKoins, List<string> allKoins)
        {
            foreach (var koin in exchangeKoins)
            {
                if (allKoins.Count(x => x.ToUpper() == koin.ToUpper()) == 0)
                {
                    allKoins.Add(koin.ToUpper());
                }
            }
        }
        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="reload">
        /// The _reload.
        /// </param>
        private static void Init(bool reload)
        {
            if (reload || tickers == null || !tickers.Any())
            {
                tickers = DataSerializer.JsonDeserializerFromFile<IEnumerable<Ticker>>(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//tickers.json"));
            }
        }
    }
}
