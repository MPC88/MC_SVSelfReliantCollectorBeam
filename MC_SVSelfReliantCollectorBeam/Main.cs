using BepInEx;
using HarmonyLib;

namespace MC_SVSelfReliantCollectorBeam
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string pluginGuid = "mc.starvalor.selfreliantcollectorbeam";
        public const string pluginName = "SV Self Reliant Collector Beam";
        public const string pluginVersion = "1.0.0";

        private const int collectorBeamID = 161;

        public void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(Main));
        }

        [HarmonyPatch(typeof(MenuControl), nameof(MenuControl.LoadGame))]
        [HarmonyPatch(typeof(GameData), nameof(GameData.CreateDefaultChar))]
        [HarmonyPostfix]
        private static void MenuControlLoadGame_Post()
        {
            if (PChar.Char != null && PChar.Char.HasPerk(324))
            {
                bool found = false;
                foreach (Blueprint bp in PChar.Char.blueprints)
                { 
                    if (bp.itemID == collectorBeamID && bp.itemType == 2)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    PChar.Char.AddBlueprint(2, collectorBeamID, 1f);
                    PChar.Char.SortBlueprints();
                }
            }
        }
    }
}
