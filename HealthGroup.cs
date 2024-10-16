﻿
using ECS.Modules.Exerussus.Health.Systems;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.Health
{
    public class HealthGroup : EcsGroup<HealthPooler>
    {
        protected override float TickSystemDelay { get; } = 0.5f;
        
        protected override void OnBeforePoolInitializing(EcsWorld world, HealthPooler pooler)
        {
            pooler.PreInitialize(Signal);
        }

        protected override void SetTickUpdateSystems(IEcsSystems tickUpdateSystems)
        {
            tickUpdateSystems.Add(new HealthRegeneration());
        }
    }
}