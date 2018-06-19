using System.Collections.ObjectModel;
using Autofac;
using System.Windows.Input;
using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;

namespace Musarium.ViewModel {
    public class PrizeShowViewModel : NotifyableObject, IPrizeShowViewModel {
        public IPrizeShowView View { get; private set; }
        private readonly IDataService dataService;
        private AppData AppData;
        public ObservableCollection<Prize> Prizes { get; set; }

        private ICommand createPrize;
        public ICommand CreatePrize {
            get {
                if (this.createPrize is null) {
                    this.createPrize = new RelayCommand(
                        (param) => {
                            this.AppData.Container.Resolve<IAddPrizeViewModel>().View.ShowDialog();
                        },
                        (param) => { return true; }
                    );
                }
                return this.createPrize;
            }
        }
        private Prize prize;
        public Prize Prize { get => prize; set { prize = value; base.OnChanged(); } }

        public PrizeShowViewModel(IPrizeShowView view, IDataService dataService) {
            this.View = view;
            this.View.BindDataContext(this);
            this.dataService = dataService;
            this.Prizes = new ObservableCollection<Prize>();
            this.AppData = AppData.GetInstance();
            var prizes = dataService.GetPrizes();
            foreach (Prize prize in prizes) {
                this.Prizes.Add(prize);
            }
        }
    }
}