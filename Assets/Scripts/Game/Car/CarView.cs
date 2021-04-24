using Tools;
using UnityEngine;

namespace Game
{
    internal sealed class CarView : MonoBehaviour
    {
        [SerializeField] private HingeJoint2D _backWheel;
        [SerializeField] private HingeJoint2D _forwardWheel;
        [SerializeField] private ParticleSystem _smoke;

        private IReadOnlySubscriptionProperty<float> _diff;
        private JointMotor2D _backWheelMotor;
        private JointMotor2D _forwardWheelMotor;
        private readonly float _coefficient = 300;
        private float _hightSpeedParam = 350;
        private float _speed = 0;

        public void Init(IReadOnlySubscriptionProperty<float> diff)
        {
            _diff = diff;
            _diff.SubscribeOnChange(Move);
            _backWheelMotor = _backWheel.motor;
            _forwardWheelMotor = _forwardWheel.motor;
        }

        private void OnDestroy()
        {
            _diff?.SubscribeOnChange(Move);
        }

        private void Move(float value)
        {
            _speed = _coefficient * value;
            // Debug.Log($"Car value = {value}");
            // Debug.Log($"Car speed {_speed}");

            _backWheelMotor.motorSpeed = _speed;
            _forwardWheelMotor.motorSpeed = _speed;
            _backWheel.motor = _backWheelMotor;
            _forwardWheel.motor = _forwardWheelMotor;

            SmokeChange(value);
        }

        private void SmokeChange(float value)
        {
            var emissionModule = _smoke.emission;
            var mainModule = _smoke.main;
            //Debug.Log(_hightSpeedParam * value);
            if (Mathf.Abs(_speed) >= _hightSpeedParam * value)
            {
                emissionModule.rateOverTime = 50;
                mainModule.startLifetime = 0.5f;
            }

            else
            {
                emissionModule.rateOverTime = 30;
                mainModule.startLifetime = 0.3f;
            }
        }
    }
}
