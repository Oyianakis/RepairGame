using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;

namespace CustomScripts.GameEntities.PlayerSystem
{
    [RequireComponent(typeof(Player))]
    public class PlayerController : MonoBehaviour
    {
        private Player player;

        private void Awake()
        {
            this.player = GetComponent<Player>();
        }

        private void Start()
        {
            UpdateManager.Instance.GlobalUpdate += this.MovePlayer;
        }


        [SerializeField] private float speed = 5f;
        [SerializeField] private float turnSpeed = 5f;
        private void MovePlayer()
        {
            var horizontalVal = Mathf.Round(Input.GetAxisRaw($"Horizontal{this.player.playerID}"));
            var verticalVal = Mathf.Round(Input.GetAxisRaw($"Vertical{this.player.playerID}"));
            var compositeMovement = (Vector3.right * horizontalVal + Vector3.forward * verticalVal) * Time.deltaTime * this.speed;
            this.player.Position += compositeMovement;

            if (compositeMovement != Vector3.zero)
                Turn();

            void Turn()
            {
                var lookVector = compositeMovement;
                var targetRotation = Quaternion.LookRotation(lookVector);
                this.player.Rotation = Quaternion.Lerp(this.player.Rotation, targetRotation, Time.deltaTime * turnSpeed);
            }
        }
    }
}
