using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CustomScripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
		public static ScoreManager Instance { get; private set; }
		[SerializeField] private GameObject p1;
		[SerializeField] private GameObject p2;

		private TextMeshPro p1Text;
		private TextMeshPro p2Text;
		private int p1Score;
		private int p2Score;


		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this;

			p1 = p1.GetComponent<GameObject>();
			p2 = p2.GetComponent<GameObject>();
			p1Text = p1.GetComponent<TextMeshPro>();
			p2Text = p2.GetComponent<TextMeshPro>();

		}

		public void addScore(int score, int playerID)
		{
			if (playerID == 1)
			{
				p1Score += score;
				p1Text.SetText(score.ToString());
			}


			else if (playerID == 2)
			{
				p2Score += score;
				p2Text.SetText(score.ToString());
			}


		}

	}
}
