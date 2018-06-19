using Musarium.Interfaces;
using System.Windows;

namespace Musarium.View {
    public partial class ManyAnswerQuestion : Window, IManyAnswerQuestionView {
        public ManyAnswerQuestion() {
            InitializeComponent();
        }

        public void BindDataContext(IManyAnswerQuestionViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }
    }
}