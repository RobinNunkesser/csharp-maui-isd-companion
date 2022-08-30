using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ISDCompanion.Resx;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class CoursesPage : ContentPage
    {
        public CoursesPage()
        {
            InitializeComponent();
            BindingContext = new CoursesViewModel();
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string previous = (e.PreviousSelection.FirstOrDefault() as ItemViewModel)?.Text;
            string current = (e.CurrentSelection.FirstOrDefault() as ItemViewModel)?.Text;

            var calendars = await CrossCalendars.Current.GetCalendarsAsync();
            var editableCalendars = calendars.Where((c) => c.CanEditCalendar).ToList();

            var selectedCalendar = editableCalendars.FirstOrDefault();

            if (editableCalendars.Count == 0)
            {
                await DisplayAlert(AppResources.Error, AppResources.NoCalendars, AppResources.OK);
                return;
            }
            else if (editableCalendars.Count > 1)
            {
                var names = editableCalendars.Select((c) => c.Name).ToArray();
                string chosenCalendar = await DisplayActionSheet("Kalender?", "Cancel", null, names);
                if (chosenCalendar.Equals("Cancel"))
                {
                    return;
                }
                foreach (var calendar in editableCalendars)
                {
                    if (calendar.Name.Equals(chosenCalendar))
                    {
                        selectedCalendar = calendar;
                        break;
                    }
                }
            }


            var calendarEvent = new CalendarEvent
            {
                Name = "Add calendar support",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                Reminders = new List<CalendarEventReminder> { new CalendarEventReminder() }
            };
            await CrossCalendars.Current.AddOrUpdateEventAsync(selectedCalendar, calendarEvent);
        }
    }
}

