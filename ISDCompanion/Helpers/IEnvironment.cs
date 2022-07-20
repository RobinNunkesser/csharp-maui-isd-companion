using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ISDCompanion.Helpers
{
    public interface IEnvironment
    {
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
        void SetNavigationBarColor(bool darkMode);
    }
}
