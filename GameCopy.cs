using ProjektGenspil;

internal class GameCopy
{
	public string Condition { get; set; }
	public decimal Price { get; set; }

	public GameCopy() { } // JsonSerializer bruger default constructor og sætter properties bagefter

	// public Game Game { get; set; }

	public GameCopy(string condition, decimal price, Game game)
	{
		Condition = condition;
		Price = price;
		//Game = game;
	}

	/*public void PrintGame()
	{
		Console.WriteLine($"{Game.Name}, {Game.Genre}, {Game.MinPlayers}-{Game.MaxPlayers} spillere, {Condition}, {Price} DKK.");
	}*/

	public void PrintGame(Game game)
	{
		Console.WriteLine($"Navn: {game.Name}");
		Console.WriteLine($"Genre: {game.Genre}");
		Console.WriteLine($"Spillere: {game.MinPlayers} - {game.MaxPlayers}");
		Console.WriteLine($"Stand: {Condition}");
		Console.WriteLine($"Pris: {Price}");
		Console.WriteLine("-----------------------");
	}
}