using Microsoft.Extensions.Configuration;
using MySqlConnector;
using PrisonRP.Database.Interfaces;
using PrisonRP.Database.Models;
using RepoDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonRP.Database.Repositories
{
    public class PlayerAccountRepository : BaseRepository<PlayerAccount, MySqlConnection>, IPlayerAccountRepository
    {
        public PlayerAccountRepository(IConfigurationRoot config) : base(config.GetConnectionString("Default"))
        {
        }

        public Task<int> DeleteAsync(PlayerAccount player)
        {
            return base.DeleteAsync(player);
        }

        public PlayerAccount Get(string name, IEnumerable<Field> fields = null)
        {
            return base.Query(e => e.Name == name, fields).FirstOrDefault();
        }

        public IEnumerable<PlayerAccount> GetAll()
        {
            return base.QueryAll();
        }

        public Task<object> InsertAsync(PlayerAccount player, IEnumerable<Field> fields = null)
        {
            return base.InsertAsync(player, fields);
        }

        public Task<int> UpdateAsync(PlayerAccount player, IEnumerable<Field> fields = null)
        {
            return base.UpdateAsync(player, fields: fields);
        }
    }
}