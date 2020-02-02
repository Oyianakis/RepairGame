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
            this.currentDirection = Direction.FORWARD;
        }

        public bool IsImmnueToSpawner { get; set; }
        private void Move()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
	    
	    //Get CurrentNode
            this.currentNode = Ground.Instance.FromWorldToNode(transform.position);

	    if (this.currentNode.TileType == TileType.OBSTACLE) 
	    {
 	      this.ReverseDirection();
            }

	    if (this.currentNode.TileType == TileType.OBJECTIVE)
	    {
		//SCORE POINTS
	    }

            if (CheckDeadEnd())
                return;
            DetectNodeAndAct();

            bool CheckDeadEnd()
            {
                var isAtCenter = CanReactToDirection();
                var isDeadEnd = Ground.Instance.ReachedDeadEnd(this.currentNode);
                var isSpawnNode = Spawner.spawnNodes.Contains(this.currentNode);

                var checkDeadEndSucceed = isAtCenter && isDeadEnd && (!isSpawnNode || this.IsImmnueToSpawner);
                if (checkDeadEndSucceed)
                    this.ReverseDirection();

                return checkDeadEndSucceed;
            }

            void DetectNodeAndAct()
            {
                if (this.currentNode.TurnTo == Direction.NONE)
                    return;

                var atCenterOfANode = CanReactToDirection();
                if (!atCenterOfANode)
                    return;

                if (this.currentDirection != this.currentNode.TurnTo) {
                    var lookVector = this.FromDirectionToVector3(this.currentNode.TurnTo);
                    transform.rotation = Quaternion.LookRotation(lookVector);
                    this.currentDirection = this.currentNode.TurnTo;
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

        private void ReverseDirection()
        {
            var lookVector = -transform.forward;
            var targetRotation = Quaternion.LookRotation(lookVector);
            transform.rotation = targetRotation;
            this.currentDirection = this.GetOppositeDireciton(this.currentDirection);
        }

        private Direction GetOppositeDireciton(Direction direction)
        {
            switch (direction)
            {
                case Direction.NONE:
                    throw new ArgumentException("opposite direction of None is non-existent");
                case Direction.RIGHT:
                    return Direction.LEFT;
                case Direction.BACK:
                    return Direction.FORWARD;
                case Direction.LEFT:
                    return Direction.RIGHT;
                case Direction.FORWARD:
                    return Direction.BACK;
                default:
                    throw new ArgumentException();
            }
        }

        private Vector3 FromDirectionToVector3(Direction direction)
        {
            switch (direction)
            {
                case Direction.NONE:
                    return Vector3.zero;
                case Direction.FORWARD:
                    return Vector3.forward;
                case Direction.RIGHT:
                    return Vector3.right;
                case Direction.BACK:
                    return Vector3.back;
                case Direction.LEFT:
                    return Vector3.left;
                default:
                    throw new ArgumentException("Direction has to be one of those listed in the enum definition");
            }
        }
    }
}
