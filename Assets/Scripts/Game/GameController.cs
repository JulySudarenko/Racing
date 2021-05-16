﻿using Company.Project.ContentData;
using Company.Project.Features.Abilities;
using Company.Project.Features.Inventory;
using Game.InputLogic;
using Game.TapeBackground;
using Profile;
using Tools;
using UnityEngine;

namespace Game
{
    internal sealed class GameController : BaseController
    {
        #region Life cycle

        public GameController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            SubscriptionProperty<float> leftMoveDiff = new SubscriptionProperty<float>();
            SubscriptionProperty<float> rightMoveDiff = new SubscriptionProperty<float>();
            
            TapeBackgroundController tapeBackgroundController =
                new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
            AddController(tapeBackgroundController);
             InputGameController inputGameController =
                 new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
            AddController(inputGameController);
            CarController carController = new CarController(leftMoveDiff, rightMoveDiff);
            AddController(carController);

            var abilityController = ConfigureAbilityController(placeForUi, carController, profilePlayer);
            abilityController.ShowAbilities();
        }

        #endregion

        #region Methods

        private IAbilitiesController ConfigureAbilityController(
            Transform placeForUi,
            IAbilityActivator abilityActivator, ProfilePlayer profilePlayer)
        {
            var abilityItemsConfigCollection
                = ContentDataSourceLoader.LoadAbilityItemConfigs(new ResourcePath
                    {PathResource = "DataSource/Ability/AbilityItemConfigDataSource"});
            var abilityRepository
                = new AbilityRepository(abilityItemsConfigCollection);
            var abilityCollectionViewPath
                = new ResourcePath {PathResource = $"Prefabs/{nameof(AbilityCollectionView)}"};
            var abilityCollectionView
                = ResourceLoader.LoadAndInstantiateObject<AbilityCollectionView>(abilityCollectionViewPath, placeForUi,
                    false);
            AddGameObjects(abilityCollectionView.gameObject);

            // загрузить в модель экипированные предметы можно любым способом
            //var inventoryModel = new InventoryModel();
            var abilitiesController = new AbilitiesController(abilityRepository, profilePlayer.InventoryModel, abilityCollectionView,
                abilityActivator);
            AddController(abilitiesController);

            return abilitiesController;
        }

        #endregion
    }
}
