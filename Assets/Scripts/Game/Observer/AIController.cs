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
        private int _allCountMoneyPlayer;
        private int _allCountHealthPlayer;
        private int _allCountForcePlayer;

        private Money _money;
        private Health _heath;
        private Force _force;

        private Enemy _enemy;

        public AIController(Transform placeForUi)
        {
            Init(placeForUi);
        }
        
        private void Init(Transform placeForUi)
        {
            _enemy = new Enemy("Enemy Flappy");

            var viewPath = new ResourcePath {PathResource = $"Prefabs/{nameof(ObserverView)}"};
            var view = ResourceLoader.LoadAndInstantiateObject<ObserverView>(viewPath, placeForUi, false);
            view.Init();
            
            _money = new Money(nameof(Money));
            _money.Attach(_enemy);

            _heath = new Health(nameof(Health));
            _heath.Attach(_enemy);

            _force = new Force(nameof(Force));
            _force.Attach(_enemy);
        }

        private void OnDestroy()
        {
            _money.Detach(_enemy);
            _heath.Detach(_enemy);
            _force.Detach(_enemy);
        }
    }
}
