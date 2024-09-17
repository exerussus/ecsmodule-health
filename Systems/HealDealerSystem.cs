
using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.Health.Systems
{
    public class HealDealerSystem : EcsSignalListener<HealthPooler, HealthSignals.CommandDealHeal>
    {
        protected override void OnSignal(HealthSignals.CommandDealHeal data)
        {
            foreach (var target in data.Targets)
            {
                var hasTarget = target.Unpack(World, out var targetEntity);
                if (!hasTarget || Pooler.DeadMark.Has(targetEntity)) continue;

                if (!Pooler.Health.Has(targetEntity)) continue;
            
                ref var healthData = ref Pooler.Health.Get(targetEntity);

                var prev = healthData.Current;

                healthData.Current = Math.Min(healthData.Max, healthData.Current + data.Amount);
            
                var difHealth = healthData.Current - prev;
            
                Signal.RegistryRaise(new HealthSignals.OnHealTaken
                {
                    Origin = data.Origin,
                    Target = target,
                    Amount = difHealth
                });
            
                Signal.RegistryRaise(new HealthSignals.OnHealthChange
                {
                    Entity = target,
                    Amount = difHealth
                });
            }
        }
    }
}