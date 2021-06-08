using Company.Project.Content;
using JetBrains.Annotations;
using UnityEngine;

namespace Company.Project.Features.Abilities
{
    public class TankOilAbility : IAbility
    {
        #region Fields

        private readonly AbilityItemConfig _config;
        private GameObject _projectile;

        #endregion

        #region Life cycle

        public TankOilAbility([NotNull] AbilityItemConfig config)
        {
            _config = config;
        }

        #endregion

        #region IAbility

        public void Apply(IAbilityActivator activator)
        {
            _projectile = Object.Instantiate(_config.view);
            //_projectile.AddForce(activator.GetViewObject().transform.right * _config.value, ForceMode2D.Force);
        }

        #endregion
    }
}
