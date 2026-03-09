using System;
using Entity.Runes;
using Game.Inventory.Runes;
using Game.UI;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.View.UI.ScreenForge
{
    public class ScreenForgeBinder : WindowBinder<ScreenForgeViewModel>
    {
        [SerializeField] public Button _btnCloseForgeScreen;
        [SerializeField] public Button _btnCreatePage;
        [SerializeField] public Button _btnUpgradePage;
        [SerializeField] public Button _btnGain;
        
        
        [SerializeField] private RuneSlotView _runeSlotPrefab;
        [SerializeField] private Transform _runesContainer;
        
        private readonly CompositeDisposable _disposables = new();

        private void Start()
        {
            _btnCloseForgeScreen?.onClick.AddListener(CloseForgeScreenButtonClicked);
            
            foreach (var runeType in ViewModel.RuneManager.UnlockedRunes)
            {
                CreateRuneSlot(runeType);
            }
            
            ViewModel.RuneManager.OnRuneUnlocked
                .Subscribe(CreateRuneSlot)
                .AddTo(_disposables);
            
        }
        
        private void CreateRuneSlot(RuneType type)
        {
            var data = ViewModel.RuneManager.GetRuneData(type);
            var slot = Instantiate(_runeSlotPrefab, _runesContainer);
            
            slot.Setup(data, () => ViewModel.OnRuneSelected(type));
            LayoutRebuilder.ForceRebuildLayoutImmediate(_runesContainer as RectTransform);
        }
        
        private void OnDestroy()
        {
            _btnCloseForgeScreen?.onClick.RemoveListener(CloseForgeScreenButtonClicked);
            _disposables.Dispose();
        }
        
        private void CloseForgeScreenButtonClicked()
        {
            ViewModel.RequestGoToScreenGameplay();
        }
        
        
    }
}