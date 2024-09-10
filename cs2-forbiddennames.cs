using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using System.Text.Json.Serialization;

namespace cs2_forbiddennames;

public class SampleConfig : BasePluginConfig
    {
        [JsonPropertyName("Cmd")] public string Cmd { get; set; } = "css_ban {USERID} 0 xD";
        [JsonPropertyName("Nicknames")] public string[] Nicknames { get; set; } = {"phrase1", "phrase2"};
    }

public class cs2_forbiddennames : BasePlugin, IPluginConfig<SampleConfig>
{
    public override string ModuleName => "cs2-forbiddennames";
    public override string ModuleVersion => "0.0.1";
    public override string ModuleAuthor => "Letaryat";

    string Plugincmd;
    string[] Names;
    public SampleConfig Config { get; set; }

    public void OnConfigParsed(SampleConfig config)
    {
       Plugincmd = config.Cmd;
       Names = config.Nicknames;
    }
    public override void Load(bool hotReload){
        Console.WriteLine("Loaded: CS2-ForbiddenNames.");
    }

    [GameEventHandler]
    public HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
    {
        checkPlayer(@event.Userid!);
        return HookResult.Continue;
    }

    public void checkPlayer(CCSPlayerController player)
    {
        if(player.IsBot || player.IsHLTV)
        {
            return;
        }
        else
        {
            if(Names.Any(player.PlayerName.Contains))
            {
                Plugincmd = Plugincmd
                .Replace("{USERID}", "#"+player!.UserId!.Value.ToString());
                AddTimer(1.0f, () => {
                    Server.ExecuteCommand(Plugincmd);
                });
            }
            else
            {
                return;
            }
        }
    }
}

