using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private readonly List<RuneSlotView> _spawnedSlots = new();
        [SerializeField] private RuneSlotView _runeSlotPrefab;
        [SerializeField] private WeaponRuneSlotView _weaponSlotPrefab;
        [SerializeField] private Transform _runesContainer;
        [SerializeField] private Transform _weaponSlotsContainer;
        [SerializeField] private TextMeshProUGUI weaponName;

        
        private readonly List<WeaponRuneSlotView> _weaponSlotViews = new();
        
        
        private readonly CompositeDisposable _disposables = new();

        private void Start()
        {
            _btnCloseForgeScreen?.onClick.AddListener(CloseForgeScreenButtonClicked);
            _btnGain?.onClick.AddListener(GainItem);
            foreach (var runeType in ViewModel.RuneManager.UnlockedRunes)
            {
                CreateRuneSlot(runeType);
            }
            
            ViewModel.RuneManager.OnRuneUnlocked
                .Subscribe(CreateRuneSlot)
                .AddTo(_disposables);
            
            InitializeWeaponSlots(); 
        }
        
        private void CreateRuneSlot(RuneType type)
        {
            var data = ViewModel.RuneManager.GetRuneData(type);
            var slot = Instantiate(_runeSlotPrefab, _runesContainer);
            
            slot.Setup(data);
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(_runesContainer as RectTransform);
            _spawnedSlots.Add(slot);
        }
        
        
        private void OnDestroy()
        {
            _btnCloseForgeScreen?.onClick.RemoveListener(CloseForgeScreenButtonClicked);
            _disposables.Dispose();
        }

        private void GainItem()
        {
            var runesToSave = new List<RuneData>();

            foreach (var slotView in _weaponSlotViews)
                runesToSave.Add(slotView.ContainedRune);

            ViewModel.SaveRunesToWeapon(runesToSave);
    
            Debug.Log("Rune setup applied to weapon!");
        }
        
        private void CloseForgeScreenButtonClicked()
        {
            ViewModel.RequestGoToScreenGameplay();
        }
        
        private void InitializeWeaponSlots()
        {
            var slots = ViewModel.GetActiveWeaponSlots();
            if (slots == null || slots.Count == 0) return;
            weaponName.text = ViewModel.GetActiveWeaponName();
            foreach (var view in _weaponSlotViews) Destroy(view.gameObject);
            _weaponSlotViews.Clear();

            for (var i = 0; i < slots.Count; i++)
            {
                var slotView = Instantiate(_weaponSlotPrefab, _weaponSlotsContainer, false);
                    
                slotView.SetSlotType(slots[i].SlotType);
                
                var rectTransform = slotView.GetComponent<RectTransform>();
                rectTransform.localScale = Vector3.one;
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0f);

                if (!slots[i].IsEmpty)
                {
                    slotView.SetRune(slots[i].EquippedRune);
                }
        
                _weaponSlotViews.Add(slotView);
            }
        }
    }
}