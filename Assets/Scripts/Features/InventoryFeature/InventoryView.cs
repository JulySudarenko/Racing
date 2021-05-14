using System;
using System.Collections.Generic;
using System.Linq;
using Company.Project.Features.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Company.Project.Features.Inventory
{
    public sealed class InventoryView : MonoBehaviour, IInventoryView
    {
        #region Fields

        public event Action OnShedExit;

        [SerializeField] private Button _buttonOilAdd;
        [SerializeField] private Button _buttonOilRemove;
        [SerializeField] private Button _buttonCannonAdd;
        [SerializeField] private Button _buttonCannonRemove;
        [SerializeField] private Button _buttonSpeedAccelerationAdd;
        [SerializeField] private Button _buttonSpeedAccelerationRemove;
        [SerializeField] private Button _buttonMainMenu;

        private List<IItem> _itemInfoCollection;

        #endregion
        
        private void ButtonOn()
        {
            string item = "none";
            foreach (var item1 in _itemInfoCollection.Where(item => item.Id == 1))
            {
                item = item1.Info.Title;
                Debug.Log(_itemInfoCollection.Count);
                OnSelected(item1);
            }
            Debug.Log($"Turn on {item}");
        }
        private void ButtonOff()
        {
            string item = "none";
            foreach (var item1 in _itemInfoCollection.Where(item => item.Id == 1))
            {
                item = item1.Info.Title;
                Debug.Log(_itemInfoCollection.Count);
                OnDeselected(item1);
            }
            Debug.Log($"Turn off {item}");
        }

        private void ExitShed()
        {
            OnShedExit?.Invoke();
        }

        #region IInventoryView

        public event EventHandler<IItem> Selected;
        public event EventHandler<IItem> Deselected;

        public void Show()
        {
            AddButtonsListeners();
        }

        
        public void Hide()
        {
            RemoveButtonsListeners();
        }
        
        private void AddButtonsListeners()
        {
            _buttonMainMenu.onClick.AddListener(ExitShed);
            _buttonSpeedAccelerationAdd.onClick.AddListener(ButtonOn);
            _buttonSpeedAccelerationRemove.onClick.AddListener(ButtonOff);
        }
        
        private void RemoveButtonsListeners()
        {
            _buttonMainMenu.onClick.RemoveAllListeners();
            _buttonSpeedAccelerationAdd.onClick.RemoveAllListeners();
            _buttonSpeedAccelerationRemove.onClick.RemoveAllListeners();
        }
       
        public void Display(List<IItem> itemInfoCollection)
        {
            _itemInfoCollection = itemInfoCollection;
            Debug.Log(_itemInfoCollection.Count);
        }

        private void OnSelected(IItem e)
        {
            Selected?.Invoke(this, e);
        }

        private void OnDeselected(IItem e)
        {
            Deselected?.Invoke(this, e);
        }

        #endregion
    }
}
