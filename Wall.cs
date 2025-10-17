using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Wall : LevelElement
    {
        public Wall(int x, int y)
        {
            X = x; Y = y;
            Symbol = '#';
            Color = ConsoleColor.Gray;
        }
    }
}
