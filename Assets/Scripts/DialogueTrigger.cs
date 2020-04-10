using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogue;
        DialogueBoxController dialogueBoxController;

        public int NumberOfActiveDialogues { get { return dialogueBoxController == null ? 0 : dialogueBoxController.NumberOfActiveDialogues; } }
        public bool IsAssigned { get { return dialogueBoxController == null ? false : dialogueBoxController.IsAssigned; } }

        private void Start()
        {
            dialogueBoxController = FindObjectOfType<DialogueBoxController>();
        }

        public void TriggerDialogue() 
        {
            if(!dialogueBoxController.IsActiveYet)
                dialogueBoxController.StartDialogue(dialogue);
            else
                dialogueBoxController.PassDialogue();
        }
    }
}
