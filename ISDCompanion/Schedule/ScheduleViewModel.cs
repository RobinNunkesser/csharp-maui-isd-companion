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
                    StartTime = DayInWeek(DayOfWeek.Monday).AddHours(8),
                    EndTime = DayInWeek(DayOfWeek.Monday).AddHours(10),
                    Subject = "Montag",
                    LabelId = 1,
                    Location = "Raum 3"
                },
                new Appointment() {
                    Id = 1,
                    StartTime = DayInWeek(DayOfWeek.Wednesday).AddHours(10),
                    EndTime = DayInWeek(DayOfWeek.Wednesday).AddHours(12),
                    Subject = "Mittwcoh",
                    LabelId = 1,
                    Location = "Raum 3"
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /*private Appointment CreateAppointment(int weekday) {
            DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + weekday);
        }*/
        private DateTime DayInWeek(DayOfWeek dayOfWeek)
        {
            return DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)dayOfWeek);
        }
    }
}
