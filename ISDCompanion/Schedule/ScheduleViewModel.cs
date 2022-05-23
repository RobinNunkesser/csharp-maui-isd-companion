using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XamarinForms.Scheduler;

namespace ISDCompanion
{
    public class ScheduleViewModel : INotifyPropertyChanged
    {

        public List<AppointmentItem> Appointments { get => Settings.Appointments; }

        public ScheduleViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
