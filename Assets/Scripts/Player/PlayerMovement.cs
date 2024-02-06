using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D player;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    private bool doubleJump;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling, doubleJumping}

    [SerializeField] private AudioSource jumpSFX1;
    [SerializeField] private AudioSource jumpSFX2;

    public static bool canMove;

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        canMove = true;
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(dirX * moveSpeed, player.velocity.y);

        if(canMove)
        {
            if (IsGrounded())
            {
                doubleJump = true;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (IsGrounded())
                {
                    jumpSFX1.Play();
                    player.velocity = new Vector2(player.velocity.x, jumpForce);
                }
                else
                {
                    if (doubleJump)
                    {
                        jumpSFX2.Play();
                        player.velocity = new Vector2(player.velocity.x, jumpForce);
                        doubleJump = false;
                    }
                }
            }

            UpdateAnimationState(); 
        }
        else
        {
            player.velocity = new Vector2(0f, 0f);
        }
    }

    private void UpdateAnimationState()
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

        if (player.velocity.y > .1f)
        {
            if (!doubleJump)
            {
                state = MovementState.doubleJumping;
            }
            else
            {
                state = MovementState.jumping;
            }
        }
        else if (player.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
