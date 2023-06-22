using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // unity elements 
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    private float dirX = 0f;
    [SerializeField] private LayerMask jumpableGround;


    // add [SerializeField] before element to acess/modify directly in Unity
    //find ex. in 'Player Movement (Script) in unity
    [SerializeField] private float moveSpeed = 7f;   //set movement speed
    [SerializeField] private float jumpForce = 14f;

    //asigns each animation their own value to be called later
    private enum MovementState { idle, running, jumping, falling }

    //audio source
    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame (add movement here)
    private void Update()
    {
            //spacebar to jump
        if (Input.GetButtonDown("Jump") && IsGrounded()) //only jump is down button is pushed AND player is on ground
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSoundEffect.Play();
        }

        //move side to side
        dirX = Input.GetAxisRaw("Horizontal"); 
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
            //activate running animation
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;           //faces player right
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;            //faces pleryer left
        }
        else
        {
            state = MovementState.idle;     //idle animation
        }

        //jumping/ falling animation
        if (rb.velocity.y > .1f)  //margin error 0 -> .1 b/c if 0 it will stay in jumping anim
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state); //(int) will turn enum into its int value
    }

    private bool IsGrounded() 
    {
        //detects that the player is standing on the ground
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
