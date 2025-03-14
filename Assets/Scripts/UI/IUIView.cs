namespace UI
{
    public interface IUIView
    {
        float ShowDuration { get; }
        float HideDuration { get; }
        void Show();
        void Hide();
    }
}