using HarmonyLib;
using Discord;
using Lotus;
using VentLib.Logging;
using InnerNet;
using Lotus.API.Odyssey;
using AmongUs.Data;

namespace LotusDiscordRPC.Patches;

[HarmonyPatch(typeof(ActivityManager), nameof(ActivityManager.UpdateActivity))]
public class DiscordPresence
{
    private static readonly StandardLogger log = LoggerFactory.GetLogger<StandardLogger>(typeof(DiscordPresence));
    private static string _gameCode = "";
    private static string _gameRegion = "";

    public static void Prefix(ref Activity activity)
    {
        var trueActivityState = "";
        var discordMessage = "Project: Lotus " + (ProjectLotus.DevVersion ? ProjectLotus.DevVersionStr : $"v{ProjectLotus.VisibleVersion}"); 
        activity.Details = discordMessage;
        if (DataManager.Settings.Gameplay.StreamerMode) return;
        
        activity.Assets = new ActivityAssets
        {
            LargeImage = "https://avatars.githubusercontent.com/u/173427715?s=1400&v=4",
            LargeText = "Project: Lotus " + (ProjectLotus.DevVersion ? $"[DEV] v{ProjectLotus.VisibleVersion}.{ProjectLotus.BuildNumber}" : "v" + ProjectLotus.VisibleVersion),
            SmallImage = "icon",
            SmallText = $"Among Us",
        };

        if (activity.State is not "In Menus" and not "In Freeplay")
        {
            _gameCode = GameCode.IntToGameName(AmongUsClient.Instance.GameId);
            _gameRegion = RPCUtils.GetCurrentRegionName(ServerManager.Instance.CurrentRegion.Name);

            if (AmongUsClient.Instance.GameId == 32) return;
            if (_gameCode != "" && _gameRegion != "")
            {
                var stateMessage = RPCUtils.GetStateMessage(Game.State);
                if (Game.State == GameState.InIntro) stateMessage += $" • ({GameData.Instance.PlayerCount}/{GameManager.Instance.LogicOptions.MaxPlayers})";
                discordMessage = "Lotus v" + (ProjectLotus.DevVersion ? $"{ProjectLotus.VisibleVersion}.{ProjectLotus.BuildNumber}" : ProjectLotus.VisibleVersion) + $" - ({_gameCode}) | ({_gameRegion})";
                trueActivityState = stateMessage;
            }
        }
        activity.Details = discordMessage;
        activity.State = trueActivityState;
    }
}