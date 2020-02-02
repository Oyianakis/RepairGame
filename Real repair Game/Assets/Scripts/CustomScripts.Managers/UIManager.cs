using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CustomScripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI winText;

        public static UIManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            GameManager.Instance.GameWon += this.OnGameWon_DisplayWinText;
        }

        public void OnGameWon_DisplayWinText(int playerID)
        {
            this.winText.text = $"Player {playerID} Won!";
            this.winText.gameObject.SetActive(true);
        }
    }
}
