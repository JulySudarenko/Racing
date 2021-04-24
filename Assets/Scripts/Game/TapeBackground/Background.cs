using UnityEngine;

namespace Game.TapeBackground
{
    internal sealed class Background : MonoBehaviour
    {
        [SerializeField] private float _leftBorder;
        [SerializeField] private float _rightBorder;

        [SerializeField] private float _relativeSpeedRate;

        private Vector3 _position;

        public void Move(float value)
        {
            transform.position += Vector3.right * (value * _relativeSpeedRate);
            _position = transform.position;
            if (_position.x <= _leftBorder)
                transform.position = new Vector3(_rightBorder - (_leftBorder - _position.x), _position.y, _position.z);
            else if (transform.position.x >= _rightBorder)
                transform.position = new Vector3(_leftBorder - (_rightBorder - _position.x), _position.y, _position.z);
        }
    }
}
