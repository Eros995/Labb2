using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class LevelData
    {
        private List<LevelElement> elements = new List<LevelElement>();
        public IReadOnlyList<LevelElement> Elements => elements;
        public Player Player { get; private set; }

        private HashSet<(int x, int y)> rememberedWalls = new HashSet<(int, int)>();
        private Dictionary<Enemy, bool> enemySeen = new Dictionary<Enemy, bool>(); 

        public int Width { get; private set; } = 0;
        public int Height { get; private set; } = 0;

        public void Load(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"Level file not found: {filename}");

            string[] lines = File.ReadAllLines(filename);
            Height = lines.Length;
            Width = lines.Max(l => l.Length);

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    switch (c)
                    {
                        case '#': elements.Add(new Wall(x, y)); break;
                        case 'r':
                            var rat = new Rat(x, y);
                            elements.Add(rat);
                            enemySeen[rat] = false;
                            break;
                        case 's':
                            var snake = new Snake(x, y);
                            elements.Add(snake);
                            enemySeen[snake] = false;
                            break;
                        case '@':
                            Player = new Player(x, y);
                            elements.Add(Player);
                            break;
                    }
                }
            }
        }

        public LevelElement GetElementAt(int x, int y) => elements.FirstOrDefault(e => e.X == x && e.Y == y);
        public Enemy GetEnemyAt(int x, int y) => elements.OfType<Enemy>().FirstOrDefault(e => e.X == x && e.Y == y && !e.IsDead);
        public bool IsBlocked(int x, int y) => x < 0 || y < 0 || x >= Width || y >= Height || elements.OfType<Wall>().Any(w => w.X == x && w.Y == y);

        public void RemoveDeadEnemies()
        {
            var dead = elements.OfType<Enemy>().Where(e => e.IsDead).ToList();
            foreach (var d in dead)
            {
                if (elements.Contains(d))
                {
                    elements.Remove(d);
                    GameLoop.GameMessageLog($"{d.Name} has been defeated!", ConsoleColor.Yellow);
                    enemySeen.Remove(d);
                }
            }
        }

        private Dictionary<LevelElement, (int x, int y)> previousPositions = new();

        public void Draw(int visionRadius)
        {
            if (Player == null) return;

            int px = Player.X;
            int py = Player.Y;

            foreach (var element in elements)
            {
                if (previousPositions.TryGetValue(element, out var pos))
                {
                    Console.SetCursorPosition(pos.x, pos.y);
                    Console.Write(' ');
                }
            }

            foreach (var wall in elements.OfType<Wall>())
            {
                double distance = Math.Sqrt(Math.Pow(wall.X - px, 2) + Math.Pow(wall.Y - py, 2));
                if (distance <= visionRadius)
                    rememberedWalls.Add((wall.X, wall.Y));

                wall.Draw(distance <= visionRadius || rememberedWalls.Contains((wall.X, wall.Y)));
            }

            foreach (var enemy in elements.OfType<Enemy>())
            {
                if (enemy.IsDead) continue;

                double distance = Math.Sqrt(Math.Pow(enemy.X - px, 2) + Math.Pow(enemy.Y - py, 2));
                bool visible = distance <= visionRadius;

                enemy.Draw(visible);
                previousPositions[enemy] = (enemy.X, enemy.Y);
            }

            Player.Draw(true);
            previousPositions[Player] = (Player.X, Player.Y);
        }
    }
}
