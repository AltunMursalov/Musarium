using Musarium.Common;
using Musarium.Interfaces;
using Autofac;
using Musarium.Model;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using System.Threading.Tasks;

namespace Musarium.ViewModel {
    public class AddEditQuestViewModel : NotifyableObject, IAddEditViewModel {
        public IAddEditView View { get; private set; }
        private AppData AppData;
        private readonly IDataService dataService;
        private int viewNumber = 1;
        public ITaskInfoAboutQuestViewModel TaskInfoAboutQuest { get; set; }
        public IPrizeShowViewModel PrizeShow { get; set; }
        public ICreateQuestsViewModel QuestionTask { get; set; }

        private Brush firstStepBG;
        public Brush FirstStepBG { get => firstStepBG; set { firstStepBG = value; base.OnChanged(); } }

        private Brush secondStepBG;
        public Brush SecondStepBG { get => secondStepBG; set { secondStepBG = value; base.OnChanged(); } }

        private Brush thirdStepBG;
        public Brush ThirdStepBG { get => thirdStepBG; set { thirdStepBG = value; base.OnChanged(); } }

        BrushConverter bc;

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

            bc = new BrushConverter();

            firstStepBG = (Brush)bc.ConvertFrom("#7E9293");
            secondStepBG = (Brush)bc.ConvertFrom("#95A5A5");
            thirdStepBG = (Brush)bc.ConvertFrom("#95A5A5");
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
                            var result = this.dataService.CreateQuest(this.TaskInfoAboutQuest.Quest, this.QuestionTask.Question, this.PrizeShow.Prize, this.AppData.CurrentMuseum);
                            if (result != null) {
                                this.View.ShowAlert("Created", "Yeee");
                            } else {
                                this.View.ShowAlert("As always", "try");
                            }
                        },
                        (param) => { return true; }
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
                            if (this.viewNumber == 0) {
                                this.TaskInfoAboutQuest.View.Show();
                                this.QuestionTask.View.Hide();
                                this.PrizeShow.View.Hide();
                                this.viewNumber++;

                                firstStepBG = (Brush)bc.ConvertFrom("#7E9293");
                                secondStepBG = (Brush)bc.ConvertFrom("#95A5A5");
                                thirdStepBG = (Brush)bc.ConvertFrom("#95A5A5");
                            } else
                            if (this.viewNumber == 1) {
                                this.QuestionTask.View.Show();
                                this.PrizeShow.View.Hide();
                                this.TaskInfoAboutQuest.View.Hide();
                                this.viewNumber++;


                                firstStepBG = (Brush)bc.ConvertFrom("#95A5A5");
                                secondStepBG = (Brush)bc.ConvertFrom("#7E9293");
                                thirdStepBG = (Brush)bc.ConvertFrom("#95A5A5");
                            } else
                            if (this.viewNumber == 2) {
                                this.PrizeShow.View.Show();
                                this.QuestionTask.View.Hide();
                                this.TaskInfoAboutQuest.View.Hide();
                                this.viewNumber = 0;
                                this.View.ChangedButtonToDone();

                                firstStepBG = (Brush)bc.ConvertFrom("#95A5A5");
                                secondStepBG = (Brush)bc.ConvertFrom("#95A5A5");
                                thirdStepBG = (Brush)bc.ConvertFrom("#7E9293");
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