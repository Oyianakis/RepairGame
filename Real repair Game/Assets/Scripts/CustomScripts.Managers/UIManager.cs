using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CustomScripts.CustomUI;

namespace CustomScripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI winText;
        private ScoreBoard[] scoreboards;

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

            this.scoreboards = FindObjectsOfType<ScoreBoard>();
        }

        public void OnGameWon_DisplayWinText(int playerID)
        {
            this.winText.text = $"Player {playerID} Won!";
            this.winText.gameObject.SetActive(true);
        }

        public void UpdateScore(int relatedPlayerID)
        {
            var relevantScoreboard = this.scoreboards.First(s => s.RelatedPlayerID == relatedPlayerID);
            relevantScoreboard.UpdateScore();
        }
    }
}
