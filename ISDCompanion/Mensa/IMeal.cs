namespace ISDCompanion
{
    public interface IMeal
    {
        string Name { get; set; }
        string Image { get; set; }
        string Price { get; set; }
        Allergens Allergens { get; set; }
        Additives Additives { get; set; }
        Category Category { get; set; }
    }
}