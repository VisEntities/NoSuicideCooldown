/*
 * Copyright (C) 2024 Game4Freak.io
 * This mod is provided under the Game4Freak EULA.
 * Full legal terms can be found at https://game4freak.io/eula/
 */

using HarmonyLib;

namespace Oxide.Plugins
{
    [Info("No Suicide Cooldown", "VisEntities", "1.0.0")]
    [Description("Removes the cooldown for the suicide command, enabling instant reuse.")]
    public class NoSuicideCooldown : RustPlugin
    {
        #region Fields

        private static NoSuicideCooldown _plugin;
        private Harmony _harmony;

        #endregion Fields

        #region Oxide Hooks

        private void Init()
        {
            _plugin = this;
            _harmony = new Harmony(Name + "PATCH");
            _harmony.PatchAll();
        }

        private void Unload()
        {
            _harmony.UnpatchAll(Name + "PATCH");
            _plugin = null;
        }

        #endregion Oxide Hooks

        #region Harmony Patches

        [HarmonyPatch(typeof(BasePlayer), "MarkSuicide")]
        public static class BasePlayer_MarkSuicide_Patch
        {
            public static bool Prefix(BasePlayer __instance)
            {
                __instance.nextSuicideTime = UnityEngine.Time.realtimeSinceStartup;
                return false;
            }
        }

        #endregion Harmony Patches
    }
}