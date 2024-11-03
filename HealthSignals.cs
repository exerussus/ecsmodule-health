
using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.Health
{
    public static class HealthSignals
    {
        public struct OnDamageTaken
        {
            public EcsPackedEntity Origin;
            public EcsPackedEntity Target;
            public float Amount;
        }
        
        public struct OnHealTaken
        {
            public EcsPackedEntity Origin;
            public EcsPackedEntity Target;
            public float Amount;
        }
        
        public struct OnHealthChange
        {
            public EcsPackedEntity Entity;
            public float Amount;
        }
        
        public struct OnHealthRegenerationTick
        {
            public EcsPackedEntity Entity;
            public float Amount;
        }
        
        public struct OnEntityDead
        {
            public EcsPackedEntity KillerEntity;
            public EcsPackedEntity DeadEntity;
        }
    }
}