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
        readonly CoursesViewModel viewModel = new();

        public CoursesPage()
        {
            InitializeComponent();
            BindingContext = viewModel;
            courses.ItemsSource = CourseDataService.Courses;
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

            viewModel.AddCourseToCalendar(e.CurrentSelection.FirstOrDefault() as CourseViewModel, selectedCalendar);

        }

        void OnSearchTextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            courses.ItemsSource = CourseDataService.GetSearchResults(searchBar.Text);
            searchBar.Focus();
        }
    }
}

