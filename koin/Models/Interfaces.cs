using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace koin.Models
{
    public interface ITradeCenter
    {
        string name { get; set; }

        double askprice { get; set; }

        double bidprice { get; set; }

        double lastprice { get; set; }

        string url { get; set; }
    }

   public interface IKoin
    {
        string name { get; set; }

        string fullname { get; set; }

        IList<ITradeCenter> exchages { get; set; }
    }
}
