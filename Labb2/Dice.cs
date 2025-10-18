using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
     public class Dice
     {
        private int numberOfDice;
        private int sidesPerDice;
        private int modifier;
        private static readonly Random rng = new Random();

        public Dice(int numberOfDice, int sidesPerDice, int modifier)
        {
            this.numberOfDice = numberOfDice;
            this.sidesPerDice = sidesPerDice;
            this.modifier = modifier;
        }

        public int Throw()
        {
            int total = modifier;
            for (int i = 0; i < numberOfDice; i++)
                total += rng.Next(1, sidesPerDice + 1);
            return total;
        }

        public override string ToString()
        {
            string sign = modifier >= 0 ? "+" : "-";
            return $"{numberOfDice}d{sidesPerDice}{sign}{Math.Abs(modifier)}";
        }
     }
}
