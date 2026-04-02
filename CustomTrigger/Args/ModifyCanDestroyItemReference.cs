using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.CustomTrigger.Args
{
    public class ModifyCanDestroyItemReference(bool canDestroy, IUnit unit, BaseWearableSO item) : IBoolHolder, IUnitHolder
    {
        public bool canDestroy = canDestroy;
        public readonly IUnit unit = unit;
        public readonly BaseWearableSO item = item;

        bool IBoolHolder.Value
        {
            get => canDestroy;
            set => canDestroy = value;
        }
        bool IBoolHolder.this[int index]
        {
            get => canDestroy;
            set => canDestroy = value;
        }

        IUnit IUnitHolder.Value
        {
            get => unit;
            set => Debug.LogWarning($"DamageReceivedValueChangeExceptionHolder's Unit value is read-only.");
        }
        IUnit IUnitHolder.this[int index]
        {
            get => unit;
            set => Debug.LogWarning($"DamageReceivedValueChangeExceptionHolder's Unit value is read-only.");
        }
    }
}
