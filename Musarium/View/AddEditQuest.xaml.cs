using Musarium.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace Musarium.View {
    public partial class AddEditQuest : Page, IAddEditView {
        public AddEditQuest() {
            InitializeComponent();
            this.Visibility = Visibility.Collapsed;
            this.Done.Visibility = Visibility.Collapsed;
        }

        public void BindDataContext(IAddEditViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void Hide() {
            this.Visibility = Visibility.Collapsed;
        }

        public void SetFramesContext(ITaskInfoAboutQuestView aboutQuestView, ICreateQuestsView createQuestsView, IPrizeShowView prizeShowView) {
            this.TaskQuestInfo.Content = aboutQuestView;
            this.QuestionTask.Content = createQuestsView;
            this.PrizeShow.Content = prizeShowView;
        }

        public void Show() {
            this.Visibility = Visibility.Visible;
        }

        public void ChangedButtonToDone() { 
            this.Next.Visibility = Visibility.Collapsed;
            this.Done.Visibility = Visibility.Visible;
        }

        public void ChangedButtonToNext() {
            this.Next.Visibility = Visibility.Visible;
            this.Done.Visibility = Visibility.Collapsed;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }
    }
}