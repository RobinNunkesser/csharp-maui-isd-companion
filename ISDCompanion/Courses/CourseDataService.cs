﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Java.Lang;
using Java.Security;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using static Android.Provider.Settings;
using Calendar = Plugin.Calendars.Abstractions.Calendar;

namespace StudyCompanion
{
    public static class CourseDataService
    {
        private static readonly string shortFormat = "dd.MM.yyyy";
        public static readonly string longFormat = "dd.MM.yyyy HH:mm";

        public static DateTime SemesterEnd { get; } = DateTime.ParseExact("25.06.2023", shortFormat, CultureInfo.InvariantCulture);

        public static List<DateTime> Holidays { get; } = new List<DateTime>
        {
            /*DateTime.ParseExact("03.10.2022",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("01.11.2022",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("24.12.2022",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("25.12.2022",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("26.12.2022",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("27.12.2022",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("28.12.2022",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("29.12.2022",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("30.12.2022",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("31.12.2022",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("01.01.2023",shortFormat, CultureInfo.InvariantCulture),*/
            DateTime.ParseExact("07.04.2023",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("10.04.2023",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("11.04.2023",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("12.04.2023",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("13.04.2023",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("14.04.2023",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("01.05.2023",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("18.05.2023",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("29.05.2023",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("30.05.2023",shortFormat, CultureInfo.InvariantCulture),
            DateTime.ParseExact("08.06.2023",shortFormat, CultureInfo.InvariantCulture)
        };

        public static List<CourseViewModel> Courses { get; } = new List<CourseViewModel>
            {
            /*new CourseViewModel {
                Semester = 1,
                StartDate = "19.09.2022 08:00",
                Name = "Chemie VL",
                Lecturer = "Berndt",
                Room = "Hörsaal Stadtwerke Hamm",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "19.09.2022 10:00",
                Name = "Technisches Englisch I VL",
                Lecturer = "Strack",
                Room = "Hörsaal Stadtwerke Hamm",
                Length = 60,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "19.09.2022 12:00",
                Name = "Mathematik I VL",
                Lecturer = "Ponick",
                Room = "Hörsaal HAM 6",
                Length = 60,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "19.09.2022 13:00",
                Name = "Grundlagen der Programmierung VL",
                Lecturer = "Stuckenholz",
                Room = "Hörsaal HAM 6",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "20.09.2022 08:00",
                Name = "Technische Informatik I VL",
                Lecturer = "Krenz-Baath",
                Room = "Hörsaal WESTPRESS",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "20.09.2022 10:00",
                Name = "Mathematik I VL",
                Lecturer = "Ponick",
                Room = "Hörsaal WESTPRESS",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "20.09.2022 14:00",
                Name = "Technische Informatik I ÜB - Gruppe A",
                Lecturer = "Krenz-Baath",
                Room = "PC-Pool H4.2-E00-140, Labor H3.3-E01-220",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "21.09.2022 13:00",
                Name = "Technische Informatik I ÜB - Gruppe B",
                Lecturer = "Krenz-Baath",
                Room = "PC-Pool H4.2-E00-140, Labor H3.3-E01-220",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "22.09.2022 08:00",
                Name = "Personal Skills I VL/ÜB",
                Lecturer = "Grewe",
                Room = "Hörsaal HAM 6",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "22.09.2022 10:00",
                Name = "Mathematik I ÜB",
                Lecturer = "Ponick",
                Room = "Seminarraum H1.2-E01-010",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "22.09.2022 14:00",
                Name = "Technisches Englisch I ÜB",
                Lecturer = "Strack",
                Room = "Seminarraum H1.2-E01-010",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "22.09.2022 16:00",
                Name = "Physik ÜB - Gruppe A",
                Lecturer = "Kientopf",
                Room = "Seminarraum H7.2-E00-002",
                Length = 60,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "21.09.2022 11:00",
                Name = "Physik ÜB - Gruppe B",
                Lecturer = "Kientopf",
                Room = "Hörsaal HAM 6",
                Length = 60,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "23.09.2022 08:00",
                Name = "Physik VL",
                Lecturer = "Kientopf",
                Room = "Seminarraum H4.2-E00-110",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "23.09.2022 10:00",
                Name = "Biologie VL",
                Lecturer = "Tickenbrock",
                Room = "Seminarraum H4.2-E00-110",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "23.09.2022 14:00",
                Name = "Grundlagen der Programmierung ÜB - Gruppe A",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "21.09.2022 09:00",
                Name = "Grundlagen der Programmierung ÜB - Gruppe B",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "19.09.2022 08:00",
                Name = "Digitaltechnik I VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "19.09.2022 10:00",
                Name = "Embedded Systems I VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "19.09.2022 12:00",
                Name = "Technisches Englisch III VL",
                Lecturer = "Strack",
                Room = "Seminarraum H1.1-E01-150",
                Length = 60,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "19.09.2022 13:00",
                Name = "System Modellierung II VL",
                Lecturer = "Runovska",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "20.09.2022 08:00",
                Name = "Mathematik III VL",
                Lecturer = "Ponick",
                Room = "Hörsaal HAM 4",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "20.09.2022 10:00",
                Name = "Personal Skills III VL",
                Lecturer = "Zips",
                Room = "Hörsaal HAM 4",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "20.09.2022 14:00",
                Name = "Embedded Systems I ÜB",
                Lecturer = "Pelzl",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "20.09.2022 16:00",
                Name = "Digitaltechnik I ÜB",
                Lecturer = "Krenz-Baath",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "21.09.2022 10:30",
                Name = "Betriebssysteme VL",
                Lecturer = "Nunkesser",
                Room = "Hörsaal HAM 5",
                Length = 90,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "21.09.2022 13:00",
                Name = "Netzwerke VL",
                Lecturer = "Nunkesser",
                Room = "Hörsaal HAM 5",
                Length = 90,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "22.09.2022 10:00",
                Name = "Technisches Englisch III ÜB",
                Lecturer = "Strack",
                Room = "Seminarraum H7.2-E00-002",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "22.09.2022 12:00",
                Name = "Mathematik III ÜB",
                Lecturer = "Ponick",
                Room = "Seminarraum H7.2-E00-002",
                Length = 60,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "23.09.2022 09:00",
                Name = "Praktische Informatik VL",
                Lecturer = "Stuckenholz",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "23.09.2022 11:00",
                Name = "Praktische Informatik ÜB",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H3.3-E00-010",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "23.09.2022 13:00",
                Name = "Personal Skills III ÜB",
                Lecturer = "Zips",
                Room = "Seminarraum H1.1-E01-150",
                Length = 60,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "19.09.2022 08:30",
                Name = "Advanced App Development VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H4.3-E00-110",
                Length = 90,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "19.09.2022 10:00",
                Name = "Advanced App Development ÜB",
                Lecturer = "Nunkesser",
                Room = "Labor H3.3-E01-220",
                Length = 60,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "19.09.2022 11:00",
                Name = "System Verifikation und System Validierung ÜB",
                Lecturer = "Krenz-Baath",
                Room = "PC-Pool H3.3-E00-010",
                Length = 120,
                Biweekly = true
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "26.09.2022 11:00",
                Name = "Embedded Programming ÜB",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum H4.3- E00-100",
                Length = 120,
                Biweekly = true
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "19.09.2022 13:00",
                Name = "Safety und Security Projektkurs VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.1-E01-140",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "19.09.2022 17:00",
                Name = "Embedded Programming VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum H4.3-E00-110",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "20.09.2022 08:30",
                Name = "Cross-Platform Development VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H1.1-E01-170",
                Length = 90,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "20.09.2022 10:00",
                Name = "Cross-Platform Development ÜB",
                Lecturer = "Nunkesser",
                Room = "PC-Pool H4.2-E00-140",
                Length = 90,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "20.09.2022 12:00",
                Name = "System Verifikation und System Validierung VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "22.09.2022 08:30",
                Name = "IT-Consulting VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H4.2-E00-110",
                Length = 90,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "22.09.2022 10:00",
                Name = "Intelligent Systems in Theory and Practice VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H4.2-E00-110",
                Length = 90,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "22.09.2022 13:00",
                Name = "Web-Backends VL",
                Lecturer = "Stuckenholz",
                Room = "Seminarraum H1.1-E01-130",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "23.09.2022 09:00",
                Name = "Safety und Security Analysis VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.2-E01-010",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 7,
                StartDate = "23.09.2022 11:00",
                Name = "Safety und Security Analysis ÜB",
                Lecturer = "Pelzl",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = false
            }*/
            new CourseViewModel {
                Semester = 2,
                StartDate = "20.03.2023 08:00",
                Name = "Praktikum Elektrotechnik",
                Lecturer = "Glasmachers",
                Room = "Labor H3.3-E01-150",
                Length = 240,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "21.03.2023 08:00",
                Name = "Personal Skills II VL",
                Lecturer = "Grewe",
                Room = "Seminarraum H1.1-E01-170",
                Length = 120,
                Biweekly = true
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "28.03.2023 08:00",
                Name = "Personal Skills II ÜB",
                Lecturer = "Grewe",
                Room = "Seminarraum H1.1-E01-170",
                Length = 120,
                Biweekly = true
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "21.03.2023 10:00",
                Name = "Mathematik II VL",
                Lecturer = "Ponick",
                Room = "Hörsaal HAM 7",
                Length = 180,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "21.03.2023 14:00",
                Name = "Technisches Englisch II VL+ÜB",
                Lecturer = "Strack",
                Room = "Seminarraum H4.3-E00-100",
                Length = 180,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "22.03.2023 09:00",
                Name = "Objektorientierte Programmierung VL",
                Lecturer = "Stuckenholz",
                Room = "Hörsaal HAM 4",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "22.03.2023 11:00",
                Name = "Objektorientierte Programmierung ÜB",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "22.03.2023 14:00",
                Name = "System Modellierung I VL",
                Lecturer = "Runovska",
                Room = "Hörsaal HAM 7",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "23.03.2023 08:00",
                Name = "Elektrotechnik VL",
                Lecturer = "Glasmachers",
                Room = "Hörsaal HAM 3",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "23.03.2023 10:00",
                Name = "Elektrotechnik ÜB",
                Lecturer = "Glasmachers",
                Room = "Hörsaal HAM 3",
                Length = 60,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "23.03.2023 12:00",
                Name = "System Modellierung I ÜB",
                Lecturer = "Runovska",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "23.03.2023 14:00",
                Name = "Mathematik II ÜB",
                Lecturer = "Ponick",
                Room = "Hörsaal WESTPRESS",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "24.03.2023 09:00",
                Name = "Algorithmen und Datenstrukturen VL",
                Lecturer = "Stuckenholz",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 2,
                StartDate = "24.03.2023 11:00",
                Name = "Algorithmen und Datenstrukturen ÜB",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "20.03.2023 11:00",
                Name = "Software Design VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H4.3-E00-110",
                Length = 240,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "20.03.2023 13:00",
                Name = "Computer Security VL",
                Lecturer = "Pelzl",
                Room = "Hörsaal HAM 6",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "20.03.2023 13:00",
                Name = "Computer Security VL",
                Lecturer = "Pelzl",
                Room = "Hörsaal HAM 6",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "20.03.2023 17:00",
                Name = "Computer Security ÜB",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "21.03.2023 08:00",
                Name = "Personal Skills IV VL",
                Lecturer = "Zips",
                Room = "Hörsaal Stadtwerke Hamm",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "21.03.2023 08:00",
                Name = "Personal Skills IV VL",
                Lecturer = "Zips",
                Room = "Hörsaal Stadtwerke Hamm",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "21.03.2023 10:00",
                Name = "Datenbanken VL",
                Lecturer = "Grewe",
                Room = "Seminarraum H7.2-E00-002",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "21.03.2023 12:00",
                Name = "Datenbanken ÜB",
                Lecturer = "Grewe",
                Room = "PC-Pool H4.2-E00-140",
                Length = 60,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "21.03.2023 14:00",
                Name = "SSP I: Digitaltechnik II VL",
                Lecturer = "Krenz-Baath",
                Room = "Hörsaal HAM 3",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "22.03.2023 09:00",
                Name = "SSP I: Embedded Systems II VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H7.2-E00-001",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "22.03.2023 12:00",
                Name = "SSP I: Embedded Systems II ÜB",
                Lecturer = "Pelzl",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = true
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "29.03.2023 12:00",
                Name = "SSP I: Digitaltechnik II ÜB",
                Lecturer = "Krenz-Baath",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = true
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "22.03.2023 12:00",
                Name = "SSP I: App Frontends VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H1.1-E01-130",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "22.03.2023 14:00",
                Name = "Corporate Management VL",
                Lecturer = "Thorn",
                Room = "Hörsaal HAM 6",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "24.03.2023 09:00",
                Name = "Software Design ÜB",
                Lecturer = "Nunkesser",
                Room = "Labor H3.3-E01-160",
                Length = 240,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "24.03.2023 13:00",
                Name = "SSP I: App Frontends ÜB",
                Lecturer = "Nunkesser",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 4,
                StartDate = "24.03.2023 16:00",
                Name = "SSP I: Web Frontends VL",
                Lecturer = "Stuckenholz",
                Room = "Seminarraum H4.2-E00-110",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 6,
                StartDate = "20.03.2023 09:00",
                Name = "SSP II: Mobile Security VL",
                Lecturer = "Pelzl",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 6,
                StartDate = "20.03.2023 12:00",
                Name = "Personal Skills V VL",
                Lecturer = "Zips",
                Room = "Seminarraum H1.1-E01-160",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 6,
                StartDate = "20.03.2023 14:00",
                Name = "Personal Skills V ÜB",
                Lecturer = "Zips",
                Room = "Seminarraum H1.1-E01-160",
                Length = 60,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 6,
                StartDate = "21.03.2023 10:00",
                Name = "SSP II: Parallel Computing VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 6,
                StartDate = "21.03.2023 12:00",
                Name = "SSP II: iOS Development VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H1.1-E01-140",
                Length = 90,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 6,
                StartDate = "21.03.2023 13:30",
                Name = "SSP II: iOS Development ÜB",
                Lecturer = "Nunkesser",
                Room = "Labor H3.3-E01-220",
                Length = 90,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 6,
                StartDate = "21.03.2023 14:00",
                Name = "SSP II: Embedded Security VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H4.3-E00-110",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 6,
                StartDate = "22.03.2023 11:00",
                Name = "Entrepreneurial Finance VL",
                Lecturer = "Thorn",
                Room = "Hörsaal HAM 4",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 6,
                StartDate = "22.03.2023 14:00",
                Name = "Künstliche Intelligenz VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 6,
                StartDate = "22.03.2023 16:00",
                Name = "Künstliche Intelligenz PR",
                Lecturer = "Krenz-Baath",
                Room = "PC-Pool H4.2-E00-130",
                Length = 120,
                Biweekly = true
            },



            };

        public static List<CourseViewModel> GetSearchResults(string queryString)
        {

            var normalizedQuery = queryString?.ToLower() ?? "";
            if (string.IsNullOrEmpty(normalizedQuery)) return Courses;
            bool success = int.TryParse(normalizedQuery, out int semester);
            if (success)
            {
                return Courses.Where(item => item.Semester == semester).ToList();
            }
            return Courses.Where(
                item => item.Name.ToLower().Contains(normalizedQuery) ||
                item.Lecturer.ToLower().Contains(normalizedQuery)).ToList();
        }

        public static List<SectionViewModel<CourseViewModel>> GetGroupedCourses(List<CourseViewModel> courses)
        {
            List<SectionViewModel<CourseViewModel>> groups = new();
            foreach (var group in courses.GroupBy(c => c.Semester))
            {
                var section = new SectionViewModel<CourseViewModel>()
                {
                    LongName = $"Semester {group.Key}"
                };
                foreach (var item in group)
                {
                    section.Add(item);
                }
                groups.Add(section);
            }
            return groups;
        }

        internal static async void AddCourseToCalendar(CourseViewModel courseViewModel, Calendar selectedCalendar)
        {
            var startDate = DateTime.ParseExact(courseViewModel.StartDate, longFormat, CultureInfo.InvariantCulture);
            var endDate = startDate.AddMinutes(courseViewModel.Length);

            while (startDate < SemesterEnd)
            {
                var isHoliday = false;
                foreach (var holiday in Holidays)
                {
                    if (startDate.Date == holiday.Date)
                    {
                        isHoliday = true;
                        break;
                    }
                }

                if (!isHoliday) await CrossCalendars.Current.AddOrUpdateEventAsync(selectedCalendar, new CalendarEvent
                {
                    Name = courseViewModel.Name,
                    Start = startDate,
                    End = endDate,
                    Location = courseViewModel.Room
                });

                startDate = startDate.AddDays(courseViewModel.Biweekly ? 14 : 7);
                endDate = endDate.AddDays(courseViewModel.Biweekly ? 14 : 7);
            }

        }
    }
}

