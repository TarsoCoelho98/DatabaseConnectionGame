using Assets.Scripts.Persistence.DAL.Interfaces;
using Assets.Scripts.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Persistence.DAL.Implementation
{
    public class WeaponDAO : IWeaponDAO
    {
        public ISqliteConnectionProvider ConnectionProvider { get; protected set; }
        public WeaponDAO(ISqliteConnectionProvider connectionProvider)
        {
            ConnectionProvider = connectionProvider;
        }

        public bool DeleteWeapon(int id)
        {
            using (var conn = ConnectionProvider.Connection)
            {
                var str = new StringBuilder();
                str.Append($"DELETE FROM TB_WEAPON WHERE ID_WEAPON = @id");

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = str.ToString();
                    command.Parameters.AddWithValue("@id", id);

                    return command.ExecuteNonQueryWithForeignKeyConstraint() > 0;
                }
            }
        }

        public Weapon GetWeapon(int id)
        {
            using (var conn = ConnectionProvider.Connection)
            {
                var str = new StringBuilder();
                Weapon weapon = null;

                str.AppendLine($"SELECT * FROM TB_WEAPON WHERE ID_WEAPON = @id");
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = str.ToString();
                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        var weapId = reader.GetInt32(0);
                        var name = reader.GetString(1);
                        var attack = reader.GetInt32(2);
                        var value = reader.GetDouble(3);

                        weapon = new Weapon(weapId, name, attack, value);
                    }

                    return weapon;
                }
            }
        }

        public bool InsertWeapon(Weapon weapon)
        {
            using (var conn = ConnectionProvider.Connection)
            {
                var str = new StringBuilder();
                str.AppendLine("INSERT INTO TB_WEAPON(NM_WEAPON, VL_ATTACK, VL_WEAPON)");
                str.AppendLine(" VALUES(@name, @attack, @value)");

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = str.ToString();

                    command.Parameters.AddWithValue("@name", weapon.NmWeapon);
                    command.Parameters.AddWithValue("@attack", weapon.VlAttack);
                    command.Parameters.AddWithValue("@value", weapon.VlWeapon);
                    
                    return command.ExecuteNonQueryWithForeignKeyConstraint() > 0;
                }
            }
        }

        public bool UpdateWeapon(Weapon weapon)
        {
            using (var conn = ConnectionProvider.Connection)
            {
                var str = new StringBuilder();

                str.AppendLine("UPDATE TB_WEAPON SET");
                str.AppendLine(" NM_WEAPON = @name,");
                str.AppendLine(" VL_ATTACK = @attack,");
                str.AppendLine(" VL_WEAPON = @value");
                str.AppendLine(" WHERE ID_WEAPON = @id");

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = str.ToString();
                    command.Parameters.AddWithValue("@name", weapon.NmWeapon);
                    command.Parameters.AddWithValue("@attack", weapon.VlAttack);
                    command.Parameters.AddWithValue("@value", weapon.VlWeapon);
                    command.Parameters.AddWithValue("@id", weapon.IdWeapon);
                    
                    return command.ExecuteNonQueryWithForeignKeyConstraint() > 0;
                }
            }

        }
    }
}
