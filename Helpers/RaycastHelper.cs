using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Wilderness.Helpers
{
    public class RaycastHelper
    {
        public static bool RaycastFromSkyToPosition(ref Vector3 position)
        {
            position.y = 1024f;
            if (Physics.Raycast(position, Vector3.down, out var hitInfo, 2048f, RayMasks.WAYPOINT))
            {
                position = hitInfo.point + Vector3.up;
                return true;
            }
            return false;
        }
    }
}
