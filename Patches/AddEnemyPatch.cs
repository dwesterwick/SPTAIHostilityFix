using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EFT;
using HarmonyLib;
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
        private static void PatchPostfix(BotsGroup __instance, IPlayer person, EBotEnemyCause cause)
        {
            FieldInfo initialBotField = AccessTools.Field(typeof(BotsGroup), "_initialBot");
            BotOwner initialBot = (BotOwner)initialBotField.GetValue(__instance);

            LoggingUtil.LogInfo(person.Profile.Nickname + " is  now an enemy of group containing " + initialBot.Profile.Nickname + " due to reason: " + cause.ToString());
            StackTrace stackTrace = new StackTrace();
            LoggingUtil.LogInfo(stackTrace.ToString());

            LoggingUtil.LogInfo("Allies of group containing " + initialBot.Profile.Nickname + ": " + string.Join(", ", __instance.Allies.Select(a => a.Profile.Nickname)));
            LoggingUtil.LogInfo("Neutrals of group containing " + initialBot.Profile.Nickname + ": " + string.Join(", ", __instance.Neutrals.Select(a => a.Key.Profile.Nickname)));
            LoggingUtil.LogInfo("Enemies of group containing " + initialBot.Profile.Nickname + ": " + string.Join(", ", __instance.Neutrals.Select(a => a.Key.Profile.Nickname)));
        }
    }
}
