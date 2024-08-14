using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comfort.Common;
using EFT;

namespace SPTAIHostilityFix.Helpers
{
    public static class BotHelpers
    {
        public static bool IsHostileWith(this BotOwner bot, IPlayer player)
        {
            if (bot.IsFriendlyWildSpawnType() || player.IsFriendlyWildSpawnType())
            {
                return false;
            }

            bool areUsecPals = (player.Profile.Info.Side == EPlayerSide.Usec) && bot.Profile.Info.Settings.Role.isAFriendlyUSECRole();
            areUsecPals |= (bot.Profile.Info.Side == EPlayerSide.Usec) && player.Profile.Info.Settings.Role.isAFriendlyUSECRole();

            if (areUsecPals)
            {
                LoggingUtil.LogInfo(player.GetText() + " is a USEC pal with " + bot.GetText());
                return false;
            }

            bool isPlayerAPMC = player.Profile.Info.Side != EPlayerSide.Savage;
            bool isBotAPMC = bot.Profile.Info.Side != EPlayerSide.Savage;

            return isPlayerAPMC || isBotAPMC || bot.BotsGroup.IsPlayerEnemy(player);
        }

        public static bool IsFriendlyWildSpawnType(this IPlayer player)
        {
            switch (player.Profile.Info.Settings.Role)
            {
                case WildSpawnType.bossZryachiy:
                case WildSpawnType.followerZryachiy:
                case WildSpawnType.peacefullZryachiyEvent:
                case WildSpawnType.gifter:
                case WildSpawnType.shooterBTR:
                    return true;
            }

            return false;
        }

        public static bool IsInTheSameGroupAs(this BotOwner bot, BotOwner otherBot)
        {
            for (int m = 0; m < bot.BotsGroup.MembersCount; m++)
            {
                if (bot.BotsGroup.Member(m) == otherBot)
                {
                    return true;
                }
            }

            return false;
        }

        public static BotOwner GetBotOwnerForPlayer(this IPlayer player)
        {
            IEnumerable<BotOwner> matchingOwners = Singleton<IBotGame>.Instance.BotsController.Bots.BotOwners
                .Where(b => b.Profile.Id == player.Profile.Id);

            if (matchingOwners.Count() == 1)
            {
                return matchingOwners.First();
            }

            return null;
        }

        private static bool isAFriendlyUSECRole(this WildSpawnType wildSpawnType)
        {
            //return wildSpawnType.IsExUsec();
            return wildSpawnType == WildSpawnType.exUsec;
        }
    }
}
