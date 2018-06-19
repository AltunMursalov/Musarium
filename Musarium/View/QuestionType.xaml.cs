using Musarium.Interfaces;
using System.Windows;

namespace Musarium.View
{
    public partial class QuestionType : Window, IQuestionTypeView
    {
        public QuestionType()
        {
            InitializeComponent();

            Owner = Application.Current.MainWindow;
        }

        public void BindDataContext(IQuestionTypeViewModel viewModel)
        {
            this.DataContext = viewModel;
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void PackIcon_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            this.Hide();
        }
    }
} 