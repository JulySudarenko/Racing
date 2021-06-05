using UnityEngine;

namespace Game.Observer
{

    interface IEnemy
    {
        void Update(DataPlayer dataPlayer, DataType dataType);
    }

    class Enemy : IEnemy
    {
        private const int K_COINS = 5;
        private const float K_FORCE = 1.5f;
        private const int MAX_HEALTH_PLAYER = 20;
    
        private readonly string _name;
        private int _moneyPlayer;
        private int _healthPlayer;
        private int _forcePlayer;

        public Enemy(string name)
        {
            _name = name;
        }

        public void Update(DataPlayer dataPlayer, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Money:
                    _moneyPlayer = dataPlayer.Money;
                    break;
                
                case DataType.Health:
                    _healthPlayer = dataPlayer.Health;
                    break;
            
                case DataType.Force:
                    _forcePlayer = dataPlayer.Force;
                    break;
            }
        
            Debug.Log($"Notified {_name} change to {dataPlayer}");
        }

        public int Force
        {
            get
            {
                var kHealth = _healthPlayer > MAX_HEALTH_PLAYER ? 100 : 5;
                var force = (int) (_moneyPlayer / K_COINS + (kHealth + _forcePlayer) / K_FORCE);
            
                return force;
            }
        }
    }
}
