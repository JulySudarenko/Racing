using Profile.Analytic;
using Tools;

namespace Profile
{
    internal sealed class ProfilePlayer
    {
        public SubscriptionProperty<GameState> CurrentState { get; }
        public Car CurrentCar { get; }
        public IAnalyticTools AnalyticTools { get; }
        
        public ProfilePlayer(float speedCar, IAnalyticTools analyticTools)
        {
            AnalyticTools = analyticTools;
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentCar = new Car(speedCar);
        }
    }
}

