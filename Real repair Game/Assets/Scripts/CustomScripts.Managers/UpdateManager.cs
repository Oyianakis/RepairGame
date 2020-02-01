using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomScripts.Managers
{
    public class UpdateManager : MonoBehaviour
    {
        public event Action GlobalUpdate;
        public static UpdateManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Update()
        {
            GlobalUpdate?.Invoke();
        }
    }
}
