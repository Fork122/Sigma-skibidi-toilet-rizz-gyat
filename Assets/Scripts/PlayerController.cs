/*****************************************
 * Edited by: Ryan Scheppler
 * Last Edited: 1/27/2021
 * Description: This should be added to the player in a simple 2D platformer 
 * *************************************/

using System.Collections;
using System.Collections.Generic;
using ErikHelmers;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    bool respawning = false;

    //speed and movement variables
    public float speed;
    public float airSpeed;
    private float moveInputH;
    //grab this to adjust physics
    private Rigidbody2D myRb;

    //used for checking what direction to be flipped
    public bool facingRight = true;

    //things for ground checking
    private bool isGrounded = false;
    private bool isGroundedLastFrame = false;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    //things for wall checking
    private bool istouchingwall = false;
    public Transform wallcheck;
    public float wallcheckRadius;
    public LayerMask whatIsWall;
    public float wall_slide_drag;
    
    //jump things
    public int extraJumps = 1;
    private int jumps;
    public float jumpForce;
    private bool jumpPressed = true;
    public float wall_jump_force;

    private float jumpTimer = 0;
    public float jumpTime = 0.2f;

    public float gravityScale = 5;

    public float groundDrag = 5;
    public float airDrag = 1;

    private AudioSource myAud;
    public AudioClip jumpNoise;
    
    public GameObject land_anim_object;

    //ladder things
    private bool isClimbing;
    public LayerMask whatIsLadder;
    public float ladderDist;
    private float moveInputV;
    public float climbSpeed;

    //Respawn info
    [HideInInspector]
    public Vector3 RespawnPoint = new Vector3();


    // Camera Restrictions
    public Vector2 screenBounds;
    private float playerHalfWidth;
    public GameObject deathZone;
    public Vector3 deathZonePos;

    private Rigidbody2D platformRb;
    public GameObject item_with_sr;
    // Start is called before the first frame update
    void Start()
    {
        // Camera Restrictions
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        deathZonePos = new Vector3(Camera.main.transform.position.x, (Camera.main.transform.position.y * 2) + -screenBounds.y - 1, Camera.main.transform.position.z);
        playerHalfWidth = item_with_sr.GetComponent<SpriteRenderer>().bounds.extents.x;

        // Player controller related
        myRb = GetComponent<Rigidbody2D>();
        myAud = GetComponent<AudioSource>();

        jumps = extraJumps;

        RespawnPoint = transform.position;
        myRb.drag = airDrag;
        RemoveControls(true);
        if (land_anim_object == null)
            land_anim_object = this.gameObject;
    }

    //Update is called once per frame
    private void Update()
    {
        if (facingRight)
            PlayerPrefs.SetInt("player_facing_right", 1);
        else if (facingRight == false)
            PlayerPrefs.SetInt("player_facing_right", 0);
        // Camera Restrictions
        float clampedX = Mathf.Clamp(transform.position.x, (Camera.main.transform.position.x * 2) + -screenBounds.x + playerHalfWidth, screenBounds.x - playerHalfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, -10000, screenBounds.y - playerHalfWidth);
        Vector3 pos = transform.position;
        pos.x = clampedX;
        pos.y = clampedY;
        transform.position = pos;
        deathZone.transform.position =  new Vector3(Camera.main.transform.position.x, (Camera.main.transform.position.y * 2) - screenBounds.y - 3, 0);

        // Player Controller Related
        if (respawning)
        {
            myRb.drag = airDrag;
            return;
        }
        moveInputH = Input.GetAxisRaw("Horizontal");
        if (isGrounded == true && isGroundedLastFrame == false)
        {
            land_anim_object.GetComponent<SquashAndStretch>().CheckForAndStartCoroutine();
        }

       
        //check if jump can be triggered
        if (Input.GetAxisRaw("Jump") == 1 && jumpPressed == false && isGrounded == true && isClimbing == false)
        {
            myAud.PlayOneShot(jumpNoise);
            myRb.drag = airDrag;
            if ((myRb.velocity.x < 0 && moveInputH > 0) || (myRb.velocity.x > 0 && moveInputH < 0))
            {
                this.gameObject.GetComponent<SquashAndStretch>().CheckForAndStartCoroutine();
                myRb.velocity = (Vector2.up * jumpForce);
            }
            else
            {
                this.gameObject.GetComponent<SquashAndStretch>().CheckForAndStartCoroutine();
                myRb.velocity = (Vector2.up * jumpForce) + new Vector2(myRb.velocity.x, 0);
            }
            jumpPressed = true;
        }
        else if (Input.GetAxisRaw("Jump") == 1 && jumpPressed == false && jumps > 0 && isClimbing == false)
        {
            myAud.PlayOneShot(jumpNoise);
            myRb.drag = airDrag;
            if ((myRb.velocity.x < 0 && moveInputH > 0) || (myRb.velocity.x > 0 && moveInputH < 0))
            {
                //this.gameObject.GetComponent<SquashAndStretch>().CheckForAndStartCoroutine();
                myRb.velocity = (Vector2.up * jumpForce);
            }
            else
            {
                //this.gameObject.GetComponent<SquashAndStretch>().CheckForAndStartCoroutine();
                myRb.velocity = (Vector2.up * jumpForce) + new Vector2(myRb.velocity.x, 0);
            }
            jumpPressed = true;
            jumps--;
        }
        //Wall Jump
        else if (Input.GetAxisRaw("Jump") == 1 && jumpPressed == false && isGrounded == false && isClimbing == false &&
                 istouchingwall)
        {
            myAud.PlayOneShot(jumpNoise);
            myRb.drag = airDrag;
            if ((myRb.velocity.x < 0 && moveInputH > 0) || (myRb.velocity.x > 0 && moveInputH < 0))
            {
                myRb.velocity = (Vector2.up * jumpForce);
            }
            else
            {
                if (facingRight == false)
                {
                    //this.gameObject.GetComponent<SquashAndStretch>().CheckForAndStartCoroutine();
                    myRb.velocity = (Vector2.up * jumpForce) + new Vector2(wall_jump_force, 0);
                }
                else if (facingRight)
                {
                    //this.gameObject.GetComponent<SquashAndStretch>().CheckForAndStartCoroutine();
                    myRb.velocity = (Vector2.up * jumpForce) - new Vector2(wall_jump_force, 0);
                }
            }
            jumpPressed = true;
        }
        else if(Input.GetAxisRaw("Jump") == 0)
        {
            jumpPressed = false;
            jumpTimer = 0;
        }
        else if(jumpPressed == true && jumpTimer < jumpTime)
        {
            jumpTimer += Time.deltaTime;
            myRb.drag = airDrag;
            myRb.velocity = (Vector2.up * jumpForce) + new Vector2(myRb.velocity.x, 0);
            jumpPressed = true;
        }
        
        if (istouchingwall)
        {
            if (facingRight)
            {
                print("Touching Wall and facing Right");
                //it is facing right
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    myRb.drag = wall_slide_drag;
                }
            }

            else if (!facingRight)
            {
                print("Touching Wall and facing Left");
                //it is facing left
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    myRb.drag = wall_slide_drag;
                }
            }
        }
        
        isGroundedLastFrame = isGrounded;
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        //check for ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        istouchingwall = Physics2D.OverlapCircle(wallcheck.position, wallcheckRadius, whatIsWall);
        if (respawning)
        {
            if (isGrounded)
            {
                myRb.drag = airDrag;
                RemoveControls(false);
            }
            return;
        }

        //ladder things

        moveInputV = Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Jump");
        //check for the ladder if around the player
        RaycastHit2D hitInfo = Physics2D.Raycast(groundCheck.position, Vector2.up, ladderDist, whatIsLadder);
        
        //if ladder was found see if we are climbing, stop falling
        if (hitInfo.collider != null)
        {
            myRb.gravityScale = 0;
            isClimbing = true;
            if(moveInputV > 0)
            {
                myRb.AddForce(new Vector2(0, climbSpeed));
            }
            else if(moveInputV < 0)
            {
                myRb.AddForce(new Vector2(0, -climbSpeed));
            }
            else
            {
                myRb.velocity = new Vector2(myRb.velocity.x, 0);
            }
        }
        else
        {
            myRb.gravityScale = gravityScale;
            isClimbing = false;
        }
        
        //horizontal movement
        moveInputH = Input.GetAxisRaw("Horizontal");

        if (isGrounded && !jumpPressed || isClimbing)
        {
            myRb.drag = groundDrag;
            myRb.AddForce(new Vector2(moveInputH * speed , 0));
        }
        else
        {
            myRb.drag = airDrag;
            myRb.AddForce(new Vector2(moveInputH * airSpeed  , 0));
        }
        //check if we need to flip the player direction
        if (facingRight == false && moveInputH > 0)
            Flip();
        else if(facingRight == true && moveInputH < 0)
        {
            Flip();
        }
        if (istouchingwall)
        {
            if (facingRight)
            {
                //it is facing right
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    myRb.drag = wall_slide_drag;
                }
            }

            else if (!facingRight)
            {
                //it is facing left
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    myRb.drag = wall_slide_drag;
                }
            }
        }

    }
    //flip the player so sprite faces the other way
    void Flip()
    {
        facingRight = !facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            myRb.drag = airDrag;
            myRb.velocity = Vector2.zero;
            transform.position = RespawnPoint;
            RemoveControls(true);
        }
        if(collision.gameObject.CompareTag("Platform"))
        {
            platformRb = collision.gameObject.GetComponent<Rigidbody2D>();
            //myRb.velocity = platformRb.velocity;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            //myRb.velocity = platformRb.velocity;
            transform.position += new Vector3(platformRb.velocity.x, platformRb.velocity.y, 0) * Time.fixedDeltaTime;
        }

    }
    public void RemoveControls(bool bol)
    {
        respawning = bol;
    }

}
