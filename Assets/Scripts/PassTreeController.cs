using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class PassTreeController : MonoBehaviour
    {
        private int numberOfBrokenEnemies = 0;
        private List<string> triggeredNames;

        public string nextLevel;

        // Start is called before the first frame update
        void Awake()
        {
            numberOfBrokenEnemies = 0;
            triggeredNames = new List<string>();
        }

        // Update is called once per frame
        void Update()
        {
            PassLevel();
        }

        private void PassLevel()
        {
            if (triggeredNames.Contains("Ruby") && numberOfBrokenEnemies == 0)
                SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
        }

        public void AddBrokenEnemy()
        {
            numberOfBrokenEnemies++;
        }

        public void RemoveBrokenEnemy()
        {
            numberOfBrokenEnemies--;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            triggeredNames.Add(collision.name);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            triggeredNames.Remove(collision.name);
        }
    }
}
