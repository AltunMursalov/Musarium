using Musarium.Common;
using Musarium.Interfaces;
using Autofac;
using System.Windows.Input;
using System;

namespace Musarium.ViewModel {
    public class AddEditQuestViewModel : NotifyableObject, IAddEditViewModel {
        public IAddEditView View { get; private set; }
        private AppData AppData;
        private readonly IDataService dataService;
        private int viewNumber = 1;
        public ITaskInfoAboutQuestViewModel TaskInfoAboutQuest { get; set; }
        public IPrizeShowViewModel PrizeShow { get; set; }
        public ICreateQuestsViewModel QuestionTask { get; set; }

        public AddEditQuestViewModel(IAddEditView view, ITaskInfoAboutQuestViewModel taskInfoAboutQuestViewModel, ICreateQuestsViewModel createQuestsViewModel,
                                     IPrizeShowViewModel prizeShowViewModel, IDataService dataService) {
            this.View = view;
            this.View.BindDataContext(this);
            this.TaskInfoAboutQuest = taskInfoAboutQuestViewModel;
            this.QuestionTask = createQuestsViewModel;
            this.PrizeShow = prizeShowViewModel;
            this.dataService = dataService;
            this.AppData = AppData.GetInstance();
            this.View.SetFramesContext(this.TaskInfoAboutQuest.View, this.QuestionTask.View, this.PrizeShow.View);
        }

        private ICommand toMuseumDeveloper;
        public ICommand ToMuseumDeveloper {
            get {
                if (this.toMuseumDeveloper is null) {
                    this.toMuseumDeveloper = new RelayCommand(
                        (param) => {
                            this.View.Hide();
                            AppData.Container.Resolve<IMuseumDeveloperViewModel>().View.Show();
                        },
                        (param) => { return true; }
                    );
                }
                return this.toMuseumDeveloper;
            }
        }

        private ICommand createQuest;
        public ICommand CreateQuest {
            get {
                if (this.createQuest is null) {
                    this.createQuest = new RelayCommand(
                        (param) => {
                            var result = dataService.CreateQuest(this.TaskInfoAboutQuest.Quest, this.QuestionTask.Questions, this.QuestionTask.Answers, this.PrizeShow.Prize);
                            if (result) {
                                this.View.ShowAlert("CREATED", "INFO");
                            }
                        },
                        (param) => { return this.PrizeShow.Prize != null; }
                    );
                }
                return this.createQuest;
            }
        }

        private ICommand next;
        public ICommand Next {
            get {
                if (this.next is null) {
                    this.next = new RelayCommand(
                        (param) => {
                            if (this.viewNumber == 1 && !String.IsNullOrEmpty(this.TaskInfoAboutQuest.Quest.Title) &&
                                !String.IsNullOrEmpty(this.TaskInfoAboutQuest.Quest.Description)) {
                                this.QuestionTask.View.Show();
                                this.PrizeShow.View.Hide();
                                this.TaskInfoAboutQuest.View.Hide();
                                this.TaskInfoAboutQuest.View.Clear();
                                this.viewNumber++;
                            } else
                            if (this.viewNumber == 2 && this.QuestionTask.Answers.Count > 0 && this.QuestionTask.Questions.Count > 0) {
                                this.PrizeShow.View.Show();
                                this.QuestionTask.View.Hide();
                                this.TaskInfoAboutQuest.View.Hide();
                                this.viewNumber = 1;
                                this.View.ChangedButtonToDone();
                            } else {
                                this.View.ShowAlert("Please fill the data!", "Error");
                            }
                        },
                        (param) => { return true; }
                    );
                }
                return this.next;
            }
        }

    }
}