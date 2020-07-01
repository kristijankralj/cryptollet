using System;
using Cryptollet.Common.Database;

namespace Cryptollet.Common.Models
{
    public class User : IDatabaseItem
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get ; set; }
    }
}