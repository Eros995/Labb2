using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Rat : Enemy
    {
        private static readonly Random rng = new Random();

        public Rat(int x, int y)
        {
            X = x; Y = y;
            Symbol = 'r';
            Color = ConsoleColor.Red;
            Name = "Rat";
            HP = 10;
            AttackDice = new Dice(1, 6, 3);
            DefenceDice = new Dice(1, 6, 1);
        }

        public override void Update(Player player, LevelData level)
        {
            int[] dx = { 0, 0, -1, 1 };
            int[] dy = { -1, 1, 0, 0 };
            int direction = rng.Next(4);
            int nx = X + dx[direction];
            int ny = Y + dy[direction];

            if (player.X == nx && player.Y == ny)
            {
                Combat.Attack(this, player);
                return;
            }

            if (!level.IsBlocked(nx, ny) && level.GetEnemyAt(nx, ny) == null)
            {
                X = nx;
                Y = ny;
            }
        }
    }
}
