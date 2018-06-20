using Musarium.Common;
using Autofac;
using Musarium.Interfaces;
using System.Windows.Input;
using Musarium.Model;
using System.Collections.ObjectModel;

namespace Musarium.ViewModel {
    public class CreateQuestionsViewModel : NotifyableObject, ICreateQuestsViewModel {
        public ICreateQuestsView View { get; private set; }
        public ObservableCollection<Question> Questions { get; set; }
        public ObservableCollection<Answer> Answers { get; set; }
        private AppData AppData;
        public CreateQuestionsViewModel(ICreateQuestsView view) {
            this.View = view;
            this.View.BindDataContext(this);
            this.AppData = AppData.GetInstance();
            this.Questions = new ObservableCollection<Question>();
            this.Answers = new ObservableCollection<Answer>();
        }

        private ICommand chooseType;
        public ICommand ChooseType {
            get {
                if (this.chooseType is null) {
                    this.chooseType = new RelayCommand(
                        (param) => {
                            AppData.Container.Resolve<IQuestionTypeViewModel>().View.ShowDialog();
                        },
                        (param) => { return true; }
                    );
                }
                return this.chooseType;
            }
        }
    }
}