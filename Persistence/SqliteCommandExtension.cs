using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Data.Sqlite;

static class SqliteCommandExtension
{
    public static int ExecuteNonQueryWithForeignKeyConstraint(this SqliteCommand command)
    {
        var tmp = command.CommandText;
        command.CommandText = "PRAGMA foreign_keys = true;" + tmp;

        return command.ExecuteNonQuery();
    }
}
