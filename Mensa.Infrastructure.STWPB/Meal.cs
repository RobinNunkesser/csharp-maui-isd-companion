using System;
using System.Text.Json.Serialization;

namespace Mensa.Infrastructure.STWPB
{
    public partial class Meal
    {
        /*[JsonPropertyName("date")]
        public DateTimeOffset Date { get; set; }*/

        [JsonPropertyName("name_de")]
        public string NameDe { get; set; }

        [JsonPropertyName("name_en")]
        public string NameEn { get; set; }

        [JsonPropertyName("description_de")]
        public string DescriptionDe { get; set; }

        [JsonPropertyName("description_en")]
        public string DescriptionEn { get; set; }

        /*[JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("category_de")]
        public CategoryDe CategoryDe { get; set; }

        [JsonPropertyName("category_en")]
        public CategoryEn CategoryEn { get; set; }*/

        [JsonPropertyName("subcategory_de")]
        public string SubcategoryDe { get; set; }

        [JsonPropertyName("subcategory_en")]
        public string SubcategoryEn { get; set; }

        [JsonPropertyName("priceStudents")]
        public double PriceStudents { get; set; }

        [JsonPropertyName("priceWorkers")]
        public double PriceWorkers { get; set; }

        [JsonPropertyName("priceGuests")]
        public double PriceGuests { get; set; }

        /*[JsonPropertyName("allergens")]
        public AllergenEnum[] Allergens { get; set; }*/

        [JsonPropertyName("order_info")]
        public long OrderInfo { get; set; }

        /*[JsonPropertyName("badges")]
        public Badge[] Badges { get; set; }

        [JsonPropertyName("restaurant")]
        public Restaurant Restaurant { get; set; }

        [JsonPropertyName("pricetype")]
        public Pricetype Pricetype { get; set; }*/

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }
    }

    public enum AllergenEnum { A1, A10, A11, A12, A3, A4, A5, A6, A7, A8, A9 };

    public enum Badge { Nonfat, Vegan, Vegetarian };

    public enum Category { Dessert, Dish, Empty, Sidedish };

    public enum CategoryDe { Beilagen, Dessert, Empty, Essen };

    public enum CategoryEn { Dessert, Dish, Empty, SideDish };

    public enum Pricetype { Fixed };

    public enum Restaurant { Cafete, MensaHamm, MensaLippstadt };
    
}
