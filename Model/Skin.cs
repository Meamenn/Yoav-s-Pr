using Model;

public class Skin : BaseEntity
{
    public string SkinName { get; set; }
    public string Rarity { get; set; }
    public int Cost { get; set; }

    public Skin() { }
}