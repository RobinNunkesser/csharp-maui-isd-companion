using ISDCompanion.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ISDCompanion.Models
{
    public class Topic : BaseModel
    {
        public override int getHeight()
        {
            if (!InitExpanded)
            {
                return base.getHeight();
            }

            int tmp = 0;
            foreach (var item in Exercises)
            {
                tmp += item.getHeight();
            }
            return tmp + 70;
        }

        public string Topic_Title { get; set; }

        public TrulyObservableCollection<Exercise> Exercises { get; set; }

    }
}
