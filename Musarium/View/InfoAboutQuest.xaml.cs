using Musarium.Interfaces;
using System.Windows;

namespace Musarium.View {
    public partial class InfoAboutQuest : Window, IInfoAboutQuestView {
        public InfoAboutQuest() {
            InitializeComponent();
        }

        public void BindDataContext(IInfoAboutQuestViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }

        private void PackIcon_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            this.Hide();
        }
    }
}
 