using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rewards
{
    public class RewardView : MonoBehaviour
    {
        private const string CurrentSlotInActiveKey = nameof(CurrentSlotInActiveKey);
        private const string TimeGetDailyRewardKey = nameof(TimeGetDailyRewardKey);
        private const string TimeGetWeeklyRewardKey = nameof(TimeGetWeeklyRewardKey);

        [Header("Settings Time Get Reward")] 
        [SerializeField] private float _timeDailyCooldown = 86400;
        [SerializeField] private float _timeWeeklyCooldown = 7;
        [SerializeField] private float _timeDailyDeadline = 88400;
        [SerializeField] private float _timeWeeklyDeadline = 7;
        [SerializeField] private Image _dailyTimeLine;
        [SerializeField] private Image _weeklyTimeLine;

        [Header("Settings Rewards")] 
        [SerializeField] private List<Reward> _rewards;

        [Header("Ui Elements")] 
        [SerializeField] private TMP_Text _timerNewDailyReward;
        [SerializeField] private TMP_Text _timerNewWeeklyReward;
        [SerializeField] private Transform _mountRootSlotsReward;
        [SerializeField] private ContainerSlotRewardView _containerSlotRewardView;
        [SerializeField] private Button _getRewardButton;
        [SerializeField] private Button _resetButton;

        public float TimeDailyCooldown => _timeDailyCooldown;

        public float TimeWeeklyCooldown => _timeWeeklyCooldown;

        public float TimeDailyDeadline => _timeDailyDeadline;

        public float TimeWeeklyDeadline => _timeWeeklyDeadline;

        public Image DailyTimeLine => _dailyTimeLine;

        public Image WeeklyTimeLine => _weeklyTimeLine;

        public List<Reward> Rewards => _rewards;

        public TMP_Text TimerNewDailyReward => _timerNewDailyReward;

        public TMP_Text TimerNewWeeklyReward => _timerNewWeeklyReward;

        public Transform MountRootSlotsReward => _mountRootSlotsReward;

        public ContainerSlotRewardView ContainerSlotRewardView => _containerSlotRewardView;

        public Button GetRewardButton => _getRewardButton;

        public Button ResetButton => _resetButton;

        public int CurrentSlotInActive
        {
            get => PlayerPrefs.GetInt(CurrentSlotInActiveKey, 0);
            set => PlayerPrefs.SetInt(CurrentSlotInActiveKey, value);
        }

        public DateTime? TimeGetDailyReward
        {
            get
            {
                var data = PlayerPrefs.GetString(TimeGetDailyRewardKey, null);

                if (!string.IsNullOrEmpty(data))
                    return DateTime.Parse(data);

                return null;
            }
            set
            {
                if (value != null)
                    PlayerPrefs.SetString(TimeGetDailyRewardKey, value.ToString());
                else
                    PlayerPrefs.DeleteKey(TimeGetDailyRewardKey);
            }
        }

        public float TimeGetWeeklyReward
        {
            get => PlayerPrefs.GetFloat(TimeGetWeeklyRewardKey, 0);
            set => PlayerPrefs.SetFloat(TimeGetWeeklyRewardKey, value);
        }

        private void OnDestroy()
        {
            _getRewardButton.onClick.RemoveAllListeners();
            _resetButton.onClick.RemoveAllListeners();
        }
    }
}
