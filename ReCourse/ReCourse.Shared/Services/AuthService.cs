using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCourse.Shared.Services
{
    public class AuthService
    {
        public bool IsLoggedIn { get; private set; } = false;
        public string? CurrentUser { get; private set; }

        public bool Login(string username, string password)
        {
            // Dummy logic: username apa saja, password harus 'admin123'
            if (password == "admin123" && !string.IsNullOrEmpty(username))
            {
                IsLoggedIn = true;
                CurrentUser = username;
                return true;
            }
            return false;
        }

        public void Logout()
        {
            IsLoggedIn = false;
            CurrentUser = null;
        }
    }
}
