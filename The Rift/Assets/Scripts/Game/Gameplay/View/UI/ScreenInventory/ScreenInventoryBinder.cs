using Game.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.View.UI.ScreenInventory
{
    public class ScreenInventoryBinder : WindowBinder<ScreenInventoryViewModel>
    {
        
        [SerializeField] private Button _btnClose;
        
        [SerializeField] private TextMeshProUGUI _healthText;
        
        private void Start()
        {
            _btnClose?.onClick.AddListener(OnCloseButtonClicked);
            

        }

        private void OnDestroy()
        {
            _btnClose?.onClick.RemoveListener(OnCloseButtonClicked);
        }


        
        private void OnCloseButtonClicked()
        {
            ViewModel.RequestClose();
        }
    }
}