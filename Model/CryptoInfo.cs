namespace cryptoibero_cyptoprice_api.Model
{
    public class CryptoInfo
    {
        public string Id{get;set;}
        public string Icoin{get;set;}
        public string Name{get;set;}
        public string Symbol{get;set;}
        public string Sell{get;set;}
        public string Buy{get;set;}
        public string Exchange{get;set;}
        public ChartInfo ChartInfo{get;set;}
    }
}