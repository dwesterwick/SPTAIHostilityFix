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
    [BepInPlugin("com.DanW.AIHostilityFix", "DanW-AIHostilityFix", "1.0.1")]
    public class SPTAIHostilityFixPlugin : BaseUnityPlugin
    {
        public static ConfigEntry<bool> EnableMod;
        public static ConfigEntry<bool> ShowDebugMessages;

        private void Awake()
        {
            Logger.LogInfo("Loading AIHostilityFix...");

            LoggingUtil.Logger = Logger;
            new Patches.BotOwnerActivatePatch().Enable();
            //new Patches.AddEnemyPatch().Enable();

            EnableMod = Config.Bind("Main", "Enabled", true, "Apply changes to new bot spawns");
            ShowDebugMessages = Config.Bind("Main", "Show debug messages", false, "Show additional debugging information when bots spawn");

            Logger.LogInfo("Loading AIHostilityFix...done.");
        }
    }
}
