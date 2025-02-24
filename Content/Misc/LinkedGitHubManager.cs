using Steamworks;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOStuffPack.Content.Misc
{
    [HarmonyPatch]
    public static class LinkedGitHubManager
    {
        public static Regex GithubRegex = new(@"(https:\/\/github+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");
        public static bool GitHubCheckComplete;

        public static HashSet<string> ModsWithGithubLinks = [];
        public static HashSet<string> ModsWithoutGithubLinks = [];

        // TODO: check if github links are real
        [HarmonyPatch(typeof(ModdingDataBase), nameof(ModdingDataBase.PrepareModInformation))]
        [HarmonyPostfix]
        public static void Yeah()
        {
            if (GitHubCheckComplete)
                return;

            foreach (var minfo in LoadedDBsHandler.ModdingDB.m_LocatedMods.Values)
            {
                if (!minfo.hasDLL || !minfo.WasEnabled || minfo.id == ModsManager.BRUTALAPI_ID)
                    continue;

                if (GithubRegex.IsMatch(minfo.description))
                {
                    ModsWithGithubLinks.Add(minfo.id);
                    continue;
                }

                if (!minfo.workshopMod)
                {
                    ModsWithoutGithubLinks.Add(minfo.id);
                    continue;
                }

                var publishedFile = new PublishedFileId_t(minfo.workshopId);
                var ugcQuery = SteamUGC.CreateQueryUGCDetailsRequest([publishedFile], 1);

                SteamUGC.SetReturnLongDescription(ugcQuery, true);
                var call = SteamUGC.SendQueryUGCRequest(ugcQuery);

                var callResult = CallResult<SteamUGCQueryCompleted_t>.Create((x, f) =>
                {
                    SteamUGC.GetQueryUGCResult(x.m_handle, 0, out var dets);

                    if (GithubRegex.IsMatch(dets.m_rgchDescription))
                        ModsWithGithubLinks.Add(minfo.id);
                    else
                        ModsWithoutGithubLinks.Add(minfo.id);
                });
                callResult.Set(call);
            }

            GitHubCheckComplete = true;
        }
    }
}
