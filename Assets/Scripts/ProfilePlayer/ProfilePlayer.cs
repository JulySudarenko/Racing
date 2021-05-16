using Company.Project.Features.Inventory;
using Company.Project.Features.Items;
using Profile.Analytic;
using Tools;

namespace Profile
{
    public sealed class ProfilePlayer
    {
        public SubscriptionProperty<GameState> CurrentState { get; }
        public Car CurrentCar { get; }
        
        public InventoryModel InventoryModel { get; private set; }
        public IAnalyticTools AnalyticTools { get; }
        
        public ProfilePlayer(float speedCar, IAnalyticTools analyticTools)
        {
            AnalyticTools = analyticTools;
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new Car(speedCar);
            InventoryModel = new InventoryModel();
        }

        public void AddEquipItem(InventoryModel inventoryModel)
        {
            InventoryModel = inventoryModel;
        }
    }
}

