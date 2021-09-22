using System;
using System.Collections.Generic;
using DevExpress.XamarinForms.Scheduler;
using Xamarin.Essentials;

namespace ISDCompanion
{
    public static class Settings
    {
        private static readonly int SemesterDefault = 0;
        private static readonly int SpecializationDefault = 0;
        private static readonly int AllergensDefault = 0b11111111111111;
        private static readonly int AdditivesDefault = 0b111111111111111;

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
        public static Allergens Allergens
        {
            get => (Allergens)Preferences.Get(nameof(Allergens), AllergensDefault);
            set => Preferences.Set(nameof(Allergens), (int)value);
        }
        public static Additives Additives
        {
            get => (Additives)Preferences.Get(nameof(Additives), AdditivesDefault);
            set => Preferences.Set(nameof(Additives), (int)value);
        }

        public static List<AppointmentItem> Appointments
        {
            get
            {
                switch (Semester)
                {
                    case 0: return AppointmentHelper.appointmentsSem1;
                    case 1: return AppointmentHelper.appointmentsSem2;
                    case 2: return AppointmentHelper.appointmentsSem3;
                    case 3: switch (Specialization) {
                            case 0: return AppointmentHelper.appointmentsSem4MoCo;
                            case 1: return AppointmentHelper.appointmentsSem4EmSy;
                            case 2: return AppointmentHelper.appointmentsSem4CySe;
                            default: return null;
                        }
                    case 5:
                        switch (Specialization)
                        {
                            case 0: return AppointmentHelper.appointmentsSem6MoCo;
                            case 1: return AppointmentHelper.appointmentsSem6EmSy;
                            case 2: return AppointmentHelper.appointmentsSem6CySe;
                            default: return null;
                        }
                    case 6:
                        switch (Specialization)
                        {
                            case 0: return AppointmentHelper.appointmentsSem7MoCo;
                            case 1: return AppointmentHelper.appointmentsSem7EmSy;
                            case 2: return AppointmentHelper.appointmentsSem7CySe;
                            default: return null;
                        }
                    default: return null;
                }
            }
        }        

    }
}
