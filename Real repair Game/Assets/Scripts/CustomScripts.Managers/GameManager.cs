using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace CustomScripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action<int> GameWon;

        public static GameManager Instance{ get; private set;}

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void OnGameWon(int playerID)
        {
            this.GameWon?.Invoke(playerID);
        }
    }
}
