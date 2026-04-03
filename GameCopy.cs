using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektGenspil
{
    internal class GameCopy
    {
        public string Condition { get; set; }
        public decimal Price { get; set; }
        public Game Game { get; set; }
        public GameCopy (string condition, decimal price, Game game)
        {
            Condition = condition;
            Price = price;
            Game = game;
        }

    }
}
