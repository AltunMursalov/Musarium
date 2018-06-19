using Musarium.Common;

namespace Musarium.Model {
    public class City : NotifyableObject {
        private int id;

        public int Id {
            get { return id; }
            set { id = value; base.OnChanged(); }
        }

        private string name;

        public string Name {
            get { return name; }
            set { name = value; base.OnChanged(); }
        }
    }
}
 