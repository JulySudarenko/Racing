using System.Collections.Generic;

namespace Game.Observer
{
    abstract class DataPlayer
    {
        private readonly List<IEnemy> _enemies = new List<IEnemy>();

        private readonly string _titleData;
        private int _countMoney;
        private int _countHealth;
        private int _countForce;

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

        protected void Notify(DataType dataType)
        {
            foreach (var investor in _enemies)
                investor.Update(this, dataType);
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
                    Notify(DataType.Money);
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
                    Notify(DataType.Health);
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
                    Notify(DataType.Force);
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
                    Notify(DataType.Force);
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
