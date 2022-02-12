using System.Collections.Generic;
using System.Threading.Tasks;
using cryptoibero_cyptoprice_api.Model;
using System.Linq;
using System;

namespace cryptoibero_cyptoprice_api.Repositories
{
    public class CryptoPriceRepository : BaseRestRepository, ICryptoPriceRepository
    {
        private string MARKET_PRICE = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=nzd&order=market_cap_desc&per_page=20&page=1&sparkline=false&price_change_percentage=1h%2C24h%2C7d";
        private string MARKET_CHART = "https://api.coingecko.com/api/v3/coins/{0}/market_chart?vs_currency=nzd&days=2";

        public async Task<ChartInfo> GetChartInfo(string currencyId)
        {
            var url = string.Format(MARKET_CHART,currencyId);
            var gekoMarketChart = await Get<CoinGekoMarketChart>(url);
            return ConvertCoinGekoMarketChartToCryptoIbero(gekoMarketChart.Prices,currencyId);
        }

        public async Task<List<CryptoInfo>> GetCryptoCurrencies()
        {
            var coinGeko = await Get<List<CoinGekoMarket>>(MARKET_PRICE);
            return await ConvertCoinGekoToCryptoIbero(coinGeko);
        }

        private ChartInfo ConvertCoinGekoMarketChartToCryptoIbero(List<List<double>> chart, string currencyId)
        {
            var data = new List<double>();
            var labels = new List<string>();
            foreach(List<double> chartData in chart)
            {
                labels.Add(UnixDateTimeToString(chartData.FirstOrDefault()));
                data.Add(chartData.LastOrDefault());
            }
            return new ChartInfo(){
                Data = data,
                Labels = labels,
                Title = currencyId
            };
        }

        private async Task<List<CryptoInfo>> ConvertCoinGekoToCryptoIbero(List<CoinGekoMarket> gekoMarkets)
        {
            var cryptoInfoList = new List<CryptoInfo>();
            foreach(CoinGekoMarket currency in gekoMarkets)
            {
                cryptoInfoList.Add(new CryptoInfo(){
                    Id = currency.Id,
                    Icoin = getIcon(currency.Icoin), 
                    Buy = currency.Buy,
                    Sell = currency.Buy,
                    Exchange = currency.Exchange,
                    Name = currency.Name.ToUpper(),
                    Symbol = currency.Symbol,
                    ChartInfo = await GetChartInfo(currency.Id)
                });
            }
            return cryptoInfoList;
        }

        private string getIcon(string icon)
        {
            int index = icon.IndexOf("?");
            if (index >= 0)
            icon = icon.Substring(0, index);
            return icon;
        }

        private string UnixDateTimeToString(double unixTime)
        {
            System.DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds( unixTime ).ToLocalTime();
            return dtDateTime.ToString();
        }
    }
}