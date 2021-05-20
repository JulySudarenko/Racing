using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Observer
{
    internal class ObserverView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countMoneyText;
        [SerializeField] private TMP_Text _countHealthText;
        [SerializeField] private TMP_Text _countPowerText;
        [SerializeField] private TMP_Text _countCrimeRateText;
        [SerializeField] private TMP_Text _countPowerEnemyText;

        [SerializeField] private Button _addCoinsButton;
        [SerializeField] private Button _minusCoinsButton;
        [SerializeField] private Button _addHealthButton;
        [SerializeField] private Button _minusHealthButton;
        [SerializeField] private Button _addPowerButton;
        [SerializeField] private Button _minusPowerButton;
        [SerializeField] private Button _addCrimeRateButton;
        [SerializeField] private Button _minusCrimeRateButton;
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _skipButton;

        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountForcePlayer;
        private int _allCountCrimeRatePlayer;

        private Money _money;
        private Health _heath;
        private Force _force;
        private CrimeRate _crimeRate;

        private Enemy _enemy;

        public void Init()
        {
            _enemy = new Enemy("Enemy Flappy");

            _money = new Money(nameof(Money));
            _money.Attach(_enemy);

            _heath = new Health(nameof(Health));
            _heath.Attach(_enemy);

            _force = new Force(nameof(Force));
            _force.Attach(_enemy);

            _crimeRate = new CrimeRate(nameof(CrimeRate));
            _crimeRate.Attach(_enemy);

            //_addCoinsButton.onClick.AddListener(() => ChangeMoney(true));
            //_minusCoinsButton.onClick.AddListener(() => ChangeMoney(false));

            _addHealthButton.onClick.AddListener(() => ChangeHealth(true));
            _minusHealthButton.onClick.AddListener(() => ChangeHealth(false));

            _addPowerButton.onClick.AddListener(() => ChangeForce(true));
            _minusPowerButton.onClick.AddListener(() => ChangeForce(false));

            _addCrimeRateButton.onClick.AddListener(() => ChangeCrimeRate(true));
            _minusCrimeRateButton.onClick.AddListener(() => ChangeCrimeRate(false));

            _fightButton.onClick.AddListener(Fight);
            _skipButton.onClick.AddListener(Skip);
        }

        private void OnDestroy()
        {
            _addCoinsButton.onClick.RemoveAllListeners();
            _minusCoinsButton.onClick.RemoveAllListeners();

            _addHealthButton.onClick.RemoveAllListeners();
            _minusHealthButton.onClick.RemoveAllListeners();

            _addPowerButton.onClick.RemoveAllListeners();
            _minusPowerButton.onClick.RemoveAllListeners();

            _addCrimeRateButton.onClick.RemoveAllListeners();
            _minusCrimeRateButton.onClick.RemoveAllListeners();

            _fightButton.onClick.RemoveAllListeners();

            _money.Detach(_enemy);
            _heath.Detach(_enemy);
            _force.Detach(_enemy);
        }

        private void ChangeMoney(bool isAddCount)
        {
            if (isAddCount)
                _allCountMoneyPlayer++;
            else
                _allCountMoneyPlayer--;

            ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
        }

        private void ChangeHealth(bool isAddCount)
        {
            if (isAddCount)
                _allCountHealthPlayer++;
            else
                _allCountHealthPlayer--;

            ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
        }

        private void ChangeForce(bool isAddCount)
        {
            if (isAddCount)
                _allCountForcePlayer++;
            else
                _allCountForcePlayer--;

            ChangeDataWindow(_allCountForcePlayer, DataType.Force);
        }

        private void ChangeCrimeRate(bool isAddCount)
        {
            if (isAddCount)
            {
                _allCountCrimeRatePlayer++;
                if (_allCountCrimeRatePlayer > 5)
                    _allCountCrimeRatePlayer = 5;
            }
            else
            {
                _allCountCrimeRatePlayer--;
                if (_allCountCrimeRatePlayer < 0)
                    _allCountCrimeRatePlayer = 0;
            }


            ChangeDataWindow(_allCountCrimeRatePlayer, DataType.CrimeRate);
        }

        private void Skip()
        {
            if (_allCountCrimeRatePlayer < 3)
            {
                Debug.Log("<color=#07FF00>Passed through</color>");
            }
        }

        private void Fight()
        {
            Debug.Log(_allCountForcePlayer >= _enemy.Force
                ? "<color=#07FF00>Win!!!</color>"
                : "<color=#FF0000>Lose!!!</color>");
        }

        private void ChangeDataWindow(int countChangeData, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Money:
                    _countMoneyText.text = $"Player Money {countChangeData.ToString()}";
                    _money.Money = countChangeData;
                    break;

                case DataType.Health:
                    _countHealthText.text = $"Player Health {countChangeData.ToString()}";
                    _heath.Health = countChangeData;
                    break;

                case DataType.Force:
                    _countPowerText.text = $"Player Force {countChangeData.ToString()}";
                    _force.Force = countChangeData;
                    break;

                case DataType.CrimeRate:
                    _countCrimeRateText.text = $"Player Crime rate {countChangeData.ToString()}";
                    _crimeRate.CrimeRate = countChangeData;
                    break;
            }

            _countPowerEnemyText.text = $"Enemy Force {_enemy.Force}";
        }
    }
}
