using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.Health
{
    public static class HealthSignals
    {
        public struct CommandDealDamage
        {
            public EcsPackedEntity Origin;
            public EcsPackedEntity[] Targets;
            public float Amount;
        }
        
        public struct CommandDealHeal
        {
            public EcsPackedEntity Origin;
            public EcsPackedEntity[] Targets;
            public float Amount;
        }
        
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
        
        public struct OnEntityDead
        {
            public EcsPackedEntity KillerEntity;
            public EcsPackedEntity DeadEntity;
        }
    }
}