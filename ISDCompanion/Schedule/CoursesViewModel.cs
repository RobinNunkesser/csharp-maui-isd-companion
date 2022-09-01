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

