namespace ProjektGenspil
{
	using System;
	using System.Collections.Generic; // Lists
	using System.Diagnostics;
	using System.Linq; // Lookup
	using System.Text.Json;
	using Genspil;

	internal class Program
	{
		static string filePath = "SpilLager.json";
		static string requestFilePath = "Request.json";

		// Lister til spil og forespørgsler
		static List<Game> games = new List<Game>();
		static List<Request> requests = new List<Request>();

		static void Main(string[] args)
		{
			Console.Title = "Genspil Lagerstyring"; // Titel på konsol-vinduet 
			bool isRunning = true;

			LoadGames();
			LoadRequests();

			while (isRunning)
			{
				ShowMainMenu();

				string input = Console.ReadKey(true).KeyChar.ToString();

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
						TilføjKopiAfSpil();
						break;

					case "8":
						isRunning = false;
						break;


					default:
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("Indtast venligst et gyldigt tal");
						Console.ResetColor();
						Console.ReadKey(true);
						Console.Clear();
						break;
				}
			}
		}

			static void ShowMainMenu()
			{
				Console.Clear();
				Console.WriteLine("=== Genspil Lagerstyring ===");
				Console.WriteLine("1) Se lager");
				Console.WriteLine("2) Søg efter spil");
				Console.WriteLine("3) Tilføj spil");
				Console.WriteLine("4) Registrer forespørgsel");
				Console.WriteLine("5) Se forespørgsler");
				Console.WriteLine("6) Udskriv lagerliste");
				Console.WriteLine("7) Tilføj kopi af spil");
				Console.WriteLine("8) Exit");
			}

