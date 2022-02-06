using Newtonsoft.Json;

namespace cryptoibero_cyptoprice_api.Model
{
    public class CoinGekoMarket
    {
        [JsonProperty("id")]
        public string Id{get;set;}
        [JsonProperty("image")]
        public string Icoin{get;set;}
        [JsonProperty("name")]
        public string Name{get;set;}
        [JsonProperty("symbol")]
        public string Symbol{get;set;}
        [JsonProperty("current_price")]
       
        public string Buy{get;set;}
        [JsonProperty("price_change_percentage_24h")]
        public string Exchange{get;set;}
    }
}