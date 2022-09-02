using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Calendar = Plugin.Calendars.Abstractions.Calendar;

namespace ISDCompanion
{
    public static class CourseDataService
    {
        private static readonly string shortFormat = "dd.MM.yyyy";

        public static List<DateTime> Holidays { get; } = new List<DateTime>
        {
            DateTime.ParseExact("08.09.2022",shortFormat, CultureInfo.InvariantCulture)
        };

        public static List<CourseViewModel> Courses { get; } = new List<CourseViewModel>
            {
            new CourseViewModel {
                Semester = 1,
                StartDate = "19.09.2022 08:00",
                Name = "Chemie VL",
                Lecturer = "Berndt",
                Room = "Hörsaal Stadtwerke Hamm",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "19.09.2022 10:00",
                Name = "Technisches Englisch I VL",
                Lecturer = "Strack",
                Room = "Hörsaal Stadtwerke Hamm",
                Length = 60,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "19.09.2022 12:00",
                Name = "Mathematik I VL",
                Lecturer = "Ponick",
                Room = "Hörsaal HAM 6",
                Length = 60,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "19.09.2022 13:00",
                Name = "Grundlagen der Programmierung VL",
                Lecturer = "Stuckenholz",
                Room = "Hörsaal HAM 6",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "20.09.2022 08:00",
                Name = "Technische Informatik I VL",
                Lecturer = "Krenz-Baath",
                Room = "Hörsaal WESTPRESS",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "20.09.2022 10:00",
                Name = "Mathematik I VL",
                Lecturer = "Ponick",
                Room = "Hörsaal WESTPRESS",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "20.09.2022 14:00",
                Name = "Technische Informatik I ÜB - Gruppe A",
                Lecturer = "Krenz-Baath",
                Room = "PC-Pool H4.2-E00-140, Labor H3.3-E01-220",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "21.09.2022 13:00",
                Name = "Technische Informatik I ÜB - Gruppe B",
                Lecturer = "Krenz-Baath",
                Room = "PC-Pool H4.2-E00-140, Labor H3.3-E01-220",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "22.09.2022 08:00",
                Name = "Personal Skills I VL/ÜB",
                Lecturer = "Grewe",
                Room = "Hörsaal HAM 6",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "22.09.2022 10:00",
                Name = "Mathematik I ÜB",
                Lecturer = "Ponick",
                Room = "Seminarraum H1.2-E01-010",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "22.09.2022 14:00",
                Name = "Technisches Englisch I ÜB",
                Lecturer = "Strack",
                Room = "Seminarraum H1.2-E01-010",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "22.09.2022 16:00",
                Name = "Physik ÜB - Gruppe A",
                Lecturer = "Kientopf",
                Room = "Seminarraum H7.2-E00-002",
                Length = 60,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "21.09.2022 11:00",
                Name = "Physik ÜB - Gruppe B",
                Lecturer = "Kientopf",
                Room = "Hörsaal HAM 6",
                Length = 60,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "23.09.2022 08:00",
                Name = "Physik VL",
                Lecturer = "Kientopf",
                Room = "Seminarraum H4.2-E00-110",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "23.09.2022 10:00",
                Name = "Biologie VL",
                Lecturer = "Tickenbrock",
                Room = "Seminarraum H4.2-E00-110",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "23.09.2022 14:00",
                Name = "Grundlagen der Programmierung ÜB - Gruppe A",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 1,
                StartDate = "21.09.2022 09:00",
                Name = "Grundlagen der Programmierung ÜB - Gruppe B",
                Lecturer = "Stuckenholz",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "19.09.2022 08:00",
                Name = "Digitaltechnik I VL",
                Lecturer = "Krenz-Baath",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "19.09.2022 10:00",
                Name = "Embedded Systems I VL",
                Lecturer = "Pelzl",
                Room = "Seminarraum H1.1-E01-150",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "19.09.2022 12:00",
                Name = "Technisches Englisch III VL",
                Lecturer = "Strack",
                Room = "Seminarraum H1.1-E01-150",
                Length = 60,
                Occurrences = 17,
                Biweekly = false
            },
            new CourseViewModel {
                Semester = 3,
                StartDate = "19.09.2022 13:00",
                Name = "System Modellierung II VL",
                Lecturer = "Runovska",
                Room = "PC-Pool H4.2-E00-140",
                Length = 120,
                Occurrences = 17,
                Biweekly = false
            }
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

        private static readonly string longFormat = "dd.MM.yyyy HH:mm";

        internal static async void AddCourseToCalendar(CourseViewModel courseViewModel, Calendar selectedCalendar)
        {
            var startDate = DateTime.ParseExact(courseViewModel.StartDate, longFormat, CultureInfo.InvariantCulture);
            var endDate = startDate.AddMinutes(courseViewModel.Length);
            for (int i = 0; i < courseViewModel.Occurrences; i++)
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

