using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float maxSpeed = 25f; //Where walk speed cap at
    const float acceleration = 120f; //acceleration per frame 
    const float stopMultifier = 5.5f; //How quick it stop when not pressing left or right
    //above value affect walking
    const float jumpForce = 2000f; //velocity of initial jump
    const float upGravityMultifier = 13f; //nagative acceleration when release space
    const float downGravityMultifier = 4f; //acceleration when falling
    //above value affect jumping
    private Rigidbody2D rig;
    private OnGround onGround;

    [SerializeField]
    private InteractionManager interaction;

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
        if (collision.gameObject.tag == "Rope" && interaction.isHookRevoking)
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

    private void walking()
    {
        float control = Input.GetAxisRaw("Horizontal");

        if (control != 0) {
            rig.AddForce(Vector2.right * acceleration * control);
        }else
        {
            if (!interaction.isHooked) {
                rig.AddForce(Vector2.right * stopMultifier * -rig.velocity.x);
                if (rig.velocity.x < 5f && rig.velocity.x > -5f)
                {
                    rig.velocity = new Vector2(0f, rig.velocity.y);
                }
            }
        }
        if (!interaction.isHooked)
        {
            rig.velocity = new Vector2(Mathf.Clamp(rig.velocity.x, -maxSpeed, maxSpeed), rig.velocity.y);
        }
    }

    private void jumping()
    {
        if (Input.GetButtonDown("Jump") && onGround.IsGrounded) {
            onGround.jumping();
            rig.AddForce(Vector2.up * jumpForce);
        }

        if (rig.velocity.y < 0)
        {
            rig.AddForce(Vector2.up * downGravityMultifier * Physics2D.gravity);
        }
        else if (rig.velocity.y > 0 && !Input.GetButton("Jump") && !interaction.isHooked)
        {
            rig.AddForce(Vector2.up * upGravityMultifier * Physics2D.gravity);
        }
    }
}
