using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CommonReferences
{
    public static class Effects
    {
        public static EffectSO MoveRight;
        public static EffectSO MoveLeft;

        public static EffectSO MoveRandom;

        public static EffectSO Damage;
        public static EffectSO IndirectDamage;

        public static EffectSO ApplyScars;
        public static EffectSO ApplyFrail;
        public static EffectSO ApplyOilSlicked;
        public static EffectSO ApplyCursed;
        public static EffectSO ApplyFocused;
        public static EffectSO ApplyDivineProtection;
        public static EffectSO ApplyLinked;
        public static EffectSO ApplySpotlight;
        public static EffectSO ApplyRuptured;
        public static EffectSO ApplyGutted;
        public static EffectSO ApplyStunned;

        public static EffectSO ApplyConstricted;
        public static EffectSO ApplyFire;
        public static EffectSO ApplyShield;

        public static EffectSO VariableForNext;

        public static void Init()
        {
            MoveRight = CreateScriptable<SwapToOneSideEffect>(x => x._swapRight = true);
            MoveLeft = CreateScriptable<SwapToOneSideEffect>(x => x._swapRight = false);

            MoveRandom = CreateScriptable<SwapToSidesEffect>();

            Damage = CreateScriptable<DamageEffect>();
            IndirectDamage = CreateScriptable<DamageEffect>(x => x._indirect = true);

            ApplyScars = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Scars);
            ApplyFrail = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Frail);
            ApplyOilSlicked = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.OilSlicked);
            ApplyCursed = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Cursed);
            ApplyFocused = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Focused);
            ApplyDivineProtection = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.DivineProtection);
            ApplyLinked = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Linked);
            ApplySpotlight = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Spotlight);
            ApplyRuptured = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Ruptured);
            ApplyGutted = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Gutted);
            ApplyStunned = CreateScriptable<StatusEffect_Apply_Effect>(x => x._Status = Status.Stunned);

            ApplyConstricted = CreateScriptable<FieldEffect_Apply_Effect>(x => x._Field = Status.Constricted);
            ApplyFire = CreateScriptable<FieldEffect_Apply_Effect>(x => x._Field = Status.OnFire);
            ApplyShield = CreateScriptable<FieldEffect_Apply_Effect>(x => x._Field = Status.Shield);

            VariableForNext = CreateScriptable<ExtraVariableForNextEffect>();
        }

        public static EffectSO PlayVisuals(AttackVisualsSO visuals) => CreateScriptable<AnimationVisualsEffect>(x =>
        {
            x._visuals = visuals;
        });
    }
}
