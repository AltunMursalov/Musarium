using Musarium.Common;

namespace Musarium.Model {
    public class Quest : NotifyableObject {
        private int id;

        public int Id {
            get { return id; }
            set { id = value; base.OnChanged(); }
        }

        private string title;

        public string Title {
            get { return title; }
            set { title = value; base.OnChanged(); }
        }

        private string description;

        public string Description {
            get { return description; } 
            set { description = value; base.OnChanged(); }
        }

        private int difficult;

        public int Difficult {
            get { return difficult; }
            set { difficult = value; base.OnChanged(); }
        }

        private string pictureSrc;

        public string PictureSrc {
            get { return pictureSrc; }
            set { pictureSrc = value; base.OnChanged(); }
        }

        private int point;

        public int Point {
            get { return point; }
            set { point = value; base.OnChanged(); }
        }

        private int museumId;

        public int MuseumId {
            get { return museumId; }
            set { museumId = value; base.OnChanged(); }
        }

        private int questsCount;

        public int QuestionCounts {
            get { return questsCount; }
            set { questsCount = value; base.OnChanged(); }
        }


        private int prizeId;

        public int PrizeId {
            get { return prizeId; }
            set { prizeId = value; base.OnChanged(); }
        }

        private string difficultColor;

        public string DifficultColor {
            get {
                if (this.Difficult == 1) {
                    return "#1DE40F";
                } else if (this.Difficult == 2) {
                    return "#F3EF11";
                } else if (this.Difficult == 3) {
                    return "#DF0D0D";
                } else {
                    return "";
                }
            }
        }
    }
}