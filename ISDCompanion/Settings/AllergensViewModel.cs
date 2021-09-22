using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ISDCompanion
{
    public class AllergensViewModel : INotifyPropertyChanged
    {
        public AllergensViewModel()
        {
        }

        public bool A1 {
            get => Settings.Allergens.HasFlag(Allergens.A1);
            set
            {
                if (value != A1)
                {
                    Settings.Allergens |= Allergens.A1;
                    if (!value) Settings.Allergens ^= Allergens.A1;
                    OnPropertyChanged();
                }
            }
        }

        public bool A2
        {
            get => Settings.Allergens.HasFlag(Allergens.A2);
            set
            {
                if (value != A2)
                {
                    Settings.Allergens |= Allergens.A2;
                    if (!value) Settings.Allergens ^= Allergens.A2;
                    OnPropertyChanged();
                }
            }
        }

        public bool A3
        {
            get => Settings.Allergens.HasFlag(Allergens.A3);
            set
            {
                if (value != A3)
                {
                    Settings.Allergens |= Allergens.A3;
                    if (!value) Settings.Allergens ^= Allergens.A3;
                    OnPropertyChanged();
                }
            }
        }

        public bool A4
        {
            get => Settings.Allergens.HasFlag(Allergens.A4);
            set
            {
                if (value != A4)
                {
                    Settings.Allergens |= Allergens.A4;
                    if (!value) Settings.Allergens ^= Allergens.A4;
                    OnPropertyChanged();
                }
            }
        }

        public bool A5
        {
            get => Settings.Allergens.HasFlag(Allergens.A5);
            set
            {
                if (value != A5)
                {
                    Settings.Allergens |= Allergens.A5;
                    if (!value) Settings.Allergens ^= Allergens.A5;
                    OnPropertyChanged();
                }
            }
        }

        public bool A6
        {
            get => Settings.Allergens.HasFlag(Allergens.A6);
            set
            {
                if (value != A6)
                {
                    Settings.Allergens |= Allergens.A6;
                    if (!value) Settings.Allergens ^= Allergens.A6;
                    OnPropertyChanged();
                }
            }
        }

        public bool A7
        {
            get => Settings.Allergens.HasFlag(Allergens.A7);
            set
            {
                if (value != A7)
                {
                    Settings.Allergens |= Allergens.A7;
                    if (!value) Settings.Allergens ^= Allergens.A7;
                    OnPropertyChanged();
                }
            }
        }

        public bool A8
        {
            get => Settings.Allergens.HasFlag(Allergens.A8);
            set
            {
                if (value != A8)
                {
                    Settings.Allergens |= Allergens.A8;
                    if (!value) Settings.Allergens ^= Allergens.A8;
                    OnPropertyChanged();
                }
            }
        }

        public bool A9
        {
            get => Settings.Allergens.HasFlag(Allergens.A9);
            set
            {
                if (value != A9)
                {
                    Settings.Allergens |= Allergens.A9;
                    if (!value) Settings.Allergens ^= Allergens.A9;
                    OnPropertyChanged();
                }
            }
        }

        public bool A10
        {
            get => Settings.Allergens.HasFlag(Allergens.A10);
            set
            {
                if (value != A10)
                {
                    Settings.Allergens |= Allergens.A10;
                    if (!value) Settings.Allergens ^= Allergens.A10;
                    OnPropertyChanged();
                }
            }
        }

        public bool A11
        {
            get => Settings.Allergens.HasFlag(Allergens.A11);
            set
            {
                if (value != A11)
                {
                    Settings.Allergens |= Allergens.A11;
                    if (!value) Settings.Allergens ^= Allergens.A11;
                    OnPropertyChanged();
                }
            }
        }

        public bool A12
        {
            get => Settings.Allergens.HasFlag(Allergens.A12);
            set
            {
                if (value != A12)
                {
                    Settings.Allergens |= Allergens.A12;
                    if (!value) Settings.Allergens ^= Allergens.A12;
                    OnPropertyChanged();
                }
            }
        }

        public bool A13
        {
            get => Settings.Allergens.HasFlag(Allergens.A13);
            set
            {
                if (value != A13)
                {
                    Settings.Allergens |= Allergens.A13;
                    if (!value) Settings.Allergens ^= Allergens.A13;
                    OnPropertyChanged();
                }
            }
        }

        public bool A14
        {
            get => Settings.Allergens.HasFlag(Allergens.A14);
            set
            {
                if (value != A14)
                {
                    Settings.Allergens |= Allergens.A14;
                    if (!value) Settings.Allergens ^= Allergens.A14;
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
