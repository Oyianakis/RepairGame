﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using CustomScripts.Managers;

namespace CustomScripts.GameEntities
{
    [RequireComponent(typeof(Rigidbody))]
    public class Destination : MonoBehaviour
    {
        [SerializeField] private int relatedPlayerID;

        private int bricksReachedCount = 0;
        [SerializeField] private int winningThreshold = 10;

        private void OnTriggerEnter(Collider other)
        {
            var brick = other.GetComponent<Brick>();
            if (brick) {
                bricksReachedCount++;
                this.CheckWinCondition();
                UpdateManager.Instance.GlobalFixedUpdate -= brick.Move;
                Destroy(brick.gameObject);
            }
        }

        private void CheckWinCondition()
        {
            if (this.bricksReachedCount >= this.winningThreshold)
                GameManager.Instance?.OnGameWon(relatedPlayerID);
        }
    }
}
