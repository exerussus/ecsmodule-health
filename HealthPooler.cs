
using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Modules.Exerussus.Health
{
    public class HealthPooler : IGroupPooler
    {
        public virtual void Initialize(EcsWorld world)
        {
            Health = new PoolerModule<HealthData.Health>(world);
            HealthRegeneration = new PoolerModule<HealthData.HealthRegeneration>(world);
            DeadMark = new PoolerModule<HealthData.DeadMark>(world);
            HealthRegenerationStopMark = new PoolerModule<HealthData.HealthRegenerationStopMark>(world);
        }
        
        [InjectSharedObject] public Signal Signal { get; private set; }
        [InjectSharedObject] public EcsWorld World { get; private set; }
        public PoolerModule<HealthData.Health> Health { get; private set; }
        public PoolerModule<HealthData.HealthRegeneration> HealthRegeneration { get; private set; }
        public PoolerModule<HealthData.DeadMark> DeadMark { get; private set; }
        public PoolerModule<HealthData.HealthRegenerationStopMark> HealthRegenerationStopMark { get; private set; }

        public void DealDamage(int originEntity, int[] targetEntities, float amount)
        {
            var origin = World.PackEntity(originEntity);
            
            foreach (var targetEntity in targetEntities)
            {
                if (DeadMark.Has(targetEntity)) continue;

                if (!Health.Has(targetEntity)) continue;
            
                ref var healthData = ref Health.Get(targetEntity);

                var prev = healthData.Current;

                healthData.Current = Math.Max(0, healthData.Current - amount);
            
                var difHealth = healthData.Current - prev;

                var target = World.PackEntity(targetEntity);
                
                Signal.RegistryRaise(new HealthSignals.OnDamageTaken
                {
                    Origin = origin,
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
                    DeadMark.Add(targetEntity);
                
                    Signal.RegistryRaise(new HealthSignals.OnEntityDead
                    {
                        KillerEntity = origin,
                        DeadEntity = target
                    });
                }
            }
        }
        
        public void DealDamage(int originEntity, int targetEntity, float amount)
        {
            if (DeadMark.Has(targetEntity)) return;
            if (!Health.Has(targetEntity)) return;
            
            ref var healthData = ref Health.Get(targetEntity);

            var prev = healthData.Current;

            healthData.Current = Math.Max(0, healthData.Current - amount);
            
            var difHealth = healthData.Current - prev;

            var origin = World.PackEntity(originEntity);
            var target = World.PackEntity(targetEntity);
                
            Signal.RegistryRaise(new HealthSignals.OnDamageTaken
            {
                Origin = origin,
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
                DeadMark.Add(targetEntity);
                
                Signal.RegistryRaise(new HealthSignals.OnEntityDead
                {
                    KillerEntity = origin,
                    DeadEntity = target
                });
            }
        }
        
        public void DealHeal(int originEntity, int[] targetEntities, float amount)
        {
            var origin = World.PackEntity(originEntity);
            
            foreach (var targetEntity in targetEntities)
            {
                if (DeadMark.Has(targetEntity)) continue;
                if (!Health.Has(targetEntity)) continue;
            
                ref var healthData = ref Health.Get(targetEntity);

                var prev = healthData.Current;

                healthData.Current = Math.Min(healthData.Max, healthData.Current + amount);
            
                var difHealth = healthData.Current - prev;

                var target = World.PackEntity(targetEntity);
                
                Signal.RegistryRaise(new HealthSignals.OnHealTaken
                {
                    Origin = origin,
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
        
        public void DealHeal(int originEntity, int targetEntity, float amount)
        {
            if (DeadMark.Has(targetEntity)) return;
            if (!Health.Has(targetEntity)) return;
            
            ref var healthData = ref Health.Get(targetEntity);

            var prev = healthData.Current;

            healthData.Current = Math.Min(healthData.Max, healthData.Current + amount);
            
            var difHealth = healthData.Current - prev;

            var origin = World.PackEntity(originEntity);
            var target = World.PackEntity(targetEntity);
                
            Signal.RegistryRaise(new HealthSignals.OnHealTaken
            {
                Origin = origin,
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