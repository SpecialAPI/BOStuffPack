using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.API.CustomEvent
{
    public interface IBooleanReferenceHolder
    {
        public BooleanReference GetBooleanReference();
    }

    public interface IIntegerReferenceHolder
    {
        public IntegerReference GetIntegerReference();
    }

    public interface ITargettedNotificationInfo
    {
        public IUnit Target { get; }
    }

    public abstract class BooleanReferenceHolderBase(BooleanReference reference) : IBooleanReferenceHolder
    {
        public BooleanReference reference = reference;

        public BooleanReference GetBooleanReference() => reference;
    }

    public abstract class IntegerReferenceHolderBase(IntegerReference reference) : IIntegerReferenceHolder
    {
        public IntegerReference reference = reference;

        public IntegerReference GetIntegerReference() => reference;
    }

    public class BooleanAndIntegerReferenceHolderBase(BooleanReference boolReference, IntegerReference intReference) : IBooleanReferenceHolder, IIntegerReferenceHolder
    {
        public BooleanReference boolReference = boolReference;
        public IntegerReference intReference = intReference;

        public BooleanReference GetBooleanReference() => boolReference;
        public IntegerReference GetIntegerReference() => intReference;
    }

    public class AbilityUsedContext(int abilityIndex, AbilitySO abilty, FilledManaCost[] cost)
    {
        public int abilityIndex = abilityIndex;
        public AbilitySO ability = abilty;
        public FilledManaCost[] cost = cost;
    }

    public class CanProducePigmentColorInfo(ManaColorSO pigment, BooleanReference reference) : BooleanReferenceHolderBase(reference)
    {
        public ManaColorSO pigment = pigment;
    }

    public class ModifyTargettingInfo(BooleanReference boolReference, IntegerReference intReference, AbilitySO ability) : BooleanAndIntegerReferenceHolderBase(boolReference, intReference)
    {
        public AbilitySO ability = ability;
    }

    public class TargettedStatusEffectApplicationInfo(IUnit guy, StatusEffect_SO statusEffect, int amountToApply) : ITargettedNotificationInfo
    {
        public IUnit unit = guy;
        public StatusEffect_SO statusEffect = statusEffect;
        public int amountToApply = amountToApply;

        public IUnit Target => unit;
    }
}
