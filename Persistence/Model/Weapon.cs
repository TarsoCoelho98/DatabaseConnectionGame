using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Persistence.Model
{
    public class Weapon
    {
        public int IdWeapon { get; set; }
        public string NmWeapon { get; set; }
        public int VlAttack { get; set; }
        public double VlWeapon { get; set; }

        public Weapon(int idWeapon, string nmWeapon, int vlAttack, double vlWeapon)
        {
            IdWeapon = idWeapon;
            NmWeapon = nmWeapon;
            VlAttack = vlAttack;
            VlWeapon = vlWeapon;
        }
    }
}
