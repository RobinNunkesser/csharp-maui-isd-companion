using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ISDCompanion.Models
{
    public class Topic
    {
        public string Topic_Title { get; set; }

        public ObservableCollection<Exercise> Exercises { get; set; }
    }
}
