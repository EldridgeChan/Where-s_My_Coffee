using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float maxSpeed = 50f; //Where walk speed cap at
    const float acceleration = 120f; //acceleration per frame 
    const float stopMultifier = 5.5f; //How quick it stop when not pressing left or right
    //above value affect walking
    const float jumpForce = 1900f; //velocity of initial jump
    const float upGravityMultifier = 14f; //nagative acceleration when release space
    const float downGravityMultifier = 4.5f; //acceleration when falling
    //above value affect jumping
    private Rigidbody2D rig;  //character's rigidbody
    private OnGround onGround; //On ground collider scrips 

    [SerializeField]
    private InteractionManager interaction;  //interaction manager

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        onGround = GetComponentInChildren<OnGround>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rope" && interaction.isHookRevoking) //When revoking the hook and hit the rope
        {
            interaction.destroyRopes(collision);
        }
    }

    // Update is called once per frame
    void Update()
    {
        walking();
        jumping();
    }

    private void walking() //how the charater walk
    {
        float control = Input.GetAxisRaw("Horizontal"); //input

        if (control != 0) {
            rig.AddForce(Vector2.right * acceleration * control);  //simple addforce
        }else
        {
            if (!interaction.isHooked) {        //slowing down when not inputing (on the ground)
                rig.AddForce(Vector2.right * stopMultifier * -rig.velocity.x);
                if (rig.velocity.x < 5f && rig.velocity.x > -5f)
                {
                    rig.velocity = new Vector2(0f, rig.velocity.y);
                }
            }
        }
        if (!interaction.isHooked) //clamp it to a max walking speed (on the ground)
        {
            rig.velocity = new Vector2(Mathf.Clamp(rig.velocity.x, -maxSpeed, maxSpeed), rig.velocity.y);
        }
    }

    private void jumping()  //How the character jump
    {
        if (Input.GetButtonDown("Jump") && onGround.IsGrounded) {
            onGround.jumping();
            rig.AddForce(Vector2.up * jumpForce);    //add force upward
        }

        if (rig.velocity.y < 0)  //Make it fall faster with increase velocity
        {
            rig.AddForce(Vector2.up * downGravityMultifier * Physics2D.gravity);
        }
        else if (rig.velocity.y > 0 && !Input.GetButton("Jump") && !interaction.isHooked )  //caontrolable hight of jumping for player
        {
            rig.AddForce(Vector2.up * upGravityMultifier * Physics2D.gravity);
        }
    }
}
