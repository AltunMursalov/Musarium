using Musarium.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace Musarium.View {
    public partial class CreateQuests : Page, ICreateQuestsView {
        public CreateQuests() {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
        }

        public void BindDataContext(ICreateQuestsViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void Hide() {
            this.Visibility = Visibility.Collapsed;
        }

        public void Show() {
            this.Visibility = Visibility.Visible;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }
    }
} 