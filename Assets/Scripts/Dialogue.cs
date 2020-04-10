using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Dialogue
    {
        public string NPCName;
        [TextArea(1,2)]
        public List<string> dialogues;
    }
}
