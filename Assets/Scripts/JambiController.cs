using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class JambiController : MonoBehaviour
    {
        List<string> triggeredNames;
        bool playerIsTriggering = false;
        bool canConstroyDialogBox = true;
        DialogueTrigger dialogueTrigger;
        GameObject dialogueBox;

        public GameObject dialogueObject;
        private bool isActive;


        // Start is called before the first frame update
        void Start()
        {
            triggeredNames = new List<string>();
            
        }

        // Update is called once per frame
        void Update()
        {
            PlayerIsTriggering();
            EnableDialogueBox();
            ActivityFunction();
        }

        private void ActivityFunction()
        {
            if (isActive)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        private void EnableDialogueBox()
        {
            if (playerIsTriggering && canConstroyDialogBox)
            {
                CreateDialogueBox();
                if (dialogueTrigger.IsAssigned)
                    isActive = true;
                if (dialogueTrigger.NumberOfActiveDialogues == 0 && dialogueTrigger.IsAssigned)
                    canConstroyDialogBox = false;
                if (Input.GetKeyDown("z"))
                    dialogueTrigger.TriggerDialogue();
            }
            else
            {
                DestroyDialogueBox();
                isActive = false;
            }
        }

        private void CreateDialogueBox() 
        {
            if (dialogueBox == null)
            {
                dialogueBox = Instantiate(dialogueObject, transform.position, Quaternion.identity);
                dialogueTrigger = dialogueBox.GetComponentInChildren<DialogueTrigger>();
            }
        }

        private void DestroyDialogueBox()
        {
            Destroy(dialogueBox);
            dialogueTrigger = null;
        }

        private void PlayerIsTriggering()
        {
            if (triggeredNames.Contains("Ruby"))
            {
                playerIsTriggering = true;
            }
            else
            {
                playerIsTriggering = false;
                canConstroyDialogBox = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            triggeredNames.Add(collision.gameObject.name);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            triggeredNames.Remove(collision.gameObject.name);
        }
    }
}
