using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EFT;
using SPT.Reflection.Patching;
using SPTAIHostilityFix.Helpers;

namespace SPTAIHostilityFix.Patches
{
    public class IsPlayerEnemyPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotsGroup).GetMethod("IsPlayerEnemy", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostfix(ref bool __result, BotsGroup __instance, IPlayer player)
        {
            bool newResult = __result;

            if (player.Profile.Info.Side != EPlayerSide.Savage)
            {
                return;
            }

            if (__instance.HaveMemberWithRole(WildSpawnType.pmcBEAR) || __instance.HaveMemberWithRole(WildSpawnType.pmcUSEC))
            {
                newResult = true;
            }

            if (newResult != __result)
            {
                LoggingUtil.LogWarning("Changing result of IsPlayerEnemy for " + player.Profile.Nickname + " from " + __result + " to " + newResult);
            }

            __result = newResult;
        }
    }
}
