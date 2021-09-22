namespace ISDCompanion
{
    internal class Meal : IMeal
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }
        public Allergens Allergens { get; set; }
        public Additives Additives { get; set; }
        public Category Category { get; set; }
    }
}