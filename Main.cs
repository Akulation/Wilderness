using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace Wilderness
{
    public class Main : RocketPlugin<Configuration>
    {
        public string PluginName = "Wilderness";
        public string PluginVersion = "v1.0.0";
        public static Main Instance { get; private set; }
        public static Color MessageColor { get; set; }
        protected override void Load()
        {
            Instance = this;
            MessageColor = UnturnedChat.GetColorFromName(Configuration.Instance.MessageColor, Color.white);
            Logger.Log($"{PluginName} {PluginVersion} has been successfully loaded!");
        }
        protected override void Unload()
        {
            Logger.Log($"{PluginName} {PluginVersion} has been successfully unloaded!");
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "Wild_Success", "You have been teleported to the wilderness!" },
            { "Wild_Error", "There are no wilderness points to teleport to!" },
            { "SetWild_Success", "Successfully added a new wilderness teleport point!" },
            { "SetWild_MissingArgs", "Missing arguments: /setwild <name>" },
            { "DelWild_Success", "Successfully removed this wilderness teleport point!" },
            { "DelWild_MissingArgs", "Missing arguments: /delwild <point_name>" },
            { "DelWild_Error", "This wilderness teleport point does not exist!" }
        };
    }
}
