﻿using System.Collections.Generic;
using System.Linq;
using EFT;

namespace SPTAIHostilityFix.Helpers
{
    public static class LoggingUtil
    {
        public static BepInEx.Logging.ManualLogSource Logger { get; set; } = null;

        public static string GetText(this IEnumerable<Player> players) => string.Join(",", players.Select(b => b?.GetText()));
        public static string GetText(this IEnumerable<IPlayer> players) => string.Join(",", players.Select(b => b?.GetText()));
        public static string GetText(this IEnumerable<BotOwner> bots) => string.Join(",", bots.Select(b => b?.GetText()));

        public static string GetText(this BotOwner bot)
        {
            if (bot == null)
            {
                return "[NULL BOT]";
            }

            return bot.GetPlayer.GetText();
        }

        public static string GetText(this Player player)
        {
            if (player == null)
            {
                return "[NULL BOT]";
            }

            return player.Profile.Nickname + " (Name: " + player.name + ", Side: " + player.Profile.Info.Side + ")";
        }

        public static string GetText(this IPlayer player)
        {
            if (player == null)
            {
                return "[NULL BOT]";
            }

            return player.Profile.Nickname + " (Name: ???, Side: " + player.Profile.Info.Side + ")";
        }

        public static void LogInfo(string message)
        {
            Logger.LogInfo(message);
        }

        public static void LogWarning(string message)
        {
            Logger.LogWarning(message);
        }

        public static void LogError(string message)
        {
            Logger.LogError(message);
        }
    }
}
