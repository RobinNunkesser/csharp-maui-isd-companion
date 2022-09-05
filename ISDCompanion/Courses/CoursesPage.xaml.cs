using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ISDCompanion.Resx;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ISDCompanion
{
    public partial class CoursesPage : ContentPage
    {
        public CoursesPage()
        {
            InitializeComponent();
            courses.ItemsSource = CourseDataService.Courses;
        }

        public async Task<PermissionStatus> CheckAndRequestCalendarReadPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.CalendarRead>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.CalendarRead>())
            {
                // Prompt the user with additional information as to why the permission is needed
            }

            status = await Permissions.RequestAsync<Permissions.CalendarRead>();

            return status;
        }

        public async Task<PermissionStatus> CheckAndRequestCalendarWritePermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.CalendarWrite>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.CalendarWrite>())
            {
                // Prompt the user with additional information as to why the permission is needed
            }

            status = await Permissions.RequestAsync<Permissions.CalendarWrite>();

            return status;
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
            else
            {
                var names = editableCalendars.Select((c) => c.Name).ToArray();
                string chosenCalendar = await DisplayActionSheet(AppResources.CalendarExportQuery, AppResources.Cancel, null, names);
                if (chosenCalendar.Equals(AppResources.Cancel))
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

            CourseDataService.AddCourseToCalendar(e.CurrentSelection.FirstOrDefault() as CourseViewModel, selectedCalendar);

        }

        void OnSearchTextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            courses.ItemsSource = CourseDataService.GetSearchResults(searchBar.Text);
            searchBar.Focus();
        }
    }
}

