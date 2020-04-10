using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class HealthCollectibleController : MonoBehaviour
    {
        AudioSource audioSource;
        public AudioClip audioClip;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (VerifyRubyCollision(collision) && !VerifyRubyHealthIsMaxHealth(collision))
            {
                RestablishRubyHealth(collision);
                Destroy(gameObject);
                audioSource.PlayOneShot(audioClip);
            }
        }

        private bool VerifyRubyHealthIsMaxHealth(Collider2D collision)
        {
            if (collision.GetComponent<RubyController>() != null)
                return collision.GetComponent<RubyController>().Health >= collision.GetComponent<RubyController>().maxHealth;
            else
                return true;
        }

        private bool VerifyRubyCollision(Collider2D collision)
        {
            return (collision.GetComponent<RubyController>() != null);
        }

        private void RestablishRubyHealth(Collider2D collision)
        {
            RubyController rubyController = collision.GetComponent<RubyController>();
            if (rubyController != null && rubyController.Health < rubyController.maxHealth)
                rubyController.AddHealth(2);
        }
    }
}