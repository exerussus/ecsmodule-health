using Exerussus._1EasyEcs.Scripts.Core;

namespace ECS.Modules.Exerussus.Health
{
    public static class HealthData
    {
        public struct Health : IEcsComponent
        {
            public float Max;
            public float Current;
        }
        
        public struct HealthRegeneration : IEcsComponent
        {
            public float TimeRemaining;
            public float Rate;
            public float Amount;
        }
        
        public struct DeadMark : IEcsComponent
        {
            
        }
        
        public struct HealthRegenerationStopMark : IEcsComponent
        {
            
        }
    }
}