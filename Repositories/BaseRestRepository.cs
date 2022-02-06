

using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace cryptoibero_cyptoprice_api.Repositories
{
    public class BaseRestRepository
    {
        public async Task<T> Get<T>(string url)
        {            
            var client = new RestClient();
            var request = new RestRequest(url);
            var response = await client.ExecuteGetAsync(request).ConfigureAwait(false); 
            Console.WriteLine(response.Content);
            return JsonConvert.DeserializeObject<T>(response.Content); 
        }

         
    }
}