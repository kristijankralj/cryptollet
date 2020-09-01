using System;
using System.Linq;
using System.Threading.Tasks;
using Cryptollet.Common.Models;

namespace Cryptollet.Common.Database.Migrations
{
    public class Migration1: IMigration
    {
        private IRepository<Transaction> _repository;

        public Migration1(IRepository<Transaction> repository)
        {
            _repository = repository;
        }

        public async Task Run()
        {
            var tableColumns = await _repository.Database.GetTableInfoAsync(typeof(Transaction).Name);
            //Does the column already exist, that is, did we already run this migration?
            bool hasUserIdColumn = tableColumns.Any(column => column.Name.Equals("UserId"));
            bool hasUserEmailColumn = tableColumns.Any(column => column.Name.Equals("UserEmail"));
            if (hasUserEmailColumn && !hasUserIdColumn)
            {
                return;
            }
            await _repository.Database.ExecuteAsync("UPDATE [Transaction] SET UserEmail = UserId");
            //get all saved transactions
            var savedObjects = await _repository.GetAllAsync();
            //delete the table
            await _repository.Database.DropTableAsync<Transaction>();
            //recreate the table
            await _repository.Database.CreateTableAsync<Transaction>();
            //insert saved objects back to the table
            await _repository.Database.InsertAllAsync(savedObjects);
        }
    }
}
