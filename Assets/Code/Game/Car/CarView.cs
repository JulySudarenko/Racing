using Tools;
using UnityEngine;

namespace Game
{
    internal sealed class CarView : MonoBehaviour
    {
        [SerializeField] private WheelJoint2D _backWheel;
        [SerializeField] private WheelJoint2D _forwardWheel;
        [SerializeField] private ParticleSystem _smoke;

        private readonly float _coefficient = 2000.0f;
        private readonly float _hightSpeedParam = 120.0f;
        private readonly float _smokeLifeTimeStart = 0.3f;
        private readonly float _smokeLifeTimeDelta = 0.2f;
        private readonly float _smokeEmissionStart = 30.0f;
        private readonly float _smokeEmissionDelta = 20.0f;

        private IReadOnlySubscriptionProperty<float> _diff;
        private JointMotor2D _backWheelMotor;
        private JointMotor2D _forwardWheelMotor;
        private float _speed = 0.0f;

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
            _backWheelMotor.motorSpeed = -_speed;
            _forwardWheelMotor.motorSpeed = -_speed;
            _backWheel.motor = _backWheelMotor;
            _forwardWheel.motor = _forwardWheelMotor;

            SmokeChange();
        }

        private void SmokeChange()
        {
            var emissionModule = _smoke.emission;
            var mainModule = _smoke.main;
            if (_speed >= _hightSpeedParam)
            {
                emissionModule.rateOverTime = _smokeEmissionStart + _smokeEmissionDelta;
                mainModule.startLifetime = _smokeLifeTimeStart + _smokeLifeTimeDelta;
            }
            else if (_speed == 0)
            {
                emissionModule.rateOverTime = _smokeEmissionStart - _smokeEmissionDelta;
                mainModule.startLifetime = _smokeLifeTimeStart - _smokeLifeTimeDelta;
            }
            else
            {
                emissionModule.rateOverTime = _smokeEmissionStart;
                mainModule.startLifetime = _smokeLifeTimeStart;
            }
        }
    }
}
