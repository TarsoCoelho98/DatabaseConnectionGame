using Assets.Scripts.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Persistence.DAL.Interfaces
{
    public interface IWeaponDAO
    {
        ISqliteConnectionProvider ConnectionProvider { get; }

        bool UpdateWeapon(Weapon weapon);
        bool InsertWeapon(Weapon weapon);
        bool DeleteWeapon(int id);
        Weapon GetWeapon(int id);

    }
}
