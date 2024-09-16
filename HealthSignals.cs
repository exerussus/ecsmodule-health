using Leopotam.EcsLite;

namespace ECS.Modules.Exerussus.Health
{
    public static class HealthSignals
    {
        public struct CommandDealDamage
        {
            public EcsPackedEntity OriginEntity;
            public EcsPackedEntity TargetEntity;
            public float Amount;
        }
        
        public struct CommandDealHeal
        {
            public EcsPackedEntity OriginEntity;
            public EcsPackedEntity TargetEntity;
            public float Amount;
        }
        
        public struct OnDamageTaken
        {
            public EcsPackedEntity OriginEntity;
            public EcsPackedEntity TargetEntity;
            public float Amount;
        }
        
        public struct OnHealTaken
        {
            public EcsPackedEntity OriginEntity;
            public EcsPackedEntity TargetEntity;
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