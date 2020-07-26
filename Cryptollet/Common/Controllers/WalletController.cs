using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryptollet.Common.Database;
using Cryptollet.Common.Models;
using Cryptollet.Common.Network;

namespace Cryptollet.Common.Controllers
{
    public interface IWalletController
    {
        Task<List<Transaction>> GetTransactions(bool forceReload = false);
        Task<List<Coin>> GetCoins(bool forceReload = false);
    }

    public class WalletController : IWalletController
    {
        private IRepository<Transaction> _transactionRepository;
        private ICryptoService _cryptoService;
        private List<Coin> _cachedCoins = new List<Coin>();
        private List<Coin> _defaultAssets = new List<Coin>
        {
                new Coin
                {
                    Name = "Bitcoin",
                    Amount = 0M,
                    Symbol = "BTC",
                    DollarValue = 0
                },
                new Coin
                {
                    Name = "Ethereum",
                    Amount = 0,
                    Symbol = "ETH",
                    DollarValue = 0
                },
                new Coin
                {
                    Name = "Litecoin",
                    Amount = 0,
                    Symbol = "LTC",
                    DollarValue = 0
                },
        };

        public WalletController(IRepository<Transaction> transactionRepository,
                                ICryptoService cryptoService)
        {
            _transactionRepository = transactionRepository;
            _cryptoService = cryptoService;
        }

        public async Task<List<Coin>> GetCoins(bool forceReload = false)
        {
            List<Transaction> transactions = await LoadTransactions(forceReload);

            if (transactions.Count == 0 || _cachedCoins.Count == 0)
            {
                return _defaultAssets;
            }
            var groupedTransactions = transactions.GroupBy(x => x.Symbol);
            var result = new List<Coin>();
            foreach (var item in groupedTransactions)
            {
                var amount = item.Where(x => x.Status == Constants.TRANSACTION_DEPOSITED).Sum(x => x.Amount)
                                - item.Where(x => x.Status == Constants.TRANSACTION_WITHDRAWN).Sum(x => x.Amount);
                var newCoin = new Coin
                {
                    Symbol = item.Key,
                    Amount = amount,
                    DollarValue = amount * (decimal)_cachedCoins.FirstOrDefault(x => x.Symbol == item.Key).Price,
                    Name = Coin.GetAvailableAssets().First(x => x.Symbol == item.Key).Name
                };
                result.Add(newCoin);
            }
            return result.OrderByDescending(x => x.DollarValue).ToList();
        }

        public async Task<List<Transaction>> GetTransactions(bool forceReload = false)
        {
            List<Transaction> transactions = await LoadTransactions(forceReload);
            if (transactions.Count == 0 || _cachedCoins.Count == 0)
            {
                return transactions;
            }
            transactions.ForEach(t =>
            {
                t.StatusImageSource = t.Status == Constants.TRANSACTION_DEPOSITED ?
                                        Constants.TRANSACTION_DEPOSITED_IMAGE :
                                        Constants.TRANSACTION_WITHDRAWN_IMAGE;
                t.DollarValue = t.Amount * (decimal)_cachedCoins.First(x => x.Symbol == t.Symbol).Price;
            });
            return transactions;
        }

        private async Task<List<Transaction>> LoadTransactions(bool forceReload)
        {
            if (_cachedCoins.Count == 0 || forceReload)
            {
                _cachedCoins = await _cryptoService.GetLatestPrices();
            }
            var transactions = await _transactionRepository.GetAllAsync();
            transactions = transactions.OrderByDescending(x => x.TransactionDate).ToList();
            return transactions;
        }
    }
}
