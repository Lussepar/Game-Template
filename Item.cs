class Item
{
    public int ItemPosX { get; set; }
    public int ItemPosY { get; set; }
    public char ItemSymbol { get; set; }
    public string ItemName { get; set; }

    public Item()
    {
        ItemPosX = 2;
        ItemPosY = 0;
        ItemSymbol = 'I';
        ItemName = "Potion";
    }
}