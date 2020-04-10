using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string command;
        public bool onHover { get; private set; }
        public bool selected { get; set; }

        TextMeshProUGUI textContainer;

        // Start is called before the first frame update
        void Awake()
        {
            textContainer = GetComponentInChildren<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            ApplySelection();
        }

        private void ApplySelection()
        {
            if (selected)
                textContainer.color = new Color(214f / 255, 62f / 255, 72f / 255, 1);
            else
                textContainer.color = new Color(50f / 255, 50f / 255, 50f / 255, 1);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onHover = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onHover = false;
        }

        public void ApplyCommand() 
        {
            Type thisType = GetType();
            MethodInfo method = thisType.GetMethod(command);
            method.Invoke(this, null);
        }

        public void Play()
        {
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        }

        public void Tutorial() 
        {
            SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void Resume() 
        {
            gameObject.GetComponentInParent<PauseMenuController>().IsPaused = false;
        }
    }
}