using System;
using System.Collections.Generic;
using System.Globalization;
using DevExpress.XamarinForms.Scheduler;

namespace ISDCompanion
{    
    public static class AppointmentHelper
    {
        private static readonly string format = "dd.MM.yyyy HH:mm";

        public static List<AppointmentItem> appointmentsSem1 = new List<AppointmentItem>();
        public static List<AppointmentItem> appointmentsSem2 = new List<AppointmentItem>();
        public static List<AppointmentItem> appointmentsSem3 = new List<AppointmentItem>();
        public static List<AppointmentItem> appointmentsSem4MoCo = new List<AppointmentItem>();
        public static List<AppointmentItem> appointmentsSem4EmSy = new List<AppointmentItem>();
        public static List<AppointmentItem> appointmentsSem4CySe = new List<AppointmentItem>();
        public static List<AppointmentItem> appointmentsSem6MoCo = new List<AppointmentItem>();
        public static List<AppointmentItem> appointmentsSem6EmSy = new List<AppointmentItem>();
        public static List<AppointmentItem> appointmentsSem6CySe = new List<AppointmentItem>();
        public static List<AppointmentItem> appointmentsSem7MoCo = new List<AppointmentItem>();
        public static List<AppointmentItem> appointmentsSem7EmSy = new List<AppointmentItem>();
        public static List<AppointmentItem> appointmentsSem7CySe = new List<AppointmentItem>();

        static AppointmentHelper()
        {

        }

        private static List<AppointmentItem> CreateRecurrentAppointment(string dateString,int lengthInMinutes, string subject, string location = "", bool allDay = false, int occurrences = 1 )
        {
            var appointments = new List<AppointmentItem>();
            var startDate = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            var endDate = startDate;
            endDate.AddMinutes(lengthInMinutes);
            for (int i = 0; i < occurrences; i++)
            {
                var appointment = new AppointmentItem()
                {
                    AllDay = allDay,
                    Start = startDate,
                    End = endDate,
                    Subject = subject,
                    Location = location
                };
                appointments.Add(appointment);
                startDate.AddDays(7);
                endDate.AddDays(7);
            }
            return appointments;
        }

        private static List<AppointmentItem> Appointments0 = new List<AppointmentItem>() {
                new AppointmentItem()
        {
            Id = 0,
                    AllDay = false,
                    Start = DayInWeek(DayOfWeek.Monday).AddHours(8),
                    End = DayInWeek(DayOfWeek.Monday).AddHours(10),
                    Subject = "Montag 1",
                    LabelId = 1,
                    Location = "Raum 3"
                },
                new AppointmentItem()
        {
            Id = 1,
                    AllDay = false,
                    Start = DayInWeek(DayOfWeek.Wednesday).AddHours(10),
                    End = DayInWeek(DayOfWeek.Wednesday).AddHours(12),
                    Subject = "Mittwcoh 1",
                    LabelId = 1,
                    Location = "Raum 3"
                } };

        private static List<AppointmentItem> Appointments1 = new List<AppointmentItem>() {
                new AppointmentItem()
        {
            Id = 0,
                    AllDay = false,
                    Start = DayInWeek(DayOfWeek.Monday).AddHours(8),
                    End = DayInWeek(DayOfWeek.Monday).AddHours(10),
                    Subject = "Montag 2",
                    LabelId = 1,
                    Location = "Raum 3"
                },
                new AppointmentItem()
        {
            Id = 1,
                    AllDay = false,
                    Start = DayInWeek(DayOfWeek.Wednesday).AddHours(10),
                    End = DayInWeek(DayOfWeek.Wednesday).AddHours(12),
                    Subject = "Mittwcoh 2",
                    LabelId = 1,
                    Location = "Raum 3"
                } };

        private static DateTime DayInWeek(DayOfWeek dayOfWeek)
        {
            return DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)dayOfWeek);
        }
    }
}
