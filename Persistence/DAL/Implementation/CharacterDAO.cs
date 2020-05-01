using Assets.Scripts.Persistence.DAL.Interfaces;
using Assets.Scripts.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Persistence.DAL.Implementation
{
    public class CharacterDAO : ICharacterDAO
    {
        public ISqliteConnectionProvider ConnectionProvider { get; protected set; }
        public CharacterDAO(ISqliteConnectionProvider connectionProvider)
        {
            ConnectionProvider = connectionProvider;
        }

        public bool DeleteCharacter(int id)
        {
            using (var conn = ConnectionProvider.Connection)
            {
                var str = new StringBuilder();
                str.Append($"DELETE FROM TB_CHARACTER WHERE ID_CHARACTER = @id");

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = str.ToString();
                    command.Parameters.AddWithValue("@id", id);

                    return command.ExecuteNonQueryWithForeignKeyConstraint() > 0;
                }
            }
        }

        public Character GetCharacter(int id)
        {
            using (var conn = ConnectionProvider.Connection)
            {
                var str = new StringBuilder();
                Character character = null;
                
                str.AppendLine($"SELECT * FROM TB_CHARACTER WHERE ID_CHARACTER = @id");
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = str.ToString();
                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();

                    if(reader.Read())
                    {
                        var charId = reader.GetInt32(0);
                        var name = reader.GetString(1);
                        var attack = reader.GetInt32(2);
                        var defense = reader.GetInt32(3);
                        var agility = reader.GetInt32(4);
                        var health = reader.GetInt32(5);
                        var weapon = reader.GetInt32(6);

                        character = new Character(charId, name, attack, defense, agility, health, weapon);                        
                    }

                    return character;
                }
            }
        }

        public bool InsertCharacter(Character character)
        {
            using (var conn = ConnectionProvider.Connection)
            {
                var str = new StringBuilder();
                str.AppendLine("INSERT INTO TB_CHARACTER(NM_CHARACTER, VL_ATTACK, VL_DEFENSE, VL_AGILITY, VL_HEALTH, FK_WEAPON)");
                str.AppendLine(" VALUES(@name, @attack, @defense, @agility, @health, @weapon)");

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = str.ToString();

                    command.Parameters.AddWithValue("@name", character.NmCharacter);
                    command.Parameters.AddWithValue("@attack", character.VlAttack);
                    command.Parameters.AddWithValue("@defense", character.VlDefense);
                    command.Parameters.AddWithValue("@agility", character.VlAgility);
                    command.Parameters.AddWithValue("@health", character.VlHealth);
                    command.Parameters.AddWithValue("@weapon", character.FkWeapon);
                    
                    return command.ExecuteNonQueryWithForeignKeyConstraint() > 0;
                }
            }
        }

        public bool UpdateCharacter(Character character)
        {
            using (var conn = ConnectionProvider.Connection)
            {
                var str = new StringBuilder();

                str.AppendLine("UPDATE TB_CHARACTER SET");
                str.AppendLine(" NM_CHARACTER = @name,");
                str.AppendLine(" VL_ATTACK = @attack,");
                str.AppendLine(" VL_DEFENSE = @defense,");
                str.AppendLine(" VL_AGILITY = @agility,");
                str.AppendLine(" VL_HEALTH = @health,");
                str.AppendLine(" FK_WEAPON = @weapon");
                str.AppendLine(" WHERE ID_CHARACTER = @id");

                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = str.ToString();
                    command.Parameters.AddWithValue("@name", character.NmCharacter);
                    command.Parameters.AddWithValue("@attack", character.VlAttack);
                    command.Parameters.AddWithValue("@defense", character.VlDefense);
                    command.Parameters.AddWithValue("@agility", character.VlAgility);
                    command.Parameters.AddWithValue("@health", character.VlHealth);
                    command.Parameters.AddWithValue("@weapon", character.FkWeapon);
                    command.Parameters.AddWithValue("@id", character.IdCharacter);

                    return command.ExecuteNonQueryWithForeignKeyConstraint() > 0;
                }
            }

        }
    }
}
