using System;
using UnityEngine;

namespace Gameplay.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 225f, jumpForce = 6f;
        private float horizontal;
        private Rigidbody2D rb;
        private bool canMove = true;
        private bool isFacingRight = true;

        [SerializeField] private Transform groundCheck;
        private const float GROUND_CHECK_RADIUS = 0.2f;
        [SerializeField] private LayerMask groundLayer;
        
        [SerializeField] private Animator animator;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();           
        }
        private void Update()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            Flip();
            animator.SetBool("isGrounded", isGrounded());
            animator.SetBool("isMoving", isMoving());           
            if (Input.GetButtonDown("Jump") && isGrounded() && canMove)
            {                
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        private void FixedUpdate()
        {
            if (canMove)
                rb.velocity = new Vector2(speed * horizontal * Time.deltaTime, rb.velocity.y);
        }
        private bool isGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, GROUND_CHECK_RADIUS, groundLayer);
        }
        public void blockMovement() 
        {          
            canMove = !canMove;
            rb.gravityScale = (canMove) ? 1f : 0f; 
        }
        public bool canMoving() { return canMove; }
        public bool isMoving() { return rb.velocity.magnitude > 0.1f; }
        private void Flip()
        {
            if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
    }
}