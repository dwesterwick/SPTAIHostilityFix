using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Configuration;
using SPTAIHostilityFix.Helpers;

namespace SPTAIHostilityFix
{
    [BepInPlugin("com.DanW.AIHostilityFix", "DanW-AIHostilityFix", "1.0.2")]
    public class SPTAIHostilityFixPlugin : BaseUnityPlugin
    {
        public static ConfigEntry<bool> EnableMod;
        public static ConfigEntry<bool> ShowDebugMessages;
        public static ConfigEntry<bool> ShowDebugAddEnemyMessages;

        private void Awake()
        {
            Logger.LogInfo("Loading AIHostilityFix...");

            LoggingUtil.Logger = Logger;
            new Patches.BotOwnerActivatePatch().Enable();
            new Patches.AddEnemyPatch().Enable();

            EnableMod = Config.Bind("Main", "Enabled", true, "Apply changes to new bot spawns");
            ShowDebugMessages = Config.Bind("Debug", "Show debug messages (Bot Spawns)", false, "Show additional debugging information when bots spawn");
            ShowDebugAddEnemyMessages = Config.Bind("Debug", "Show debug messages (Enemy Added)", false, "Show additional debugging information when players are added to bot enemy lists");

            Logger.LogInfo("Loading AIHostilityFix...done.");
        }
    }
}
