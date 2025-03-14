using UI.Popup;

namespace Managers
{
    public interface IPopupManager
    {
        void Initialize();
        void ShowPopup(string popupName, PopupInfo info = null);
        void HidePopup(string popupName);
    }
}