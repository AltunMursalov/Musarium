using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System.Collections.Generic;

namespace Musarium.ViewModel {
    public class InfoAboutQuestViewModel : NotifyableObject, IInfoAboutQuestViewModel {
        public IInfoAboutQuestView View { get; set; }
        private Quest quest;

        public Quest Quest {
            get { return quest; }
            set { quest = value; base.OnChanged(); }
        }

        public List<Question> Questions { get; set; }

        public InfoAboutQuestViewModel(IInfoAboutQuestView view, Quest quest, List<Question> questions) {
            this.Questions = questions;
            this.Quest = quest;
            this.View = view;
            this.View.BindDataContext(this);
        }
    }
}