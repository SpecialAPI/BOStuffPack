namespace BOStuffPack.DynamicAppearances
{
    public static class DynamicItemAppearanceTools
    {
        public static readonly Dictionary<BaseWearableSO, DynamicItemAppearanceBase> dynAppearances = [];

        public static T AttachDynamicAppearance<T>(this T w, DynamicItemAppearanceBase appearance) where T : BaseWearableSO
        {
            dynAppearances[w] = appearance;

            return w;
        }

        public static bool TryGetDynamicAppearance(this BaseWearableSO w, out DynamicItemAppearanceBase appearance)
        {
            if(dynAppearances.TryGetValue(w, out appearance))
                return true;

            appearance = null;
            return false;
        }
    }
}
