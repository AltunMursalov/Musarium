using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using Autofac;
using System.Windows.Input;

namespace Musarium.ViewModel {
    public class AddPrizeViewModel : NotifyableObject, IAddPrizeViewModel {
        public IAddPrizeView View { get; private set; }
        private readonly IDataService dataService;
        private Prize prize;
        private AppData AppData;
        public Prize Prize {
            get { return prize; }
            set { prize = value; base.OnChanged(); }
        }

        public AddPrizeViewModel(IAddPrizeView view, IDataService dataService) {
            this.View = view;
            this.View.BindDataContext(this);
            this.AppData = AppData.GetInstance();
            this.dataService = dataService;
            this.Prize = new Prize {
                PictureSrc = "Sry have not accsses to server"
            };
        }

        private ICommand save;
        public ICommand Save {
            get {
                if (this.save is null) {
                    this.save = new RelayCommand(
                        (param) => {
                            var result = dataService.CreatePrize(this.Prize);
                            if (result) {
                                var showPrize = this.AppData.Container.Resolve<IPrizeShowViewModel>();
                                showPrize.Prizes.Clear();
                                foreach (var item in dataService.GetPrizes()) {
                                    showPrize.Prizes.Add(item);
                                }
                                this.View.ShowAlert("The prize created!", "INFO");
                            }
                            this.View.Hide();
                        },
                        (param) => {
                            return this.Prize.PrizeName?.Length > 0;
                        }
                    );
                }
                return this.save;
            }
        }
    }
}