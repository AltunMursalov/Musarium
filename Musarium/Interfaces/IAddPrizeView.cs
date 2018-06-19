namespace Musarium.Interfaces {
    public interface IAddPrizeView {
        void BindDataContext(IAddPrizeViewModel viewModel);
        bool? ShowDialog();
        void Hide();
        void ShowAlert(string text, string caption);
    }
}
