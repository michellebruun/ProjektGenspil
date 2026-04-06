namespace ProjektGenspil
{
	internal class GameCopy
	{
		public string Condition { get; set; }
		public decimal Price { get; set; }
		/* public Game Game { get; set; } */ // Fjernet, da det skabte gentagne reference ved JSON serialisering

		public GameCopy(string condition, decimal price, Game game)
		{
			Condition = condition;
			Price = price;
			/*Game = game;*/
		}
	}
}