using CustomScripts.Environment;
using CustomScripts.Managers;
using UnityEngine;

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
                this.MarkTile();
            if (Input.GetButtonDown($"Remove {this.playerID}"))
                this.RemoveTile();
        }

        private void MarkTile()
        {
            var node = Ground.Instance.FromWorldToNode(transform.position);
            if (node.IsMarked) {
                node.Tile.Rotate();
                node.ChangeDirection();
            }
            else {
                node.Tile.MarkArrow();
                node.ChangeDirection();
            }
        }

        private void RemoveTile()
        {
            Debug.Log("remove");
            var node = Ground.Instance.FromWorldToNode(transform.position);
            if (node.IsMarked)
                node.RemoveSign();                
        }
    }
}
