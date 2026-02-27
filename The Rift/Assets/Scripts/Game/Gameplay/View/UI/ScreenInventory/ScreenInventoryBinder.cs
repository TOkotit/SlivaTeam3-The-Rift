using System.Collections.Generic;
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
        
        
        [SerializeField] private List<GameObject> mainWeaponSlots;

        [SerializeField] private GameObject additionalWeaponSlot;
        
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