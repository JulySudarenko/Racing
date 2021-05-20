using System.Collections.Generic;
using UnityEditor;

namespace Game.Observer
{
    abstract class DataPlayer
    {
        private string _titleData;
        private int _countMoney;
        private int _countHealth;
        private int _countForce;

        private List<IEnemy> _enemies = new List<IEnemy>();

        protected DataPlayer(string titleData)
        {
            _titleData = titleData;
        }

        public void Attach(IEnemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void Detach(IEnemy enemy)
        {
            _enemies.Remove(enemy);
        }

        protected void Notify(LightingExplorerTableColumn.DataType dataType)
        {
            foreach (var investor in _enemies)
                investor.Update(this, (DataType) dataType);
        }

        public string TitleData => _titleData;

        public int Money
        {
            get => _countMoney;
            set
            {
                if (_countMoney != value)
                {
                    _countMoney = value;
                    Notify((LightingExplorerTableColumn.DataType) DataType.Money);
                }
            }
        }

        public int Health
        {
            get => _countHealth;
            set
            {
                if (_countHealth != value)
                {
                    _countHealth = value;
                    Notify((LightingExplorerTableColumn.DataType) DataType.Health);
                }
            }
        }

        public int Force
        {
            get => _countForce;
            set
            {
                if (_countForce != value)
                {
                    _countForce = value;
                    Notify((LightingExplorerTableColumn.DataType) DataType.Force);
                }
            }
        }
        
        public int CrimeRate
        {
            get => _countForce;
            set
            {
                if (_countForce != value)
                {
                    _countForce = value;
                    Notify((LightingExplorerTableColumn.DataType) DataType.Force);
                }
            }
        }
    }


    internal class Money : DataPlayer
    {
        public Money(string titleData)
            : base(titleData)
        {
        }
    }

    internal class Health : DataPlayer
    {
        public Health(string titleData)
            : base(titleData)
        {
        }
    }

    internal class Force : DataPlayer
    {
        public Force(string titleData)
            : base(titleData)
        {
        }
    }
    
    internal class CrimeRate : DataPlayer
    {
        public CrimeRate(string titleData)
            : base(titleData)
        {
        }
    }
}
