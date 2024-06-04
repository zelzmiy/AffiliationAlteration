using System.IO;
using COTL_API.CustomFollowerCommand;
using ForceFriendship.Commands;

namespace ForceFriendship
{
    [BepInPlugin(PluginGuid, PluginName, PluginVer)]
    [BepInDependency("io.github.xhayper.COTL_API")]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "xyz.zelzmiy.ForceFriendship";
        public const string PluginName = "ForceFriendship";
        public const string PluginVer = "1.0.0";

        internal static ManualLogSource Log;
        internal readonly static Harmony Harmony = new(PluginGuid);

        internal static string PluginPath;

        private void Awake()
        {
            Log = Logger;
            PluginPath = Path.GetDirectoryName(Info.Location);
            CustomFollowerCommandManager.Add(new SwoonCommand());
            CustomFollowerCommandManager.Add(new BefriendCommand());
            CustomFollowerCommandManager.Add(new EstrangeCommand());
            CustomFollowerCommandManager.Add(new DismayCommand());
        }

        private void OnEnable()
        {
            Harmony.PatchAll();
            LogInfo($"Loaded {PluginName}!");
        }

        private void OnDisable()
        {
            Harmony.UnpatchSelf();
            LogInfo($"Unloaded {PluginName}!");
        }

    }
}