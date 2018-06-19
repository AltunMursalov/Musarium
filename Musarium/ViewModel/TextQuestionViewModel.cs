using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System.Windows.Input;

namespace Musarium.ViewModel {
    public class TextQuestionViewModel : NotifyableObject, ITextQuestionViewModel {
        public ITextQuestionView View { get; private set; }
        private Question question;
        public Question Question {
            get { return question; }
            set { question = value; base.OnChanged(); }
        }

        private Answer answer;
        public Answer Answer {
            get { return answer; }
            set { answer = value; base.OnChanged(); }
        }

        private readonly IDataService dataService;

        public TextQuestionViewModel(ITextQuestionView view, IDataService dataService) {
            View = view;
            View.BindDataContext(this);
            this.Question = new Question {
                Points = 10
            };
            this.dataService = dataService;
        }

        private ICommand save;
        public ICommand Save {
            get {
                if (this.save is null) {
                    this.save = new RelayCommand(
                        (param) => {
                            this.View.Hide();
                            this.dataService.CreateQuestion(this.Question, this.Answer);
                        },
                        (param) => { return true; }
                    );
                }
                return this.save;
            }
        }
    }
}