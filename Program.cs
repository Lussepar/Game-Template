class Program //Blueprint 
{
    public static void GameMenu() // Visar menyn
    {
        Console.WriteLine("1) Play");
        Console.WriteLine("2) About");
        Console.WriteLine("3) Exit");
    }

    public static void ShowStats(Player player) // Metod för att visa spelarens stats
    {
        Console.WriteLine("=====================");
        Console.WriteLine("Y O U R     S T A T S");
        Console.WriteLine();
        Console.WriteLine($"Health: {player.Health}");
        Console.WriteLine($"Attack: {player.Attack}");
        Console.WriteLine($"Defence: {player.Defence}");
        Console.WriteLine("======================");
    }

    static void StartGame() // Start av spel med alternativ 1
    {
        Console.Clear();
        Player player = new Player();

        Console.WriteLine("Welcome to character creation!");
        Console.WriteLine();
        Console.Write("Name your character: ");

        string input = Console.ReadLine();
        player.Name = input;

        SkillPoints(player); // Argument = det du skickar in
    }

    /*
    player skickas in som ett argument till metoden SkillPoints.
    Metoden tar emot objektet genom parametern Player player.
    */

    static void SkillPoints(Player player) // Parameter = det som tar emot
    {
        while (player.SkillPoints > 0)
        {
            Console.Clear();
            ShowStats(player);

            Console.WriteLine();
            Console.WriteLine($"Assign your stats. You have {player.SkillPoints} left.");
            Console.WriteLine("[A] for Attack");
            Console.WriteLine("[D] for Defence");
            Console.WriteLine("[H] for Health");
            string SkillAssignment = Console.ReadLine().ToUpper();

            if (SkillAssignment == "A")
            {
                player.SkillPoints--;
                player.Attack++;
                continue;
            }
            else if (SkillAssignment == "H")
            {
                player.SkillPoints--;
                player.Health += 10;
                continue;
            }
            else if (SkillAssignment == "D")
            {
                player.SkillPoints--;
                player.Defence++;
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

    static void GameRun(Player player) // Fixat ReadKey istället för ReadLine
    {
        List<Enemy> enemies = new List<Enemy>(); // Lista med alla enemies i spelet
        enemies.Add(new Enemy());

        List<Item> items = new List<Item>(); //Lista med alla items i spelet
        items.Add(new Item());

        while (true)
        {

            Console.Clear();

            // Statisk karta (bakgrund) En tvådimensionell array av typen char
            var map = new char[,]{
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                {'#','.','.','.','#','.','.','.','.','.','.','.','#'},
                                {'#','#','#','.','#','.','#','#','#','#','#','.','#'},
                                {'#','.','.','.','#','.','#','.','.','.','.','.','#'},
                                {'#','.','#','#','#','.','#','.','#','#','#','#','#'},
                                {'#','.','.','.','.','.','#','.','.','.','.','.','D'},
                                {'#','#','#','#','#','#','#','#','#','#','#','#','#'},
                                };

            // Skapar en tom karta som ska renderas. var används för implicit typning, vilket innebär att kompilatorns automatiskt identifierar vilken datatyp variablen ska ha baserat på värdet som tilldelas.
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
                    renderMap[item.PosY, item.PosX] = item.Symbol;
                }


                if (!item.IsPickedUp && item.PosY == player.PosY && item.PosX == player.PosX)

                {
                    item.IsPickedUp = true;
                    player.Health += 10;
                }
            }

            // Placerar ut alla enemies på kartan
            foreach (Enemy enemy in enemies)
            {
                if (enemy.IsAlive)
                {
                    renderMap[enemy.PosY, enemy.PosX] = enemy.Symbol;
                }

                if (enemy.IsAlive && enemy.PosY == player.PosY && enemy.PosX == player.PosX)
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
                        Console.ReadKey();
                        return;
                    }
                    else if (result == "RUN")
                    {
                        Console.WriteLine("You ran away!");
                        Console.ReadKey();
                    }
                }
            }

            // Placerar ut spelaren sist på kartan
            renderMap[player.PosY, player.PosX] = '@';

            /*
            render loop:
            Här används två nästlade for-loopar för att rendera ut hela kartan i konsolen.

            */
            for (int i = 0; i < renderMap.GetLength(0); i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < renderMap.GetLength(1); j++)
                {
                    Console.Write(renderMap[i, j] + " ");
                }
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press [W, A, S, D] to control the player");
            Console.WriteLine("");
            ShowStats(player);

            ConsoleKeyInfo key = Console.ReadKey(true);

            // Rörelselogik, Kolla först med newX/newY, flytta sedan player.posY/posX
            // ReadKey(true) istället för ReadLine för input

            int newX = player.PosX;
            int newY = player.PosY;

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

            /*
            Här används Console.ReadKey(true) för att läsa av ett tangenttryck direkt från användaren utan att användaren behöver trycka Enter. 
            Argumentet true gör även att tangenttrycket inte skrivs ut i konsolen.

            Tangenttrycket lagras i variabeln key av typen ConsoleKeyInfo.

            Därefter skapas variablerna newX och newY, där spelarens nuvarande koordinater kopieras. 
            Dessa används som temporära testkoordinater för att kontrollera om spelaren kan flytta till den nya positionen innan spelarens riktiga position uppdateras.

            Om användaren exempelvis trycker på W, så minskas newY med 1 eftersom spelaren ska röra sig uppåt i arrayen.

            Efter detta kontrollerar en if-sats att de nya koordinaterna befinner sig
            inom arrayens gränser genom att jämföra koordinaterna med arrayens storlek via GetLength().

            Om koordinaterna är giltiga kontrollerar nästa if-sats att positionen inte innehåller en vägg ('#').

            Om positionen inte är en vägg uppdateras spelarens riktiga koordinater genom att newX och newY 
            kopieras över till player.PosX och player.PosY. Detta resulterar i att spelaren flyttas på kartan.



            */
            if (newY >= 0 && newY < map.GetLength(0) && newX >= 0 && newX < map.GetLength(1))
            {
                if (map[newY, newX] != '#')
                {
                    player.PosX = newX;
                    player.PosY = newY;
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