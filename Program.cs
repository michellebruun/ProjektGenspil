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
		// Lister til spil og forespørgsler
		static List<game> games = new List<game>();
		static List<Request> requests = new List<Request>();

		static void Main(string[] args)
		{
			Console.Title = "Genspil Lagerstyring"; // Titel på konsol-vinduet 
			bool isRunning = true;

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
					isRunning = false;
					break;

				case "8":
					TilføjKopiAfSpil();
					break;

				default:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Indtast venligst et gyldigt tal");
					Console.ReadKey(true);
					Console.Clear();
					break;
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
			Console.WriteLine("7) Exit");
			Console.WriteLine("8) Tilføj kopi af spil");
		}

		static void VisLagerAfSpil()
		{
		}

		static void SøgEfterSpil()
		{
		}

		static void TilføjSpil()
		{
		}

		static void TilføjKopiAfSpil()
		{
			Console.Clear();

			if (games.Count == 0)
			{
				Console.WriteLine("Ingen spil at kopiere.");
				ReturnToMenu();
				return;
			}

			Console.WriteLine("Vælg et spil du vil kopiere:");

			for (int i = 0; i < games.Count; i++)
			{
				Console.WriteLine($"{i + 1}) {games[i].Name}");
			}

			int.TryParse(Console.ReadLine(), out int choice);

			if (choice < 1 || choice > games.Count)
			{
				Console.WriteLine("Ugyldigt valg.");
				ReturnToMenu();
				return;
			}

			game original = games[choice - 1];

			Console.WriteLine("Indtast ny stand: ");
			string condition = Console.ReadLine();

			Console.WriteLine("Indtast ny pris: ");
			decimal.TryParse(Console.ReadLine(), out decimal price);

			Console.WriteLine("Er spillet på lager? (ja/nej): ");
			string stockInput = Console.ReadLine();
			bool inStock = stockInput.ToLower() == "ja";

			game copy = new game
			{
				Name = original.Name,
				Genre = original.Genre,
				MinPlayers = original.MinPlayers,
				MaxPlayers = original.MaxPlayers,
				Condition = condition,
				Price = price,
				InStock = inStock
			};

			games.Add(copy);

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
		}

		static void LoadGames()
		{
		}

		static void SaveGames()
		{
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