using System;
using Xamarin.Essentials;

namespace ISDCompanion
{
    public static class Settings
    {
        private static readonly int SemesterDefault = 0;
        private static readonly int SpecializationDefault = 0;

        public static int Semester
        {
            get => Preferences.Get(nameof(Semester), SemesterDefault);
            set => Preferences.Set(nameof(Semester), value);
        }
        public static int Specialization
        {
            get => Preferences.Get(nameof(Specialization), SpecializationDefault);
            set => Preferences.Set(nameof(Specialization), value);
        }
    }
}
