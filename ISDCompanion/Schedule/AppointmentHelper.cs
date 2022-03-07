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
            // 2
            appointmentsSem2.AddRange(CreateRecurrentAppointment("21.03.2022 08:00", 180, "Mathematik II VL", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("21.03.2022 11:00", 120, "System Modellierung I VL", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("21.03.2022 15:00", 240, "Elektrotechnik PR", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("29.03.2022 08:00", 120, "Personal Skills II VL", biweekly: true, occurrences: 7));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("22.03.2022 08:00", 120, "Personal Skills II ÜB", biweekly: true, occurrences: 7));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("22.03.2022 12:00", 120, "Mathematik II ÜB", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("22.03.2022 14:00", 60, "Elektrotechnik ÜB", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("23.03.2022 09:00", 120, "Objektorientierte Programmierung VL", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("23.03.2022 11:00", 60, "Objektorientierte Programmierung ÜB", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("23.03.2022 14:00", 120, "Elektrotechnik VL", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("24.03.2022 10:00", 120, "System Modellierung I ÜB", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("24.03.2022 13:00", 60, "Technisches Englisch II VL", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("24.03.2022 14:00", 120, "Technisches Englisch II ÜB", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("25.03.2022 09:00", 120, "Algorihmen und Datenstrukturen VL", occurrences: 14));
            appointmentsSem2.AddRange(CreateRecurrentAppointment("25.03.2022 11:00", 120, "Algorihmen und Datenstrukturen ÜB", occurrences: 14));

            // 4
            var dbue = CreateRecurrentAppointment("21.03.2022 08:00", 60, "Datenbanken ÜB", occurrences: 14);
            appointmentsSem4CySe.AddRange(dbue);
            appointmentsSem4EmSy.AddRange(dbue);
            appointmentsSem4MoCo.AddRange(dbue);
            var sdue = CreateRecurrentAppointment("21.03.2022 09:00", 240, "Software Design ÜB", occurrences: 14);
            appointmentsSem4CySe.AddRange(sdue);
            appointmentsSem4EmSy.AddRange(sdue);
            appointmentsSem4MoCo.AddRange(sdue);
            var csvl = CreateRecurrentAppointment("21.03.2022 15:00", 120, "Computer Security VL", occurrences: 14);
            appointmentsSem4CySe.AddRange(csvl);
            appointmentsSem4EmSy.AddRange(csvl);
            appointmentsSem4MoCo.AddRange(csvl);
            var csue = CreateRecurrentAppointment("21.03.2022 17:00", 120, "Computer Security ÜB", occurrences: 14);
            appointmentsSem4CySe.AddRange(csue);
            appointmentsSem4EmSy.AddRange(csue);
            appointmentsSem4MoCo.AddRange(csue);
            var dbvl = CreateRecurrentAppointment("22.03.2022 10:00", 120, "Datenbanken VL", occurrences: 14);
            appointmentsSem4CySe.AddRange(dbvl);
            appointmentsSem4EmSy.AddRange(dbvl);
            appointmentsSem4MoCo.AddRange(dbvl);
            var ps = CreateRecurrentAppointment("22.03.2022 14:00", 120, "Personal Skills VL/ÜB", occurrences: 14);
            appointmentsSem4CySe.AddRange(ps);
            appointmentsSem4EmSy.AddRange(ps);
            appointmentsSem4MoCo.AddRange(ps);
            var cm = CreateRecurrentAppointment("22.03.2022 16:00", 120, "Corporate Management VL", occurrences: 14);
            appointmentsSem4CySe.AddRange(cm);
            appointmentsSem4EmSy.AddRange(cm);
            appointmentsSem4MoCo.AddRange(cm);
            var dtvl = CreateRecurrentAppointment("23.03.2022 08:00", 120, "Digitaltechnik II VL", occurrences: 14);
            appointmentsSem4CySe.AddRange(dtvl);
            appointmentsSem4EmSy.AddRange(dtvl);
            var esvl = CreateRecurrentAppointment("23.03.2022 10:00", 120, "Embedded Systems II VL", occurrences: 14);
            appointmentsSem4CySe.AddRange(esvl);
            appointmentsSem4EmSy.AddRange(esvl);
            var esue = CreateRecurrentAppointment("23.03.2022 13:00", 120, "Embedded Systems II ÜB", biweekly: true, occurrences: 7);
            appointmentsSem4CySe.AddRange(esue);
            appointmentsSem4EmSy.AddRange(esue);
            var dtue = CreateRecurrentAppointment("30.03.2022 13:00", 120, "Digitaltechnik II ÜB", biweekly: true, occurrences: 7);
            appointmentsSem4CySe.AddRange(dtue);
            appointmentsSem4EmSy.AddRange(dtue);
            var sdvl = CreateRecurrentAppointment("24.03.2022 13:00", 240, "Software Design VL", occurrences: 14);
            appointmentsSem4CySe.AddRange(sdvl);
            appointmentsSem4EmSy.AddRange(sdvl);
            appointmentsSem4MoCo.AddRange(sdvl);
            appointmentsSem4MoCo.AddRange(CreateRecurrentAppointment("23.03.2022 13:00", 120, "Android Development VL", occurrences: 14));
            appointmentsSem4MoCo.AddRange(CreateRecurrentAppointment("23.03.2022 15:00", 120, "Android Development ÜB", occurrences: 14));
            appointmentsSem4MoCo.AddRange(CreateRecurrentAppointment("25.03.2022 13:00", 120, "Web Frontends VL", occurrences: 14));

            // 6
            var ef = CreateRecurrentAppointment("21.03.2022 15:00", 120, "Entrepreneurial Finance VL", occurrences: 14);
            appointmentsSem6CySe.AddRange(ef);
            appointmentsSem6EmSy.AddRange(ef);
            appointmentsSem6MoCo.AddRange(ef);
            appointmentsSem6EmSy.AddRange(CreateRecurrentAppointment("22.03.2022 13:00", 120, "Parallel Computing VL", occurrences: 14));
            var kig1 = CreateRecurrentAppointment("29.03.2022 15:00", 120, "Künstliche Intelligenz PR - Gr. II", biweekly: true, occurrences: 7);
            appointmentsSem6CySe.AddRange(kig1);
            appointmentsSem6EmSy.AddRange(kig1);
            appointmentsSem6MoCo.AddRange(kig1);
            var kig2 = CreateRecurrentAppointment("22.03.2022 15:00", 120, "Künstliche Intelligenz PR - Gr. II", biweekly: true, occurrences: 7);
            appointmentsSem6CySe.AddRange(kig2);
            appointmentsSem6EmSy.AddRange(kig2);
            appointmentsSem6MoCo.AddRange(kig2);
            var kivl = CreateRecurrentAppointment("23.03.2022 11:00", 120, "Künstliche Intelligenz VL", occurrences: 14);
            appointmentsSem6CySe.AddRange(kivl);
            appointmentsSem6EmSy.AddRange(kivl);
            appointmentsSem6MoCo.AddRange(kivl);
            var ps5vl = CreateRecurrentAppointment("23.03.2022 14:00", 120, "Personal Skills V VL", occurrences: 14);
            appointmentsSem6CySe.AddRange(ps5vl);
            appointmentsSem6EmSy.AddRange(ps5vl);
            appointmentsSem6MoCo.AddRange(ps5vl);
            var ps5ue = CreateRecurrentAppointment("25.03.2022 11:00", 60, "Personal Skills V ÜB", occurrences: 14);
            appointmentsSem6CySe.AddRange(ps5ue);
            appointmentsSem6EmSy.AddRange(ps5ue);
            appointmentsSem6MoCo.AddRange(ps5ue);
            var esvl2 = CreateRecurrentAppointment("25.03.2022 12:00", 120, "Embedded Security VL", occurrences: 14);
            appointmentsSem6CySe.AddRange(esvl2);
            appointmentsSem6EmSy.AddRange(esvl2);
            var espr = CreateRecurrentAppointment("25.03.2022 14:00", 60, "Embedded Security PR", occurrences: 14);
            appointmentsSem6CySe.AddRange(espr);
            appointmentsSem6EmSy.AddRange(espr);
            appointmentsSem6MoCo.AddRange(CreateRecurrentAppointment("22.03.2022 09:00", 120, "iOS Development VL", occurrences: 14));
            appointmentsSem6MoCo.AddRange(CreateRecurrentAppointment("22.03.2022 11:00", 120, "iOS Development ÜB", occurrences: 14));
            var msvl = CreateRecurrentAppointment("25.03.2022 08:00", 120, "Mobile Security VL", occurrences: 14);
            appointmentsSem6CySe.AddRange(msvl);
            appointmentsSem6MoCo.AddRange(msvl);
            var msue = CreateRecurrentAppointment("25.03.2022 10:00", 60, "Mobile Security PR", occurrences: 14);
            appointmentsSem6CySe.AddRange(msue);
            appointmentsSem6MoCo.AddRange(msue);

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
