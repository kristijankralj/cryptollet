using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptollet.Common.Models;

namespace Cryptollet.Common.Network
{
    public interface ICrypoService
    {
        Task<Dictionary<string, Dictionary<string, double?>>> GetLatestPrices();
    }

    public class CrypoService : ICrypoService
    {
        private INetworkService _networkService;

        public CrypoService(INetworkService networkService)
        {
            _networkService = networkService;
        }

        public async Task<Dictionary<string, Dictionary<string, double?>>> GetLatestPrices()
        {
            var url = Constants.COINGECKO_API + "simple/price?ids=bitcoin%2Clitecoin%2Cethereum&vs_currencies=usd";
            var result = await _networkService.GetAsync<Dictionary<string, Dictionary<string, double?>>>(url);

            return result;
        }
    }
}
