using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class EndController : MonoBehaviour
    {
        public float returnTime;

        float passedTime;
        // Start is called before the first frame update
        void Start()
        {
            passedTime = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            passedTime += Time.deltaTime;
            ReturnToMainMenu();
        }

        private void ReturnToMainMenu()
        {
            if (passedTime >= returnTime)
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
