using ISDCompanion.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ISDCompanion.Models
{
    public class Exercise : BaseModel
    {
        public override int getHeight()
        {
            if (!InitExpanded)
            {
                return base.getHeight();
            }

            return 20;
        }

        public string Exercise_Title { get; set; }
        public string Command { get; set; }
        public string CommandParameter { get; set; }
    }
}
