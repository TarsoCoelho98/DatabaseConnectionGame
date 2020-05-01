using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Data.Sqlite;

namespace Assets.Scripts.Persistence.DAL.Interfaces
{
    public interface ISqliteConnectionProvider
    {
        SqliteConnection Connection { get; }
    }
}
