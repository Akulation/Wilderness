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
using UnityEngine;
using Wilderness.Helpers;
using Wilderness.Models;
using Random = System.Random;

namespace Wilderness.Commands
{
    public class CommandWild : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "Wild";

        public string Help => "Teleports you to a random location";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>();

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (!System.IO.File.Exists("Plugins/Wilderness/WildPoints.json"))
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("Wild_Error"), Main.MessageColor);
                return;
            }

            Random random = new Random();
            string fileContents = System.IO.File.ReadAllText("Plugins/Wilderness/WildPoints.json");
            List<WildNode> wildNodes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WildNode>>(fileContents);

            if (wildNodes.Count < 1)
            {
                UnturnedChat.Say(caller, Main.Instance.Translate("Wild_Error"));
                return;
            }

            WildNode node = wildNodes.ElementAt(random.Next(0, wildNodes.Count));
            Player player = PlayerTool.getPlayer((CSteamID)ulong.Parse(caller.Id));

            var pos = new Vector3(node.Position[0], node.Position[1], node.Position[2]);

            RaycastHelper.RaycastFromSkyToPosition(ref pos);
            player.teleportToLocation(pos,  player.transform.rotation.eulerAngles.y);
            UnturnedChat.Say(caller, Main.Instance.Translate("Wild_Success"), Main.MessageColor);
        }
    }
}
