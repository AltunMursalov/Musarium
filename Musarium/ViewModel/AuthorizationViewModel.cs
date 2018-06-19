using Musarium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using System.Text;
using System.Threading.Tasks;
using Musarium.Common;
using Musarium.Model;

namespace Musarium.ViewModel {
    public class AuthorizationViewModel : IAuthorizationViewModel {
        private AppData AppData = AppData.GetInstance();
        private IDataService dataService;
        public AuthorizationViewModel(ILogInViewModel logInViewModel, IRegistrationViewModel registrationViewModel, IAuthorizationView view, IDataService dataService) {
            LogInViewModel = logInViewModel;
            this.dataService = dataService;
            RegistrationViewModel = registrationViewModel;
            View = view;

            View.BindDataContext(this);
            LogInViewModel.Loginned += LogInViewModel_Loginned;
            this.View.SetFramesContent(LogInViewModel.View, RegistrationViewModel.View);
        }

        private void LogInViewModel_Loginned(object sender, EventArgs e) {
            this.View.Hide();
            var mainWindow = AppData.Container.Resolve<IMusariumViewModel>();
            mainWindow.View.Show();
            var museumEditing = AppData.Container.Resolve<IMuseumEditingViewModel>();
            var museumDeveloper = AppData.Container.Resolve<IMuseumDeveloperViewModel>();
            museumDeveloper.Museum = dataService.GetByLogin(LogInViewModel.Museum.Login);
            museumDeveloper.Museum.CityName = dataService.GetCityName(museumDeveloper.Museum.CityId);
            var editingMuseum = dataService.GetByLogin(LogInViewModel.Museum.Login);
            museumEditing.Museum.Point = editingMuseum.Point;
            museumEditing.Museum.Adress = editingMuseum.Adress;
            museumEditing.Museum.PictureSrc = editingMuseum.PictureSrc;
            museumEditing.Museum.Description = editingMuseum.Description;
            museumEditing.Museum.Phone = editingMuseum.Phone;
            museumEditing.Museum.Radius = editingMuseum.Radius;
            museumEditing.Museum.CityName = museumDeveloper.Museum.CityName;
            museumEditing.Museum.WebSite = editingMuseum.WebSite;
            museumEditing.Museum.Id = editingMuseum.Id;
            museumEditing.Museum.Title = editingMuseum.Title;
            museumEditing.Museum.CityName = editingMuseum.CityName;
            museumEditing.View.SetCenter(museumEditing.Museum.Point);
            museumDeveloper.Prizes.Clear();
            museumDeveloper.Quests.Clear();
            foreach (Prize prize in dataService.GetMuseumPrizes(museumDeveloper.Museum.Id)) {
                museumDeveloper.Prizes.Add(prize);
            }
            foreach (Quest quest in dataService.GetMuseumQuests(museumDeveloper.Museum.Id)) {
                museumDeveloper.Quests.Add(quest);
            }
        }

        public ILogInViewModel LogInViewModel { get; private set; }
        public IRegistrationViewModel RegistrationViewModel { get; private set; }
        public IAuthorizationView View { get; private set; }
    }
}