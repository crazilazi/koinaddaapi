// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KoinexController.cs" company="">
//   @rajeev
// </copyright>
// <summary>
//   Defines the KoinexController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Koin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Http;

    using koin.Models;
    using koin.Utility;

    using Newtonsoft.Json.Linq;

    using RestSharp;

    /// <summary>
    /// The koinex controller.
    /// </summary>
    public class KoinexController : ApiController
    {
        // GET: api/Koinex

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Get()
        {
            var konexTicker = DataSerializer.JsonDeserializerFromFile<IEnumerable<Ticker>>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//tickers.json"));
            
            if ((DateTime.UtcNow - konexTicker.FirstOrDefault(x => x.id == 1).time).Minutes > 5)
            {
                var client = new RestClient("https://koinex.in/api/ticker");
                var response = client.Execute(new RestRequest());
                var tickerContent = DataSerializer.JsonDeserializer<object>(response.Content);
                DataSerializer.JsonSerializerSaveAsFile<object>(tickerContent, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//koinex-ticker.json"));
                konexTicker.FirstOrDefault(x => x.id == 1).time = DateTime.UtcNow;
                DataSerializer.JsonSerializerSaveAsFile<IEnumerable<Ticker>>(konexTicker, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//tickers.json"));
            }

            return DataSerializer.JsonDeserializerFromFile<object>(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data//koinex-ticker.json"));
        }
        
    }
}
