using ISDCompanion.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ISDCompanion.Models
{
    public abstract class BaseModel : IExpandable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        private bool _InitExpanded = false;

        public bool InitExpanded
        {
            get => _InitExpanded;
            set
            {
                var oldValue = _InitExpanded;
                _InitExpanded = value;
                IsExpanded = value;
  
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

        public virtual int getHeight()
        {
            return -1;
        }

    }
}
