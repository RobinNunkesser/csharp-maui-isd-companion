using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DevExpress.XamarinForms.Core.ConditionalFormatting;
using DevExpress.XamarinForms.Scheduler;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Xamarin.Essentials;
using Calendar = Plugin.Calendars.Abstractions.Calendar;

namespace ISDCompanion
{
    public class CoursesViewModel : ViewModel
    {
        private static readonly string format = "dd.MM.yyyy HH:mm";

        public List<CourseViewModel> Items { get; set; } = new List<CourseViewModel>
            {
            new CourseViewModel { Name = "Mathematik II VL",
                                  Room = "Detail 1",
                                    StartDate = "31.08.2022 08:00",
            Length = 180},
            new CourseViewModel { Name = "Banana", Room = "Detail 2" },
            new CourseViewModel { Name = "Laptop", Room = "Detail 3" },
            new CourseViewModel { Name = "Teddy Bear", Room = "Detail 4" }
            };

        // "21.03.2022 08:00", 180, "Mathematik II VL", occurrences: 14


        public List<CourseViewModel> FilteredItems
        {
            get
            {
                if (string.IsNullOrEmpty(searchTerm)) return Items;
                return Items.Where(
                    item => item.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    item.Room.ToLower().Contains(searchTerm.ToLower())).ToList();
            }
        }

        private string searchTerm = "";
        public string SearchTerm
        {
            get => searchTerm;
            set
            {
                if (searchTerm != value)
                {
                    searchTerm = value;
                    OnPropertyChanged("FilteredItems");
                }
            }
        }

        public CoursesViewModel()
        {
        }

        internal async void AddCourseToCalendar(CourseViewModel courseViewModel, Calendar selectedCalendar)
        {
            var startDate = DateTime.ParseExact(courseViewModel.StartDate, format, CultureInfo.InvariantCulture);
            var endDate = startDate.AddMinutes(courseViewModel.Length);
            for (int i = 0; i < courseViewModel.Occurrences; i++)
            {
                var calendarEvent = new CalendarEvent
                {
                    Name = courseViewModel.Name,
                    Start = startDate,
                    End = endDate,
                    Location = courseViewModel.Room
                };
                await CrossCalendars.Current.AddOrUpdateEventAsync(selectedCalendar, calendarEvent);

                startDate = startDate.AddDays(courseViewModel.Biweekly ? 14 : 7);
                endDate = endDate.AddDays(courseViewModel.Biweekly ? 14 : 7);
            }

        }
    }
}

