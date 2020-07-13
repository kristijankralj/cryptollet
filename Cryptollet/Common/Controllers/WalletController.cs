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
    }

    public class WalletController : IWalletController
    {
        private IRepository<Transaction> _transactionRepository;
        private ICrypoService _crypoService;
        private List<Coin> _cachedCoins = new List<Coin>();

        public WalletController(IRepository<Transaction> transactionRepository,
                                ICrypoService crypoService)
        {
            _transactionRepository = transactionRepository;
            _crypoService = crypoService;
        }

        public async Task<List<Transaction>> GetTransactions(bool forceReload = false)
        {
            if (_cachedCoins.Count == 0 || forceReload)
            {
                _cachedCoins = await _crypoService.GetLatestPrices();
            }
            var transactions = await _transactionRepository.GetAllAsync();
            transactions = transactions.OrderByDescending(x => x.TransactionDate).ToList();
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
    }
}
