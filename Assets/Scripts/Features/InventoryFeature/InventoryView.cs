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
        
        private void SpeedOn()
        {
            foreach (var item1 in _itemInfoCollection.Where(item => item.Id == 1))
            {
                OnSelected(item1);
            }
        }
        private void SpeedOff()
        {
            foreach (var item1 in _itemInfoCollection.Where(item => item.Id == 1))
            {
                OnDeselected(item1);
            }
        }
        private void BombOn()
        {
            foreach (var item1 in _itemInfoCollection.Where(item => item.Id == 2))
            {
                OnSelected(item1);
            }
        }
        private void BombOff()
        {
            foreach (var item1 in _itemInfoCollection.Where(item => item.Id == 2))
            {
                OnDeselected(item1);
            }
        }
       
        private void OilOn()
        {
            foreach (var item1 in _itemInfoCollection.Where(item => item.Id == 3))
            {
                OnSelected(item1);
            }
        }
        
        private void OilOff()
        {
            foreach (var item1 in _itemInfoCollection.Where(item => item.Id == 3))
            {
                OnDeselected(item1);
            }
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
            _buttonSpeedAccelerationAdd.onClick.AddListener(SpeedOn);
            _buttonSpeedAccelerationRemove.onClick.AddListener(SpeedOff);
            _buttonCannonAdd.onClick.AddListener(BombOn);
            _buttonCannonRemove.onClick.AddListener(BombOff);
            _buttonOilAdd.onClick.AddListener(OilOn);
            _buttonOilRemove.onClick.AddListener(OilOff);
        }
        
        private void RemoveButtonsListeners()
        {
            _buttonMainMenu.onClick.RemoveAllListeners();
            _buttonSpeedAccelerationAdd.onClick.RemoveAllListeners();
            _buttonSpeedAccelerationRemove.onClick.RemoveAllListeners();
            _buttonCannonAdd.onClick.RemoveAllListeners();
            _buttonCannonRemove.onClick.RemoveAllListeners();
            _buttonOilAdd.onClick.RemoveAllListeners();
            _buttonOilRemove.onClick.RemoveAllListeners();
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
