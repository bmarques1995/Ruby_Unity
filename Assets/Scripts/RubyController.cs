using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class RubyController : MonoBehaviour
    {
        public float maxHealth;
        public float speed;
        public GameObject projectilePrefab;
        public float projectileForce;
        public float maxInvencibilityTime;

        public float Health { get { return currentHealth; } }

        UIHealthBarController healthBarController;
        float currentHealth;
        float horizontal;
        float vertical;
        private Rigidbody2D rb2d;
        private Animator animator;
        private Vector2 movementDirection;
        private Vector2 lookingDirection;
        float invencibityTime;

        void Start()
        {
            currentHealth = maxHealth;
            invencibityTime = 0;
            rb2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            lookingDirection = Vector2.down;
            healthBarController = GameObject.Find("UI").GetComponent<UIHealthBarController>();
        }

        // Update is called once per frame
        void Update()
        {
            ReadInputs();
            DefineLookingDirection();
            DefineMovementDirection();
            MoveCharacter();
            healthBarController.UpdateScale(currentHealth/maxHealth);
            ApplyAnimations();
            ApplyLaunchCommand();
            UpdateInvencibilityTime();
        }

        private void UpdateInvencibilityTime()
        {
            invencibityTime = invencibityTime > 0 ? invencibityTime - Time.deltaTime : 0.0f; 
        }

        private void ApplyLaunchCommand()
        {
            if(Input.GetButtonDown("Fire1"))
                LaunchProjectile();
        }

        private void LaunchProjectile()
        {
            GameObject projectileObject = Instantiate(projectilePrefab, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);

            ProjectileController projectile = projectileObject.GetComponent<ProjectileController>();
            projectile.Launch(lookingDirection, projectileForce);

            animator.SetTrigger("Launch");
        }

        private void DefineMovementDirection()
        {
            movementDirection = new Vector2(horizontal,vertical);
        }

        private void ApplyAnimations()
        {
            animator.SetFloat("Speed", Mathf.Abs(speed * movementDirection.magnitude));
            animator.SetFloat("LookX", lookingDirection.x);
            animator.SetFloat("LookY", lookingDirection.y);
        }

        private void DefineLookingDirection()
        {
            if (horizontal != 0.0f || vertical != 0.0f)
                lookingDirection = new Vector2(horizontal, vertical);
        }

        void ReadInputs()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        void MoveCharacter()
        {          
            rb2d.MovePosition((Vector2)transform.position + movementDirection * speed * Time.deltaTime);
        }

        public void AddHealth(float amount)
        {
            if(amount > 0)
                currentHealth = Mathf.Clamp(currentHealth + (float) (Math.Round(amount * 10)/10), 0, maxHealth);
            if (amount < 0 && invencibityTime <= 0)
            {
                currentHealth = Mathf.Clamp(currentHealth + (float)(Math.Round(amount * 10) / 10), 0, maxHealth);
                invencibityTime = maxInvencibilityTime;
            }
        }

        public void ReceiveAttack() 
        {
            animator.SetTrigger("Hit");
        }
    }
}
