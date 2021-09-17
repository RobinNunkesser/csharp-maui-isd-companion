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
            var svsv = CreateRecurrentAppointment("21.09.2021 08:00", 120, "SSP CySe III - System Verifikation und System Validierung VL", occurrences: 20);            
            appointmentsSem7CySe.AddRange(svsv);
        }

        private static List<AppointmentItem> CreateRecurrentAppointment(string dateString,int lengthInMinutes, string subject, string location = "", bool allDay = false, int occurrences = 1 )
        {
            var appointments = new List<AppointmentItem>();
            var startDate = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
            var endDate = startDate.AddMinutes(lengthInMinutes);
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
                startDate = startDate.AddDays(7);
                endDate = endDate.AddDays(7);
            }
            return appointments;
        }

        private static DateTime DayInWeek(DayOfWeek dayOfWeek)
        {
            return DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)dayOfWeek);
        }
    }
}
