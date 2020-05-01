using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Persistence.Model
{
    public class Character
    {
        public int IdCharacter { get; set; }
        public string NmCharacter { get; set; }
        public int VlAttack { get; set; }
        public int VlDefense { get; set; }
        public int VlAgility { get; set; }
        public int VlHealth { get; set; }
        public int FkWeapon { get; set; }
        public Character(int idCharacter, string nmCharacter, int vlAttack, int vlDefense, int vlAgility, int vlHealth, int fkWeapon)
        {
            IdCharacter = idCharacter;
            NmCharacter = nmCharacter;
            VlAttack = vlAttack;
            VlDefense = vlDefense;
            VlAgility = vlAgility;
            VlHealth = vlHealth;
            FkWeapon = fkWeapon;
        }
    }
}
