using JoostenProductions;
using UnityEngine;

namespace Game
{
    internal sealed class CarView : MonoBehaviour
    {
        [SerializeField] private HingeJoint2D _backWheel;
        [SerializeField] private HingeJoint2D _forwardWheel;
        [SerializeField] private ParticleSystem _smoke;

        private float _coefficient = 200;

        
        private void Move(float value)
        {
            var backWheelMotor = _backWheel.motor;
            backWheelMotor.motorSpeed = _coefficient * value;
            var forwardWheelMotor = _forwardWheel.motor;
            forwardWheelMotor.motorSpeed = _coefficient * value;
            Debug.Log(value);
        }
    }
}

