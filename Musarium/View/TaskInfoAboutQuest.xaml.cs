using Musarium.Interfaces;
using System;
using System.Windows.Controls;
using System.Windows;

namespace Musarium.View {
    public partial class TaskInfoAboutQuest : Page, ITaskInfoAboutQuestView {
        public TaskInfoAboutQuest() {
            InitializeComponent();
            this.Visibility = Visibility.Visible;
        }

        public void BindDataContext(ITaskInfoAboutQuestViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void Hide() {
            this.Visibility = Visibility.Collapsed;
        }

        public void Show() {
            this.Visibility = Visibility.Visible;
        }

        public void Clear() {
            this.QuestTitle.Clear();
            this.Description.Clear();
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }
    }
}
 