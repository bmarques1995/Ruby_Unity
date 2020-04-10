using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ProjectileController : MonoBehaviour
    {
        Rigidbody2D rb2d;
        Vector2 initialPosition;
        
        void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            initialPosition = transform.position;
        }

        void Update()
        {
            VerifyIfNeedsToBeDestroyed();
        }

        private void VerifyIfNeedsToBeDestroyed()
        {
            Vector2 distanceTracked = (Vector2) transform.position - initialPosition;
            if (distanceTracked.magnitude > 20.0f)
                Destroy(gameObject);
        }

        public void Launch(Vector2 direction, float force)
        {
            rb2d.AddForce(direction * force);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            //we also add a debug log to know what the projectile touch
            if (collision.gameObject.GetComponent<EnemyController>() != null)
                collision.gameObject.GetComponent<EnemyController>().FixRobot();
            Destroy(gameObject);
        }
    }
}
