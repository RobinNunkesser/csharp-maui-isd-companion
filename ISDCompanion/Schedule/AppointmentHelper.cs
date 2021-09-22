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
            // 1
            appointmentsSem1.AddRange(CreateRecurrentAppointment("27.09.2021 09:00", 120, "Physik VL", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("27.09.2021 11:00", 120, "Biologie VL", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("27.09.2021 15:00", 120, "Chemie VL", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("28.09.2021 08:00", 120, "Personal Skills I VL/ÜB", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("28.09.2021 10:00", 60, "Technisches Englisch I VL", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("28.09.2021 11:00", 60, "Physik ÜB", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("28.09.2021 13:00", 120, "Technische Informatik I ÜB", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("29.09.2021 09:00", 120, "Grundlagen der Programmierung VL", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("29.09.2021 14:00", 120, "Mathematik I VL", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("29.09.2021 16:00", 120, "Technische Informatik I VL", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("30.09.2021 09:00", 60, "Mathematik I", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("30.09.2021 11:00", 120, "Grundlagen der Programmierung ÜB", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("30.09.2021 13:00", 120, "Technisches Englisch I ÜB", occurrences: 19));
            appointmentsSem1.AddRange(CreateRecurrentAppointment("30.09.2021 15:00", 120, "Mathematik I ÜB", occurrences: 19));

            // 3
            appointmentsSem3.AddRange(CreateRecurrentAppointment("20.09.2021 09:00", 120, "Praktische Informatik VL", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("20.09.2021 11:00", 120, "Embedded Systems I VL", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("20.09.2021 15:00", 60, "Technisches Englisch III VL", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("20.09.2021 16:00", 120, "Technisches Englisch III ÜB", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("21.09.2021 08:00", 120, "Mathematik III VL", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("21.09.2021 10:00", 60, "Mathematik III ÜB", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("21.09.2021 12:00", 120, "Embedded Systems I ÜB", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("21.09.2021 16:00", 120, "System Modellierung II VL", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("22.09.2021 08:00", 120, "Netzwerke VL", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("22.09.2021 10:00", 120, "Betriebssysteme VL", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("22.09.2021 14:00", 120, "Praktische Informatik ÜB", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("23.09.2021 13:00", 120, "Digitaltechnik I VL", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("23.09.2021 15:00", 120, "Digitaltechnik I ÜB", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("24.09.2021 11:00", 120, "Personal Skills III VL", occurrences: 20));
            appointmentsSem3.AddRange(CreateRecurrentAppointment("24.09.2021 13:00", 60, "Personal Skills III ÜB", occurrences: 20));
            // 7
            var svsv = CreateRecurrentAppointment("21.09.2021 08:00", 120, "System Verifikation und System Validierung VL", occurrences: 20);            
            appointmentsSem7CySe.AddRange(svsv);
            appointmentsSem7EmSy.AddRange(svsv);
            appointmentsSem7MoCo.AddRange(svsv);
            var istp = CreateRecurrentAppointment("21.09.2021 10:00", 120, "WF: Intelligent Systems in Theory and Practice VL", occurrences: 20);
            appointmentsSem7CySe.AddRange(istp);
            appointmentsSem7EmSy.AddRange(istp);
            appointmentsSem7MoCo.AddRange(istp);
            var itc = CreateRecurrentAppointment("21.09.2021 13:00", 120, "WF: IT-Consulting VL", occurrences: 20);
            appointmentsSem7CySe.AddRange(itc);
            appointmentsSem7EmSy.AddRange(itc);
            appointmentsSem7MoCo.AddRange(itc);
            var ssp = CreateRecurrentAppointment("21.09.2021 15:00", 120, "WF: Safety und Security Projektkurs VL", occurrences: 20);
            appointmentsSem7CySe.AddRange(ssp);
            appointmentsSem7EmSy.AddRange(ssp);
            appointmentsSem7MoCo.AddRange(ssp);
            var ssa = CreateRecurrentAppointment("22.09.2021 08:00", 120, "WF: Safety und Security Analysis VL", occurrences: 20);
            appointmentsSem7CySe.AddRange(ssa);
            appointmentsSem7EmSy.AddRange(ssa);
            appointmentsSem7MoCo.AddRange(ssa);
            var ssau = CreateRecurrentAppointment("22.09.2021 10:00", 60, "WF: Safety und Security Analysis ÜB", occurrences: 20);
            appointmentsSem7CySe.AddRange(ssau);
            appointmentsSem7EmSy.AddRange(ssau);
            appointmentsSem7MoCo.AddRange(ssau);
            var wb = CreateRecurrentAppointment("22.09.2021 11:00", 120, "WF: Web-Backends VL", occurrences: 20);
            appointmentsSem7CySe.AddRange(wb);
            appointmentsSem7EmSy.AddRange(wb);
            appointmentsSem7MoCo.AddRange(wb);
            appointmentsSem7MoCo.AddRange(CreateRecurrentAppointment("22.09.2021 14:00", 120, "SSP: MoCo III - Cross-Platform Development VL", occurrences: 20));
            appointmentsSem7MoCo.AddRange(CreateRecurrentAppointment("22.09.2021 16:00", 120, "SSP: MoCo III - Cross-Platform Development ÜB", occurrences: 20));
            appointmentsSem7EmSy.AddRange(CreateRecurrentAppointment("23.09.2021 08:00", 120, "SSP: EmSy III - Embedded Programming VL", occurrences: 20));
            var svsvu = CreateRecurrentAppointment("23.09.2021 10:00", 120, "WF: System Verifikation und System Validierung ÜB", biweekly: true, occurrences: 10);
            appointmentsSem7CySe.AddRange(svsvu);
            appointmentsSem7EmSy.AddRange(svsvu);
            appointmentsSem7MoCo.AddRange(svsvu);
            appointmentsSem7EmSy.AddRange(CreateRecurrentAppointment("30.09.2021 10:00", 120, "SSP: EmSy III - Embedded Programming ÜB", biweekly: true, occurrences: 10));
            appointmentsSem7MoCo.AddRange(CreateRecurrentAppointment("23.09.2021 13:00", 120, "SSP: MoCo III - Advanced App Development VL", occurrences: 20));
            appointmentsSem7MoCo.AddRange(CreateRecurrentAppointment("23.09.2021 15:00", 60, "SSP: MoCo III - Advanced App Development ÜB", occurrences: 20));

        }

        private static List<AppointmentItem> CreateRecurrentAppointment(string dateString,int lengthInMinutes, string subject, string location = "", bool allDay = false, int occurrences = 1, bool biweekly = false )
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
                startDate = startDate.AddDays(biweekly? 14 : 7);
                endDate = endDate.AddDays(biweekly? 14 : 7);
            }
            return appointments;
        }

        private static DateTime DayInWeek(DayOfWeek dayOfWeek)
        {
            return DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)dayOfWeek);
        }
    }
}
