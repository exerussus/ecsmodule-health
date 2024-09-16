Модуль для 1EasyEcs.   
Реализует механику здоровья, через дату:   


```csharp
public static class HealthData
{
    public struct Health : IEcsComponent
    {
        public float Max;
        public float Current;
    }

    public struct HealthRegeneration : IEcsComponent
    {
        public float TimeRemaining;
        public float Rate;
        public float Amount;
    }
    
    /// <summary> Накладывается при Health.Current == 0 </summary>
    public struct DeadMark : IEcsComponent
    {
        
    }
    
    /// <summary> Отключает регенерацию здоровья </summary>
    public struct HealthRegenerationStopMark : IEcsComponent
    {
        
    }
}
```

С сигналами:

```csharp
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
```

Основные зависимости:  
[Ecs-Lite](https://github.com/Leopotam/ecslite.git)  
[1EasyEcs](https://github.com/exerussus/1EasyEcs.git)