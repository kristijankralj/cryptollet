using System;
using System.Threading.Tasks;
using Cryptollet.Common.Models;

namespace Cryptollet.Common.Network
{
    public interface ICrypoService
    {
        Task<SimplePricesResult> GetLatestPrices();
    }

    public class CrypoService : ICrypoService
    {
        private INetworkService _networkService;

        public CrypoService(INetworkService networkService)
        {
            _networkService = networkService;
        }

        public async Task<SimplePricesResult> GetLatestPrices()
        {
            var url = Constants.COINGECKO_API + "simple/price?ids=bitcoin%2Clitecoin%2Cethereum&vs_currencies=usd";
            var result = await _networkService.GetAsync<SimplePricesResult>(url);

            return result;
        }
    }
}
