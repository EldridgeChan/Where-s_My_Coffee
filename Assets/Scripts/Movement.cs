using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float maxSpeed = 50f; //Where walk speed cap at
    const float acceleration = maxSpeed * 0.2f; //acceleration per frame 
    const float exceedDeceleration = acceleration * 0.05f;
    const float stopDeceleration = maxSpeed * 0.2f; //How quick it stop when not pressing left or right
    //above value affect walking
    const float jumpForce = 2700f; //velocity of initial jump
    const float fallSpeedCap = 170f;
    const float jumpGravityMultifier = 0.5f;
    const float upGravityMultifier = 0.1f; //nagative acceleration when release space
    const float downGravityMultifier = 0.2f; //acceleration when falling
    const float missJumpBuffer = 0.1f;
    const float offEdgeBuffer = 0.2f;
    public float OffEdgeBuffer { get { return offEdgeBuffer; } }
    //above value affect jumping

    private Rigidbody2D rig;  //character's rigidbody
    private AudioSource jumpSound;
    public OnGround onGround; //On ground collider scrips 

    [SerializeField]
    private InteractionManager interaction;  //interaction manager

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        onGround = GetComponentInChildren<OnGround>();
        jumpSound = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!interaction.isWin && collision.tag == "Traps")
        {
            Destroy(Instantiate(interaction.BloodPrefab, collision.transform.position + Vector3.up * 1f, Quaternion.identity), 2f);
            interaction.RespawnScript.characterDie();
        }
    }

    private void FixedUpdate()
    {
        walking();
        fallControl();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void walking() //how the charater walk
    {
        if (interaction.Inputman.Control != 0)
        {
            if (Mathf.Abs(rig.velocity.x) <= maxSpeed)
            {
                rig.velocity += Vector2.right * Mathf.Clamp((interaction.Inputman.Control * maxSpeed) - rig.velocity.x, -acceleration, acceleration);
            } else if (rig.velocity.x * interaction.Inputman.Control < 0)   //against momentum
            {
                rig.velocity += Vector2.right * interaction.Inputman.Control * acceleration * 0.5f;
            }
            else  //for momentum
            {
                rig.velocity += Vector2.left * interaction.Inputman.Control * exceedDeceleration;
            }
        } else if (!InteractionManager.isHooked)
        {
            rig.velocity += Vector2.right * Mathf.Clamp(-rig.velocity.x , -stopDeceleration, stopDeceleration);
        }
    }

    public void jump()  //How the character jump
    {
        if (onGround.IsGrounded) {
            onGround.jump();
            interaction.isJumped = true;
            jumpSound.Play();
            rig.velocity = new Vector2(rig.velocity.x, 0f);
            rig.AddForce(Vector2.up * jumpForce);    //add force upward
        } else
        {
            interaction.missedJump = true;
            Invoke("stopMissedJump", missJumpBuffer);
        }
    }

    private void stopMissedJump()
    {
        interaction.missedJump = false;
    }

    private void fallControl()
    {
        if (rig.velocity.y < 0)  //Make it fall faster with increase velocity
        {
            if (interaction.isJumped)
            {
                interaction.isJumped = false;
            }
            rig.velocity += Vector2.up * downGravityMultifier * Physics2D.gravity;
            rig.velocity = new Vector2(rig.velocity.x, Mathf.Clamp(rig.velocity.y, -fallSpeedCap, fallSpeedCap));
        }
        else if (rig.velocity.y > 0 && !Input.GetButton("Jump") && !Input.GetKey(KeyCode.UpArrow) && !InteractionManager.isHooked && interaction.isJumped)  //caontrolable hight of jumping for player
        {
            rig.velocity += Vector2.up * jumpGravityMultifier * Physics2D.gravity;
        }
        else if (rig.velocity.y != 0 && !interaction.isJumped) 
        {
            rig.velocity += Vector2.up * upGravityMultifier * Physics2D.gravity;
        }
    }
}
