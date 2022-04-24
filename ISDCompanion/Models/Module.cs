using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ISDCompanion.Models
{
    public class Module
    {
        public string Module_Title { get; set; }

        public ObservableCollection<Topic> Topics { get; set; }
    }
}
