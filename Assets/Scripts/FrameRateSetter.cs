using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class FrameRateSetter : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}
