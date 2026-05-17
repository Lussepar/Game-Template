using System.Collections.Concurrent;
using System.Reflection.Metadata.Ecma335;

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

        SkillPoints(player);
    }

    static void SkillPoints(Player player)
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

    static void GameRun(Player player)
    {
        List<Enemy> enemies = new List<Enemy>();
        enemies.Add(new Enemy { PosX = 3, PosY = 1 });
        enemies.Add(new Enemy { PosX = 10, PosY = 3 });

        List<Item> items = new List<Item>();
        items.Add(new Item());

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

        int doorX = 12;
        int doorY = 5;



        while (player.Health > 0)
        {
            Console.Clear();

            // Skapar en tom karta som ska renderas. var används för implicit typning, kompilatorn automatiskt identifierar vilken datatyp variablen ska ha baserat på värdet som tilldelas.
            var renderMap = new char[map.GetLength(0), map.GetLength(1)];

            CopyMapToRenderMap(map, renderMap);

            // Placerar ut alla items på kartan
            HandleItems(items, renderMap, player);

            // Placerar ut fienden på kartan
            DrawEnemy(enemies, renderMap);

            // Hanterar battle logik
            HandleEnemyBattle(enemies, player);

            // Placerar ut spelaren sist på kartan
            renderMap[player.PosY, player.PosX] = '@';

            RenderMap(renderMap);

            UI.ControlPlayer();
            UI.ShowStats(player);

            PlayerMovement(player, map);

            if (player.PosY == doorY && player.PosX == doorX)
            {
                Console.WriteLine("You reached the exit!");
                Console.ReadKey();
                return;
            }
        }
    }
    public static void CopyMapToRenderMap(char[,] map, char[,] renderMap)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                renderMap[i, j] = map[i, j];
            }
        }
    }
    public static void HandleItems(List<Item> items, char[,] renderMap, Player player)
    {
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
    }
    public static void DrawEnemy(List<Enemy> enemies, char[,] renderMap)
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy.IsAlive)
            {
                renderMap[enemy.PosY, enemy.PosX] = enemy.Symbol;
            }
        }
    }
    public static void HandleEnemyBattle(List<Enemy> enemies, Player player)
    {
        foreach (Enemy enemy in enemies)
        {
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
    }
    public static void RenderMap(char[,] renderMap)
    {
        for (int i = 0; i < renderMap.GetLength(0); i++)
        {
            Console.WriteLine("");
            for (int j = 0; j < renderMap.GetLength(1); j++)
            {
                Console.Write(renderMap[i, j] + " ");
            }
        }
    }
    public static bool PlayerMovement(Player player, char[,] map)
    {
        ConsoleKeyInfo key = Console.ReadKey(true);

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
            return false;
        }

        if (newY >= 0 && newY < map.GetLength(0) && newX >= 0 && newX < map.GetLength(1))
        {
            if (map[newY, newX] != '#')
            {
                player.PosX = newX;
                player.PosY = newY;
            }
        }
        return true;
    }
}
