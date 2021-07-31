using System;
using System.IO;
using System.Text.RegularExpressions;

namespace BeatSaberFullComboViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            var playerLogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+"Low", "Hyperbolic Magnetism", "Beat Saber", "PlayerData.dat");
            if(!File.Exists(playerLogFilePath))
            {
                Console.WriteLine($"No Player data found at\n{playerLogFilePath}");
                Console.ReadKey();
                return;
            }
            Console.WriteLine($"Reading:\n{playerLogFilePath}...\n");
            string playerLog = File.ReadAllText(playerLogFilePath);

            var difficulties = new string[]
            {
                "Easy",
                "Normal",
                "Hard",
                "Expert",
                "Expert+",
                "Export++?",
                "Export+++(WTF?)",
            };

            Console.WriteLine($"=== FULL COMBO ===\n\n");

            for (int i = 0; i < difficulties.Length; i++)
            {
                var difficultyIndex = i;
                var search = @"({""levelId"":""\w+"",""difficulty"":"+ difficultyIndex + @",""beatmapCharacteristicName"":""Standard"",""[\w\d"":,]+""fullCombo"":true)";
                var matches = Regex.Matches(playerLog, search);
                if(matches.Count == 0)
                {
                    continue;
                }
                Console.WriteLine($"{difficulties[difficultyIndex]}: {matches.Count}\n");
            }

            Console.WriteLine($"\n\nPress something to exit");
            Console.ReadKey();
        }
    }
}