		static void VisLagerAfSpil()
		{
			Console.Clear();

			if (games.Count == 0)
			{
				Console.WriteLine("Ingen spil på lager.");
				ReturnToMenu();
				return;
			}

			Console.WriteLine("=== Nuværende lager ===\n");

			foreach (Game game in games)
			{
				// Hvis der ikke er kopier
				if (game.gameCopies.Count == 0)
				{
					Console.WriteLine($"{game.Name} (Ingen kopier registreret)");
				}
				else
				{
					foreach (GameCopy copy in game.gameCopies)
					{
						copy.PrintGame(game);
					}
				}
			}

			ReturnToMenu();
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

                Console.WriteLine("\n\n[ Tryk S for at søge ]");
                Console.WriteLine("[ Tryk M for at gå tilbage til hovedmenuen ]");
                string input = Console.ReadKey(true).KeyChar.ToString();

                switch (input)
                {
                    case "1":
                        Console.Write("Indtast titel (efterlad blank for at fjerne søgekriteriet): ");
                        title = Console.ReadLine();
                        if (title == "")
                        {
                            title = null;
                        }
                        break;
                    case "2":
                        Console.Write("Indtast genre (efterlad blank for at fjerne søgekriteriet): ");
                        genre = Console.ReadLine();
                        if (genre == "")
                        {
                            genre = null;
                        }
                        break;
                    case "3":
                        Console.Write("Indtast antal spillere (efterlad blank for at fjerne søgekriteriet): ");
                        string input3 = Console.ReadLine();
                        if (input3 == "")
                        {
                            players = -1;
                        }
                        else
                        {
                            players = Convert.ToInt32(input3);
                        }
                        break;
                    case "4":
                        Console.Write("Indtast minimum pris (efterlad blank for at fjerne søgekriteriet): ");
                        string input4 = Console.ReadLine();
                        if (input4 == "")
                        {
                            minPrice = -1;
                        }
                        else
                        {
                            minPrice = Convert.ToInt32(input4);

                            Console.Write($"Indtast maksimal pris: {minPrice} - ");
                            input4 = Console.ReadLine();
                            if (input4 == "")
                            {
                                maxPrice = -1;
                            }
                            else
                            {
                                maxPrice = Convert.ToInt32(input4);
                            }
                        }

                        break;
                    case "5":
                        Console.Write("Indtast stand (efterlad blank for at fjerne søgekriteriet): ");
                        condition = Console.ReadLine();
                        if (condition == "")
                        {
                            condition = null;
                        }
                        break;
                    case "s":
                        Console.WriteLine("\n=== Søgeresultat === ");
                        foreach (Game game in games)
						{
							foreach (GameCopy copy in game.gameCopies)
							{
								if ((title == null || game.Name == title)
									&& (genre == null || game.Genre == genre)
									&& (players == -1 || (game.MinPlayers <= players && game.MaxPlayers >= players))
									&& (minPrice == -1 || (copy.Price >= minPrice && copy.Price <= maxPrice))
									&& (condition == null || copy.Condition == condition))
								{
									copy.PrintGame(game); // tester om det virker
								}
							}
						}

                        Console.WriteLine("\n[ Tryk på en vilkårlig tast for at gå tilbage til menuen ]");
                        Console.ReadKey(true);
						break;
					case "m":
						exit = true;
						break;
				}
			} while (!exit);
		}

	

		static void TilføjSpil()
			{
			Console.Clear();
				Console.WriteLine("Du kan tilføje et eller flere spil til Systemet.");
				Console.WriteLine("----------------------------------------------");
				Console.Write("Indtast spillets navn: ");
				string userInputName = Console.ReadLine().ToLower();

				foreach (Game item in games) 
				{
					if (item.Name == userInputName)
					{
						Console.WriteLine($"{userInputName} eksisterer allerede i systemet.");
						ContinueOrMenu();
						return;
					}
				}

				Console.Write("Indtast genre: ");
				string userInputGenre = Console.ReadLine().ToLower();

				Console.Write("Índtast MinPlayers: ");
				int userInputMinPlayers = GetPlayerNrValid();

				Console.Write("Indtast MaxPlayers: ");
				int userInputMaxPlayers = GetPlayerNrValid();
				while (userInputMinPlayers > userInputMaxPlayers)
			{
		
				Console.WriteLine("MaxPlayers skal være større end eller lig med MinPlayers.");
				Console.WriteLine();
				Console.Write("Indtast MaxPlayers igen: ");
					userInputMaxPlayers = GetPlayerNrValid();
				}

				Game game = new Game(userInputName, userInputGenre, userInputMinPlayers, userInputMaxPlayers);
			games.Add(game);
			SaveGames();

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"{userInputName} - er nu tilføjet til systemet");
			Console.ResetColor();

			Console.WriteLine();
			ContinueOrMenu();
			}

			static string ContinueOrMenu()
			{
				Console.WriteLine("Vil du tilføje Spil mere? \n 1.ja \n 2.nej");

				string userAnswer = Console.ReadLine();

				while ((userAnswer != "1") && (userAnswer != "2"))
				{
					Console.WriteLine("Ugyldigt svar. Indtast venligst 1 eller 2.");
					userAnswer = Console.ReadLine();
				}

				Console.Clear();

				switch (userAnswer)
				{
					case "1":
						TilføjSpil(); // fortsæt med at tilføje
						break;

					case "2":
						ReturnToMenu(); // tilbage til din menu
						break;
				}

				return userAnswer;
			}
		
	

		public static int GetPlayerNrValid()
		{
			bool isValid = false;
			string userInputNumbers;
			int value = -1;
			//true --> while loop running.
			while (!isValid)
			{
				userInputNumbers = Console.ReadLine();
				bool isInteger = int.TryParse(userInputNumbers, out value);
				if (isInteger)
				{
					if (value > 0)
						isValid = true;
					else
					{
						Console.WriteLine("Indtast venligst positivt heltal."); //isValid is still false.--> while loop continues.
					}
				}
				else
				{
					Console.WriteLine("Indtast venligst kun heltal. Ikke abc og/eller decimeltal.");
				}
			}
			return value;
		}

		static void TilføjKopiAfSpil()
		{
			Console.Clear();

			if (games.Count == 0)
			{
				Console.WriteLine("Ingen spil på lager, tilføj venligst.");
                Console.WriteLine();
				ReturnToMenu();
				return;
			}

			Console.WriteLine("Vælg venligst et spil, du gerne vil lave en kopi af: ");
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Nedenstående spil, er spil vi har på lager");
			Console.ResetColor();
			Console.WriteLine();

			for (int i = 0; i < games.Count; i++)
			{
				Console.WriteLine($"{i + 1}) {games[i].Name}");
			}

            Console.WriteLine();
			Console.Write("Indtast venligst et tal fra ovenstående liste og afslut med <Enter>: ");

			int.TryParse(Console.ReadLine(), out int choice);

			if (choice < 1 || choice > games.Count)
			{
				Console.WriteLine("Ugyldigt valg.");
				ReturnToMenu();
				return;
			}

			Game original = games[choice - 1];

			Console.WriteLine("Indtast ny stand: ");
			string condition = Console.ReadLine();

			Console.WriteLine("Indtast ny pris: ");
			decimal.TryParse(Console.ReadLine(), out decimal price);

			GameCopy newCopy = new GameCopy(condition, price, original);

			original.gameCopies.Add(newCopy);
			SaveGames();

			Console.WriteLine("Kopi af spil er tilføjet!");

			ReturnToMenu();
		}

		static void RegistrerForespørgelser()
		{
			Console.Clear();

			Console.WriteLine("Indtast spillets navn: ");
			string gameName = Console.ReadLine();

			Console.WriteLine("Indtast kundens navn: ");
			string customerName = Console.ReadLine();

            Console.WriteLine("Indtast kundens email: ");
			string customerMail = Console.ReadLine();

            Console.WriteLine("Indtast kundens telefonnummer: ");
			string customerPhone = Console.ReadLine();

			Request newRequest = new Request
			{
				GameName = gameName,
				CustomerName = customerName,
				CustomerMail = customerMail,
				CustomerPhoneNumber = customerPhone
			};

			requests.Add(newRequest);
			SaveRequests();

			Console.WriteLine("Forespørgsel er registreret!");

			ReturnToMenu();
		}

		static void SeForespørgelser()
		{
			Console.Clear();

			if (requests.Count == 0)
			{
				Console.WriteLine("Ingen forespørgsler registreret.");
			}
			else
			{
				foreach (Request r in requests)
				{
					Console.WriteLine($"Spil: {r.GameName}");
					Console.WriteLine($"Kunde: {r.CustomerName}");
                    Console.WriteLine($"Email: {r.CustomerMail}");
                    Console.WriteLine($"TelefonNummer: {r.CustomerPhoneNumber}");
					Console.WriteLine("-----------------------");
				}
			}

			ReturnToMenu();
		}

		static void UdskrivLagerListe()
		{
			Console.Clear();

			if (games.Count == 0)
			{
				Console.WriteLine("Ingen spil på lager.");
				ReturnToMenu();
				return;
			}

			Console.WriteLine("=== Lagerliste ===\n");
			
			foreach (Game game in games)
			{
				foreach (GameCopy copy in game.gameCopies)
				{
					copy.PrintGame(game);
				}
			}

			Console.WriteLine();
			Console.WriteLine("\n=== Sorteret alfabetisk ===\n");

			var sorted = games.OrderBy(g => g.Name).ToList();

			foreach (Game game in sorted)
			{
				foreach (GameCopy copy in game.gameCopies)
				{
					copy.PrintGame(game);
				}
			}

            Console.WriteLine();
			Console.WriteLine("\n=== Omvendt alfabetisk ===\n");

			var sortedDescending = games.OrderByDescending(g => g.Name).ToList();

			foreach (Game game in sortedDescending)
			{
				foreach (GameCopy copy in game.gameCopies)
				{
					copy.PrintGame(game);
				}
			}

			ReturnToMenu();
		}

		static void LoadGames()
		{
			if (File.Exists(filePath))
			{
				string json = File.ReadAllText(filePath);

				games = JsonSerializer.Deserialize<List<Game>>(json) ?? new List<Game>();
			}
			else
			{
				games = new List<Game>();
			}
		}

		static void SaveGames()
		{
			string json = JsonSerializer.Serialize(games, new JsonSerializerOptions
			{
				WriteIndented = true
			});

			File.WriteAllText(filePath, json);
		}

		static void LoadRequests()
		{
			if (File.Exists(requestFilePath)) // Tjekker om forespørgsels-filen findes
			{
				string json = File.ReadAllText(requestFilePath); // Læser JSON

				requests = JsonSerializer.Deserialize<List<Request>>(json) ?? new List<Request>();
				// Konverterer JSON til liste
			}
			else
			{
				requests = new List<Request>(); // Opretter tom liste hvis fil ikke findes
			}
		}

		static void SaveRequests()
		{
			string json = JsonSerializer.Serialize(requests); // Konverterer forespørgsler til JSON

			File.WriteAllText(requestFilePath, json); // Gemmer i fil
		}

		static void ReturnToMenu()
		{
			Console.WriteLine();
			Console.WriteLine("Press 'M' to return to menu");

			while (true)
			{
				var key = Console.ReadKey(true).KeyChar;

				if (char.ToUpper(key) == 'M')
				{
					Console.Clear();
					break;
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("Tryk venligst på M for at gå tilbage: ");
					Console.ResetColor();
				}
			}
		}
	}
}