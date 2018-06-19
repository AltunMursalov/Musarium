using Musarium.Common;

namespace Musarium.Model {
    public class Question : NotifyableObject {
        private int id;

        public int Id {
            get { return id; }
            set { id = value; base.OnChanged(); }
        }

        private string description;

        public string Description {
            get { return description; }
            set { description = value; base.OnChanged(); }
        }

        private string pictureSrc;

        public string PictureSrc {
            get { return pictureSrc; }
            set { pictureSrc = value; base.OnChanged(); }
        }

        private int points;

        public int Points {
            get { return points; }
            set { points = value; base.OnChanged(); }
        }

        private string hint;

        public string Hint {
            get { return hint; }
            set { hint = value; base.OnChanged(); }
        }

        private int questionType;

        public int QuestionType {
            get { return questionType; }
            set { questionType = value; base.OnChanged(); }
        }

        private int questId;

        public int QuestId {
            get { return questId; }
            set { questId = value; base.OnChanged(); }
        }

        private string answer;

        public string Answer {
            get { return answer; }
            set { answer = value; base.OnChanged(); }
        }
    }
}
