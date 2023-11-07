using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Italbytz.Ports.Common;
using StudyCompanion.Ports;

namespace ISDCompanion.Core
{
    public class GetCoursesService : IGetCoursesService
    {
        private readonly List<ICourse> Courses = new List<ICourse>
            {
            new Course {
                Semester = 1,
                StartDate = "25.09.2023 09:00",
                Name = "Personal Skills I VL/ÜB",
                Lecturer = "Grewe",
                Room = "Seminarraum H4.3-E00-100",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "25.09.2023 11:00",
                Name = "Mathematik I VL",
                Lecturer = "Ponick",
                Room = "Seminarraum H1.1-E01-140",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "26.09.2023 09:00",
                Name = "Mathematik I VL",
                Lecturer = "Ponick",
                Room = "Seminarraum H1.1-E01-140",
                Length = 60,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "26.09.2023 10:00",
                Name = "Mathematik I ÜB",
                Lecturer = "Ponick",
                Room = "Seminarraum H1.1-E01-140",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "26.09.2023 13:30",
                Name = "Technische Informatik I VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum HS1-E02-010",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "27.09.2023 08:00",
                Name = "Physik VL",
                Lecturer = "Kientopf",
                Room = "Seminarraum H1.1-E01-140",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "27.09.2023 14:00",
                Name = "Technische Informatik I ÜB",
                Lecturer = "Krenz-Baath",
                Room = "PC-Pool H4.2-E00-130, Labor H3.3-E01-220",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "27.09.2023 12:00",
                Name = "Physik ÜB",
                Lecturer = "Kientopf",
                Room = "Seminarraum H1.1-E01-160",
                Length = 60,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "27.09.2023 14:00",
                Name = "Biologie VL",
                Lecturer = "Klein",
                Room = "Seminarraum H1.1-E01-130",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "28.09.2023 10:00",
                Name = "Technisches Englisch I VL",
                Lecturer = "Strack",
                Room = "Seminarraum H4.3-E00-110",
                Length = 60,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "28.09.2023 11:00",
                Name = "Technisches Englisch I ÜB",
                Lecturer = "Strack",
                Room = "Seminarraum H4.3-E00-110",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "28.09.2023 14:00",
                Name = "Chemie VL",
                Lecturer = "Kirner",
                Room = "Hörsaal HAM 5",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "29.09.2023 09:00",
                Name = "Grundlagen der Programmierung VL",
                Lecturer = "Stuckenholz",
                Room = "Hörsaal Stadtwerke Hamm",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 1,
                StartDate = "29.09.2023 11:00",
                Name = "Grundlagen der Programmierung ÜB",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H3.3-E00-010",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "25.09.2023 08:00",
                Name = "Embedded Systems I VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.2-E01-020",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "25.09.2023 10:00",
                Name = "Mathematik III ÜB",
                Lecturer = "Ponick",
                Room = "Seminarraum H1.2-E01-020",
                Length = 60,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "25.09.2023 11:00",
                Name = "Digitaltechnik I VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum H4.2-E00-110",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "25.09.2023 15:00",
                Name = "Digitaltechnik I ÜB",
                Lecturer = "Krenz-Baath",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "26.09.2023 08:00",
                Name = "System Modellierung II VL",
                Lecturer = "Runovska",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "26.09.2023 12:00",
                Name = "Technisches Englisch III VL+ÜB",
                Lecturer = "Strack",
                Room = "Seminarraum H4.3-E00-100",
                Length = 180,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "26.09.2023 14:00",
                Name = "Personal Skills III VL",
                Lecturer = "Zips",
                Room = "Seminarraum H1.1-E01-170",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "26.09.2023 16:00",
                Name = "Personal Skills III ÜB",
                Lecturer = "Zips",
                Room = "Seminarraum H1.1-E01-170",
                Length = 60,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "27.09.2023 10:00",
                Name = "Embedded Systems I ÜB",
                Lecturer = "Pelzl",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "28.09.2023 09:30",
                Name = "Betriebssysteme VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H4.2-E00-110",
                Length = 90,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "28.09.2023 11:15",
                Name = "Netzwerke VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H4.2-E00-110",
                Length = 90,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "29.09.2023 11:00",
                Name = "Mathematik III VL",
                Lecturer = "Ponick",
                Room = "Seminarraum H1.1-E01-130",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "29.09.2023 14:00",
                Name = "Praktische Informatik VL",
                Lecturer = "Stuckenholz",
                Room = "Seminarraum H4.1-E00-100",
                Length = 90,
                Biweekly = false
            },
            new Course {
                Semester = 3,
                StartDate = "29.09.2023 15:30",
                Name = "Praktische Informatik ÜB",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H3.3-E00-010",
                Length = 90,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "25.09.2023 08:00",
                Name = "Embedded Programming VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum HS1-E02-010",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "25.09.2023 10:00",
                Name = "Web-Backends VL",
                Lecturer = "Stuckenholz",
                Room = "Seminarraum HS1-E02-010",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "25.09.2023 12:15",
                Name = "Advanced App Development VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H4.1-E00-100",
                Length = 90,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "25.09.2023 14:00",
                Name = "Advanced App Development ÜB",
                Lecturer = "Nunkesser",
                Room = "Labor H3.3-E01-220",
                Length = 45,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "26.09.2023 08:00",
                Name = "System Verifikation und System Validierung VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum HS1-E02-010",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "26.09.2023 11:00",
                Name = "System Verifikation und System Validierung ÜB",
                Lecturer = "Krenz-Baath",
                Room = "PC-Pool H4.2-E00-130, Seminarraum HS1-E02-010",
                Length = 120,
                Biweekly = true
            },
            new Course {
                Semester = 7,
                StartDate = "17.10.2023 11:00",
                Name = "Embedded Programming ÜB",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum H4.3- E00-100",
                Length = 120,
                Biweekly = true
            },
            new Course {
                Semester = 7,
                StartDate = "27.09.2023 09:30",
                Name = "Intelligent Systems in Theory and Practice VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H4.3-E00-100",
                Length = 90,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "27.09.2023 12:00",
                Name = "IT-Consulting VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H1.1-E01-170",
                Length = 90,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "28.09.2023 08:00",
                Name = "Safety und Security Analysis VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "28.09.2023 10:00",
                Name = "Safety und Security Analysis ÜB",
                Lecturer = "Pelzl",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "28.09.2023 11:00",
                Name = "Safety und Security Projektkurs VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "29.09.2023 09:30",
                Name = "Interaktive Grafikanwendungen VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum HS1-E02-010",
                Length = 90,
                Biweekly = false
            },
            new Course {
                Semester = 7,
                StartDate = "29.09.2023 11:15",
                Name = "Interaktive Grafikanwendungen ÜB",
                Lecturer = "Nunkesser",
                Room = "PC-Pool H4.2-E00-130",
                Length = 90,
                Biweekly = false
            },
            /*new Course {
                Semester = 2,
                StartDate = "20.03.2024 08:00",
                Name = "Praktikum Elektrotechnik",
                Lecturer = "Glasmachers",
                Room = "Labor H3.3-E01-150",
                Length = 240,
                Biweekly = false
            },
            new Course {
                Semester = 2,
                StartDate = "21.03.2024 08:00",
                Name = "Personal Skills II VL",
                Lecturer = "Grewe",
                Room = "Seminarraum H1.1-E01-170",
                Length = 120,
                Biweekly = true
            },
            new Course {
                Semester = 2,
                StartDate = "28.03.2024 08:00",
                Name = "Personal Skills II ÜB",
                Lecturer = "Grewe",
                Room = "Seminarraum H1.1-E01-170",
                Length = 120,
                Biweekly = true
            },
            new Course {
                Semester = 2,
                StartDate = "21.03.2024 10:00",
                Name = "Mathematik II VL",
                Lecturer = "Ponick",
                Room = "Hörsaal HAM 7",
                Length = 180,
                Biweekly = false
            },
            new Course {
                Semester = 2,
                StartDate = "21.03.2024 14:00",
                Name = "Technisches Englisch II VL+ÜB",
                Lecturer = "Strack",
                Room = "Seminarraum H4.3-E00-100",
                Length = 180,
                Biweekly = false
            },
            new Course {
                Semester = 2,
                StartDate = "22.03.2024 09:00",
                Name = "Objektorientierte Programmierung VL",
                Lecturer = "Stuckenholz",
                Room = "Hörsaal HAM 4",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 2,
                StartDate = "22.03.2024 11:00",
                Name = "Objektorientierte Programmierung ÜB",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 2,
                StartDate = "22.03.2024 14:00",
                Name = "System Modellierung I VL",
                Lecturer = "Runovska",
                Room = "Hörsaal HAM 7",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 2,
                StartDate = "23.03.2024 08:00",
                Name = "Elektrotechnik VL",
                Lecturer = "Glasmachers",
                Room = "Hörsaal HAM 3",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 2,
                StartDate = "23.03.2024 10:00",
                Name = "Elektrotechnik ÜB",
                Lecturer = "Glasmachers",
                Room = "Hörsaal HAM 3",
                Length = 60,
                Biweekly = false
            },
            new Course {
                Semester = 2,
                StartDate = "23.03.2024 12:00",
                Name = "System Modellierung I ÜB",
                Lecturer = "Runovska",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 2,
                StartDate = "23.03.2024 14:00",
                Name = "Mathematik II ÜB",
                Lecturer = "Ponick",
                Room = "Hörsaal WESTPRESS",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 2,
                StartDate = "24.03.2024 09:00",
                Name = "Algorithmen und Datenstrukturen VL",
                Lecturer = "Stuckenholz",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 2,
                StartDate = "24.03.2024 11:00",
                Name = "Algorithmen und Datenstrukturen ÜB",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "20.03.2024 09:00",
                Name = "Software Design VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H4.3-E00-110",
                Length = 240,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "20.03.2024 13:00",
                Name = "Computer Security VL",
                Lecturer = "Pelzl",
                Room = "Hörsaal HAM 6",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "20.03.2024 13:00",
                Name = "Computer Security VL",
                Lecturer = "Pelzl",
                Room = "Hörsaal HAM 6",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "20.03.2024 17:00",
                Name = "Computer Security ÜB",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "21.03.2024 08:00",
                Name = "Personal Skills IV VL",
                Lecturer = "Zips",
                Room = "Hörsaal Stadtwerke Hamm",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "21.03.2024 08:00",
                Name = "Personal Skills IV VL",
                Lecturer = "Zips",
                Room = "Hörsaal Stadtwerke Hamm",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "21.03.2024 10:00",
                Name = "Datenbanken VL",
                Lecturer = "Grewe",
                Room = "Seminarraum H7.2-E00-002",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "21.03.2024 12:00",
                Name = "Datenbanken ÜB",
                Lecturer = "Grewe",
                Room = "PC-Pool H4.2-E00-140",
                Length = 60,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "21.03.2024 14:00",
                Name = "SSP I: Digitaltechnik II VL",
                Lecturer = "Krenz-Baath",
                Room = "Hörsaal HAM 3",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "22.03.2024 09:00",
                Name = "SSP I: Embedded Systems II VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H7.2-E00-001",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "22.03.2024 12:00",
                Name = "SSP I: Embedded Systems II ÜB",
                Lecturer = "Pelzl",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = true
            },
            new Course {
                Semester = 4,
                StartDate = "29.03.2024 12:00",
                Name = "SSP I: Digitaltechnik II ÜB",
                Lecturer = "Krenz-Baath",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = true
            },
            new Course {
                Semester = 4,
                StartDate = "22.03.2024 12:00",
                Name = "SSP I: App Frontends VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H1.1-E01-130",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "22.03.2024 14:00",
                Name = "Corporate Management VL",
                Lecturer = "Thorn",
                Room = "Hörsaal HAM 6",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "24.03.2024 09:00",
                Name = "Software Design ÜB",
                Lecturer = "Nunkesser",
                Room = "Labor H3.3-E01-160",
                Length = 240,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "24.03.2024 13:00",
                Name = "SSP I: App Frontends ÜB",
                Lecturer = "Nunkesser",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 4,
                StartDate = "24.03.2024 16:00",
                Name = "SSP I: Web Frontends VL",
                Lecturer = "Stuckenholz",
                Room = "Seminarraum H4.2-E00-110",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 6,
                StartDate = "20.03.2024 09:00",
                Name = "SSP II: Mobile Security VL",
                Lecturer = "Pelzl",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 6,
                StartDate = "20.03.2024 12:00",
                Name = "Personal Skills V VL",
                Lecturer = "Zips",
                Room = "Seminarraum H1.1-E01-160",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 6,
                StartDate = "20.03.2024 14:00",
                Name = "Personal Skills V ÜB",
                Lecturer = "Zips",
                Room = "Seminarraum H1.1-E01-160",
                Length = 60,
                Biweekly = false
            },
            new Course {
                Semester = 6,
                StartDate = "21.03.2024 10:00",
                Name = "SSP II: Parallel Computing VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 6,
                StartDate = "21.03.2024 12:00",
                Name = "SSP II: iOS Development VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H1.1-E01-140",
                Length = 90,
                Biweekly = false
            },
            new Course {
                Semester = 6,
                StartDate = "21.03.2024 13:30",
                Name = "SSP II: iOS Development ÜB",
                Lecturer = "Nunkesser",
                Room = "Labor H3.3-E01-220",
                Length = 90,
                Biweekly = false
            },
            new Course {
                Semester = 6,
                StartDate = "21.03.2024 14:00",
                Name = "SSP II: Embedded Security VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H4.3-E00-110",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 6,
                StartDate = "22.03.2024 11:00",
                Name = "Entrepreneurial Finance VL",
                Lecturer = "Thorn",
                Room = "Hörsaal HAM 4",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 6,
                StartDate = "22.03.2024 14:00",
                Name = "Künstliche Intelligenz VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new Course {
                Semester = 6,
                StartDate = "22.03.2024 16:00",
                Name = "Künstliche Intelligenz PR",
                Lecturer = "Krenz-Baath",
                Room = "PC-Pool H4.2-E00-130",
                Length = 120,
                Biweekly = true
            },*/
            };

        public List<ICourse> Execute() => Courses;
    }
}

