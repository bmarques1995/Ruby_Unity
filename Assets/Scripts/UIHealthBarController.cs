using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIHealthBarController : MonoBehaviour
    {
        public Image image;
        float initialWidth;
        float scale;

        // Start is called before the first frame update
        void Start()
        {
            initialWidth = image.rectTransform.rect.width;
            scale = 1.0f;
        }

        public void UpdateScale(float value) 
        {
            scale = value <= 1.0f && value >= 0.0f ? value : scale;
        }

        // Update is called once per frame
        void Update()
        {
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, initialWidth * scale);
        }
    }
}
