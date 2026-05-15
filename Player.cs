/* 

Klasserna Enemy och Player fungerar som mallar för att skapa objekt i spelet, och 
definierar vilka egenskaper (properties) spelaren har.”

*/

class Player
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public char Symbol { get; set; }
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }
    public int SkillPoints { get; set; }
    public bool IsAlive { get; set; }

    public Player()

    {
        PosY = 1;
        PosX = 1;
        Health = 100;
        Attack = 5;
        Defence = 5;
        SkillPoints = 10;
        Symbol = '@';
        IsAlive = true;
    }

    public static string ShowSkillPointMenu(Player player)
    {
        Console.WriteLine();
        Console.WriteLine($"Assign your stats. You have {player.SkillPoints} left.");
        Console.WriteLine("[A] for Attack");
        Console.WriteLine("[D] for Defence");
        Console.WriteLine("[H] for Health");
        string SkillAssignment = Console.ReadLine().ToUpper();
        return SkillAssignment;
    }

    public static void IncreaseAttack(Player player)
    {
        player.SkillPoints--;
        player.Attack++;
    }

    public static void IncreaseHealth(Player player)
    {
        player.SkillPoints--;
        player.Health++;
    }

    public static void IncreaseDefence(Player player)
    {
        player.SkillPoints--;
        player.Defence++;
    }
}
