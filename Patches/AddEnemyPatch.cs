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

        [PatchPrefix]
        private static bool PatchPrefix(BotsGroup __instance, IPlayer person, EBotEnemyCause cause)
        {
            if (!person.IsYourPlayer)
            {
                return true;
            }

            // Get the ID's of all group members
            List<BotOwner> groupMemberList = new List<BotOwner>();
            for (int m = 0; m < __instance.MembersCount; m++)
            {
                groupMemberList.Add(__instance.Member(m));
            }
            string[] groupMemberIDs = groupMemberList.Select(m => m.Profile.Nickname).ToArray();

            LoggingUtil.LogInfo("You are now an enemy of " + string.Join(", ", groupMemberIDs) + " due to reason: " + cause.ToString());
            StackTrace stackTrace = new StackTrace();
            LoggingUtil.LogInfo(stackTrace.ToString());

            return true;
        }
    }
}
