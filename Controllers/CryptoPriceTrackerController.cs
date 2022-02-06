using System.Collections.Generic;
using System.Threading.Tasks;
using cryptoibero_cyptoprice_api.Model;
using cryptoibero_cyptoprice_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace cryptoibero_cyptoprice_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoPriceTrackerController : ControllerBase
    {

        private ICryptoPriceRepository _repository;
        public CryptoPriceTrackerController(ICryptoPriceRepository repository)
        {
                _repository = repository;
        }


         [HttpGet]
        public async Task<List<CryptoInfo>> Get()
        {
           return await _repository.GetCryptoCurrencies();
        }
    }
}