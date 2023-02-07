using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wilderness
{
    public class Configuration : IRocketPluginConfiguration
    {
        public string MessageColor { get; set; }
        public void LoadDefaults()
        {
            MessageColor = "white";
        }
    }
}
