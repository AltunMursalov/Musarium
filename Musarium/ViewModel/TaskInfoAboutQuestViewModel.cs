using Musarium.Common;
using Musarium.Interfaces;
using Autofac;
using Musarium.Model;
using System.Collections.Generic;

namespace Musarium.ViewModel {
    public class TaskInfoAboutQuestViewModel : NotifyableObject, ITaskInfoAboutQuestViewModel {
        private AppData AppData = AppData.GetInstance();
        public List<string> Difficults { get; set; }

        private string selectedDifficult;
        public string SelectedDifficult {
            get { return selectedDifficult; }
            set {
                selectedDifficult = value;
                if (value == "Легкий") {
                    this.Quest.Difficult = 1;
                } else if (value == "Средний") {
                    this.Quest.Difficult = 2;
                } else if (value == "Сложный") {
                    this.Quest.Difficult = 3;
                }
            }
        }

        private Quest quest;
        public Quest Quest { get => quest; set { quest = value; base.OnChanged(); } }
        public ITaskInfoAboutQuestView View { get; private set; }
        public TaskInfoAboutQuestViewModel(ITaskInfoAboutQuestView view) {
            this.View = view;
            this.View.BindDataContext(this);
            this.Quest = new Quest {
                PictureSrc = "Test1",
                PrizeId = 1
            };
            Difficults = new List<string> {
                "Легкий", 
                "Средний",
                "Сложный"
            };
        }
    }
}