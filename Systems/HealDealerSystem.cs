using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.Health.Systems
{
    public class HealDealerSystem : EcsSignalListener<HealthPooler, HealthSignals.CommandDealHeal>
    {
        protected override void OnSignal(HealthSignals.CommandDealHeal data)
        {
            var hasTarget = data.OriginEntity.Unpack(World, out var targetEntity);
            if (!hasTarget || Pooler.DeadMark.Has(targetEntity)) return;

            if (!Pooler.Health.Has(targetEntity)) return;
            
            ref var healthData = ref Pooler.Health.Get(targetEntity);

            var prev = healthData.Current;

            healthData.Current = Math.Min(healthData.Max, healthData.Current + data.Amount);
            
            var difHealth = healthData.Current - prev;
            
            Signal.RegistryRaise(new HealthSignals.OnHealTaken
            {
                OriginEntity = data.OriginEntity,
                TargetEntity = data.TargetEntity,
                Amount = difHealth
            });
            
            Signal.RegistryRaise(new HealthSignals.OnHealthChange
            {
                Entity = data.TargetEntity,
                Amount = difHealth
            });
        }
    }
}