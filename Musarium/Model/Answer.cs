using Musarium.Common;

namespace Musarium.Model {
    public class Answer : NotifyableObject {
        private int id;

        public int Id {
            get { return id; }
            set { id = value; base.OnChanged(); }
        }

        private string answer;

        public string QuestionAnswer {
            get { return answer; }
            set { answer = value; base.OnChanged(); }
        }

        private bool isRight;
         
        public bool IsRight {
            get { return isRight; }
            set { isRight = value; base.OnChanged(); }
        }

        private int questionId;

        public int QuestionID {
            get { return questionId; }
            set { questionId = value; base.OnChanged(); }
        }
    }
}
