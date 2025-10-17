using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    public static class Combat
    {
        public static void Attack(ICombatant attacker, ICombatant defender)
        {
            int attackRoll = attacker.AttackDice.Throw();
            int defenceRoll = defender.DefenceDice.Throw();
            int dmg = attackRoll - defenceRoll;

            string message;

            if (dmg > 0)
            {
                defender.HP -= dmg;
                if (defender.HP <= 0)
                {
                    message = $"{GetName(attacker)} ({attacker.AttackDice} => {attackRoll}) attacked {GetName(defender)} " +
                              $"({defender.DefenceDice} => {defenceRoll}) and dealt {dmg} damage, killing them!";
                }
                else
                {
                    message = $"{GetName(attacker)} ({attacker.AttackDice} => {attackRoll}) attacked {GetName(defender)} " +
                              $"({defender.DefenceDice} => {defenceRoll}) and dealt {dmg} damage.";
                }
            }
            else
            {
                message = $"{GetName(attacker)} ({attacker.AttackDice} => {attackRoll}) attacked {GetName(defender)} " +
                          $"({defender.DefenceDice} => {defenceRoll}) but did not manage to deal damage.";
            }

            ConsoleColor color = ConsoleColor.White;

            if (attacker is Player)
                color = ConsoleColor.Yellow;
            else if (attacker is Enemy)
                color = dmg > 0 ? ConsoleColor.Red : ConsoleColor.Green;

            GameLoop.GameMessageLog(message, color);
        }

        public static void CombatExchange(ICombatant attacker, ICombatant defender)
        {
            Attack(attacker, defender);
            if (defender.HP > 0)
                Attack(defender, attacker);
        }

        private static string GetName(ICombatant c)
        {
            return c switch
            {
                Player p => p.NameOrType(),
                Enemy e => e.Name,
                _ => "Unknown"
            };
        }
    }
}
