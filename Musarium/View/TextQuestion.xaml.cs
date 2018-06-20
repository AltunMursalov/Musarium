using Musarium.Interfaces;
using System.Windows;
using System.Windows.Input;

namespace Musarium.View {
    public partial class TextQuestion : Window, ITextQuestionView {
        public TextQuestion() {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }

        public void BindDataContext(ITextQuestionViewModel viewModel) {
            DataContext = viewModel;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }
    }
}
 