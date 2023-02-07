using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wilderness.Models;

namespace Wilderness.Commands
{
    public class CommandSetWild : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "SetWild";

        public string Help => "Creates a new wilderness teleport point";

        public string Syntax => "<name>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("SetWild_MissingArgs"), Main.MessageColor);
                return;
            }

            UnturnedPlayer unturnedPlayer = (UnturnedPlayer)caller;

            WildNode node = new WildNode
            {
                Name = command[0],
                Position = new float[] { unturnedPlayer.Position.x, unturnedPlayer.Position.y, unturnedPlayer.Position.z }
            };

            if (System.IO.File.Exists("Plugins/Wilderness/WildPoints.json"))
            {
                string fileContents = System.IO.File.ReadAllText("Plugins/Wilderness/WildPoints.json");
                List<WildNode> nodes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WildNode>>(fileContents);
                nodes.Add(node);

                string updatedJson = "[" + string.Join(",", nodes.Select(r => Newtonsoft.Json.JsonConvert.SerializeObject(r))) + "]";
                System.IO.File.WriteAllText("Plugins/Wilderness/WildPoints.json", updatedJson);
                UnturnedChat.Say(caller, Main.Instance.Translate("SetWild_Success"), Main.MessageColor);
            }
            else
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(node);
                System.IO.File.WriteAllText("Plugins/Wilderness/WildPoints.json", "[" + json + "]");
                UnturnedChat.Say(caller, Main.Instance.Translate("SetWild_Success"), Main.MessageColor);
            }
        }
    }
}
