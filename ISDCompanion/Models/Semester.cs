using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ISDCompanion.Models
{
    public class Semester
    {
        public string Semester_Title { get; set; }

        public ObservableCollection<Module> Modules { get; set; }    
    }
}
