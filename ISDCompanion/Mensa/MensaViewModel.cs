using System;
using System.Collections.ObjectModel;

namespace ISDCompanion
{
    public class MensaViewModel
    {
        public ObservableCollection<SectionViewModel<IMeal>> Meals { get; set; } 

        public MensaViewModel()
        {
            Meals = new ObservableCollection<SectionViewModel<IMeal>>();
            var section = new SectionViewModel<IMeal>()
            {
                LongName = "Suppe", ShortName = "S"
            };
            section.Add(new Meal() {
                Name = "Ochsenschwanzsuppe",
                Image = "http://www.studentenwerk-pb.de/fileadmin/imports/images/speiseleitsystem/7244.jpg"
            });

            Meals.Add(section);
        }
    }
}
