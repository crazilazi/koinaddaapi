using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace koin.Models
{
    public class Ticker
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime time { get; set; }
    }
}