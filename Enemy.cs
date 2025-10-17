using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    abstract class Enemy : LevelElement, ICombatant
    {
        public string Name { get; protected set; }
        public int HP { get; set; }
        public Dice AttackDice { get; protected set; }
        public Dice DefenceDice { get; protected set; }
        public int LastSeen { get; set; } = -1;
        public bool IsDead => HP <= 0;

        public abstract void Update(Player player, LevelData level);
    }
}
