using Musarium.Common;

namespace Musarium.Model {
    public class Prize : NotifyableObject {
        private int id;

        public int Id {
            get { return id; }
            set { id = value; base.OnChanged(); }
        }

        private string prizeName;

        public string PrizeName {
            get { return prizeName; }
            set { prizeName = value; base.OnChanged(); }
        }

        private string picSrc;

        public string PictureSrc {
            get { return picSrc; }
            set { picSrc = value; base.OnChanged(); }
        }
    } 
}
