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
    internal class BotEnemiesControllerSetInfoPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotEnemiesController).GetMethod("SetInfo", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostfix(BotEnemiesController __instance, IPlayer enemy, EnemyInfo info)
        {
            LoggingUtil.LogInfo("Updating info about enemy " + enemy.Profile.Nickname);
            StackTrace stackTrace = new StackTrace();
            LoggingUtil.LogInfo(stackTrace.ToString());
        }
    }
}
