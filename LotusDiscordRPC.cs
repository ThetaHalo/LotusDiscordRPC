using Lotus.Addons;
using LotusDiscordRPC.Version;
using VentLib.Logging;
using HarmonyLib;

namespace LotusDiscordRPC;

public class LotusDiscordRPC: LotusAddon // welcome to my epic addon you lurker
{
    private static readonly StandardLogger log = LoggerFactory.GetLogger<StandardLogger>(typeof(LotusDiscordRPC));

    public override void Initialize()
    {
        Harmony harmony = new Harmony("lol.eps.customrpc");
        harmony.PatchAll();
        log.Info("Successfully loaded LotusDiscordRPC!");
    }

    public override string Name { get; } = "LotusDiscordRpc";

    public override VentLib.Version.Version Version { get; } = new LotusDiscordRPCVersion();
}


