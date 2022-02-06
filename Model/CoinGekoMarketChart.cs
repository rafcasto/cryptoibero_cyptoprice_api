using System.Collections.Generic;
using Newtonsoft.Json;

namespace cryptoibero_cyptoprice_api.Model
{
    public class CoinGekoMarketChart
    {
        [JsonProperty("prices")]
        public List<List<double>> Prices{get;set;}
    }

}