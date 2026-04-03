namespace ProjektGenspil
{
    using System;
    using System.Collections.Generic; // Lists
    using System.Diagnostics;
    using System.Linq; // Lookup
    using System.Text.Json;
    using System.Xml.Linq;

    internal class Program
    {
        // Lister til spil og forespørgsler
        static List<Game> games = new List<Game>();
        static List<Request> requests = new List<Request>();

        static void Main(string[] args)
        {

            Console.Title = "Genspil Lagerstyring"; // Titel på konsol-vinduet 
            bool isRunning = true;
            // Changed
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
                        Console.Clear();
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

                        //case "8":
                        //TilføjKopiAfSpil();
                        //break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Indtast venligst et gyldigt tal");
                        Console.ReadKey(true);
                        Console.ResetColor(); // Changed
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
            Console.WriteLine("7) Exit");
            Console.WriteLine("8) Tilføj kopi af spil");
        }

        static void VisLagerAfSpil()
        {
        }

        static void SøgEfterSpil()
        {
        }

        //Note: min,max--> negative nr, Spil already exist --> Error message (x), back to menu --> while isRunning ?? (x)
        static void TilføjSpil()
        {
            Console.WriteLine("TilføjSpil: Du kan tilføje spil til Systemet.");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Indsat spil navn.");
            string userInputName = Console.ReadLine().ToLower();

            foreach (Game item in games)
            {
                if (item.Name == userInputName)
                {
                    Console.WriteLine($"{userInputName} eksisterer allerede i systemet.");
                    Console.WriteLine("Tryk en tast, for at komme tilbage til Menu. ");
                    Console.ReadKey();
                    return;
                }

            }

            Console.WriteLine("Indsat genre.");
            string userInputGenre = Console.ReadLine().ToLower();

            //Fix 2 things : String and int error (x), negative number()
            Console.WriteLine("Indsat MinPlayers.");
            bool isInteger = false;
            string userInputMinPlayers;
            //true --> while loop running
            while (!isInteger) 
            {
                userInputMinPlayers = Console.ReadLine();
                int value;
                isInteger = int.TryParse(userInputMinPlayers, out value);
                if(value < 0)
                {
                    isInteger = true;
                    Console.WriteLine("Indsat venligst positivt heltal.");
                }
                Console.WriteLine("Indsat venligst kun heltal.");
            }
          
                
            Console.WriteLine("Indsat MaxPlayers.");
            int userInputMaxPlayers = int.Parse(Console.ReadLine());

           
          
            
            // Game game = new Game(userInputName, userInputGenre, userInputMinPlayers, userInputMaxPlayers);
            // games.Add(game);
    
            Console.WriteLine($"Nu {userInputName} var tilføjet til systemet.");
            Console.WriteLine("Vil du tilføje Spil mere? \n 1.ja \n 2.nej");

            String userAnswer = Console.ReadLine();
            while ((userAnswer != "1") && (userAnswer !="2"))
            {
                Console.WriteLine("Ugyldigt svar.Indsat venligst 1 eller 2");
                userAnswer = Console.ReadLine();
            }
            Console.Clear();
            switch (userAnswer) 
            {
                case "1":
                    TilføjSpil();
                    break;
                case "2":
                    Console.WriteLine("Du vil tilbage til Menu.");
                    Console.ReadLine();
                    ShowMainMenu();                    
                    break;
            }     
        }

        /*
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

            Game original = games[choice - 1];

            Console.WriteLine("Indtast ny stand: ");
            string condition = Console.ReadLine();

            Console.WriteLine("Indtast ny pris: ");
            decimal.TryParse(Console.ReadLine(), out decimal price);

            Console.WriteLine("Er spillet på lager? (ja/nej): ");
            string stockInput = Console.ReadLine();
            bool inStock = stockInput.ToLower() == "ja";

            
            Game copy = new Game
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
        */
       

        static void RegistrerForespørgelser()
        {
            Console.Clear();

            Console.WriteLine("Indtast spillets navn: ");
            string gameName = Console.ReadLine();

            Console.WriteLine("Indtast kundens navn: ");
            string customerName = Console.ReadLine();

            Request newRequest = new Request
            {
                GameName = gameName,
                CustomerName = customerName
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