using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Environment;

using CustomScripts.Managers;

namespace CustomScripts.GameEntities
{
    public class Brick :MonoBehaviour
    {
        [SerializeField] private float speed;
        private Node currentNode;

        private void Start()
        {
            UpdateManager.Instance.GlobalUpdate += this.Move;
            UpdateManager.Instance.GlobalUpdate += this.CheckCurrentNode;
        }

        private void Move()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        private void CheckCurrentNode()
        {
            this.currentNode = Ground.Instance.FromWorldToNode(transform.position);
        }   
    }
}
