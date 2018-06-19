namespace Musarium.Interfaces {
    public interface IPrizeShowView {
        void BindDataContext(IPrizeShowViewModel viewModel);
        void Show();
        void Hide();
    }
}