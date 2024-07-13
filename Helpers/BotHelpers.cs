using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            bool areUsecPals = (player.Profile.Info.Side == EPlayerSide.Usec) && bot.Profile.Info.Settings.Role.IsExUsec();
            areUsecPals |= (bot.Profile.Info.Side == EPlayerSide.Usec) && player.Profile.Info.Settings.Role.IsExUsec();

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
    }
}
