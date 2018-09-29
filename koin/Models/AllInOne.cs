using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace koin.Models
{
    public class AllInOne : IKoin
    {
        public AllInOne()
        {
            this.exchages = new List<ITradeCenter>();
        }
        public string name { get; set; }

        public string fullname { get; set; }

        public IList<ITradeCenter> exchages { get; set; }
    }

    public class TradeCenter : ITradeCenter
    {
        public string name { get; set; }
        [JsonIgnore]
        public double askprice { get; set; }
        [JsonIgnore]
        public double bidprice { get; set; }

        public double lastprice { get; set; }

        public string url { get; set; }
    }

    // Coindelta
    public class Coinddelta
    {
        [JsonIgnore]
        public double Ask { get; set; }
        [JsonIgnore]
        public double Bid { get; set; }
        public string MarketName { get; set; }
        public double Last { get; set; }
    }
    // koinex
    public class Koinex
    {
        // [JsonProperty("highest_bid")]
        [JsonIgnore]
        public string highest_bid { get; set; }
        [JsonIgnore]
        // [JsonProperty("lowest_ask")]
        public string lowest_ask { get; set; }
        // [JsonProperty("last_traded_price")]
        public string last_traded_price { get; set; }
        public string currency_full_form { get; set; }
        public string currency_short_form { get; set; }
        // ignored
        [JsonIgnore]
        public string min_24hrs { get; set; }
        [JsonIgnore]
        public string max_24hrs { get; set; }
        [JsonIgnore]
        public string vol_24hrs { get; set; }
        [JsonIgnore]
        public string per_change { get; set; }
        [JsonIgnore]
        public string trade_volume { get; set; }
    }
    // Bitbns
    public class Volume
    {
        public double max { get; set; }
        public int min { get; set; }
        public string rate { get; set; }
        public double volume { get; set; }
    }

    public class Bitbns
    {
        public string currency_short_form { get; set; }
        [JsonIgnore]
        public double highest_buy_bid { get; set; }
        [JsonIgnore]
        public double lowest_sell_bid { get; set; }
        public double last_traded_price { get; set; }
        [JsonIgnore]
        public double yes_price { get; set; }
        // ignored
        [JsonIgnore]
        public Volume volume { get; set; }
    }

    // Wazix
    public class Bid
    {
        public double maker { get; set; }
        public double taker { get; set; }
    }

    public class Ask
    {
        public double maker { get; set; }
        public double taker { get; set; }
    }

    public class Fee
    {
        public Bid bid { get; set; }
        public Ask ask { get; set; }
    }

    public class Wazirx
    {
        public string baseMarket { get; set; }
        public string quoteMarket { get; set; }
        [JsonIgnore]
        public string low { get; set; }
        [JsonIgnore]
        public string high { get; set; }
        public string last { get; set; }
        // ignored
        [JsonIgnore]
        public int minBuyAmount { get; set; }
        [JsonIgnore]
        public int minSellAmount { get; set; }
        [JsonIgnore]
        public Fee fee { get; set; }
        [JsonIgnore]
        public int basePrecision { get; set; }
        [JsonIgnore]
        public int quotePrecision { get; set; }
        [JsonIgnore]
        public string type { get; set; }
        [JsonIgnore]
        public string status { get; set; }
        [JsonIgnore]
        public double open { get; set; }
        [JsonIgnore]
        public string volume { get; set; }
        [JsonIgnore]
        public string sell { get; set; }
        [JsonIgnore]
        public string buy { get; set; }
        [JsonIgnore]
        public int at { get; set; }
    }
}