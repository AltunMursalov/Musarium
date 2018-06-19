using Musarium.Interfaces;
using System.Threading.Tasks;
using System.Windows;

namespace Musarium.View {
    public partial class AddPrize : Window, IAddPrizeView {
        public AddPrize() {
            InitializeComponent();
        }

        public void BindDataContext(IAddPrizeViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void ShowAlert(string text, string caption) {
            MessageBox.Show(text, caption);
        }
    }
}