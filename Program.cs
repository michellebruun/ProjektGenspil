namespace ProjektGenspil
{
	using System;
	using System.Collections.Generic; // Lists
    using System.Diagnostics;
    using System.Linq; // Lookup
	using System.Text.Json;



	internal class Program
	{

		static List<GameCopy> GameList = new List<GameCopy>();
		static void Main(string[] args)
		{
			Console.Title = "Genspil Lagerstyring"; // Titel på konsol-vinduet 
			bool isRunning = true;

			do
			{
                ShowMainMenu();
                string input = Console.ReadKey(true).KeyChar.ToString(); // Denne konvetering betyder, at consollen venter på én tast, og derefter konvetere den tallet til string > Brug den i nedenstående Switch

                switch (input)
                {
                    case "1":
                        VisLagerAfSpil();
                        break;

                    case "2":
                        SøgEfterSpil();
                        break;

                    case "3":
                        TilføjSpil();
                        break;

                    case "4":
                        RegistrerForespørgelser();
                        break;

                    case "5":
                        SeForespørgelser();
                        break;

                    case "6":
                        UdskrivLagerListe();
                        break;

                    case "7":
                        isRunning = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Indtast venligst et gyldigt tal");
                        Console.ReadKey(true); // Betyder: "Hvis ikke denne tast i consollen"
                        Console.Clear(); // Rydder fejl-beskeden ved fejl-input
                        break;
                }
            } while (isRunning);
		}

		static void ShowMainMenu()
		{
			Console.Clear();
			Console.ResetColor();
            Console.WriteLine("=== Genspil Lagerstyring ===");
			Console.WriteLine("1) Se lager");
			Console.WriteLine("2) Søg efter spil");
			Console.WriteLine("3) Tilføj spil");
			Console.WriteLine("4) Registrer forespørgsel");
			Console.WriteLine("5) Se forespørgsler");
			Console.WriteLine("6) Udskriv lagerliste");
			Console.WriteLine("7) Exit");
		}

		static void VisLagerAfSpil()
		{
			foreach (GameCopy game in GameList)
			{
				game.PrintGame();
			}
			Console.ReadKey(true);
        }

		static void SøgEfterSpil()
		{
			string title = null;
			string genre = null;
			int players = -1;
			decimal minPrice = -1;
			decimal maxPrice = 9999;
			string condition = null;

			bool exit = false;

            Console.Clear();
            do
            {
				Console.Clear();
                Console.WriteLine("=== Søg efter spil ===");
				Console.WriteLine("Vælg et søgekriterie: ");
				Console.Write("\n1) Titel");
				if (title != null)
				{
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($" [ {title} ]");
                    Console.ResetColor();
                }
                Console.Write("\n2) Genre");
                if (genre != null)
				{
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($" [ {genre} ]");
                    Console.ResetColor();
                }
				Console.Write("\n3) Antal Spillere");
				if (players != -1)
				{
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($" [ {players} ]");
                    Console.ResetColor();
                }
				//decimal minPriceDisplay = minPrice >= 0 ? minPrice : 0;
                Console.Write("\n4) Pris");
				if (minPrice > 0)
				{
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.Write($" [ {minPrice} - {maxPrice} DKK. ]");
					Console.ResetColor();
				}
				Console.Write("\n5) Stand");
				if (condition != null)
				{
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($" [ {condition} ]");
                    Console.ResetColor();
                }

				Console.WriteLine("\n\nTryk S for at søge");
				string input = Console.ReadKey(true).KeyChar.ToString();

				switch (input)
				{
					case "1":
						Console.Write("Indtast titel (efterlad blank for at fjerne søgekriteriet): ");
						title = Console.ReadLine();
						if (title == "")
							title = null;
						break;
					case "2":
						Console.Write("Indtast genre (efterlad blank for at fjerne søgekriteriet): ");
						genre = Console.ReadLine();
						if (genre == "")
							genre = null;
						break;
					case "3":
						Console.Write("Indtast antal spillere (efterlad blank for at fjerne søgekriteriet): ");
						string input3 = Console.ReadLine();
						if (input3 == "")
							players = -1;
						else
							players = Convert.ToInt32(input3);

						break;
					case "4":
						Console.Write("Indtast minimum pris (efterlad blank for at fjerne søgekriteriet): ");
						string input4 = Console.ReadLine();
						if (input4 == "")
							minPrice = -1;
						else
							minPrice = Convert.ToInt32(input4);
						Console.Write("Indtast maksimal pris (efterlad blank for at fjerne søgekriteriet): ");
                        input4 = Console.ReadLine();
                        if (input4 == "")
                            maxPrice = -1;
                        else
                            maxPrice = Convert.ToInt32(input4);
                        break;
					case "5":
						Console.Write("Indtast stand (efterlad blank for at fjerne søgekriteriet): ");
						condition = Console.ReadLine();
						if (condition == "")
							condition = null;
						break;
					case "s":
                        Console.WriteLine("\nSøgeresultat (efterlad blank for at fjerne søgekriteriet): ");
                        foreach (GameCopy game in GameList)
                        {
                            if ((title == null || game.Name == title)
								&& (genre == null || game.Genre == genre) 
								&& (players == -1 || (game.MinPlayers <= players && game.MaxPlayers >= players))
								&& (minPrice == -1 || (game.Price >= minPrice && game.Price <= maxPrice))
                                && (condition == null || game.Condition == condition))
							{
                                game.PrintGame();
                            }	
                        }
                        Console.ReadKey(true);
                        break;
                    case "x":
                        exit = true;
                        break;
                }
            } while (!exit);
        }

		static void TilføjSpil()
		{
            Console.Write("Titel: ");
			string title = Console.ReadLine();
            Console.Write("Genre: ");
            string genre = Console.ReadLine();
            Console.Write("Min players: ");
            int minPlayers = Convert.ToInt32(Console.ReadLine());
            Console.Write("Max players: ");
            int maxPlayers = Convert.ToInt32(Console.ReadLine());
            Console.Write("Stand: ");
            string condition = Console.ReadLine();
            Console.Write("Pris: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

			GameList.Add(new GameCopy(title, genre, minPlayers, maxPlayers, condition, price));
		}

		static void RegistrerForespørgelser()
		{

		}

		static void SeForespørgelser()
		{

		}

		static void UdskrivLagerListe()
		{

		}

		static void LoadGames()
		{

		}

		static void SaveGames()
		{

		}

		static void ReturnToMenu()
		{

		}

	}

}


