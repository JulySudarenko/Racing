using JoostenProductions;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.InputLogic
{
    internal sealed class InputSwipeView : BaseInputView, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private readonly float _threshold = 40f;
        private readonly float _acceleration = 3.0f;
        private float _diff;
        private Vector2 _startPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = eventData.position;
        }

        public override void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove,
            float speed)
        {
            base.Init(leftMove, rightMove, speed);
            UpdateManager.SubscribeToUpdate(MoveToRight);
        }

        private void OnDestroy()
        {
            UpdateManager.UnsubscribeFromUpdate(MoveToRight);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _diff = eventData.position.x - _startPosition.x;
            if (Mathf.Abs(_diff) >= _threshold)
            {
                if (_diff > 0)
                {
                    _speed += _acceleration;
                }
                
                else if(_diff < 0)
                {
                    _speed -= _acceleration;
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        private void MoveToRight()
        {
            OnRightMove(_speed * Time.deltaTime);
        }
    }
}
