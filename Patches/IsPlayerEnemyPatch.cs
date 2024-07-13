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

            FieldInfo initialBotField = AccessTools.Field(typeof(BotsGroup), "_initialBot");
            BotOwner initialBot = (BotOwner)initialBotField.GetValue(__instance);

            LoggingUtil.LogInfo("Checking if " + player.Profile.Nickname + " (Side: " + player.Profile.Info.Side.ToString() + ") should be an enemy of group containing " + initialBot.Profile.Nickname + "...");
            StackTrace stackTrace = new StackTrace();
            LoggingUtil.LogInfo(stackTrace.ToString());

            if (player.Profile.Info.Side != EPlayerSide.Savage)
            {
                LoggingUtil.LogInfo("Checking if " + player.Profile.Nickname + " (Side: " + player.Profile.Info.Side.ToString() + ") should be an enemy of group containing " + initialBot.Profile.Nickname + "...Ignoring");
                return;
            }

            if ((initialBot.Profile.Info.Settings.Role == WildSpawnType.pmcBEAR) || (initialBot.Profile.Info.Settings.Role == WildSpawnType.pmcUSEC))
            {
                newResult = true;
            }

            if (newResult != __result)
            {
                LoggingUtil.LogWarning("Changing result of IsPlayerEnemy for " + player.Profile.Nickname + " from " + __result + " to " + newResult + " for group containing " + initialBot.Profile.Nickname);

                FieldInfo initialBotOfOtherGroupField = AccessTools.Field(typeof(BotsGroup), "_initialBot");
                BotOwner initialBotOfOtherGroup = (BotOwner)initialBotField.GetValue(player.GroupId);
            }
            else
            {
                LoggingUtil.LogInfo("Checking if " + player.Profile.Nickname + " (Side: " + player.Profile.Info.Side.ToString() + ") should be an enemy of group containing " + initialBot.Profile.Nickname + "..." + __result);
            }

            __result = newResult;
        }
    }
}
