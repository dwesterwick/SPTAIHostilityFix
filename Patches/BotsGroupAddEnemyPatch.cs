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
    public class BotsGroupAddEnemyPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotsGroup).GetMethod("AddEnemy", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        protected static void PatchPostfix(BotOwner ___InitialBot, BotsGroup __instance, IPlayer person, EBotEnemyCause cause)
        {
            if (!SPTAIHostilityFixPlugin.ShowDebugAddEnemyMessages.Value)
            {
                return;
            }

            LoggingUtil.LogInfo(person.Profile.Nickname + " (" + person.GetType().FullName + ") is now an enemy of group containing " + ___InitialBot.Profile.Nickname + " due to reason: " + cause.ToString());
            StackTrace stackTrace = new StackTrace();
            LoggingUtil.LogInfo(stackTrace.ToString());

            __instance.LogAllianceInfo(___InitialBot);
        }
    }
}