
using ECS.Modules.Exerussus.Health.Systems;
using Exerussus._1EasyEcs.Scripts.Custom;
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.Health
{
    public class HealthGroup : EcsGroup<HealthPooler>
    {
        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems.Add(new HealthRegeneration());
        }
    }
}