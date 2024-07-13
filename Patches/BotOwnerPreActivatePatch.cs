using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EFT;
using SPT.Reflection.Patching;
using SPTAIHostilityFix.Helpers;

namespace SPTAIHostilityFix.Patches
{
    public class BotOwnerPreActivatePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotOwner).GetMethod("PreActivate", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostfix(BotOwner __instance)
        {
            BotOwnerCreatePatch.CheckAndSetEnemies(__instance);

            LoggingUtil.LogInfo("Allies of " + __instance.Profile.Nickname + ": " + string.Join(", ", __instance.BotsGroup.Allies.Select(a => a.Profile.Nickname)));
            LoggingUtil.LogInfo("Neutrals of " + __instance.Profile.Nickname + ": " + string.Join(", ", __instance.BotsGroup.Neutrals.Select(a => a.Key.Profile.Nickname)));
            LoggingUtil.LogInfo("Enemies of " + __instance.Profile.Nickname + ": " + string.Join(", ", __instance.BotsGroup.Neutrals.Select(a => a.Key.Profile.Nickname)));
        }
    }
}
