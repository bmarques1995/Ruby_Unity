using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    class UINumberOfEnemiesController : MonoBehaviour
    {
        private int enemies = 0;

        TextMeshProUGUI textContainer;

        void Awake()
        {
            textContainer = GetComponentInChildren<TextMeshProUGUI>();
        }

        void Update()
        {
            textContainer.text = enemies.ToString();
        }

        public void AddBrokenEnemy() 
        {
            enemies++;
        }

        public void RemoveBrokenEnemy() 
        {
            enemies--;
        }
    }
}
