using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class DialogueBoxController : MonoBehaviour
    {
        private Queue<string> Dialogues;
        private bool isActive;

        TextMeshProUGUI NPCName;
        TextMeshProUGUI currentDialogue;

        public int NumberOfActiveDialogues { get { return Dialogues == null ? 0 : Dialogues.Count; } }
        public bool IsAssigned { get; private set; }


        public bool IsActiveYet { get { return isActive; } }

        private void Start()
        {
            Dialogues = new Queue<string>();
            Dialogues.Clear();

            NPCName = GameObject.Find("NPC").GetComponent<TextMeshProUGUI>();
            currentDialogue = GameObject.Find("Dialogue").GetComponent<TextMeshProUGUI>();
            IsAssigned = false;
        }

        public void StartDialogue(Dialogue dialogue)
        {
            isActive = true;
            NPCName.text = dialogue.NPCName;
            foreach (string dialog in dialogue.dialogues)
                Dialogues.Enqueue(dialog);
            IsAssigned = true;
            PassDialogue();
        }

        public void PassDialogue()
        {
            if (Dialogues.Count == 0)
                EndDialogues();
            else
                currentDialogue.text = Dialogues.Dequeue();
        }

        

        private void EndDialogues() 
        {
            isActive = false;
            Dialogues.Clear();
        }
    }
}
