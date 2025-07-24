using Lotus.API.Odyssey;

namespace LotusDiscordRPC;

public class Helper
{
    public static string GetMapName(int mapId)
    {
        return mapId switch
        {
            0 => "The Skeld",
            1 => "MIRA HQ",
            2 => "Polus",
            3 => "delkS ehT",
            4 => "Airship",
            5 => "The Fungle",
            6 => "Submerged",
            _ => "An Unknown Map"
        };
    }

    public static string GetCurrentRegionName(string region)
    { // there's a better way to do this, i'm just purely lazy.
        return region switch
        {
            "North America" => "NA",
            "Europe" => "EU",
            "Asia" => "AS",
            "Modded NA" => "MNA",
            "Modded EU" => "MEU",
            "Modded AS" => "MAS",
            "Modded NA (MNA)" => "MNA",
            "Modded EU (MEU)" => "MEU",
            "Modded AS (MAS)" => "MAS",
            _ => region
        };
    }

    public static string getStateMessage(GameState state)
    {
        string currentMap = GetMapName(GameManager.Instance.LogicOptions.MapId);
        return state switch
        { // the only time this will update is when InIntro and InLobby, the rest are there in case innersloth decides to be cool 🔥
            GameState.None => "Idle",
            GameState.InIntro => $"Roaming {currentMap}",
            GameState.InMeeting => "In Meeting",
            GameState.InLobby => "Waiting in Lobby",
            GameState.Roaming => $"Roaming {currentMap}",
            _ => "Idle",
        };
    }
}