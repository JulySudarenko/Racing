using System;
using System.Collections.Generic;
using Company.Project.Features.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Company.Project.Features.Abilities
{
    public class AbilityCollectionView : MonoBehaviour, IAbilityCollectionView
    {
        #region Fields

        [SerializeField] private Button _buttonOil;
        [SerializeField] private Button _buttonBomb;

        private IReadOnlyList<IItem> _abilityItems;

        #endregion

        #region Methods

        protected virtual void OnUseRequested(IItem e)
        {
            UseRequested?.Invoke(this, e);
        }

        #endregion

        #region IAbilityCollectionView

        public event EventHandler<IItem> UseRequested;

        public void Display(IReadOnlyList<IItem> abilityItems)
        {
            _abilityItems = abilityItems;
        }

        public void Show()
        {
            _buttonBomb.onClick.AddListener(OnOilClick);
            // красиво показать какой-то объект
        }

        private void OnBombClick()
        {
            foreach (var item in _abilityItems)
            {
                if (item.Id == 2) 
                    OnUseRequested(item);
            }
        }
        
        private void OnOilClick()
        {
            foreach (var item in _abilityItems)
            {
                if (item.Id == 2) 
                    OnUseRequested(item);
            }
        }
        
        public void Hide()
        {
            // красиво спрятать какой-то объект
        }

        #endregion
    }
}
