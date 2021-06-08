using Company.Project.Features.Abilities;
using Tools;
using UnityEngine;

namespace Game
{
    internal sealed class CarController : BaseController, IAbilityActivator
    {
        #region Fields

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/Car"};

        private CarView _view;
        private readonly SubscriptionProperty<float> _diff;
        private readonly IReadOnlySubscriptionProperty<float> _leftMove;
        private readonly IReadOnlySubscriptionProperty<float> _rightMove;

        #endregion


        #region Life cycle

        public CarController(IReadOnlySubscriptionProperty<float> leftMove,
            IReadOnlySubscriptionProperty<float> rightMove)
        {
            _view = LoadView();
            _diff = new SubscriptionProperty<float>();
            _leftMove = leftMove;
            _rightMove = rightMove;
            _view.Init(_diff);
            _leftMove.SubscribeOnChange(Move);
            _rightMove.SubscribeOnChange(Move);
        }

        protected override void OnDispose()
        {
            _leftMove.UnSubscriptionOnChange(Move);
            _rightMove.UnSubscriptionOnChange(Move);
            base.OnDispose();
        }

        #endregion

        #region Methods

        private CarView LoadView()
        {
            GameObject objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObjects(objView);
            return objView.GetComponent<CarView>();
        }

        #endregion

        private void Move(float value)
        {
            _diff.Value = value;
        }

        #region IAbilityActivator

        public GameObject GetViewObject()
        {
            return _view.gameObject;
        }

        #endregion
    }
}
