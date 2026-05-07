/* 

Klasserna Enemy och Player fungerar som mallar för att skapa objekt i spelet, och 
definierar vilka egenskaper (properties) spelaren har.”

*/

class Player
{
    public int posX { get; set; }
    public int posY { get; set; }
    public char PlayerSymbol { get; set; }
    public string PlayerName { get; set; }
    public int PlayerHealth { get; set; }
    public int PlayerAttack { get; set; }
    public int PlayerDefence { get; set; }
    public int PlayerSkillPoints { get; set; }

    public Player()

    {
        posY = 0;
        posX = 0;
        PlayerHealth = 100;
        PlayerAttack = 5;
        PlayerDefence = 5;
        PlayerSkillPoints = 10;
        PlayerSymbol = '@';
    }
}
