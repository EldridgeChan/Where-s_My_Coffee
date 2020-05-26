using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float maxSpeed = 50f; //Where walk speed cap at
    const float acceleration = maxSpeed * 0.3f; //acceleration per frame 
    const float stopDeceleration = maxSpeed * 0.5f; //How quick it stop when not pressing left or right
    //above value affect walking
    const float jumpForce = 2500f; //velocity of initial jump
    const float upGravityMultifier = 30f; //nagative acceleration when release space
    const float downGravityMultifier = 6f; //acceleration when falling
    //above value affect jumping

    float horizontalMove = 0f;
    private Rigidbody2D rig;  //character's rigidbody
    private OnGround onGround; //On ground collider scrips 

    [SerializeField]
    private InteractionManager interaction;  //interaction manager

    public Animator animator; // Animation animator
    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        onGround = GetComponentInChildren<OnGround>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        walking();
        fallControl();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * maxSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    private void walking() //how the charater walk
    {
        if (interaction.Inputman.Control != 0)
        {
            rig.velocity += Vector2.right * Mathf.Clamp((interaction.Inputman.Control * maxSpeed) - rig.velocity.x, -acceleration, acceleration);
        } else if (!interaction.isHooked)
        {
            rig.velocity += Vector2.right * Mathf.Clamp(-rig.velocity.x , -stopDeceleration, stopDeceleration);
        }
    }

    public void jump()  //How the character jump
    {
        if (onGround.IsGrounded) {
            onGround.jump();
            rig.AddForce(Vector2.up * jumpForce);    //add force upward
        }
    }

    private void fallControl()
    {
        if (rig.velocity.y < 0)  //Make it fall faster with increase velocity
        {
            rig.AddForce(Vector2.up * downGravityMultifier * Physics2D.gravity);
        }
        else if (rig.velocity.y > 0 && !Input.GetButton("Jump") && !Input.GetKey(KeyCode.UpArrow) && !interaction.isHooked)  //caontrolable hight of jumping for player
        {
            rig.AddForce(Vector2.up * upGravityMultifier * Physics2D.gravity);
        }
    }
}
