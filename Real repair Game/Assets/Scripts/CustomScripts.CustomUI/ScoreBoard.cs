using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CustomScripts.CustomUI
{
    public class ScoreBoard : MonoBehaviour
    {
        [SerializeField] private int _relatedPlayerID;
        public int RelatedPlayerID { get => _relatedPlayerID; }
        public TextMeshProUGUI ScoreText { get; private set; }
        public int Score { get; private set; }

        private void Start()
        {
            this.ScoreText = GetComponent<TextMeshProUGUI>();

            this.ScoreText.text = $"Player {this.RelatedPlayerID}\n0";
        }

        public void UpdateScore()
        {
            this.Score++;
            this.ScoreText.text = $"Player {this.RelatedPlayerID}\n{this.Score}";
        }
    }
}
