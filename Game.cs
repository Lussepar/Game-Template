class Game
{
    public static void StartGame()
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

    static void SkillPoints(Player player) // Parameter = det som tar emot
    {
        while (player.SkillPoints > 0)
        {
            Console.Clear();
            UI.ShowStats(player);

            string choice = Player.ShowSkillPointMenu(player);

            if (choice == "A")
            {
                Player.IncreaseAttack(player);
                continue;
            }
            else if (choice == "H")
            {
                Player.IncreaseHealth(player);
                continue;
            }
            else if (choice == "D")
            {
                Player.IncreaseDefence(player);
                continue;
            }
            else
            {
                Console.WriteLine("Invalid input. Press any key...");
                Console.ReadKey();
                continue;
            }
        }
        Console.Clear();
        UI.ShowStats(player);
        UI.AnyKey();
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
            UI.ShowStats(player);

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
}