class UI
{
    public static void GameMenu() // Visar menyn
    {
        Console.WriteLine("1) Play");
        Console.WriteLine("2) About");
        Console.WriteLine("3) Exit");
    }

    public static void ShowAbout()
    {
        Console.Clear();
        Console.WriteLine("This is a game.");
        Console.ReadKey();
        return;
    }

    public static void ShowStats(Player player) // Metod för att visa spelarens stats
    {
        Console.WriteLine("=====================");
        Console.WriteLine("Y O U R     S T A T S");
        Console.WriteLine();
        Console.WriteLine($"Health: {player.Health}");
        Console.WriteLine($"Attack: {player.Attack}");
        Console.WriteLine($"Defence: {player.Defence}");
        Console.WriteLine("=====================");
    }

    public static void AnyKey()
    {
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
    }
}