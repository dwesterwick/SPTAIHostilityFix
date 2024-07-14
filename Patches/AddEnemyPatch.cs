using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EFT;
using SPT.Reflection.Patching;
using SPTAIHostilityFix.Helpers;

namespace SPTAIHostilityFix.Patches
{
    public class AddEnemyPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotsGroup).GetMethod("AddEnemy", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostfix(BotOwner ____initialBot, BotsGroup __instance, IPlayer person, EBotEnemyCause cause)
        {
            LoggingUtil.LogInfo(person.Profile.Nickname + " is now an enemy of group containing " + ____initialBot.Profile.Nickname + " due to reason: " + cause.ToString());
            StackTrace stackTrace = new StackTrace();
            LoggingUtil.LogInfo(stackTrace.ToString());

            LoggingUtil.LogInfo("Allies of group containing " + ____initialBot.Profile.Nickname + ": " + string.Join(", ", __instance.Allies.Select(a => a.Profile.Nickname)));
            LoggingUtil.LogInfo("Neutrals of group containing " + ____initialBot.Profile.Nickname + ": " + string.Join(", ", __instance.Neutrals.Select(a => a.Key.Profile.Nickname)));
            LoggingUtil.LogInfo("Enemies of group containing " + ____initialBot.Profile.Nickname + ": " + string.Join(", ", __instance.Neutrals.Select(a => a.Key.Profile.Nickname)));
        }
    }
}