using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musarium.Common {
    public class RegisterData : NotifyableObject {
        private string login;

        public string Login {
            get { return login; }
            set { login = value; base.OnChanged(); }
        }

        private string password; 

        public string Password {
            get { return password; }
            set { password = value; base.OnChanged(); }
        }

        private string validatePassword;

        public string ValidatePassword {
            get { return validatePassword; }
            set { validatePassword = value; base.OnChanged(); }
        }

        private string city;

        public string City {
            get { return city; }
            set { city = value; base.OnChanged(); }
        }
    }
}