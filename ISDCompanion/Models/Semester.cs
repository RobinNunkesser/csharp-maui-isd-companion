using ISDCompanion.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ISDCompanion.Models
{
    public class Semester : BaseModel
    {
        public override int getHeight()
        {
            if (!InitExpanded)
            {
                return base.getHeight();
            }

            int tmp = 0;
            foreach (var item in Modules)
            {
                tmp += item.getHeight();
            }
            return tmp + 80;
        }

        private string _Semester_Title;
        public string Semester_Title
        {
            get
            {
                return _Semester_Title;
            }
            set
            {
                _Semester_Title = value;
                OnPropertyChanged(nameof(Semester_Title));
            }
        }
        public TrulyObservableCollection<Module> Modules { get; set; }

    }
}
