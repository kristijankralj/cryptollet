using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptollet.Common.Models;

namespace Cryptollet.Common.Database.Migrations
{
    public interface IMigrationService
    {
        Task RunDatabaseMigrations();
    }

    public class MigrationService : IMigrationService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private List<IMigration> _migrations;

        public MigrationService(IRepository<Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
            SetupMigrations();
        }

        public async Task RunDatabaseMigrations()
        {
            foreach (var migration in _migrations)
            {
                await migration.Run();
            }
        }

        private void SetupMigrations()
        {
            _migrations = new List<IMigration>
            {
                new Migration1(_transactionRepository)
            };
        }
    }
}
