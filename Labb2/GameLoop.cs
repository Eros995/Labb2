using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    static class GameLoop
    {
        private static LevelData level;
        private static List<(string message, ConsoleColor color)> messages = new List<(string, ConsoleColor)>();
        private static int visionRadius = 5;

        public static void GameMessageLog(string text, ConsoleColor color = ConsoleColor.White)
        {
            messages.Add((text, color));
            if (messages.Count > 6) messages.RemoveAt(0);
        }

        static void DrawUI()
        {
            try
            {
                for (int i = 0; i < messages.Count; i++)
                {
                    Console.SetCursorPosition(0, level.Height + 1 + i);
                    Console.Write(new string(' ', Console.WindowWidth - 1));
                }

                for (int i = 0; i < messages.Count; i++)
                {
                    Console.SetCursorPosition(0, level.Height + 1 + i);
                    Console.ForegroundColor = messages[i].color;
                    Console.Write(messages[i].message);
                }

                Console.SetCursorPosition(0, level.Height);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Player: Eros - {level.Player.HP}/100hp -- turn: {level.Player.CurrentTurn}");
                Console.ResetColor();
            }
            catch (ArgumentOutOfRangeException) { }
        }

        public static void Run(string levelFile)
        {
            level = new LevelData();
            level.Load(levelFile);
            Console.CursorVisible = false;

            bool running = true;
            while (running)
            {
                level.Draw(visionRadius);

                DrawUI();

                if (!Console.KeyAvailable)
                {
                    Thread.Sleep(30);
                    continue;
                }

                var keyInfo = Console.ReadKey(true);
                int dx = 0, dy = 0;
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow: dy = -1; break;
                    case ConsoleKey.DownArrow: dy = 1; break;
                    case ConsoleKey.LeftArrow: dx = -1; break;
                    case ConsoleKey.RightArrow: dx = 1; break;
                    case ConsoleKey.Escape: running = false; continue;
                }

                int targetX = level.Player.X + dx;
                int targetY = level.Player.Y + dy;

                var enemyAtTarget = level.GetEnemyAt(targetX, targetY);

                if (enemyAtTarget != null)
                {
                    Combat.CombatExchange(level.Player, enemyAtTarget);
                    level.RemoveDeadEnemies();

                    if (level.Player.HP <= 0)
                    {
                        GameLoop.GameMessageLog("You have died! Game over.", ConsoleColor.Red);
                        DrawUI();
                        running = false;
                        break;
                    }
                }
                else if (!level.IsBlocked(targetX, targetY))
                {
                    level.Player.X = targetX;
                    level.Player.Y = targetY;
                }

                level.Player.Turn(); // Hoppar upp med 100 vid combat lyckdes inte fixa detta.

                foreach (var enemy in level.Elements.OfType<Enemy>().ToList())
                {
                    if (enemy.IsDead) continue;

                    enemy.Update(level.Player, level);

                    if (enemy.X == level.Player.X && enemy.Y == level.Player.Y && !enemy.IsDead)
                    {
                        Combat.Attack(enemy, level.Player);
                        if (level.Player.HP <= 0)
                        {
                            GameLoop.GameMessageLog("You have died! Game over.", ConsoleColor.Red);
                            running = false;
                            break;
                        }
                    }
                }

                level.RemoveDeadEnemies();
                Thread.Sleep(50);
            }

            Console.SetCursorPosition(0, level.Height + 8);
            Console.CursorVisible = true;
            Console.WriteLine("Thanks for playing. Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}
