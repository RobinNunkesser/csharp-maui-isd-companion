using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ISDCompanion
{
    public class AdditivesViewModel : INotifyPropertyChanged
    {
        public AdditivesViewModel()
        {
        }

        public bool A1
        {
            get => Settings.Additives.HasFlag(Additives.A1);
            set
            {
                if (value != A1)
                {
                    Settings.Additives |= Additives.A1;
                    if (!value) Settings.Additives ^= Additives.A1;
                    OnPropertyChanged();
                }
            }
        }

        public bool A2
        {
            get => Settings.Additives.HasFlag(Additives.A2);
            set
            {
                if (value != A2)
                {
                    Settings.Additives |= Additives.A2;
                    if (!value) Settings.Additives ^= Additives.A2;
                    OnPropertyChanged();
                }
            }
        }

        public bool A3
        {
            get => Settings.Additives.HasFlag(Additives.A3);
            set
            {
                if (value != A3)
                {
                    Settings.Additives |= Additives.A3;
                    if (!value) Settings.Additives ^= Additives.A3;
                    OnPropertyChanged();
                }
            }
        }

        public bool A4
        {
            get => Settings.Additives.HasFlag(Additives.A4);
            set
            {
                if (value != A4)
                {
                    Settings.Additives |= Additives.A4;
                    if (!value) Settings.Additives ^= Additives.A4;
                    OnPropertyChanged();
                }
            }
        }

        public bool A5
        {
            get => Settings.Additives.HasFlag(Additives.A5);
            set
            {
                if (value != A5)
                {
                    Settings.Additives |= Additives.A5;
                    if (!value) Settings.Additives ^= Additives.A5;
                    OnPropertyChanged();
                }
            }
        }

        public bool A6
        {
            get => Settings.Additives.HasFlag(Additives.A6);
            set
            {
                if (value != A6)
                {
                    Settings.Additives |= Additives.A6;
                    if (!value) Settings.Additives ^= Additives.A6;
                    OnPropertyChanged();
                }
            }
        }

        public bool A7
        {
            get => Settings.Additives.HasFlag(Additives.A7);
            set
            {
                if (value != A7)
                {
                    Settings.Additives |= Additives.A7;
                    if (!value) Settings.Additives ^= Additives.A7;
                    OnPropertyChanged();
                }
            }
        }

        public bool A8
        {
            get => Settings.Additives.HasFlag(Additives.A8);
            set
            {
                if (value != A8)
                {
                    Settings.Additives |= Additives.A8;
                    if (!value) Settings.Additives ^= Additives.A8;
                    OnPropertyChanged();
                }
            }
        }

        public bool A9
        {
            get => Settings.Additives.HasFlag(Additives.A9);
            set
            {
                if (value != A9)
                {
                    Settings.Additives |= Additives.A9;
                    if (!value) Settings.Additives ^= Additives.A9;
                    OnPropertyChanged();
                }
            }
        }

        public bool A10
        {
            get => Settings.Additives.HasFlag(Additives.A10);
            set
            {
                if (value != A10)
                {
                    Settings.Additives |= Additives.A10;
                    if (!value) Settings.Additives ^= Additives.A10;
                    OnPropertyChanged();
                }
            }
        }

        public bool A11
        {
            get => Settings.Additives.HasFlag(Additives.A11);
            set
            {
                if (value != A11)
                {
                    Settings.Additives |= Additives.A11;
                    if (!value) Settings.Additives ^= Additives.A11;
                    OnPropertyChanged();
                }
            }
        }

        public bool A12
        {
            get => Settings.Additives.HasFlag(Additives.A12);
            set
            {
                if (value != A12)
                {
                    Settings.Additives |= Additives.A12;
                    if (!value) Settings.Additives ^= Additives.A12;
                    OnPropertyChanged();
                }
            }
        }

        public bool A13
        {
            get => Settings.Additives.HasFlag(Additives.A13);
            set
            {
                if (value != A13)
                {
                    Settings.Additives |= Additives.A13;
                    if (!value) Settings.Additives ^= Additives.A13;
                    OnPropertyChanged();
                }
            }
        }

        public bool A14
        {
            get => Settings.Additives.HasFlag(Additives.A14);
            set
            {
                if (value != A14)
                {
                    Settings.Additives |= Additives.A14;
                    if (!value) Settings.Additives ^= Additives.A14;
                    OnPropertyChanged();
                }
            }
        }

        public bool A15
        {
            get => Settings.Additives.HasFlag(Additives.A15);
            set
            {
                if (value != A15)
                {
                    Settings.Additives |= Additives.A15;
                    if (!value) Settings.Additives ^= Additives.A15;
                    OnPropertyChanged();
                }
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
    }
}
