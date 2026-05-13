class Item
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public char Symbol { get; set; }
    public string Name { get; set; }
    public bool IsPickedUp { get; set; }

    public Item()
    {
        PosX = 2;
        PosY = 5;
        Symbol = '+';
        Name = "Potion";
        IsPickedUp = false;
    }
}