using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rewards
{
    public class RewardController : BaseController
    {
        private RewardView _rewardView;
        private List<ContainerSlotRewardView> _slots;

        private bool _isGetDailyReward;

        public RewardController()
        {
            var rewardPath = new ResourcePath {PathResource = $"Prefabs/{nameof(RewardView)}"};
            var reward = Resources.Load<RewardView>(rewardPath.PathResource);
            _rewardView = Object.Instantiate(reward);
            AddGameObjects(_rewardView.gameObject);

            var currencyViewPath = new ResourcePath {PathResource = $"Prefabs/{nameof(CurrencyView)}"};
            var currencyViewPrefab = Resources.Load<CurrencyView>(currencyViewPath.PathResource);
            var currencyView = Object.Instantiate(currencyViewPrefab);
            currencyView.Init();
            AddGameObjects(currencyView.gameObject);

            RefreshView();
        }

        private void RefreshView()
        {
            InitSlots();

            _rewardView.StartCoroutine(RewardsStateUpdater());

            RefreshUi();
            RefreshWeeklyRewardUi();
            SubscribeButtons();
        }

        private void InitSlots()
        {
            _slots = new List<ContainerSlotRewardView>();

            for (var i = 0; i < _rewardView.Rewards.Count; i++)
            {
                var instanceSlot = GameObject.Instantiate(_rewardView.ContainerSlotRewardView,
                    _rewardView.MountRootSlotsReward, false);

                _slots.Add(instanceSlot);
            }
        }

        private IEnumerator RewardsStateUpdater()
        {
            while (true)
            {
                RefreshDailyRewardsState();
                yield return new WaitForSeconds(1);
            }
        }

        private void RefreshDailyRewardsState()
        {
            _isGetDailyReward = true;

            if (_rewardView.TimeGetDailyReward.HasValue)
            {
                var timeSpan = DateTime.UtcNow - _rewardView.TimeGetDailyReward.Value;

                if (timeSpan.Seconds > _rewardView.TimeDailyDeadline)
                {
                    _rewardView.TimeGetDailyReward = null;
                    _rewardView.CurrentSlotInActive = 0;
                }
                else if (timeSpan.Seconds < _rewardView.TimeDailyCooldown)
                {
                    _isGetDailyReward = false;
                }
            }

            RefreshUi();
        }

        private void RefreshWeeklyRewardsState()
        {
            if (_rewardView.TimeGetWeeklyReward > _rewardView.TimeWeeklyDeadline)
            {
                _rewardView.TimeGetWeeklyReward = 0;
            }
            else if (_rewardView.TimeGetWeeklyReward == _rewardView.TimeWeeklyCooldown)
            {
                ClaimWeeklyReward();
                _rewardView.TimeGetWeeklyReward = 0;
            }

            RefreshWeeklyRewardUi();
        }


        private void RefreshUi()
        {
            _rewardView.GetRewardButton.interactable = _isGetDailyReward;

            if (_isGetDailyReward)
            {
                _rewardView.TimerNewDailyReward.text = "The reward is received today";
            }

            else
            {
                if (_rewardView.TimeGetDailyReward != null)
                {
                    var nextClaimTime = _rewardView.TimeGetDailyReward.Value.AddSeconds(_rewardView.TimeDailyCooldown);
                    var currentClaimCooldown = nextClaimTime - DateTime.UtcNow;
                    var timeGetReward =
                        $"{currentClaimCooldown.Days:D2}:{currentClaimCooldown.Hours:D2}:{currentClaimCooldown.Minutes:D2}:{currentClaimCooldown.Seconds:D2}";

                    _rewardView.TimerNewDailyReward.text = $"Time to get the next reward: {timeGetReward}";
                    _rewardView.DailyTimeLine.fillAmount =
                        (_rewardView.TimeDailyCooldown - currentClaimCooldown.Seconds) / _rewardView.TimeDailyCooldown;
                }
            }

            for (var i = 0; i < _slots.Count; i++)
                _slots[i].SetData(_rewardView.Rewards[i], i + 1, i == _rewardView.CurrentSlotInActive);

            RefreshWeeklyRewardUi();
        }

        private void RefreshWeeklyRewardUi()
        {
            if (_rewardView.TimerNewWeeklyReward != null)
            {
                var currentClaimCooldown = _rewardView.TimeWeeklyCooldown - _rewardView.TimeGetWeeklyReward;

                _rewardView.TimerNewWeeklyReward.text = $"To get the next reward days left {currentClaimCooldown}";
                _rewardView.WeeklyTimeLine.fillAmount = 
                    (_rewardView.TimeWeeklyCooldown - currentClaimCooldown) / _rewardView.TimeWeeklyCooldown;
            }
        }

        private void SubscribeButtons()
        {
            _rewardView.GetRewardButton.onClick.AddListener(ClaimReward);
            _rewardView.ResetButton.onClick.AddListener(ResetTimer);
        }

        private void ClaimReward()
        {
            if (!_isGetDailyReward)
                return;

            var reward = _rewardView.Rewards[_rewardView.CurrentSlotInActive];

            switch (reward.RewardType)
            {
                case RewardType.Wood:
                    CurrencyView.Instance.AddWood(reward.CountCurrency);
                    break;
                case RewardType.Diamond:
                    CurrencyView.Instance.AddDiamonds(reward.CountCurrency);
                    break;
            }

            _rewardView.TimeGetDailyReward = DateTime.UtcNow;
            _rewardView.CurrentSlotInActive =
                (_rewardView.CurrentSlotInActive + 1) % _rewardView.Rewards.Count;

            _rewardView.TimeGetWeeklyReward++;

            RefreshWeeklyRewardsState();
            RefreshDailyRewardsState();
        }

        private void ClaimWeeklyReward()
        {
            CurrencyView.Instance.AddWood(1000);
            CurrencyView.Instance.AddDiamonds(1000);
            _rewardView.TimeGetWeeklyReward = 0;
        }

        private void ResetTimer()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
