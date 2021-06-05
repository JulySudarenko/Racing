using JetBrains.Annotations;
using Company.Project.Content;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Company.Project.Features.Abilities
{
    public class GunAbility : IAbility
    {
        #region Fields

        private readonly AbilityItemConfig _config;
        private Rigidbody2D _projectile;

        #endregion

        #region Life cycle

        public GunAbility([NotNull] AbilityItemConfig config)
        {
            _config = config;
        }

        #endregion

        #region IAbility

        public void Apply(IAbilityActivator activator)
        {
            _projectile = Object.Instantiate(_config.view).GetComponent<Rigidbody2D>();
            _projectile.AddForce(activator.GetViewObject().transform.right * _config.value, ForceMode2D.Force);
        }

        #endregion
    }
}