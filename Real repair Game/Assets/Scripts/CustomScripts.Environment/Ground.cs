using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Fundamentals;
using CustomScripts.Managers;

namespace CustomScripts.Environment
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private Vector2 mapSize;
        [SerializeField] private GameObject tilePrefab;
        public Node[,] grid;

        public static Ground Instance { get; private set; }
        private void Awake()
        {
            #region Singleton
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            #endregion
            var x = (int)mapSize.x;
            var y = (int)mapSize.y;
            this.grid = new Node[x, y];

            var tilesContainer = new GameObject("Tiles Container");
            tilesContainer.transform.position = Vector3.zero;

            GenerateNodes();

             void GenerateNodes()
             {
                for (int i = 0; i < this.mapSize.x; i++)
                {
                    for (int j = 0; j < this.mapSize.y; j++)
                    {
                        var fix = Vector3.up * 0.1f;
                        var center = new Vector3(i + 0.5f, 0, j + 0.5f);
                        var rotVector = new Vector3(90f, 0, 0);
                        var rotQuaternion = Quaternion.Euler(rotVector);
                        var tile = Instantiate(tilePrefab, center + fix, Quaternion.identity * rotQuaternion);
                        tile.transform.parent = tilesContainer.transform;

                        this.grid[i, j] = new Node(i, j, tile.GetComponent<Tile>());
                    }
                }
             }
        }

        public Node FromWorldToNode(Vector3 worldPos)
        {
            var vector2WorldPos = worldPos.ToGridWorldPos();
            
            int x = (int)Mathf.Floor(vector2WorldPos.x);
            int y = (int)Mathf.Floor(vector2WorldPos.y);
            return grid[x, y];
        }

        public bool ReachedDeadEnd(Node node)
        {
            var xLimit_left = 0;
            var xLimit_right = this.mapSize.x - 1;
            var yLimit_lower = 0;
            var yLimit_upper = this.mapSize.y - 1;

            var isDeadEnd =
                node.xCoord == xLimit_left ||
                node.xCoord == xLimit_right ||
                node.yCoord == yLimit_lower ||
                node.yCoord == yLimit_upper;

            return isDeadEnd;
        }
    }

    public enum Direction { NONE, RIGHT, BACK, LEFT, FORWARD }
    public enum TileType { NONE, EMPTY, TILE, OBSTACLE, OBJECTIVE } 
    public class Node
    {
        public int xCoord { get; }
        public int yCoord { get; }
        public Vector3 Center { get => new Vector3(xCoord + 0.5f, 0, yCoord + 0.5f); }
        public Tile Tile { get; set; }
        public Direction TurnTo { get; private set; }
        private Queue<Direction> directionQueue;

        public Node(int xCoord, int yCoord, Tile tile, Direction turnDirection = Direction.NONE)
        {
            this.xCoord = xCoord;
            this.yCoord = yCoord;
            this.TurnTo = turnDirection;
            this.Tile = tile;

            this.directionQueue = new Queue<Direction>();
            var directions = Enum.GetValues(typeof(Direction))
                .OfType<Direction>()
                .Where(d => d != Direction.NONE);
            foreach (var dir in directions)
                this.directionQueue.Enqueue(dir);
        }

        public void ChangeDirection()
        {
            var dir = this.directionQueue.Dequeue();
            this.TurnTo = dir;
            this.directionQueue.Enqueue(dir);
            //for test purpose
            Debug.Log(this.TurnTo);
        }
    }

    public static class NodeExtensions
    {
        public static bool EqualTo(this Node @this, Node node)
        {
            var xSame = @this.xCoord == node.xCoord;
            var ySame = @this.yCoord == node.yCoord;
            return xSame && ySame;
        }
    }
}
