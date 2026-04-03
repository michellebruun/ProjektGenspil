using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektGenspil
{
    internal class Game
    {
        /*
       private string name;

       public string getName()
       {
           return name;
       }
       public void setName(string name)
       {
           this.name = name;
       }
       */
        //auto‑implemented properties.
        public string Name { get; set; }
        public string Genre { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public List<GameCopy> gameCopies { get; set; }

        public Game (string name, string genre, int minPlayers, int maxPlayers)
        {
            Name = name;
            Genre = genre;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
            gameCopies = new List<GameCopy>();
        }
    }
}
