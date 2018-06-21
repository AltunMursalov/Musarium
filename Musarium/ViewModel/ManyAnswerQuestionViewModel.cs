using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System;
using Autofac;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Musarium.ViewModel {
    public class ManyAnswerQuestionViewModel : NotifyableObject, IManyAnswerQuestionViewModel {
        public ObservableCollection<Answer> Answers { get; set; }
        private readonly IDataService dataService;
        private readonly AppData appData;
        public ManyAnswerQuestionViewModel(IManyAnswerQuestionView view, IDataService dataService) {
            this.View = view;
            this.View.BindDataContext(this);
            this.dataService = dataService;
            this.appData = AppData.GetInstance();
            this.Answers = new ObservableCollection<Answer> {
                new Answer {
                    Type = 2,
                    IsRight = false
                }
            };
            this.Question = new Question {
                PictureSrc = "awdawdaw",
                QuestId = appData.Container.Resolve<ITaskInfoAboutQuestViewModel>().Quest.Id
            };
        }

        private Question question;
        public Question Question {
            get { return question; }
            set { question = value; base.OnChanged(); }
        }


        private ICommand newAnswer;
        public ICommand NewAnswer {
            get {
                if (this.newAnswer is null) {
                    this.newAnswer = new RelayCommand(
                        (param) => {
                            this.Answers.Add(new Answer {
                                IsRight = false,
                                Type = 2
                            });
                        },
                        (param) => { return this.Answers.Count < 6; }
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
                            var createQuestions = this.appData.Container.Resolve<ICreateQuestsViewModel>();
                            foreach (var item in this.Answers) {
                                createQuestions.Answers.Add(new Answer {
                                    Id = item.Id,
                                    IsRight = item.IsRight,
                                    QuestionAnswer = item.QuestionAnswer,
                                    QuestionID = item.QuestionID,
                                    Type = item.Type
                                });
                                if (item.IsRight) {
                                    this.Question.Answer = item.QuestionAnswer;
                                }
                            }
                            createQuestions.Questions.Add(new Question {
                                Answer = this.Question.Answer,
                                Description = this.Question.Description,
                                Hint = this.Question.Hint,
                                Id = this.Question.Id,
                                PictureSrc = this.Question.PictureSrc,
                                Points = this.Question.Points,
                                QuestId = this.Question.QuestId,
                                QuestionType = this.Question.QuestionType
                            });
                            this.View.Hide();
                            this.View.Clear();
                        },
                        (param) => {
                            int i = 0;
                            foreach (var item in this.Answers) {
                                if (item.IsRight) {
                                    i++;
                                } else if (String.IsNullOrEmpty(item.QuestionAnswer)) {
                                    return false;
                                }
                            }
                            if (!String.IsNullOrEmpty(this.Question.Description) && i > 0) {
                                return true;
                            } else {
                                return false;
                            }
                        }
                    );
                }
                return this.save;
            }
        }

        public IManyAnswerQuestionView View { get; private set; }
    }
}