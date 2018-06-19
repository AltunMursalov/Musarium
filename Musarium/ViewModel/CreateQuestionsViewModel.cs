using Musarium.Common;
using Autofac;
using Musarium.Interfaces;
using System.Windows.Input;
using Musarium.Model;

namespace Musarium.ViewModel {
    public class CreateQuestionsViewModel : NotifyableObject, ICreateQuestsViewModel {
        public ICreateQuestsView View { get; private set; }
        private Question question;
        public Question Question { get => question; set { question = value; base.OnChanged(); } }
        private AppData AppData;
        public CreateQuestionsViewModel(ICreateQuestsView view) {
            this.View = view;
            this.View.BindDataContext(this);
            this.AppData = AppData.GetInstance();
            this.Question = new Question();
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