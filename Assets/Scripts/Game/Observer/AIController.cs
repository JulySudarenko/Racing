using DoTween.Configs;
using Rewards;
using Tools;
using UnityEngine;

namespace Game.Observer
{
    public enum DataType
    {
        Money,
        Health,
        Force,
        CrimeRate
    }

    public class AIController : BaseController
    {
        public AIController(Transform placeForUi)
        {
            Init(placeForUi);
        }

        private void Init(Transform placeForUi)
        {
            var currencyViewPath = new ResourcePath {PathResource = $"Prefabs/{nameof(CurrencyView)}"};
            var currencyViewPrefab = Resources.Load<CurrencyView>(currencyViewPath.PathResource);
            var currencyView = Object.Instantiate(currencyViewPrefab);
            currencyView.Init();
            AddGameObjects(currencyView.gameObject);

            var aiViewPath = new ResourcePath {PathResource = $"Prefabs/{nameof(AIView)}"};
            var aiView = ResourceLoader.LoadAndInstantiateObject<AIView>(aiViewPath, placeForUi, false);
            aiView.Init(currencyView);
        }
    }
}
