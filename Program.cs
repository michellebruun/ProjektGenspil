namespace ProjektGenspil
{
	using System;
	using System.Collections.Generic; // Lists
    using System.Diagnostics;
    using System.Linq; // Lookup
	using System.Text.Json;



	internal class Program
	{

		static void Main(string[] args)

		{
			Console.Title = "Genspil Lagerstyring"; // Titel på konsol-vinduet 
			bool isRunning = true;

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


