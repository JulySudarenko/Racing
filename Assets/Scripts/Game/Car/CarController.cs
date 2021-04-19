using Tools;
using UnityEngine;

namespace Game
{
    internal sealed class CarController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/Car"};

        private CarView _view;
        private readonly SubscriptionProperty<float> _diff;
        private readonly IReadOnlySubscriptionProperty<float> _leftMove;
        private readonly IReadOnlySubscriptionProperty<float> _rightMove;

       
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

        private CarView LoadView()
        {
            GameObject objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
            AddGameObjects(objView);
            return objView.GetComponent<CarView>();
        }
        
        private void Move(float value)
        {
            _diff.Value = value;
        }
    } 
}

