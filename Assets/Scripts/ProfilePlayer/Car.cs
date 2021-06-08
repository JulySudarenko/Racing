using Company.Project.Features.Shed;

namespace Profile
{
    public sealed class Car : IUpgradable
    {
        #region Properties

        public float Speed { get; set; }
        public float Force { get; set; }
        public int CrimeRate { get; set; }

        #endregion

        #region Fields

        private readonly float _defaultSpeed;
        private readonly float _defaultForce;
        private readonly int _defaultCrimeRate;

        #endregion

        #region Life cycle

        public Car(float speed, float force, int crimeRate)
        {
            _defaultSpeed = speed;
            _defaultForce = force;
            _defaultCrimeRate = crimeRate;
            Restore();
        }

        #endregion

        #region IUpgradable

        public void Restore()
        {
            Speed = _defaultSpeed;
            Force = _defaultForce;
            CrimeRate = _defaultCrimeRate;
        }

        #endregion
    }
}
