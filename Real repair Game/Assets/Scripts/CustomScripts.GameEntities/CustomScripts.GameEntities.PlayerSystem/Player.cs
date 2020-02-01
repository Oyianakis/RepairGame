using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Environment;
using CustomScripts.Managers;

namespace CustomScripts.GameEntities.PlayerSystem
{
    [RequireComponent(typeof(PlayerController))]
    public class Player : MonoBehaviour
    {
        public int playerID;
        public Vector3 Position { get => transform.position; set { transform.position = value; } }
        public Quaternion Rotation { get => transform.rotation; set { transform.rotation = value; } }

        private void Start()
        {
            UpdateManager.Instance.GlobalUpdate += this.CheckPlayerBehavior;   
        }

        public void CheckPlayerBehavior()
        {
            if (Input.GetButtonDown($"Mark {this.playerID}"))
                MarkTile();
            
        }

        private void MarkTile()
        {
            var node = Ground.Instance.FromWorldToNode(transform.position);
            if (node.Tile.Marked)
                return;
            node.Tile.Mark();
        }

        private void TurnTile()
		{
            return;
		}
    }
}
