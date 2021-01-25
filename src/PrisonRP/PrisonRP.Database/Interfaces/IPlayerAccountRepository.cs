using PrisonRP.Database.Models;
using RepoDb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrisonRP.Database.Interfaces
{
    public interface IPlayerAccountRepository
    {
        PlayerAccount Get(string name, IEnumerable<Field> fields = null);

        IEnumerable<PlayerAccount> GetAll();

        Task<int> DeleteAsync(PlayerAccount player);

        Task<object> InsertAsync(PlayerAccount player, IEnumerable<Field> fields = null);

        Task<int> UpdateAsync(PlayerAccount player, IEnumerable<Field> fields = null);
    }
}