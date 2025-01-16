using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityModManagerNet;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.JsonSystem;
using EbonsUnfixerMod.Equipment;
using EbonsUnfixerMod.Feats;
using BlueprintCore.Blueprints.Configurators.Root;

namespace EbonsUnfixerMod;

#if DEBUG
[EnableReloading]
#endif
public static class Main {
    internal static Harmony HarmonyInstance;
    internal static UnityModManager.ModEntry.ModLogger log;

    public static bool Load(UnityModManager.ModEntry modEntry) {
        log = modEntry.Logger;
#if DEBUG
        modEntry.OnUnload = OnUnload;
#endif
        modEntry.OnGUI = OnGUI;
        HarmonyInstance = new Harmony(modEntry.Info.Id);
        HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        return true;
    }

    public static void OnGUI(UnityModManager.ModEntry modEntry) {

    }

#if DEBUG
    public static bool OnUnload(UnityModManager.ModEntry modEntry) {
        HarmonyInstance.UnpatchAll(modEntry.Info.Id);
        return true;
    }
#endif
    [HarmonyPatch(typeof(BlueprintsCache))]
    public static class BlueprintsCaches_Patch {
        private static bool Initialized = false;

        [HarmonyPriority(Priority.First)]
        [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
        public static void Init_Postfix() {
            try {
                if (Initialized) {
                    log.Log("Already initialized blueprints cache.");
                    return;
                }
                Initialized = true;

                log.Log("Patching blueprints.");
                
                BaneOfSpirit.Configure();
                ShatterDefenses.Configure();
            } catch (Exception e) {
                log.Log(string.Concat("Failed to initialize.", e));
            }
        }
    }

    [HarmonyPatch(typeof(StartGameLoader))]
    static class StartGameLoader_Patch
    {
        private static bool Initialized = false;

        [HarmonyPatch(nameof(StartGameLoader.LoadPackTOC)), HarmonyPostfix]
        static void LoadPackTOC()
        {
            try
            {
                if (Initialized)
                {
                    log.Log("Already configured delayed blueprints.");
                    return;
                }
                Initialized = true;

                RootConfigurator.ConfigureDelayedBlueprints();
            }
            catch (Exception e)
            {
                log.Log(string.Concat("Failed to configure delayed blueprints.", e));
            }
        }
    }
}
