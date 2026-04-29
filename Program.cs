
class Program
{
    static void GameMenu()
    {
        Console.Clear();
        Console.WriteLine("1) Play");
        Console.WriteLine("2) About");
        Console.WriteLine("3) Exit");
    }

    static void StartGame()
    {
        Console.Clear();
        Player player = new Player();

        player.PlayerHealth = 100;
        player.PlayerAttack = 10;
        player.PlayerDefence = 5;
        player.PlayerSkillPoints = 10;

        Console.WriteLine("Welcome to character creation!");
        Console.WriteLine();
        Console.Write("Name your character: ");

        string input = Console.ReadLine();
        player.PlayerName = input;

        SkillPoints(player);
    }

    static void SkillPoints(Player player)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=====================");
            Console.WriteLine("Y O U R     S T A T S");
            Console.WriteLine();
            Console.WriteLine($"Health: {player.PlayerHealth}");
            Console.WriteLine($"Attack: {player.PlayerAttack}");
            Console.WriteLine($"Defence: {player.PlayerDefence}");
            Console.WriteLine("======================");
            Console.WriteLine();
            Console.WriteLine($"Assign your stats. You have {player.PlayerSkillPoints} left.");
            Console.WriteLine("[A] for Attack");
            Console.WriteLine("[D] for Defence");
            Console.WriteLine("[H] for Health");
            string SkillAssignment = Console.ReadLine();

            if (SkillAssignment == "A") ;
        }

    }

    static void ShowAbout()
    {
        Console.WriteLine("This is a game.");
        Console.ReadKey();
        return;
    }

    static void Main()
    {
        while (true)
        {
            Console.Clear();
            GameMenu();

            int gameMenuChoice = int.Parse(Console.ReadLine());

            switch (gameMenuChoice)
            {
                case 1:
                    StartGame();
                    break;

                case 2:
                    ShowAbout();
                    break;

                case 3:
                    break;
            }


        }
    }
}