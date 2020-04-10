using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PauseMenuController : MonoBehaviour
    {
        GameObject RubyInterface;
        GameObject PauseInterface;
        GameObject EnemyInterface;
        
        public bool IsPaused { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            RubyInterface = GameObject.Find("RubyPortait");
            PauseInterface = GameObject.Find("Pause");
            EnemyInterface = GameObject.Find("EnemyPortait");
            IsPaused = false;
        }

        // Update is called once per frame
        void Update()
        {
            ControlExhibitionStates();
            ActivatePauseState();
        }

        private void ActivatePauseState()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
                IsPaused = !IsPaused;
        }

        private void ControlExhibitionStates()
        {
            if (IsPaused)
                PauseGame();
            else
                ResumeGame();
        }



        private void ResumeGame()
        {
            Time.timeScale = 1.0f;
            RubyInterface.SetActive(true);
            EnemyInterface.SetActive(true);
            PauseInterface.SetActive(false);
        }

        private void PauseGame()
        {
            Time.timeScale = 0.0f;
            RubyInterface.SetActive(false);
            EnemyInterface.SetActive(false);
            PauseInterface.SetActive(true);
        }
    }
}