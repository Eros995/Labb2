using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Player : LevelElement ,ICombatant
    {
        public int HP { get; set; } = 100;
        public Dice AttackDice { get; } = new Dice(2, 6, 2);
        public Dice DefenceDice { get; } = new Dice(2, 6, 0);

        public int CurrentTurn { get; private set; } = 0;

        public Player(int x, int y)
        {
            X = x;
            Y = y;
            Symbol = '@';
            Color = ConsoleColor.Yellow;
        }

        public void Turn()
        {
            CurrentTurn += 1;
        }

        public string NameOrType() => "Player";
    }
}
