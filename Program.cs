
class Program //Blueprint 
{
    public static void GameMenu()
    {
        Console.WriteLine("1) Play");
        Console.WriteLine("2) About");
        Console.WriteLine("3) Exit");
    }

    public static void ShowStats(Player player)
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
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','.','.','.','#','.','.','.','.','.','.','.','#'},
                                {'#','#','#','.','#','.','#','#','#','#','#','.','#'},
                                {'#','.','.','.','#','.','#','.','.','.','.','.','#'},
                                {'#','.','#','#','#','.','#','.','#','#','#','#','#'},
                                {'#','.','.','.','.','.','#','.','.','.','.','.','D'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#'},
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
                if (!item.IsPickedUp)
                {
                    renderMap[item.ItemPosY, item.ItemPosX] = item.ItemSymbol;
                }


                if (!item.IsPickedUp && item.ItemPosY == player.posY && item.ItemPosX == player.posX)

                {
                    item.IsPickedUp = true;
                    player.PlayerHealth += 10;


                }
            }

            // Placerar ut alla enemies på kartan
            foreach (Enemy enemy in enemies)
            {
                if (enemy.IsAlive)
                {
                    renderMap[enemy.EnemyPosY, enemy.EnemyPosX] = enemy.EnemySymbol;
                }

                if (enemy.IsAlive && enemy.EnemyPosY == player.posY && enemy.EnemyPosX == player.posX)
                {
                    Battle battle = new Battle();
                    string result = battle.StartBattle(player, enemy);

                    if (result == "WIN")
                    {
                        enemy.IsAlive = false;
                        Console.ReadKey();
                    }
                    else if (result == "LOSE")
                    {
                        Console.WriteLine("Back to menu...");
                        Program.GameMenu();
                    }
                    else if (result == "RUN")
                    {
                        Console.WriteLine("You ran away!");
                        Console.ReadKey();
                    }
                }
            }


            // Placerar ut spelaren sist på kartan
            renderMap[player.posY, player.posX] = '@';





            // render loop (ej map utan rendermap )
            for (int i = 0; i < renderMap.GetLength(0); i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < renderMap.GetLength(1); j++)
                    Console.Write(renderMap[i, j] + " ");
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press [W, A, S, D] to control the player");
            Console.WriteLine("");
            ShowStats(player);


            ConsoleKeyInfo key = Console.ReadKey(true);


            // Rörelselogik, Kolla först med newX/newY, flytta sedan player.posY/posX
            // ReadKey(true) istället för ReadLine för input

            int newX = player.posX;
            int newY = player.posY;


            if (key.Key == ConsoleKey.W)
            {
                newY--;
            }
            else if (key.Key == ConsoleKey.A)
            {
                newX--;
            }
            else if (key.Key == ConsoleKey.D)
            {
                newX++;
            }
            else if (key.Key == ConsoleKey.S)
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
            Console.Clear();
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