using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using EFT;
using HarmonyLib;
using SPTAIHostilityFix.Helpers;

namespace SPTAIHostilityFix
{
    [BepInPlugin("com.DanW.AIHostilityFix", "DanW-SPTAIHostilityFix", "1.0.0")]
    public class SPTAIHostilityFixPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("Loading AIHostilityFix...");

            LoggingUtil.Logger = Logger;

            new Patches.IsPlayerEnemyPatch().Enable();
            new Patches.AddEnemyPatch().Enable();

            Logger.LogInfo("Loading AIHostilityFix...done.");
        }

        private void fixBSGCrap()
        {
            FieldInfo botSettingsRepoField = AccessTools.Field(typeof(BotSettingsRepoAbstractClass), "dictionary_0");
            Dictionary<WildSpawnType, GClass696> botSettingsRepo = (Dictionary<WildSpawnType, GClass696>)botSettingsRepoField.GetValue(null);
            if (botSettingsRepo == null)
            {
                throw new TypeLoadException("Could not load Dictionary<WildSpawnType, GClass696> from BotSettingsRepoAbstractClass");
            }

            botSettingsRepo[WildSpawnType.pmcBEAR] = new GClass696(false, false, false, botSettingsRepo[WildSpawnType.pmcBEAR].ScavRoleKey, botSettingsRepo[WildSpawnType.pmcBEAR].PhraseTag);
            botSettingsRepo[WildSpawnType.pmcUSEC] = new GClass696(false, false, false, botSettingsRepo[WildSpawnType.pmcUSEC].ScavRoleKey, botSettingsRepo[WildSpawnType.pmcUSEC].PhraseTag);

            botSettingsRepoField.SetValue(null, botSettingsRepo);

            LoggingUtil.LogInfo("Updated BotSettingsRepoAbstractClass");
        }
    }
}
