using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptollet.Common.Models;

namespace Cryptollet.Common.Network
{
    public interface ICryptoService
    {
        Task<List<Coin>> GetLatestPrices();
    }

    public class CryptoService : ICryptoService
    {
        private INetworkService _networkService;

        public CryptoService(INetworkService networkService)
        {
            _networkService = networkService;
        }

        public async Task<List<Coin>> GetLatestPrices()
        {
            var url = Constants.EXAMPLE_DATA_API;
            var result = await _networkService.GetAsync<List<Coin>>(url);

            return result;
        }
    }
}
