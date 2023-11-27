using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Text;
using ZooApp.Models;

namespace ZooApp.Views
{
    public class ZooView
    {
        public void DrawMenu(string[] menuOptions, int selectedOption)
        {
            Console.Clear();
            string zooArt = @"
   ███████╗ ██████╗  ██████╗ 
   ╚══███╔╝██╔═══██╗██╔═══██╗
     ███╔╝ ██║   ██║██║   ██║
    ███╔╝  ██║   ██║██║   ██║
   ███████╗╚██████╔╝╚██████╔╝
   ╚══════╝ ╚═════╝  ╚═════╝                                    
";
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(zooArt);
            Console.ResetColor();
            Console.WriteLine("");
            for (int i = 0; i < menuOptions.Length; i++)
            {
                Console.Write("");
                if (i == selectedOption)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                }

                Console.Write($" ==> {menuOptions[i]} <== ");

                if (i == selectedOption)
                {
                    Console.ResetColor();
                }

                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        public void ShowAnimalList(List<Animal> animals)
        {
            Console.WriteLine("+----------------------------------------------------------------------------------------------------------------+");
            Console.WriteLine("|                                             Lista zwierząt:                                                    |");

            // ASCII ART dla tabeli
            Console.WriteLine("+-------------------+-------------------+-------------------+----------------------------------------------------+");

            Console.WriteLine("| {0,-17} | {1,-17} | {2,-17} | {3,-50} |", "Nazwa", "Gatunek", "Środowisko", "Opis");

            Console.WriteLine("+-------------------+-------------------+-------------------+----------------------------------------------------+");

            foreach (var animal in animals)
            {
                string wrappedName = WrapText(animal.Name, 17);
                string wrappedSpecies = WrapText(animal.Species, 17);
                string wrappedHabitat = WrapText(animal.Habitat, 17);
                string wrappedDescription = WrapText(animal.Description, 50);

                string[] nameLines = wrappedName.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                string[] speciesLines = wrappedSpecies.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                string[] habitatLines = wrappedHabitat.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                string[] descriptionLines = wrappedDescription.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                int maxLines = Math.Max(Math.Max(nameLines.Length, speciesLines.Length), Math.Max(habitatLines.Length, descriptionLines.Length));

                for (int i = 0; i < maxLines; i++)
                {
                    Console.WriteLine("| {0,-17} | {1,-17} | {2,-17} | {3,-50} |",
                                      i < nameLines.Length ? nameLines[i] : string.Empty,
                                      i < speciesLines.Length ? speciesLines[i] : string.Empty,
                                      i < habitatLines.Length ? habitatLines[i] : string.Empty,
                                      i < descriptionLines.Length ? descriptionLines[i] : string.Empty);
                    Thread.Sleep(50);
                }
            }

            Console.WriteLine("+-------------------+-------------------+-------------------+----------------------------------------------------+");
        }

        private string WrapText(string text, int maxLength)
        {
            StringBuilder wrappedText = new StringBuilder();

            int index = 0;
            while (index < text.Length)
            {
                int length = Math.Min(maxLength, text.Length - index);
                wrappedText.AppendLine(text.Substring(index, length));
                index += length;
            }

            return wrappedText.ToString();
        }

        public void ShowMessage(string message)
        {
            // Animacja dla ShowMessage
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(20); // Aby uzyskać efekt pisania, można dostosować czas opóźnienia
            }
            Console.WriteLine();
        }
    }
}
