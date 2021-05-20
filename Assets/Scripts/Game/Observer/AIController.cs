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
        }
    }
}
