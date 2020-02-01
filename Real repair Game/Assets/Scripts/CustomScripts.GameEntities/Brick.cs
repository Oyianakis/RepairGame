using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Environment;
using CustomScripts.Managers;
using CustomScripts.Fundamentals;

namespace CustomScripts.GameEntities
{
    public class Brick :MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float boostFactor;
        [SerializeField] private float boostDuration;
        private Node currentNode;
        private Direction currentDirection;

        private void Start()
        {
            UpdateManager.Instance.GlobalUpdate += this.Move;
            this.currentDirection = Direction.Straight;
        }

        private void Move()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            DetectNodeAndAct();

            void DetectNodeAndAct()
            {
                this.currentNode = Ground.Instance.FromWorldToNode(transform.position);
                Debug.Log($"{currentDirection}, {currentNode.TurnTo}");
                if (this.currentNode.TurnTo == Direction.None)
                    return;

                var atCenterOfANode = CanReactToDirection();
                if (!atCenterOfANode)
                    return;
                Debug.Log("at Center check");

                Debug.Log($"Current Dir: {currentDirection}\nNode TUrnto: {currentNode.TurnTo}");
                Debug.Log(this.currentDirection != this.currentNode.TurnTo);
                if (this.currentDirection != this.currentNode.TurnTo) {
                    Debug.Log("turn check");
                    var lookVector = this.FromDirectionToVector3(this.currentNode.TurnTo);
                    transform.rotation = Quaternion.LookRotation(lookVector);
                }
            }
        

            bool CanReactToDirection()
            {
                var thisToNodeCenterVector = (this.currentNode.Center - transform.position).Flatten();
                var sqrDistanceToNodeCenter = Vector3.SqrMagnitude(thisToNodeCenterVector);
                var withinRange = sqrDistanceToNodeCenter < 0.025f; //that is, distance is < 0.05f
                return withinRange;
            }
        }

        private Vector3 FromDirectionToVector3(Direction direction)
        {
            switch (direction)
            {
                case Direction.None:
                    return Vector3.zero;
                case Direction.Straight:
                    return Vector3.forward;
                case Direction.Right:
                    return Vector3.right;
                case Direction.Back:
                    return Vector3.back;
                case Direction.Left:
                    return Vector3.left;
                default:
                    throw new ArgumentException("Direction has to be one of those listed in the enum definition");
            }
        }
    }
}
