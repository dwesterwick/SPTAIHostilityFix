using System;
using System.Collections.Generic;
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
    public class BotOwnerActivatePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotOwner).GetMethod("method_10", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPostfix]
        private static void PatchPostfix(BotOwner __instance)
        {
            if (!SPTAIHostilityFixPlugin.EnableMod.Value)
            {
                return;
            }

            LoggingUtil.LogInfo("Checking enemies of " + __instance.GetText() + "...");

            CheckAndSetAllEnemies(__instance);

            LoggingUtil.LogInfo(__instance.GetText() + " enemy checks complete");
        }

        public static void CheckAndSetAllEnemies(BotOwner newBot)
        {
            IEnumerable<Player> humanPlayers = Singleton<GameWorld>.Instance.AllAlivePlayersList
                .Where(p => !p.IsAI);
            
            foreach (Player humanPlayer in humanPlayers)
            {
                LoggingUtil.LogInfo("Checking if " + newBot.GetText() + " and " + humanPlayer.GetText() + " should be enemies...");
                checkAndSetEnemiesForBot(newBot, humanPlayer);
            }

            IEnumerable<BotOwner> activatedBots = Singleton<IBotGame>.Instance.BotsController.Bots.BotOwners
                .Where(b => b.BotState == EBotState.Active);

            foreach (BotOwner bot in activatedBots)
            {
                if (bot.Profile.Id == newBot.Profile.Id)
                {
                    continue;
                }

                if (newBot.IsInTheSameGroupAs(bot))
                {
                    LoggingUtil.LogInfo(newBot.GetText() + " and " + bot.GetText() + " are in the same group");
                    continue;
                }

                LoggingUtil.LogInfo("Checking if " + newBot.GetText() + " and " + bot.GetText() + " should be enemies...");
                checkAndSetEnemiesForBot(newBot, bot);
                checkAndSetEnemiesForBot(bot, newBot);
            }
        }

        private static void checkAndSetEnemiesForBot(BotOwner bot, IPlayer player)
        {
            if (bot.IsHostileWith(player) && bot.BotsGroup.CheckAndAddEnemy(player))
            {
                LoggingUtil.LogWarning(player.GetText() + " is now an enemy of " + bot.GetText());
            }
        }        
    }
}
