using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomScripts.GameEntities
{
    public class Spawner : MonoBehaviour
    {
        public Brick brickPrefab;
        [SerializeField] float spawnRate = 1f;

        private void Start()
        {
            StartCoroutine(this.Spawn());
        }

        public IEnumerator Spawn()
        {
            var rotation = Quaternion.LookRotation(transform.forward);
            var brick = Instantiate(this.brickPrefab, transform.position, rotation) as Brick;
            yield return new WaitForSeconds(spawnRate);

            StartCoroutine(Spawn());
        }
    }
}
