
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.Health
{
    public class HealthPooler : IGroupPooler
    {
        public void Initialize(EcsWorld world)
        {
            Health = new PoolerModule<HealthData.Health>(world);
            HealthRegeneration = new PoolerModule<HealthData.HealthRegeneration>(world);
            DeadMark = new PoolerModule<HealthData.DeadMark>(world);
            HealthRegenerationStopMark = new PoolerModule<HealthData.HealthRegenerationStopMark>(world);
        }
        
        public PoolerModule<HealthData.Health> Health { get; private set; }
        public PoolerModule<HealthData.HealthRegeneration> HealthRegeneration { get; private set; }
        public PoolerModule<HealthData.DeadMark> DeadMark { get; private set; }
        public PoolerModule<HealthData.HealthRegenerationStopMark> HealthRegenerationStopMark { get; private set; }
    }
}