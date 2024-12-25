using HarmonyLib;
using Discord;
using Lotus;
using VentLib.Logging;
using InnerNet;
using Lotus.API.Odyssey;
using AmongUs.Data;

namespace LotusDiscordRPC.Patches;

[HarmonyPatch(typeof(ActivityManager), nameof(ActivityManager.UpdateActivity))]
public class DiscordSecondPatch
{
    private static readonly StandardLogger log = LoggerFactory.GetLogger<StandardLogger>(typeof(DiscordSecondPatch));
    private static string gameCode = "";
    private static string gameRegion = "";

    public static void Prefix(ref Activity activity)
    {
        var trueActivityState = "";
        var DiscordMessage = "Project: Lotus " + (ProjectLotus.DevVersion ? ProjectLotus.DevVersionStr : $"v{ProjectLotus.VisibleVersion}"); 
        activity.Details = DiscordMessage;
        if (DataManager.Settings.Gameplay.StreamerMode) return;

        if (activity.State is not "In Menus" and not "In Freeplay")
        {
            gameCode = GameCode.IntToGameName(AmongUsClient.Instance.GameId);
            gameRegion = Helper.GetCurrentRegionName(ServerManager.Instance.CurrentRegion.Name);    

            if (AmongUsClient.Instance.GameId == 32) return;
            if (gameCode != "" && gameRegion != "")
            {
                var stateMessage = Helper.getStateMessage(Game.State);
                if (Game.State == GameState.InIntro) stateMessage += $" • ({GameData.Instance.PlayerCount}/{GameManager.Instance.LogicOptions.MaxPlayers})"; // this doesn't update when a player leaves, oh well nothing i can do.
                DiscordMessage = "Lotus v" + (ProjectLotus.DevVersion ? $"{ProjectLotus.VisibleVersion}.{ProjectLotus.BuildNumber}" : "v" + ProjectLotus.VisibleVersion) + $" - ({gameCode}) | ({gameRegion})";
                trueActivityState = stateMessage;
            }
        }
        activity.Details = DiscordMessage;
        activity.State = trueActivityState;
    }
}