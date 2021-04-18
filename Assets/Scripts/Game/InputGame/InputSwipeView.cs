using JoostenProductions;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.InputLogic
{
    internal sealed class InputSwipeView : BaseInputView, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private float _threshold = 40;
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
            UpdateManager.UnsubscribeFromUpdate(MoveToLeft);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            float diff = eventData.position.x - _startPosition.x;
            if (Mathf.Abs(diff) >= _threshold)
            {
                if (diff > 0)
                    UpdateManager.SubscribeToUpdate(MoveToRight);

                else
                    UpdateManager.SubscribeToUpdate(MoveToLeft);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        private void MoveToRight()
        {
            OnRightMove(_speed * Time.deltaTime);
        }

        private void MoveToLeft()
        {
            OnLeftMove(-_speed * Time.deltaTime);

            if (_speed < 0)
                _speed = 0;
        }
    }
}
