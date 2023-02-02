using UnityEngine;

namespace Gameplay.Movement.Ladder
{
    [RequireComponent(typeof(Movement))]
    public class LadderMovement : MonoBehaviour
    {
        [SerializeField] private float ladderSpeed = 2f;
        [SerializeField] private Animator animator;
        private Movement PlayerMovement;
        private Rigidbody2D rb;
        private float vertical;
        private bool isLadder, isClimbing;
        void Awake()
        {
            PlayerMovement = GetComponent<Movement>();
            rb = GetComponent<Rigidbody2D>();            
        }

        // Update is called once per frame
        void Update()
        {
            vertical = Input.GetAxis("Vertical");
            if(isLadder && Mathf.Abs(vertical) > 0f && PlayerMovement.canMoving())
            {
                isClimbing = true;
                animator.SetBool("isClimbing", isClimbing);
            }
        }
        private void FixedUpdate()
        {
            if (isClimbing)
            {
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(rb.velocity.x, vertical * ladderSpeed);
            }
            else if (PlayerMovement.canMoving())
                rb.gravityScale = 1f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Ladder"))
            {
                isLadder = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                isLadder = false;
                isClimbing = false;
                animator.SetBool("isClimbing", isClimbing);
            }
        }
    }
}
