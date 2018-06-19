using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using Autofac;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Musarium.ViewModel {
    public class MuseumEditingViewModel : NotifyableObject, IMuseumEditingViewModel {
        public IMuseumEditingView View { get; private set; }
        private IDataService dataService;
        private Museum museum = new Museum();
        public Museum Museum { get => museum; set { museum = value; base.OnChanged(); } }
        private AppData AppData = AppData.GetInstance();

        public MuseumEditingViewModel(IMuseumEditingView view, IDataService dataService) {
            this.View = view;
            this.View.BindDataContext(this);
            this.dataService = dataService;
        }

        private ICommand toMuseumDeveloper;

        public ICommand ToMuseumDeveloper {
            get {
                if (this.toMuseumDeveloper is null) {
                    this.toMuseumDeveloper = new RelayCommand(
                        (param) => {
                            this.View.Hide();
                            AppData.Container.Resolve<IMuseumDeveloperViewModel>().View.Show();
                        },
                        (param) => {
                            return true;
                        }
                    );
                }
                return this.toMuseumDeveloper;
            }
        }


        private ICommand saveChanges;
        public ICommand SaveChanges {
            get {
                if (this.saveChanges is null) {
                    this.saveChanges = new RelayCommand(
                        (param) => {
                            this.Museum.Point = param as Location;
                            this.Museum.CityName = this.GetSelectedCountry(this.Museum.Point);
                            var result = dataService.EditMuseum(this.Museum);
                            if (result != false) {
                                var devMuseum = AppData.Container.Resolve<IMuseumDeveloperViewModel>();
                                devMuseum.Museum.Description = this.Museum.Description;
                                devMuseum.Museum.WebSite = this.Museum.WebSite;
                                devMuseum.Museum.Title = this.Museum.Title;
                                devMuseum.Museum.CityName = this.Museum.CityName;
                                devMuseum.Museum.Radius = this.Museum.Radius;
                                devMuseum.Museum.Phone = this.Museum.Phone;
                                devMuseum.Museum.Point = this.Museum.Point;
                                devMuseum.View.SetCenter(this.Museum.Point);
                                this.View.ShowAlert("Successful edited!", "INFO");
                            } else {
                                this.View.ShowAlert("Error editing the museum!", "Error!");
                            }
                        },
                        (param) => {
                            return true;
                        }
                    );
                }
                return this.saveChanges;
            }
        }


        private string GetSelectedCountry(Location location) {
            NumberFormatInfo nfi = new NumberFormatInfo {
                NumberDecimalSeparator = "."
            };
            string latitude = location.Latitude.ToString(nfi);
            string longitude = location.Longitude.ToString(nfi);
            string query =
                $"http://dev.virtualearth.net/REST/v1/Locations/{latitude},{longitude}?o=json&key=eLuL2PZapgx4D2vS7Kqe~od5P4iK7p9ddyG9mmJ8nVg~ArF1qzf8-FieeGj-EVPFCiMRoLKPRu8i5p7pQWxZQIS1I2nOuavBsPLPVCl4o2TS";
            WebClient webClient = new WebClient();
            var data = webClient.DownloadString(query);
            dynamic json = JObject.Parse(data);
            string result = "";
            foreach (var set in json.resourceSets) {
                foreach (var resource in set.resources) {
                    result = resource.address.adminDistrict.Value;
                }
            }
            return result;
        }
    }
}