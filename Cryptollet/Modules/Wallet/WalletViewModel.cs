using System;
using System.Collections.ObjectModel;
using Cryptollet.Common.Base;
using Cryptollet.Common.Models;

namespace Cryptollet.Modules.Wallet
{
    public class WalletViewModel: BaseViewModel
    {

        public WalletViewModel()
        {
            Assets = new ObservableCollection<Coin>
            {
                new Coin { Name = "Bitcoin", Amount= 0.8934M, Symbol = "BTC", DollarValue = 8452.98M, Change= 5.24M },
                new Coin { Name = "Ethereum", Amount= 8.0175M, Symbol = "ETH", DollarValue = 1825.72M, Change = 1.45M },
                new Coin { Name = "Litecoin", Amount= 24.82M, Symbol = "LTC", DollarValue = 1378.45M, Change = -0.91M },
            };

            LatestTransactions = new ObservableCollection<Transaction>
            {
                new Transaction
                {
                    Status = Constants.TRANSACTION_WITHDRAWN,
                    TransactionDate = new DateTime(2019, 8, 19),
                    Amount = 0.021M,
                    DollarValue = 204,
                    Symbol = "BTC"
                },
                new Transaction
                {
                    Status = Constants.TRANSACTION_DEPOSITED,
                    TransactionDate = new DateTime(2019, 8, 16),
                    Amount = 3.21M,
                    DollarValue = 695.03M,
                    Symbol = "ETH"
                },
                new Transaction
                {
                    Status = Constants.TRANSACTION_DEPOSITED,
                    TransactionDate = new DateTime(2019, 8, 10),
                    Amount = 37.81M,
                    DollarValue = 250M,
                    Symbol = "NEO"
                },
                new Transaction
                {
                    Status = Constants.TRANSACTION_WITHDRAWN,
                    TransactionDate = new DateTime(2019, 8, 5),
                    Amount = 0.021M,
                    DollarValue = 204,
                    Symbol = "BTC"
                },
                new Transaction
                {
                    Status = Constants.TRANSACTION_DEPOSITED,
                    TransactionDate = new DateTime(2019, 8, 1),
                    Amount = 3.21M,
                    DollarValue = 695.03M,
                    Symbol = "ETH"
                },
            };
        }

        private ObservableCollection<Coin> _assets;
        public ObservableCollection<Coin> Assets
        {
            get => _assets;
            set { SetProperty(ref _assets, value); }
        }

        private ObservableCollection<Transaction> _latestTransactions;
        public ObservableCollection<Transaction> LatestTransactions
        {
            get => _latestTransactions;
            set { SetProperty(ref _latestTransactions, value); }
        }
    }
}
