using ISDCompanion.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ISDCompanion.Models
{
    public class Semester : INotifyPropertyChanged, IExpandable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        public int getHeight()
        {
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
        private bool _IsExpanded = false;


        public bool IsExpanded
        {
            get
            {
                return _IsExpanded;
            }
            set
            {
                _IsExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public TrulyObservableCollection<Module> Modules { get; set; }

    }
}
