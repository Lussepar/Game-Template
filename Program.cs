
class Program //Blueprint 
{
    static void GameMenu()
    {
        Console.WriteLine("1) Play");
        Console.WriteLine("2) About");
        Console.WriteLine("3) Exit");
    }

    static void ShowStats(Player player)
    {
        Console.WriteLine("=====================");
        Console.WriteLine("Y O U R     S T A T S");
        Console.WriteLine();
        Console.WriteLine($"Health: {player.PlayerHealth}");
        Console.WriteLine($"Attack: {player.PlayerAttack}");
        Console.WriteLine($"Defence: {player.PlayerDefence}");
        Console.WriteLine("======================");
    }

    static void StartGame()
    {
        Console.Clear();
        Player player = new Player();

        Console.WriteLine("Welcome to character creation!");
        Console.WriteLine();
        Console.Write("Name your character: ");

        string input = Console.ReadLine();
        player.PlayerName = input;

        SkillPoints(player); // Argument = det du skickar in
    }

    static void SkillPoints(Player player) // Parameter = det som tar emot
    {
        while (player.PlayerSkillPoints > 0)
        {
            Console.Clear();
            ShowStats(player);

            Console.WriteLine();
            Console.WriteLine($"Assign your stats. You have {player.PlayerSkillPoints} left.");
            Console.WriteLine("[A] for Attack");
            Console.WriteLine("[D] for Defence");
            Console.WriteLine("[H] for Health");
            string SkillAssignment = Console.ReadLine().ToUpper();

            if (SkillAssignment == "A")
            {
                player.PlayerSkillPoints--;
                player.PlayerAttack++;
                continue;
            }
            else if (SkillAssignment == "H")
            {
                player.PlayerSkillPoints--;
                player.PlayerHealth += 10;
                continue;
            }
            else if (SkillAssignment == "D")
            {
                player.PlayerSkillPoints--;
                player.PlayerDefence++;
                continue;
            }
            else
            {
                Console.WriteLine("Invalid input. Press any key...");
                Console.ReadKey();

            }
        }
        Console.Clear();
        ShowStats(player);

        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();

        GameRun(player);
    }

    static void GameRun(Player player) // Läs om ReadChar
    {
        List<Enemy> enemies = new List<Enemy>(); // Lista med alla enemies i spelet
        enemies.Add(new Enemy());

        List<Item> items = new List<Item>(); //Lista med alla items i spelet
        items.Add(new Item());

        while (true)
        {

            Console.Clear();
            var map = new char[,]{
                                {'.','.','.','.'},
                                {'#','#','#','.'},
                                {'#','.','.','.'},
                                {'#','D','#','#'}
                                }; // Statisk karta (bakgrund)


            // Skapar en tom karta som ska renderas
            var renderMap = new char[map.GetLength(0), map.GetLength(1)];

            // Kopierar hela map till renderMap
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    renderMap[i, j] = map[i, j];
                }

            }
            // Placerar ut alla items på kartan
            foreach (Item item in items)
            {
                renderMap[item.ItemPosY, item.ItemPosX] = item.ItemSymbol;
            }

            // Placerar ut alla enemies på kartan
            foreach (Enemy enemy in enemies)
            {
                renderMap[enemy.EnemyPosY, enemy.EnemyPosX] = enemy.EnemySymbol;
            }


            // Placerar ut spelaren sist på kartan
            renderMap[player.posY, player.posX] = '@';





            // render loop (ej map utan rendermap )
            for (int i = 0; i < renderMap.GetLength(0); i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < renderMap.GetLength(1); j++)
                    Console.Write(renderMap[i, j]);
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press W, A, S, D to control the player");
            Console.WriteLine("");
            string playerMovement = Console.ReadLine().ToUpper();


            // Rörelselogik, Kolla först med newX/newY, flytta sedan player.posY/posX

            int newX = player.posX;
            int newY = player.posY;

            if (playerMovement == "W")
            {
                newY--;
            }
            else if (playerMovement == "A")
            {
                newX--;
            }
            else if (playerMovement == "D")
            {
                newX++;
            }
            else if (playerMovement == "S")
            {
                newY++;
            }
            else
            {
                continue;
            }

            if (newY >= 0 && newY < map.GetLength(0) && newX >= 0 && newX < map.GetLength(1))
            {
                if (map[newY, newX] != '#')
                {
                    player.posX = newX;
                    player.posY = newY;
                }
            }
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
            GameMenu();

            if (!int.TryParse(Console.ReadLine(), out int gameMenuChoice)) //“Här används int.TryParse för att kontrollera om input kan omvandlas till ett heltal, utan att programmet kraschar.”
            {
                Console.WriteLine("Invalid input. Press any key...");
                Console.ReadKey();
                continue;
            }

            switch (gameMenuChoice)
            {
                case 1:
                    StartGame();
                    break;

                case 2:
                    ShowAbout();
                    break;

                case 3:
                    return;
            }


        }
    }
}