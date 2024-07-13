using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Comfort.Common;
using EFT;
using SPT.Reflection.Patching;
using SPTAIHostilityFix.Helpers;

namespace SPTAIHostilityFix.Patches
{
    internal class BotOwnerCreatePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotOwner).GetMethod("Create", BindingFlags.Public | BindingFlags.Static);
        }

        [PatchPostfix]
        private static void PatchPostfix(BotOwner __result)
        {
            //BotsController botsController = Singleton<IBotGame>.Instance.BotsController;
            //botsController.BotSpawner.method_5(__result.GetPlayer);
        }

        public static void CheckAndSetEnemies(BotOwner bot)
        {
            List<Player> allPlayers = Singleton<GameWorld>.Instance.AllAlivePlayersList;
            foreach (Player player in allPlayers)
            {
                LoggingUtil.LogInfo("Checking if " + bot.GetText() + " and " + player.GetText() + " should be enemies...");

                bool isSameSide = player.Profile.Info.Side == bot.Profile.Info.Side;
                bool shouldBeEnemy = !isSameSide || player.BotsGroup.IsPlayerEnemy(bot.GetPlayer);
                if (shouldBeEnemy && player.BotsGroup.CheckAndAddEnemy(bot.GetPlayer))
                {
                    LoggingUtil.LogWarning(bot.GetText() + " is now an enemy of " + player.GetText());
                }

                shouldBeEnemy = !isSameSide || bot.GetPlayer.BotsGroup.IsPlayerEnemy(player);
                if (shouldBeEnemy && bot.GetPlayer.BotsGroup.CheckAndAddEnemy(player))
                {
                    LoggingUtil.LogWarning(player.GetText() + " is now an enemy of " + bot.GetText());
                }
            }
        }
    }
}
