using Musarium.Common;
using System;

namespace Musarium.Model {
    public class Statistic : NotifyableObject{
        private int id;

        public int Id {
            get { return id; }
            set { id = value; base.OnChanged(); }
        }

        private int userId;

        public int UserId {
            get { return userId; }
            set { userId = value; base.OnChanged(); }
        }

        private int questId;

        public int QuestId {
            get { return questId; }
            set { questId = value; base.OnChanged(); }
        }

        private bool isComplete;

        public bool Iscomplete {
            get { return isComplete; }
            set { isComplete = value; base.OnChanged(); }
        }

        private int points;

        public int Points {
            get { return points; }
            set { points = value; base.OnChanged(); }
        }

        private DateTime duration;

        public DateTime Duration {
            get { return duration; }
            set { duration = value; base.OnChanged(); }
        }

        private DateTime dateTime;

        public DateTime DateTime {
            get { return dateTime; }
            set { dateTime = value; base.OnChanged(); }
        }

        private int prizeId;

        public int PrizeId {
            get { return prizeId; }
            set { prizeId = value; base.OnChanged(); }
        }
    }
}