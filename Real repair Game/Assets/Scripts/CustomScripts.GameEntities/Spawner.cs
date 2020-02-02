using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomScripts.Environment;
using CustomScripts.Managers;

namespace CustomScripts.GameEntities
{
    public class Spawner : MonoBehaviour
    {
        public static List<Node> spawnNodes = new List<Node>();
        public Brick brickPrefab;
        [SerializeField] float spawnRate = 1f;

        private void Start()
        {
            StartCoroutine(this.Spawn());
            var spawnNode = Ground.Instance.FromWorldToNode(transform.position);
            Spawner.spawnNodes.Add(spawnNode);

            GameManager.Instance.GameWon += delegate { OnGameWon_StopSpawning(); };
        }

        private void OnTriggerExit(Collider other)
        {
            var brick = other.GetComponent<Brick>();
            if (brick)
                brick.IsImmnueToSpawner = true;
        }

        public IEnumerator Spawn()
        {
            var rotation = Quaternion.LookRotation(transform.forward);
            var brick = Instantiate(this.brickPrefab, transform.position, rotation) as Brick;
            yield return new WaitForSeconds(spawnRate);

            StartCoroutine(Spawn());
        }

        public void OnGameWon_StopSpawning()
        {
            StopAllCoroutines();
        }
    }
}
