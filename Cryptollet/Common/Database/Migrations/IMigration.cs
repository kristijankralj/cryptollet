using System;
using System.Threading.Tasks;

namespace Cryptollet.Common.Database.Migrations
{
    public interface IMigration
    {
        Task Run();
    }
}
