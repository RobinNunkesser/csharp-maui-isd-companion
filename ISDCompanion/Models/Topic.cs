using ISDCompanion.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ISDCompanion.Models
{
    public class Topic : INotifyPropertyChanged, IExpandable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public int getHeight()
        {
            int tmp = 0;
            foreach (var item in Exercises)
            {
                tmp += item.getHeight();
            }
            return tmp + 70;
        }

        public string Topic_Title { get; set; }
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
        public TrulyObservableCollection<Exercise> Exercises { get; set; }

    }
}
