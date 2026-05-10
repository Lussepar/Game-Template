/* 

Klasserna Enemy och Player fungerar som mallar för att skapa objekt i spelet, och 
definierar vilka egenskaper (properties) spelaren och fienden har.”

*/

class Enemy
{
    public int EnemyPosX { get; set; }
    public int EnemyPosY { get; set; }
    public char EnemySymbol { get; set; }
    public string EnemyName { get; set; }
    public int EnemyHP { get; set; }
    public int EnemyAttack { get; set; }
    public int EnemyDefence { get; set; }
    public bool IsAlive { get; set; }

    public Enemy()
    {
        EnemyHP = 50;
        EnemyAttack = 4;
        EnemyDefence = 4;
        EnemySymbol = 'G';
        EnemyPosX = 9;
        EnemyPosY = 1;
        IsAlive = true;
    }
}
