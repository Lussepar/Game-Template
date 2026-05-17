/* 

Klasserna Enemy och Player fungerar som mallar för att skapa objekt i spelet, och 
definierar vilka egenskaper (properties) spelaren och fienden har.”

*/

class Enemy
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public char Symbol { get; set; }
    public string Name { get; set; }
    public int HP { get; set; }
    public int Attack { get; set; }
    public int Defence { get; set; }
    public bool IsAlive { get; set; }

    public Enemy()
    {
        HP = 50;
        Attack = 7;
        Defence = 4;
        Symbol = 'G';
        IsAlive = true;
    }
}
