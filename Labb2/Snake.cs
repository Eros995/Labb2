using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Snake : Enemy
    {
        public Snake(int x, int y)
        {
            X = x; Y = y;
            Symbol = 's';
            Color = ConsoleColor.Green;
            Name = "Snake";
            HP = 25;
            AttackDice = new Dice(3, 4, 2);
            DefenceDice = new Dice(1, 8, 5);
        }

        public override void Update(Player player, LevelData level)
        {
            double distance = Math.Sqrt(Math.Pow(player.X - X, 2) + Math.Pow(player.Y - Y, 2));
            if (distance > 2) return;

            int dx = Math.Sign(X - player.X);
            int dy = Math.Sign(Y - player.Y);

            int nx = X + dx;
            int ny = Y + dy;

            if (!level.IsBlocked(nx, ny) && level.GetEnemyAt(nx, ny) == null)
            {
                X = nx;
                Y = ny;
            }
        }
    }
}
