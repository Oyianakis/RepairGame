using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomScripts.GameEntities.PlayerSystem
{
    [RequireComponent(typeof(PlayerController))]
    public class Player : MonoBehaviour
    {
        public int playerID;
        public Vector3 Position { get => transform.position; set { transform.position = value; } }
        public Quaternion Rotation { get => transform.rotation; set { transform.rotation = value; } }
    }
}
