using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Services
{
    public class ThemeService
    {
        public bool IsDarkMode { get; private set; }
        public event Action? OnChange;

        public void ToggleDarkMode()
        {
            IsDarkMode = !IsDarkMode;
            OnChange?.Invoke(); // Memberitahu UI untuk render ulang
        }
    }
}
