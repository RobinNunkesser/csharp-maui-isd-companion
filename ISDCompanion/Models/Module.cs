using ISDCompanion.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ISDCompanion.Models
{
    public class Module : BaseModel
    {
        public override int getHeight()
        {
            if (!InitExpanded)
            {
                return base.getHeight();
            }

            int tmp = 0;
            foreach (var item in Topics)
            {
                tmp += item.getHeight();
            }
            return tmp + 70;
        }

        private string _Module_Title;
        public string Module_Title
        {
            get { return _Module_Title; }
            set
            {
                _Module_Title = value;
                OnPropertyChanged(nameof(Module_Title));
            }
        }

        public TrulyObservableCollection<Topic> Topics { get; set; }
    }
}
