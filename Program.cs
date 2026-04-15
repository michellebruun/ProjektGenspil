namespace ProjektGenspil
{
	using Genspil;
	using System;
	using System.Collections.Generic; // Lists
	using System.Diagnostics;
	using System.Linq; // Lookup
	using System.Text.Json;

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
                        Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue

                        break;
				}
			}
		}

			static void ShowMainMenu()
			{
				Console.Clear();
				Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue
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
            Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue

            if (games.Count == 0)
			{
				Console.WriteLine("Ingen spil på lager.");
				ReturnToMenu();
				return;
			}

			Console.WriteLine("=== Spil på lager ===\n");

			foreach (Game game in games)
			{
				Console.WriteLine($"- {game.Name}");
			}

			ReturnToMenu();
		}

		static void SøgEfterSpil()
        {
            string title = null;
            string genre = null;
            int players = -1;
            decimal minPrice = -1;
            decimal maxPrice = -1;
            string condition = null;

            bool exit = false;

            Console.Clear();
            Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue
            do
            {
                Console.Clear();
                Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue
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
                if (minPrice > -1)
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
                        Console.Write("\nIndtast titel (efterlad blank for at fjerne søgekriteriet): ");
                        title = Console.ReadLine();
                        if (title == "")
                        {
                            title = null;
                        }
                        break;
                    case "2":
                        Console.Write("\nIndtast genre (efterlad blank for at fjerne søgekriteriet): ");
                        genre = Console.ReadLine();
                        if (genre == "")
                        {
                            genre = null;
                        }
                        break;
                    case "3":
                        Console.Write("\nIndtast antal spillere (efterlad blank for at fjerne søgekriteriet): ");
                        string input3 = Console.ReadLine();
                        if (input3 == "")
                        {
                            players = -1;
                        }
                        else
                        {
                            try
                            {
								players = Convert.ToInt32(input3);
							}
							catch
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine("Fejl: Indtast venligst et helt tal");
								Console.ResetColor();
                                Console.ReadKey(true);
                            }
                        }
                        break;
                    case "4":
                        Console.Write("\nIndtast minimumspris (efterlad blank for at fjerne søgekriteriet): ");
                        string input4 = Console.ReadLine();
                        if (input4 == "")
                        {
							minPrice = -1;
                        }
                        else
                        {
                            try
                            {
								minPrice = Convert.ToInt32(input4);
                                if (minPrice < 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Fejl: Prisen kan ikke være lavere end 0");
                                    Console.ResetColor();
                                    minPrice = -1;
                                    Console.ReadKey(true);
									break;
                                }
                            }
							catch
							{
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Fejl: Indtast venligst et helt tal");
                                Console.ResetColor();
                                Console.ReadKey(true);
                            }
                            Console.Write($"\nIndtast maksimalpris: {minPrice} - ");
                            input4 = Console.ReadLine();
                            if (input4 == "")
                            {
								minPrice = -1;
                            }
                            else
                            {
                                try
                                {
									maxPrice = Convert.ToInt32(input4);
									if (maxPrice <= minPrice)
									{
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Fejl: Maksimalprisen kan ikke være højere end minimumsprisen");
                                        Console.ResetColor();
                                        minPrice = -1;
                                        maxPrice = -1;
                                        Console.ReadKey(true);
                                    }
								}
                                catch
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Fejl: Indtast venligst et helt tal");
                                    Console.ResetColor();
                                    Console.ReadKey(true);
                                }
                            }
                        }

                        break;
                    case "5":
                        Console.Write("\nIndtast stand (efterlad blank for at fjerne søgekriteriet): ");
                        condition = Console.ReadLine();
                        if (condition == "")
                        {
                            condition = null;
                        }
                        break;
                    case "s":
						Console.Clear();
                        Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue

                        Console.WriteLine("=== Søgeresultat === ");

                        int longestTitle = games.Max(g => g.Name.Length);
                        int longestGenre = games.Max(g => g.Genre.Length);

						string header = "Titel".PadRight(longestTitle) + " | Genre".PadRight(longestGenre + 3) + " | Antal spillere".PadRight(14) + " | Pris i DKK.".PadRight(5) + " | Stand";
						Console.WriteLine(header);
						string separator = "----------------";
						for (int i = 0; i < header.Length; i++)
							separator += "-";
                        Console.WriteLine(separator);

                        foreach (Game game in games)
						{
							foreach (GameCopy copy in game.gameCopies)
							{
								if ((title == null || game.Name.ToLower() == title.ToLower())
									&& (genre == null || game.Genre.ToLower() == genre.ToLower())
									&& (players == -1 || (game.MinPlayers <= players && game.MaxPlayers >= players))
									&& (minPrice == -1 || (copy.Price >= minPrice && copy.Price <= maxPrice))
									&& (condition == null || copy.Condition.ToLower() == condition.ToLower()))
								{
                                    copy.PrintGame(game, longestTitle, longestGenre);
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
            Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue
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
				Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue

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
					Console.WriteLine("Indtast venligst kun heltal. Ikke abc og/eller decimaltal.");
				}
			}
			return value;
		}

		static void TilføjKopiAfSpil()
		{
			Console.Clear();
            Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue

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
            Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue

            Console.WriteLine("Indtast spillets navn: ");
			string gameName = Console.ReadLine();

			Console.WriteLine("Indtast kundens navn: ");
			string customerName = Console.ReadLine();

            Console.WriteLine("Indtast kundens email: ");
			string customerMail = Console.ReadLine();

            Console.WriteLine("Indtast kundens telefonnummer: ");
			string customerPhone = Console.ReadLine();

			Console.WriteLine("Indtast ønskede stand: ");
			string customerCondition = Console.ReadLine();

			Console.WriteLine("Indtast ønskede pris: ");
			string customerPrice = Console.ReadLine();

			Request newRequest = new Request
			{
				GameName = gameName,
				CustomerName = customerName,
				CustomerMail = customerMail,
				CustomerPhoneNumber = customerPhone,
				CustomerCondition = customerCondition,
				CustomerPrice = customerPrice
			};

			requests.Add(newRequest);
			SaveRequests();

			Console.WriteLine("Forespørgsel er registreret!");

			ReturnToMenu();
		}

		static void SeForespørgelser()
		{
			Console.Clear();
            Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue

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
                    Console.WriteLine($"Ønskede stand: {r.CustomerCondition}");
                    Console.WriteLine($"Ønskede pris: {r.CustomerPrice}");
					Console.WriteLine("-----------------------");
				}
			}

			ReturnToMenu();
		}

		static void UdskrivLagerListe()
		{
			Console.Clear();
            Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue

            if (games.Count == 0)
			{
				Console.WriteLine("Ingen spil på lager.");
				ReturnToMenu();
				return;
			}
            /*
			Console.WriteLine("=== Lagerliste ===\n");

            foreach (Game game in games)
            {
                foreach (GameCopy copy in game.gameCopies)
                {
                    copy.PrintGame(game);
                }
            }
			*/
            Console.WriteLine("=== Lagerliste ===");
            Console.WriteLine("Vælg en sortering:\n");
            Console.WriteLine("1) Titel (A-Z)");
            Console.WriteLine("2) Titel (Z-A)");
            Console.WriteLine("3) Genre (A-Z)");
            Console.WriteLine("4) Genre (Z-A)");
			string input = Console.ReadKey(true).KeyChar.ToString();

            int longestTitle = games.Max(g => g.Name.Length);
            int longestGenre = games.Max(g => g.Genre.Length);

            string header = "Titel".PadRight(longestTitle) + " | Genre".PadRight(longestGenre + 3) + " | Antal spillere".PadRight(14) + " | Pris i DKK.".PadRight(5) + " | Stand";
            string separator = "----------------";
            for (int i = 0; i < header.Length; i++)
                separator += "-";


            switch (input)
			{
				case "1":
					Console.Clear();
                    Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue
                    Console.WriteLine("=== Lagerliste - Efter titel (A-Z) ===\n");

                    Console.WriteLine(header);
                    Console.WriteLine(separator);

                    var sortedTitle = games.OrderBy(g => g.Name).ToList();
					
                    foreach (Game game in sortedTitle)
                    {
                        foreach (GameCopy copy in game.gameCopies)
                        {
                            copy.PrintGame(game, longestTitle, longestGenre);
                        }
                    }
                    break;
				case "2":
					Console.Clear();
                    Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue
                    Console.WriteLine("=== Lagerliste - Efter titel (Z-A) ===\n");

                    Console.WriteLine(header);
                    Console.WriteLine(separator);

                    var sortedTitleDescending = games.OrderByDescending(g => g.Name).ToList();

                    foreach (Game game in sortedTitleDescending)
                    {
                        foreach (GameCopy copy in game.gameCopies)
                        {
                            copy.PrintGame(game, longestTitle, longestGenre);
                        }
                    }
					break;
                case "3":
					Console.Clear();
                    Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue
                    Console.WriteLine("=== Lagerliste - Efter genre (A-Z) ===\n");

                    Console.WriteLine(header);
                    Console.WriteLine(separator);

                    var sortedGenre = games.OrderBy(g => g.Genre).ToList();

                    foreach (Game game in sortedGenre)
                    {
                        foreach (GameCopy copy in game.gameCopies)
                        {
                            copy.PrintGame(game, longestTitle, longestGenre);
                        }
                    }
                    break;
                case "4":
					Console.Clear();
                    Console.Write("\x1b[3J"); // Fix til Console.Clear() bug i Windows 11's konsolvindue
                    Console.WriteLine("\n=== Lagerliste - Efter genre (Z-A) ===\n");

                    Console.WriteLine(header);
                    Console.WriteLine(separator);

                    var sortedGenreDescending = games.OrderByDescending(g => g.Genre).ToList();

                    foreach (Game game in sortedGenreDescending)
                    {
                        foreach (GameCopy copy in game.gameCopies)
                        {
                            copy.PrintGame(game, longestTitle, longestGenre);
                        }
                    }
                    break;
				default:
                    Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Ugyldigt input, prøv venligst igen");
					Console.ResetColor();
                    break;
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