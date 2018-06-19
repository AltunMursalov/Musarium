using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using Autofac;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Musarium.ViewModel {
    public class MusariumViewModel : IMusariumViewModel {
        public IMusariumView View { get; private set; }
        public IMuseumDeveloperViewModel MuseumDeveloper { get; set; }
        public IAddEditViewModel AddEditQuest { get; set; }
        public IMuseumEditingViewModel MuseumEditing { get; set; }
        private AppData AppData = AppData.GetInstance();

        public MusariumViewModel(IMusariumView view, IMuseumDeveloperViewModel museumDeveloperViewModel, 
            IMuseumEditingViewModel museumEditingViewModel, IAddEditViewModel addEditViewModel) {
            this.MuseumDeveloper = museumDeveloperViewModel;
            this.AddEditQuest = addEditViewModel;
            this.MuseumEditing = museumEditingViewModel;
            this.View = view;
            view.BindDataContext(this);

            this.View.SetFramesContent(this.MuseumDeveloper.View, this.MuseumEditing.View, this.AddEditQuest.View);
        }

        private ICommand logOut;

        public ICommand LogOut {
            get {
                if (this.logOut is null) { 
                    this.logOut = new RelayCommand(
                        (param) => {
                            this.View.Hide();
                            AppData.Container.Resolve<IAuthorizationViewModel>().View.Show();
                        },
                        (param) => {
                            return true;
                        }
                    );
                }
                return this.logOut;
            }
        }
    }
}