using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektGenspil
{
    internal class Game
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public bool InStock { get; set; }

    }
}
