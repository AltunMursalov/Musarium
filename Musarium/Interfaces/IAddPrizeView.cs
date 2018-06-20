namespace Musarium.Interfaces {
    public interface IAddPrizeView {
        void BindDataContext(IAddPrizeViewModel viewModel);
        bool? ShowDialog();
        void Hide();
        void Clear();
        void ShowAlert(string text, string caption);
    }
}
 