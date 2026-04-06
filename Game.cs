namespace ProjektGenspil
{
	internal class Game
	{
		public string Name { get; set; }
		public string Genre { get; set; }
		public int MinPlayers { get; set; }
		public int MaxPlayers { get; set; }
		public List<GameCopy> gameCopies { get; set; }

		public Game(string name, string genre, int minPlayers, int maxPlayers)
		{
			Name = name;
			Genre = genre;
			MinPlayers = minPlayers;
			MaxPlayers = maxPlayers;
			gameCopies = new List<GameCopy>();
		}
	}
}