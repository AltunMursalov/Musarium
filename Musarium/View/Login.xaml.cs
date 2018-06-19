using Musarium.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace Musarium.View {
    public partial class Login : Page, ILoginView {
        public Login() {
            InitializeComponent();
        }



        public void BindDataContext(ILogInViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }
    } 
}
