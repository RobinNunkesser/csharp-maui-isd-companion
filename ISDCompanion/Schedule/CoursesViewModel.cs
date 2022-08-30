using System;
using System.Collections.Generic;

namespace ISDCompanion
{
    public class CoursesViewModel : ViewModel
    {

        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>
            {
            new ItemViewModel { Text = "Sponge", Detail = "Detail 1" },
            new ItemViewModel { Text = "Banana", Detail = "Detail 2" },
            new ItemViewModel { Text = "Laptop", Detail = "Detail 3" },
            new ItemViewModel { Text = "Teddy Bear", Detail = "Detail 4" }
            };

        public CoursesViewModel()
        {
        }
    }

    public class ItemViewModel
    {
        public string Text { get; set; }
        public string Detail { get; set; }
    }
}

