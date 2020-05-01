using Assets.Scripts.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Persistence.DAL.Interfaces
{
    public interface ICharacterDAO
    {
        ISqliteConnectionProvider ConnectionProvider { get; }

        bool UpdateCharacter(Character character);
        bool InsertCharacter(Character character);
        bool DeleteCharacter(int id);
        Character GetCharacter(int id);

    }
}
