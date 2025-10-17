using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    public interface ICombatant
    {
        int HP { get; set; }
        Dice AttackDice { get; }
        Dice DefenceDice { get; }
    }
}
