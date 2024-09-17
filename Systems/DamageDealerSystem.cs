
using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.Health.Systems
{
    public class DamageDealerSystem : EcsSignalListener<HealthPooler, HealthSignals.CommandDealDamage>
    {
        protected override void OnSignal(HealthSignals.CommandDealDamage data)
        {
            foreach (var target in data.Targets)
            {
                var hasTarget = data.Origin.Unpack(World, out var targetEntity);
                if (!hasTarget || Pooler.DeadMark.Has(targetEntity)) continue;

                if (!Pooler.Health.Has(targetEntity)) continue;
            
                ref var healthData = ref Pooler.Health.Get(targetEntity);

                var prev = healthData.Current;

                healthData.Current = Math.Max(0, healthData.Current - data.Amount);
            
                var difHealth = healthData.Current - prev;
            
                Signal.RegistryRaise(new HealthSignals.OnDamageTaken
                {
                    Origin = data.Origin,
                    Target = target,
                    Amount = Mathf.Abs(difHealth)
                });
            
                Signal.RegistryRaise(new HealthSignals.OnHealthChange
                {
                    Entity = target,
                    Amount = difHealth
                });
            
                if (healthData.Current == 0)
                {
                    Pooler.DeadMark.Add(targetEntity);
                
                    Signal.RegistryRaise(new HealthSignals.OnEntityDead
                    {
                        KillerEntity = data.Origin,
                        DeadEntity = target
                    });
                }
            }
        }
    }
}