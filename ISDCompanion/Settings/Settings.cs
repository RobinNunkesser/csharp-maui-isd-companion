﻿using System;
using System.Collections.Generic;
using DevExpress.XamarinForms.Scheduler;
using ISDCompanion.Enums;
using Italbytz.Ports.Meal;
using Xamarin.Essentials;

namespace ISDCompanion
{
    public static class Settings
    {
        private static readonly int StatusDefault = 0;
        private static readonly int MainEmphasisDefault = 0;
        private static readonly int SemesterDefault = 0;
        private static readonly int SpecializationDefault = 0;
        private static readonly int WelcomeStatusDefault = 0;
        private static readonly int AllergensDefault = 0b11111111111111;
        private static readonly int AdditivesDefault = 0b111111111111111;


        public static int MainEmphasis
        {
            get => Preferences.Get(nameof(MainEmphasis), MainEmphasisDefault);
            set => Preferences.Set(nameof(MainEmphasis), value);
        }
        public static int WelcomeStatus
        {
            get => Preferences.Get(nameof(WelcomeStatus), WelcomeStatusDefault);
            set => Preferences.Set(nameof(WelcomeStatus), value);
        }
        public static int Status
        {
            get => Preferences.Get(nameof(Status), StatusDefault);
            set => Preferences.Set(nameof(Status), value);
        }
        public static int Semester
        {
            get => Preferences.Get(nameof(Semester), SemesterDefault);
            set => Preferences.Set(nameof(Semester), value);
        }
        public static int RealSemester
        {
            get => Semester + 1;
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
                    case 1: return AppointmentHelper.appointmentsSem1;
                    case 2: return AppointmentHelper.appointmentsSem2;
                    case 3: return AppointmentHelper.appointmentsSem3;
                    case 4:
                        switch (Specialization)
                        {
                            case 0: return AppointmentHelper.appointmentsSem4MoCo;
                            case 1: return AppointmentHelper.appointmentsSem4EmSy;
                            case 2: return AppointmentHelper.appointmentsSem4CySe;
                            default: return null;
                        }
                    case 6:
                        switch (Specialization)
                        {
                            case 0: return AppointmentHelper.appointmentsSem6MoCo;
                            case 1: return AppointmentHelper.appointmentsSem6EmSy;
                            case 2: return AppointmentHelper.appointmentsSem6CySe;
                            default: return null;
                        }
                    case 7:
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
