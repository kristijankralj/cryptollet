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
        private const string PRICES_ENDPOINT = "simple/price?ids=bitcoin%2Cbitcoin-cash%2Cdash%2Cethereum%2Ceos%2Clitecoin%2Cmonero%2Cripple%2Cstellar&vs_currencies=usd";

        public CryptoService(INetworkService networkService)
        {
            _networkService = networkService;
        }

        public async Task<List<Coin>> GetLatestPrices()
        {
            var url = Constants.CRYPTO_API + PRICES_ENDPOINT;
            var result = await _networkService.GetAsync<Dictionary<string,Dictionary<string, double?>>>(url);
            var coins = Coin.GetAvailableAssets();
            foreach (var coin in coins)
            {
                Dictionary<string,double?> coinPrices = result[coin.Name.Replace(' ', '-').ToLower()];
                coin.Price = coinPrices["usd"].HasValue ? coinPrices["usd"].Value : 0;
            }

            return coins;
        }
    }
}
