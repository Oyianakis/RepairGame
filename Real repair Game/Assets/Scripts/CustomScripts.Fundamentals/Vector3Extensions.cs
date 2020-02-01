using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomScripts.Fundamentals
{
    public static class Vector3Extensions
    {
        public static Vector2 ToGridWorldPos(this Vector3 @this) => new Vector2(@this.x, @this.z);
    }
}
