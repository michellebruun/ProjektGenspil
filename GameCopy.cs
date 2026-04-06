using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektGenspil
{
    internal class GameCopy : Game
    {
        public string Condition { get; set; }
        public decimal Price { get; set; }

        public GameCopy(string name, string genre, int minPlayers, int maxPlayers, string condition, decimal price)
        {
            Name = name;
            Genre = genre;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            Condition = condition;
            Price = price;
        }

        public void PrintGame()
        {
            Console.WriteLine($"{Name}, {Genre}, {MinPlayers}-{MaxPlayers} spillere, {Condition}, {Price} DKK.");
        }
    }
}
