/*
 * Copyright (C) 2024 Game4Freak.io
 * This mod is provided under the Game4Freak EULA.
 * Full legal terms can be found at https://game4freak.io/eula/
 */

using HarmonyLib;
using Oxide.Core.Plugins;

namespace Oxide.Plugins
{
    [Info("No Suicide Cooldown", "VisEntities", "1.0.1")]
    [Description("Removes the cooldown for the suicide command, enabling instant reuse.")]
    public class NoSuicideCooldown : RustPlugin
    {
        #region Fields

        private static NoSuicideCooldown _plugin;

        #endregion Fields

        #region Oxide Hooks

        private void Init()
        {
            _plugin = this;
        }

        private void Unload()
        {
            _plugin = null;
        }

        #endregion Oxide Hooks

        #region Harmony Patches

        [AutoPatch]
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