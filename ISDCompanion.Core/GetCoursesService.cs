using System.Collections.Generic;
using StudyCompanion.Ports;

namespace ISDCompanion.Core
{
    public class GetCoursesService : IGetCoursesService
    {
        private readonly List<ICourse> Courses = new()
        {
            
            // --------------- Semester 1 ---------------
        new Course {
            Semester = 1,
            StartDate = "22.09.2025 10:00",
            Name = "Mathematik I VL",
            Lecturer = "Ponick",
            Room = "Seminarraum H1.1-E01-130",
            Length = 60,
            Biweekly = false
        },
                new Course {
            Semester = 1,
            StartDate = "22.09.2025 11:00",
            Name = "Technisches Englisch I VL",
            Lecturer = "Birkhahn",
            Room = "Seminarraum H1.1-E01-140",
            Length = 60,
            Biweekly = false
        },
                new Course {
            Semester = 1,
            StartDate = "22.09.2025 12:00",
            Name = "Technisches Englisch I ÜB",
            Lecturer = "Birkhahn",
            Room = "Seminarraum H1.1-E01-140",
            Length = 120,
            Biweekly = false
        },
                        new Course {
            Semester = 1,
            StartDate = "22.09.2025 15:00",
            Name = "Angleichungskurs Mathematik",
            Lecturer = "Runovska",
            Room = "PC-Pool H4.2-E00-130",
            Length = 120,
            Biweekly = false
        },
                new Course {
            Semester = 1,
            StartDate = "23.09.2025 10:00",
            Name = "Grundlagen der Informatik VL",
            Lecturer = "Nunkesser",
            Room = "Hörsaal HAM 4",
            Length = 90,
            Biweekly = false
        },
        new Course {
            Semester = 1,
            StartDate = "23.09.2025 12:00",
            Name = "Grundlagen der Informatik ÜB",
            Lecturer = "Nunkesser",
            Room = "PC-Pool H4.1-E00-130",
            Length = 90,
            Biweekly = false
        },
        new Course {
            Semester = 1,
            StartDate = "24.09.2025 09:00",
            Name = "Grundlagen der Programmierung VL",
            Lecturer = "Stuckenholz",
            Room = "Hörsaal HAM 4",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 1,
            StartDate = "24.09.2025 11:00",
            Name = "Physik VL",
            Lecturer = "Kientopf",
            Room = "Hörsaal HAM 4",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 1,
            StartDate = "24.09.2025 13:00",
            Name = "Mathematik I ÜB",
            Lecturer = "Ponick",
            Room = "Seminarraum H1.2-E01-010",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 1,
            StartDate = "25.09.2025 08:00",
            Name = "Chemie VL",
            Lecturer = "Kirner",
            Room = "Seminarraum H1.2-E01-020",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 1,
            StartDate = "25.09.2025 10:00",
            Name = "Personal Skills I VL/ÜB",
            Lecturer = "Grewe",
            Room = "Seminarraum H1.1-E01-170",
            Length = 120,
            Biweekly = false
        },
                new Course {
            Semester = 1,
            StartDate = "25.09.2025 12:00",
            Name = "Grundlagen der Programmierung ÜB",
            Lecturer = "Stuckenholz",
            Room = "PC-Pool H4.2-E00-140",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 1,
            StartDate = "25.09.2025 14:00",
            Name = "Physik ÜB",
            Lecturer = "Kientopf",
            Room = "Seminarraum H4.2-E00-100",
            Length = 60,
            Biweekly = false
        },
        new Course {
            Semester = 1,
            StartDate = "26.09.2025 10:00",
            Name = "Biologie VL",
            Lecturer = "Tickenbrock",
            Room = "Seminarraum H1.1-E01-140",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 1,
            StartDate = "26.09.2025 12:00",
            Name = "Mathematik I VL",
            Lecturer = "Ponick",
            Room = "Hörsaal HAM 6",
            Length = 120,
            Biweekly = false
        },
            // --------------- Semester 3 ---------------
                new Course {
            Semester = 3,
            StartDate = "22.09.2025 11:00",
            Name = "Embedded Systems I VL",
            Lecturer = "Pelzl",
            Room = "Seminarraum H1.1-E01-160",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 3,
            StartDate = "22.09.2025 14:00",
            Name = "Technisches Englisch III VL",
            Lecturer = "Birkhahn",
            Room = "Seminarraum H1.1-E01-160",
            Length = 60,
            Biweekly = false
        },
        new Course {
            Semester = 3,
            StartDate = "22.09.2025 14:00",
            Name = "Technisches Englisch III ÜB",
            Lecturer = "Birkhahn",
            Room = "Seminarraum H1.1-E01-160",
            Length = 120,
            Biweekly = false
        },
                new Course {
            Semester = 3,
            StartDate = "24.09.2025 08:00",
            Name = "Embedded Systems I ÜB",
            Lecturer = "Pelzl",
            Room = "Labor H3.3-E01-160",
            Length = 120,
            Biweekly = false
        },
                new Course {
            Semester = 3,
            StartDate = "24.09.2025 10:00",
            Name = "System Modellierung II VL",
            Lecturer = "Runovska",
            Room = "PC-Pool H4.2-E00-130",
            Length = 120,
            Biweekly = false
        },
                new Course {
            Semester = 3,
            StartDate = "24.09.2025 12:00",
            Name = "Mathematik III ÜB",
            Lecturer = "Ponick",
            Room = "Seminarraum H1.2-E01-010",
            Length = 60,
            Biweekly = false
        },
        new Course {
            Semester = 3,
            StartDate = "24.09.2025 15:00",
            Name = "IT Sicherheit VL",
            Lecturer = "Pelzl",
            Room = "Hörsaal HAM 3",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 3,
            StartDate = "24.09.2025 17:00",
            Name = "IT Sicherheit ÜB",
            Lecturer = "Pelzl",
            Room = "Seminarraum H1.1-E01-150",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 3,
            StartDate = "25.09.2025 09:30",
            Name = "Betriebssysteme VL",
            Lecturer = "Nunkesser",
            Room = "Seminarraum H1.1-E01-150",
            Length = 90,
            Biweekly = false
        },
        new Course {
            Semester = 3,
            StartDate = "25.09.2025 11:00",
            Name = "Netzwerke VL",
            Lecturer = "Nunkesser",
            Room = "Seminarraum H1.1-E01-150",
            Length = 90,
            Biweekly = false
        },
        new Course {
            Semester = 3,
            StartDate = "25.09.2025 14:00",
            Name = "Praktische Informatik VL",
            Lecturer = "Stuckenholz",
            Room = "Seminarraum H4.3-E00-110",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 3,
            StartDate = "25.09.2025 16:00",
            Name = "Praktische Informatik ÜB",
            Lecturer = "Stuckenholz",
            Room = "PC-Pool H3.3-E00-010",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 3,
            StartDate = "26.09.2025 10:00",
            Name = "Mathematik III VL",
            Lecturer = "Ponick",
            Room = "Seminarraum H4.1-E00-100",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 3,
            StartDate = "26.09.2025 13:00",
            Name = "Personal Skills III VL",
            Lecturer = "Zips",
            Room = "Hörsaal HAM 4",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 3,
            StartDate = "26.09.2025 15:00",
            Name = "Personal Skills III ÜB",
            Lecturer = "Zips",
            Room = "Seminarraum H4.1-E00-100",
            Length = 60,
            Biweekly = false
        },
            // --------------- Semester 7 ---------------
                new Course {
            Semester = 7,
            StartDate = "22.09.2025 11:00",
            Name = "IT-Consulting VL",
            Lecturer = "Nunkesser",
            Room = "Seminarraum H4.2-E00-100",
            Length = 90,
            Biweekly = false
        },
                new Course {
            Semester = 7,
            StartDate = "24.09.2025 09:30",
            Name = "Interaktive Grafikanwendungen VL",
            Lecturer = "Nunkesser",
            Room = "Seminarraum  H7.2-E00-020",
            Length = 90,
            Biweekly = false
        },
        new Course {
            Semester = 7,
            StartDate = "24.09.2025 11:00",
            Name = "Angewandte Künstliche Intelligenz ÜB",
            Lecturer = "Nunkesser",
            Room = "Labor H3.3-E01-220",
            Length = 45,
            Biweekly = false
        },
                new Course {
            Semester = 7,
            StartDate = "24.09.2025 12:00",
            Name = "Safety und Security Projektkurs",
            Lecturer = "Pelzl",
            Room = "",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 7,
            StartDate = "24.09.2025 14:00",
            Name = "System Verifikation und System Validierung ÜB",
            Lecturer = "Beckmann, Günther",
            Room = "PC-Pool H3.3-E00-010",
            Length = 120,
            Biweekly = true
        },
        new Course {
            Semester = 7,
            StartDate = "25.09.2025 08:00",
            Name = "Safety und Security Analysis VL",
            Lecturer = "Pelzl",
            Room = "Seminarraum H1.2-E01-010",
            Length = 120,
            Biweekly = false
        },
                new Course {
            Semester = 7,
            StartDate = "25.09.2025 10:00",
            Name = "Safety und Security Hackathon",
            Lecturer = "Pelzl",
            Room = "",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 7,
            StartDate = "25.09.2025 12:00",
            Name = "Safety und Security Analysis ÜB",
            Lecturer = "Pelzl",
            Room = "Labor H3.3-E01-160",
            Length = 60,
            Biweekly = false
        },
new Course {
            Semester = 7,
            StartDate = "26.09.2025 09:30",
            Name = "Interaktive Grafikanwendungen ÜB",
            Lecturer = "Nunkesser",
            Room = "PC-Pool H4.2-E00-140",
            Length = 90,
            Biweekly = false
        },
                new Course {
            Semester = 7,
            StartDate = "26.09.2025 11:00",
            Name = "Angewandte Künstliche Intelligenz VL",
            Lecturer = "Nunkesser",
            Room = "Seminarraum H7.2-E00-020",
            Length = 90,
            Biweekly = false
        },
        new Course {
            Semester = 7,
            StartDate = "26.09.2025 12:00",
            Name = "Embedded Programming ÜB",
            Lecturer = "Beckmann, Günther",
            Room = "Seminarraum H1.1-E01-170",
            Length = 60,
            Biweekly = false
        },

        new Course {
            Semester = 7,
            StartDate = "26.09.2025 13:00",
            Name = "System Verifikation und System Validierung VL",
            Lecturer = "Hirsch",
            Room = "Seminarraum H1.1-E01-160",
            Length = 120,
            Biweekly = false
        },
        new Course {
            Semester = 7,
            StartDate = "26.09.2025 15:00",
            Name = "Web-Backends VL",
            Lecturer = "Stuckenholz",
            Room = "Seminarraum H1.1-E01-170",
            Length = 120,
            Biweekly = false
        },

        new Course {
            Semester = 7,
            StartDate = "25.09.2025 12:00",
            Name = "Embedded Programming VL",
            Lecturer = "Hayek",
            Room = "Seminarraum H4.2-E00-110",
            Length = 120,
            Biweekly = false
        },
        /*
            // --------------- Semester 2 ---------------
            new Course
            {
                Semester = 2,
                StartDate = "24.03.2025 08:00",
                Name = "Praktikum Elektrotechnik",
                Lecturer = "Glasmachers",
                Room = "Labor H3.3-E01-150",
                Length = 240,
                Biweekly = false
            },
            new Course
            {
                Semester = 2,
                StartDate = "25.03.2025 08:00",
                Name = "Mathematik II VL",
                Lecturer = "Ponik",
                Room = "Seminarraum H1.1-E01-170",
                Length = 180,
                Biweekly = false
            },
            new Course
            {
                Semester = 2,
                StartDate = "25.03.2025 11:00",
                Name = "System Modellierung I VL",
                Lecturer = "Runovska",
                Room = "Seminarraum H1.1-E01-170",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 2,
                StartDate = "25.03.2025 13:00",
                Name = "System Modellierung I ÜB",
                Lecturer = "Runovska",
                Room = "PC-Pool H4.2-E00-130",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 2,
                StartDate = "26.03.2025 09:00",
                Name = "Objektorientierte Programmierung VL",
                Lecturer = "Stuckenholz",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 2,
                StartDate = "26.03.2025 11:00",
                Name = "Objektorientierte Programmierung ÜB",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H4.1-E00-130",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 2,
                StartDate = "26.03.2025 13:00",
                Name = "Mathematik II ÜB",
                Lecturer = "Ponick",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 2,
                StartDate = "27.03.2025 08:00",
                Name = "Elektrotechnik VL",
                Lecturer = "Glasmachers",
                Room = "Hörsaal HAM 7",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 2,
                StartDate = "27.03.2025 10:00",
                Name = "Elektrotechnik ÜB",
                Lecturer = "Glasmachers",
                Room = "Hörsaal HAM 5",
                Length = 60,
                Biweekly = false
            },

            new Course
            {
                Semester = 2,
                StartDate = "27.03.2025 11:00",
                Name = "Technisches Englisch II VL+ÜB",
                Lecturer = "Strack",
                Room = "Seminarraum H4.3-E00-110",
                Length = 180,
                Biweekly = false
            },

            new Course
            {
                Semester = 2,
                StartDate = "28.03.2025 08:00",
                Name = "Personal Skills II VL",
                Lecturer = "Grewe",
                Room = "H1.1-E01-140",
                Length = 120,
                Biweekly = true
            },
            new Course
            {
                Semester = 2,
                StartDate = "04.04.2025 08:00",
                Name = "Personal Skills II ÜB",
                Lecturer = "Grewe",
                Room = "H1.1-E01-140",
                Length = 120,
                Biweekly = true
            },
            new Course
            {
                Semester = 2,
                StartDate = "28.03.2025 11:00",
                Name = "Algorithmen und Datenstrukturen VL",
                Lecturer = "Stuckenholz",
                Room = "Seminarraum H4.3-E00-110",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 2,
                StartDate = "28.03.2025 13:00",
                Name = "Algorithmen und Datenstrukturen ÜB",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H4.1-E00-140",
                Length = 120,
                Biweekly = false
            },
            // --------------- Semester 4 ---------------
            new Course
            {
                Semester = 4,
                StartDate = "24.03.2025 08:00",
                Name = "Datenbanken ÜB",
                Lecturer = "Grewe",
                Room = "PC-Pool H4.2-E00-140",
                Length = 60,
                Biweekly = false
            },
            new Course
            {
                Semester = 4,
                StartDate = "24.03.2025 09:00",
                Name = "Computer Security VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H4.1-E00-100",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 4,
                StartDate = "24.03.2025 11:00",
                Name = "Datenbanken VL",
                Lecturer = "Grewe",
                Room = "Seminarraum H4.1-E00-110",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 4,
                StartDate = "24.03.2025 15:00",
                Name = "Computer Security ÜB",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.1-E01-140",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 4,
                StartDate = "25.03.2025 10:00",
                Name = "Software Design VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H1.1-E01-160",
                Length = 180,
                Biweekly = false
            },
            new Course
            {
                Semester = 4,
                StartDate = "25.03.2025 14:00",
                Name = "SSP I: Embedded Systems II VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.1-E01-130",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 4,
                StartDate = "26.03.2025 12:00",
                Name = "Corporate Management VL",
                Lecturer = "Thorn",
                Room = "Hörsaal HAM 5",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 4,
                StartDate = "26.03.2025 14:00",
                Name = "SSP I: Embedded Systems II ÜB",
                Lecturer = "Pelzl",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = true
            },
            new Course
            {
                Semester = 4,
                StartDate = "28.03.2025 10:00",
                Name = "Software Design ÜB",
                Lecturer = "Nunkesser",
                Room = "Labor H3.3-E01-160",
                Length = 180,
                Biweekly = false
            },
            new Course
            {
                Semester = 4,
                StartDate = "28.03.2025 13:00",
                Name = "Personal Skills IV VL",
                Lecturer = "Zips",
                Room = "Hörsaal HAM 3",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 4,
                StartDate = "25.03.2025 13:00",
                Name = "SSP I: App Frontends VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H7.2-E00-020",
                Length = 90,
                Biweekly = false
            },
            new Course
            {
                Semester = 4,
                StartDate = "26.03.2025 10:00",
                Name = "SSP I: App Frontends ÜB",
                Lecturer = "Nunkesser",
                Room = "Labor H3.3-E01-160",
                Length = 90,
                Biweekly = false
            },
            new Course
            {
                Semester = 4,
                StartDate = "26.03.2025 14:00",
                Name = "SSP I: Web Frontends ÜB",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H4.1-E00-140",
                Length = 120,
                Biweekly = false
            },


            new Course
            {
                Semester = 4,
                StartDate = "28.03.2025 15:00",
                Name = "SSP I: Web Frontends VL",
                Lecturer = "Stuckenholz",
                Room = "Seminarraum H4.1-E00-100",
                Length = 120,
                Biweekly = false
            },
            // --------------- Semester 6 ---------------
            new Course
            {
                Semester = 6,
                StartDate = "24.03.2025 08:00",
                Name = "Personal Skills V ÜB",
                Lecturer = "Zips",
                Room = "Seminarraum H1.2-E01-010",
                Length = 60,
                Biweekly = false
            },
            new Course
            {
                Semester = 6,
                StartDate = "24.03.2025 09:30",
                Name = "Künstliche Intelligenz VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H1.1-E01-140",
                Length = 90,
                Biweekly = false
            },
            new Course
            {
                Semester = 6,
                StartDate = "24.03.2025 11:00",
                Name = "Personal Skills V VL",
                Lecturer = "Zips",
                Room = "Seminarraum H1.2-E01-020",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 6,
                StartDate = "25.03.2025 10:00",
                Name = "SSP II: Embedded Security Projektkurs VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.1-E01-140",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 6,
                StartDate = "25.03.2025 12:00",
                Name = "SSP II: Embedded Security Projektkurs ÜB",
                Lecturer = "Pelzl",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = true
            },
            new Course
            {
                Semester = 6,
                StartDate = "26.03.2025 10:00",
                Name = "Entrepreneurial Finance VL",
                Lecturer = "Thorn",
                Room = "Seminarraum H4.3-E00-100",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 6,
                StartDate = "02.04.2025 12:00",
                Name = "Künstliche Intelligenz PR",
                Lecturer = "Nunkesser",
                Room = "PC-Pool H4.2-E00-130",
                Length = 90,
                Biweekly = true
            },
            new Course
            {
                Semester = 6,
                StartDate = "25.03.2025 08:00",
                Name = "SSP II: Mobile Security Projektkurs VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H4.1-E00-100",
                Length = 120,
                Biweekly = false
            },
            new Course
            {
                Semester = 6,
                StartDate = "01.04.2025 12:00",
                Name = "SSP II: Mobile Security Projektkurs ÜB",
                Lecturer = "Pelzl",
                Room = "Labor H3.3-E01-160",
                Length = 120,
                Biweekly = true
            },
            new Course
            {
                Semester = 6,
                StartDate = "27.03.2025 11:00",
                Name = "SSP II: App Development VL",
                Lecturer = "Nunkesser",
                Room = "Seminarraum H4.1-E00-100",
                Length = 90,
                Biweekly = false
            },
            new Course
            {
                Semester = 6,
                StartDate = "27.03.2025 09:30",
                Name = "SSP II: App Development ÜB",
                Lecturer = "Nunkesser",
                Room = "Labor H3.3-E01-220",
                Length = 90,
                Biweekly = false
            }*/
        };

        public List<ICourse> Execute()
        {
            return Courses;
        }
    }
}