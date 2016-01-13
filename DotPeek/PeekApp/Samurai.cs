using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeekApp
{
    class Samurai
    {
        readonly IWeapon weapon;
        public Samurai(IWeapon weapon)
        {
            this.weapon = weapon;
        }

        public void Attack(string target)
        {
            this.weapon.Hit(target);
        }
    }

    interface IWeapon
    {
        void Hit(string target);
    }

}