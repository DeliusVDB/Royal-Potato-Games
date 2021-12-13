using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator anim;

    public float distance;
    public LayerMask ladder;
    private bool isClimbing;

    private float dirX;
    private float dirY;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource sound;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
