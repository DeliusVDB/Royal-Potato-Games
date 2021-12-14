using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private CircleCollider2D circleColl;
    private Animator anim;

    public float distance;

    public LayerMask ladder;

    private bool isClimbing;

    private float dirX, dirY;

    [SerializeField] private float moveSpeed, jumpForce;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource sound;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        circleColl = gameObject.GetComponent<CircleCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            sound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.up, distance, ladder);

        if (raycastHit.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
        }

        if (isClimbing)
        {
            dirY = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, dirY * moveSpeed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1;
        }

        AnimationState();
    }

    private void AnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(circleColl.bounds.center, circleColl.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}

//    private Rigidbody2D rb;

//    [SerializeField] private float moveSpeed, jumpForce;

//    private float moveHorizontal, moveVertical;
//    private bool isJumping;

//    private void Start()
//    {
//        rb = gameObject.GetComponent<Rigidbody2D>();
//    }

//    private void Update()
//    {
//        moveHorizontal = Input.GetAxisRaw("Horizontal");
//        moveVertical = Input.GetAxisRaw("Vertical");
//    }

//    // FixedUpdate is used for Physics
//    private void FixedUpdate()
//    {
//        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
//        {
//            rb.AddForce(new Vector2(moveHorizontal * moveSpeed, rb.velocity.y), ForceMode2D.Impulse);
//        }

//        if (moveVertical > 0.1f && isJumping)
//        {
//            rb.AddForce(new Vector2(rb.velocity.x, moveVertical * jumpForce), ForceMode2D.Impulse);
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isJumping = false;
//        }
//    }

//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (collision.gameObject.CompareTag("Ground"))
//        {
//            isJumping = true;
//        }
//    }
//}