using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System.Windows.Input;
using Autofac;

namespace Musarium.ViewModel {
    public class TextQuestionViewModel : NotifyableObject, ITextQuestionViewModel {
        public ITextQuestionView View { get; private set; }
        private AppData AppData;
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
            this.Answer = new Answer {
                IsRight = true
            };
            this.AppData = AppData.GetInstance();
            this.Question = new Question {
                Points = 10,
                PictureSrc = "awdbhawd"
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
                            this.Question.Answer = Answer.QuestionAnswer;
                            this.AppData.Container.Resolve<ICreateQuestsViewModel>().Answers.Add(new Answer {
                                Id = this.Answer.Id,
                                IsRight = this.Answer.IsRight,
                                QuestionAnswer = this.Answer.QuestionAnswer,
                                QuestionID = this.Answer.QuestionID,
                                Type = 1
                            });
                            this.AppData.Container.Resolve<ICreateQuestsViewModel>().Questions.Add(new Question {
                                Answer = this.Question.Answer,
                                Description = this.Question.Description,
                                Hint = this.Question.Hint,
                                PictureSrc = this.Question.PictureSrc,
                                Id = this.Question.Id,
                                Points = this.Question.Points,
                                QuestId = this.Question.QuestId,
                                QuestionType = this.Question.QuestionType
                            });
                            this.View.ShowAlert("Question added!", "INFO");
                            this.View.Clear();
                        },
                        (param) => { return true; }
                    );
                }
                return this.save;
            }
        }
    }
}