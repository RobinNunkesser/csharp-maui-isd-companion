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
            new CourseViewModel { Name = "Mathematik II VL",
                                  Room = "Detail 1",
                                    StartDate = "01.09.2022 08:00",
            Length = 180, Lecturer = "Bubu", Semester=3, Occurrences=3},
            new CourseViewModel { Name = "Banana", Room = "Detail 2", Lecturer = "Buba" },
            new CourseViewModel { Name = "Laptop", Room = "Detail 3", Lecturer = "Bubi" },
            new CourseViewModel { Name = "Teddy Bear", Room = "Detail 4", Lecturer = "Bub" }
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

