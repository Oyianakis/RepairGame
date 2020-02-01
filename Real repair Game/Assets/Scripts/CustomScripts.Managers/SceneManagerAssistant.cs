using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CustomScripts.Managers
{

    public class SceneManagerAssistant : MonoBehaviour
    {

        public static SceneManagerAssistant Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Scene 1");
        }
    }
}
