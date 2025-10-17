using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    abstract class LevelElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; protected set; }
        public ConsoleColor Color { get; protected set; }

        public virtual void Draw(bool visible)
        {
            if (!visible) return;
            try
            {
                Console.ForegroundColor = Color;
                Console.SetCursorPosition(X, Y);
                Console.Write(Symbol);
                Console.ResetColor();
            }
            catch (ArgumentOutOfRangeException) { }
        }
    }
}