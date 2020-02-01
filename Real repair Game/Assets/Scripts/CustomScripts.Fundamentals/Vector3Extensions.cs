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

        public static Vector3 Flatten(this Vector3 @this) => new Vector3(@this.x, 0, @this.z);
    }
}
