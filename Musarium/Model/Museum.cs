using Microsoft.Maps.MapControl.WPF;
using Musarium.Common;
using System.Globalization;

namespace Musarium.Model {
    public class Museum : NotifyableObject {
        private int id;

        public int Id {
            get { return id; }
            set { id = value; base.OnChanged(); }
        }

        private string title;

        public string Title {
            get { return title; }
            set { title = value; base.OnChanged(); }
        }

        private string description;

        public string Description {
            get { return description; } 
            set { description = value; base.OnChanged(); }
        }

        private string adress;

        public string Adress {
            get { return adress; }
            set { adress = value; base.OnChanged(); }
        }

        private string phone;

        public string Phone {
            get { return phone; }
            set { phone = value; base.OnChanged(); }
        }

        private string picSrc;

        public string PictureSrc {
            get { return picSrc; }
            set { picSrc = value; base.OnChanged(); }
        }

        private string webSite;

        public string WebSite {
            get { return webSite; }
            set { webSite = value; base.OnChanged(); }
        }

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

        private float latitude;

        public float Latitude {
            get { return latitude; }
            set { latitude = value; base.OnChanged(); base.OnChanged(nameof(Point)); }
        }

        private float longitude;

        public float Longitude {
            get { return longitude; }
            set { longitude = value; base.OnChanged(); base.OnChanged(nameof(Point)); }
        }

        private string cityName;

        public string CityName {
            get { return cityName; }
            set { cityName = value; base.OnChanged(); }
        }

        public Location Point {
            get {
                return new Location(this.Latitude, this.Longitude);
            }
            set {
                this.Latitude = (float)value.Latitude;
                this.Longitude = (float)value.Longitude;
                base.OnChanged();
            }
        }

        private float radius;

        public float Radius {
            get { return radius; }
            set { radius = value; base.OnChanged(); }
        }

        private int cityId;

        public int CityId {
            get { return cityId; }
            set { cityId = value; base.OnChanged(); }
        }
    }
}
