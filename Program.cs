class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            UI.GameMenu();

            if (!int.TryParse(Console.ReadLine(), out int gameMenuChoice)) //“Här används int.TryParse för att kontrollera om input kan omvandlas till ett heltal, utan att programmet kraschar.”
            {
                Console.WriteLine("Invalid input. Press any key...");
                Console.ReadKey();
                continue;
            }

            switch (gameMenuChoice)
            {
                case 1:
                    Game.StartGame();
                    break;

                case 2:
                    UI.ShowAbout();
                    break;

                case 3:
                    return;
            }
        }
    }
}