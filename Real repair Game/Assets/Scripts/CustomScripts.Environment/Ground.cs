﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Fundamentals;

namespace CustomScripts.Environment
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private Vector2 mapSize;
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
            this.GenerateNodes();
        }

        private void GenerateNodes()
        {
            for (int i = 0; i < this.mapSize.x; i++)
            {
                for (int j = 0; j < this.mapSize.y; j++)
                {
                    this.grid[i, j] = new Node(i, j);
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
    }

    public enum Direction { None, Left, Right, Straight }
    public struct Node
    {
        public int xCoord { get; set; }
        public int yCoord { get; set; }
        public Direction TurnTo { get; set; }

        public Node(int xCoord, int yCoord, Direction turnDirection = Direction.None)
        {
            this.xCoord = xCoord;
            this.yCoord = yCoord;
            this.TurnTo = turnDirection;
        }
    }
}
