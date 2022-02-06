using System.Collections.Generic;
using System.Threading.Tasks;
using cryptoibero_cyptoprice_api.Model;

namespace cryptoibero_cyptoprice_api.Repositories
{
    public interface ICryptoPriceRepository
    {
        public Task<List<CryptoInfo>> GetCryptoCurrencies();
        public Task<ChartInfo> GetChartInfo(string currencyId);
    }
}