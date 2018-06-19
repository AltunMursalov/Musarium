using Musarium.Interfaces;
using System.Windows.Controls;

namespace Musarium.View {
    public partial class PrizeShow : Page, IPrizeShowView {
        public PrizeShow() {
            InitializeComponent();
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void BindDataContext(IPrizeShowViewModel viewModel) {
            this.DataContext = viewModel;
        }

        public void Hide() {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        public void Show() {
            this.Visibility = System.Windows.Visibility.Visible;
        }
    }
}