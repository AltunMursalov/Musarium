using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Musarium.ViewModel {
    public class ManyAnswerQuestionViewModel : NotifyableObject, IManyAnswerQuestionViewModel {
        public ObservableCollection<Answer> Answers { get; set; }
        private readonly IDataService dataService;
        public ManyAnswerQuestionViewModel(IManyAnswerQuestionView view, IDataService dataService) {
            this.View = view;
            this.View.BindDataContext(this);
            this.dataService = dataService;
            this.Answers = new ObservableCollection<Answer> {
                new Answer {
                    IsRight = false
                }
            };
        }

        private ICommand newAnswer;
        public ICommand NewAnswer {
            get {
                if (this.newAnswer is null) {
                    this.newAnswer = new RelayCommand(
                        (param) => {
                            if (this.Answers.Count == 6) {
                                this.View.ShowAlert("Answers count is cannot be more than 6!", "Error");
                            } else {
                                this.Answers.Add(new Answer {
                                    IsRight = false
                                });
                            }
                        },
                        (param) => { return true; }
                    );
                }
                return this.newAnswer;
            } 
        }

        private ICommand save;
        public ICommand Save {
            get {
                if (this.save is null) {
                    this.save = new RelayCommand(
                        (param) => {
                            this.dataService.CreateQuestion()
                        },
                        (param) => { return true; }
                    );
                }
                return this.save;
            }
        }

        public IManyAnswerQuestionView View { get; private set; }
    }
}