using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wilderness.Models;

namespace Wilderness.Commands
{
    public class CommandDeleteWild : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "DeleteWild";

        public string Help => "Removed specified teleport point";

        public string Syntax => "<point>";

        public List<string> Aliases => new List<string>() {"delwild"};

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length < 1)
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("DelWild_MissingArgs"), Main.MessageColor);
                return;
            }

            UnturnedPlayer unturnedPlayer = (UnturnedPlayer)caller;

            if (System.IO.File.Exists("Plugins/Wilderness/WildPoints.json"))
            {
                string fileContents = System.IO.File.ReadAllText("Plugins/Wilderness/WildPoints.json");
                List<WildNode> nodes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WildNode>>(fileContents);
                WildNode node = nodes.FirstOrDefault(n => n.Name == command[0]);

                nodes.Remove(node);

                if (node == null)
                {
                    UnturnedChat.Say(caller, Main.Instance.Translate("DelWild_Error"), Main.MessageColor);
                    return;
                }

                string updatedJson = "[" + string.Join(",", nodes.Select(r => Newtonsoft.Json.JsonConvert.SerializeObject(r))) + "]";
                System.IO.File.WriteAllText("Plugins/Wilderness/WildPoints.json", updatedJson);
                UnturnedChat.Say(caller, Main.Instance.Translate("DelWild_Success"), Main.MessageColor);
            }
            else
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("DelWild_Error"), Main.MessageColor);
            }
        }
    }
}
