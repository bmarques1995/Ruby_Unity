using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class EnemyController: MonoBehaviour
    {
        public float speed;
        public bool directionIsHorizontal;
        public float changeTime;

        PassTreeController passTree;
        private UINumberOfEnemiesController uiNumberOfEnemiesController;
        Rigidbody2D rb2d;
        AudioSource audioSource;
        float direction = 1.0f;
        float timeElapsed = 0;
        private Animator animator;
        private Vector2 movementDirection;
        private bool broken;

        private void Start()
        {
            broken = true;
            rb2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            passTree = FindObjectOfType<PassTreeController>();
            uiNumberOfEnemiesController = FindObjectOfType<UINumberOfEnemiesController>();
            passTree.AddBrokenEnemy();
            uiNumberOfEnemiesController.AddBrokenEnemy();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (broken)
            {
                ChangeDirection();
                DefineMovementDirection();
                MoveCharacter();
                ApplyAnimations();
            }   
        }

        public void FixRobot()
        {
            broken = false;
            rb2d.simulated = false;
            animator.SetTrigger("Fixed");
            GetComponentInChildren<SmokeReturner>().gameObject.GetComponent<ParticleSystem>().Stop();
            passTree.RemoveBrokenEnemy();
            uiNumberOfEnemiesController.RemoveBrokenEnemy();
            audioSource.Stop();
        }

        private void ChangeDirection()
        {
            if (timeElapsed >= changeTime)
            {
                direction *= -1.0f;
                timeElapsed = 0;
            }
            else
                timeElapsed += Time.deltaTime;
        }

        private void MoveCharacter()
        {
            rb2d.MovePosition((Vector2) transform.position + movementDirection * speed * Time.deltaTime);
        }

        private void DefineMovementDirection()
        {
            movementDirection = new Vector2
            (
                directionIsHorizontal ? 1.0f : 0.0f,
                directionIsHorizontal ? 0.0f : 1.0f
            ) * direction;
        }

        private void ApplyAnimations()
        {
            animator.SetFloat("LookX", movementDirection.x);
            animator.SetFloat("LookY", movementDirection.y);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (CollidedWithPlayer(collision)) 
            {
                collision.gameObject.GetComponent<RubyController>().AddHealth(-.2f);
                collision.gameObject.GetComponent<RubyController>().ReceiveAttack();
            }
                
        }

        private bool CollidedWithPlayer(Collision2D collision)
        {
            return (collision.gameObject.GetComponent<RubyController>() != null);
        }
    }
}
