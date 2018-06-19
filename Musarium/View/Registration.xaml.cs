using Musarium.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace Musarium.View {
    public partial class Registration : Page, IRegistrationView {
        public Registration() {
            InitializeComponent();
        }

        public void BindDataContext(IRegistrationViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }
    }
}
 