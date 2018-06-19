using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System.Collections.ObjectModel;
using Autofac;
using System.Windows.Input;
using System.Collections.Generic;

namespace Musarium.ViewModel {
    class MuseumDeveloperViewModel : NotifyableObject, IMuseumDeveloperViewModel {
        private IDataService dataService;
        private Museum museum;
        public Museum Museum { get => museum; set { museum = value; base.OnChanged(); } }
        public ObservableCollection<Quest> Quests { get; set; }
        public ObservableCollection<Prize> Prizes { get; set; }

        private AppData AppData = AppData.GetInstance();

        public MuseumDeveloperViewModel(IMuseumDeveloperView view) {
            View = view;
            this.View.BindDataContext(this);
            this.dataService = AppData.Container.Resolve<IDataService>();
            this.Quests = new ObservableCollection<Quest>();
            this.Prizes = new ObservableCollection<Prize>();
        }

        private ICommand addQuest;
        public ICommand AddQuest {
            get {
                if (this.addQuest is null) {
                    this.addQuest = new RelayCommand( 
                        (param) => {
                            this.View.Hide();
                            AppData.Container.Resolve<IMuseumEditingViewModel>().View.Hide();
                            AppData.Container.Resolve<IAddEditViewModel>().View.Show();
                        },
                        (param) => { return true; }
                    );
                }
                return this.addQuest;
            }
        }

        private ICommand deleteQuest;
        public ICommand DeleteQuest {
            get {
                if (this.deleteQuest is null) {
                    this.deleteQuest = new RelayCommand(
                        (param) => {
                            var confirmRemove = this.View.ShowAlert("Do you really want to delete this quest?", "WARNING");
                            if (confirmRemove == System.Windows.MessageBoxResult.Yes) {
                                var result = dataService.DeleteQuest(param as Quest);
                                if (result != false) {
                                    Quests.Remove(param as Quest);
                                }
                            }
                        },
                        (param) => { return true; }
                    );
                }
                return this.deleteQuest;
            }
        }

        private ICommand openQuest;
        public ICommand OpenQuest {
            get {
                if (this.openQuest is null) {
                    this.openQuest = new RelayCommand(
                        (param) => {
                            var questions = this.dataService.GetQuestQuestions(param as Quest);
                            var infoAboutQuest = AppData.Container.Resolve<IInfoAboutQuestViewModel>(new TypedParameter[] { new TypedParameter(typeof(List<Question>), questions), new TypedParameter(typeof(Quest), param) });
                            infoAboutQuest.View.Show();
                        },
                        (param) => { return true; }
                    );
                }
                return this.openQuest;
            }
        }

        private ICommand toMuseumEditing;

        public ICommand ToMuseumEditing {
            get {
                if (this.toMuseumEditing is null) {
                    this.toMuseumEditing = new RelayCommand(
                        (param) => {
                            this.View.Hide();
                            this.AppData.Container.Resolve<IAddEditViewModel>().View.Hide();
                            this.AppData.Container.Resolve<IMuseumEditingViewModel>().View.Show();
                        },
                        (param) => {
                            return true;
                        }
                    );
                }
                return this.toMuseumEditing;
            }
        }

        public IMuseumDeveloperView View { get; private set; }
    }
}
