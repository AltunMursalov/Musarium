using Musarium.Common;
using System;

namespace Musarium.Model {
    public class User : NotifyableObject{
        private int id;

        public int Id {
            get { return id; }
            set { id = value; base.OnChanged(); }
        }

        private string firstName;

        public string FirstName {
            get { return firstName; }
            set { firstName = value; base.OnChanged(); }
        }

        private string lastName;

        public string LastName {
            get { return lastName; }
            set { lastName = value; base.OnChanged(); } 
        }

        private DateTime birthDate;

        public DateTime BirthDate {
            get { return birthDate; }
            set { birthDate = value; base.OnChanged(); }
        }
    }
}
