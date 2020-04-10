using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class CanvasController : MonoBehaviour
    {
        List<ButtonController> buttonControllers;
        Queue<float> arrowUpDirection;
        int active = 0;
        private bool dequeueAuthorization;
        private bool enterPressed = false;

        float frameRate;
        float minTimeTransition = .1f;
        float releaseButtonTime = 0;

        void Awake()
        {
            buttonControllers = new List<ButtonController>();
            arrowUpDirection = new Queue<float>();
            var buttons = GetComponentsInChildren<ButtonController>();
            frameRate = Application.targetFrameRate;
            foreach (var button in buttons)
                buttonControllers.Add(button);
        }

        private void Update()
        {
            UpdateOption();
            ListenButtons();
            EnqueueArrows();
            RemoveHover();
            ApplyHover();
            VerifyEnterPress();
            ApplyCommand();
        }

        private void ApplyCommand()
        {
            if (active >= 0 && active < buttonControllers.Count && enterPressed)
                buttonControllers[active].ApplyCommand();
        }

        private void VerifyEnterPress()
        {
            enterPressed = Input.GetButtonDown("Submit") || Input.GetButtonDown("Fire1");
        }

        private void EnqueueArrows()
        {
            var vertical = Input.GetAxisRaw("Vertical") * -1.0f;
            if (releaseButtonTime <= 0.0f)
            {
                dequeueAuthorization = true;
                if (vertical != 0)
                {
                    releaseButtonTime = minTimeTransition; 
                    arrowUpDirection.Enqueue(vertical);
                }
            }
            else
            {
                if (releaseButtonTime > 0.0f) 
                {
                    releaseButtonTime -= 1/frameRate;
                }
                    
                dequeueAuthorization = false;
            }
        }

        private void RemoveHover()
        {
            foreach (ButtonController button in buttonControllers)
                button.selected = false;
        }

        private void ApplyHover()
        {
            if (active >= 0 && active < buttonControllers.Count)
                buttonControllers[active].selected = true;
        }

        private void ListenButtons()
        {
            for (int i = 0; i < buttonControllers.Count; ++i)
                if (buttonControllers[i].onHover)
                    active = i;
        }

        private void UpdateOption()
        {
            if (dequeueAuthorization) 
            {
                active = (active + buttonControllers.Count + (int)Math.Round(arrowUpDirection.Count > 0 ? arrowUpDirection.Dequeue() : 0.0)) % buttonControllers.Count;
            }
                
        }

        
    }
}