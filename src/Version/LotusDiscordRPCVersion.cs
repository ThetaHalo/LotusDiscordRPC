using Hazel;

namespace LotusDiscordRPC.Version;


/// <summary>
/// Version Representing this Addon
/// </summary>
public class LotusDiscordRPCVersion: VentLib.Version.Version
{
    public override VentLib.Version.Version Read(MessageReader reader)
    {
        return new LotusDiscordRPCVersion();
    }

    protected override void WriteInfo(MessageWriter writer)
    {
    }

    public override string ToSimpleName()
    {
        return "LotusDiscordRpc v1.0.0";
    }

    public override string ToString() => "LotusDiscordRPC";
}