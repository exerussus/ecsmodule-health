using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using UnityEngine;
using LogType = Exerussus._1EasyEcs.Scripts.Custom.LogType;

namespace ECS.Modules.Exerussus.Health.Systems
{
    public class HealthRegeneration : EasySystem<HealthPooler>
    {
        private EcsFilter _regenerationFilter;
        
        protected override void Initialize()
        {
            _regenerationFilter = World.Filter<HealthData.Health>().Inc<HealthData.HealthRegeneration>().Exc<HealthData.HealthRegenerationStopMark>().Exc<HealthData.DeadMark>().End();
        }

        protected override void Update()
        {
            foreach (var entity in _regenerationFilter)
            {
                ref var healthData = ref Pooler.Health.Get(entity);
                ref var regenerationData = ref Pooler.HealthRegeneration.Get(entity);

                regenerationData.TimeRemaining -= DeltaTime;
                
                if (regenerationData.TimeRemaining < 0)
                {
                    regenerationData.TimeRemaining = regenerationData.Rate;
                    
                    if (healthData.Current >= healthData.Max && regenerationData.Amount >= 0) continue;
                    
                    var prev = healthData.Current;
                    healthData.Current = Mathf.Min(healthData.Current + regenerationData.Amount, healthData.Max);

                    var packedEntity = World.PackEntity(entity);
                    var amount = healthData.Current - prev;
                    
                    if (healthData.Current < prev)
                    {
                        Signal.RegistryRaise(new HealthSignals.OnDamageTaken
                        {
                            Origin = packedEntity,
                            Target = packedEntity,
                            Amount = Mathf.Abs(amount)
                        });
            
                        if (healthData.Current <= 0)
                        {
                            healthData.Current = 0;
                            Pooler.DeadMark.Add(entity);
                
                            Signal.RegistryRaise(new HealthSignals.OnEntityDead
                            {
                                KillerEntity = packedEntity,
                                DeadEntity = packedEntity
                            });
                        }
                    }
                    
                    Signal.RegistryRaise(new HealthSignals.OnHealthChange
                    {
                        Entity = packedEntity,
                        Amount = amount
                    });
                }
            }
        }
    }
}