using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ISDCompanion
{
    public class ScheduleViewModel : INotifyPropertyChanged
    {

        public List<Appointment> Appointments { get; set; }

        public ScheduleViewModel()
        {
            Appointments = new List<Appointment>() {
                new Appointment() {
                    Id = 0,
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    Subject = "Betrieb",
                    LabelId = 1,
                    Location = "Raum 3"
                } };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
