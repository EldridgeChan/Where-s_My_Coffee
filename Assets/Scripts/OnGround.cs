using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    private InteractionManager interaction;
    private Movement movementScript;

    private bool isGrounded = false;  //a bool to indicate if the character is on ground
    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    private void Awake()
    {
        movementScript = GetComponentInParent<Movement>();
        interaction = GameObject.FindWithTag("GameManager").GetComponent<InteractionManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "UnHookable")
        {
            if (!interaction.isJumped)
            {
                Invoke("jump", movementScript.OffEdgeBuffer);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "UnHookable")
        {
            isGrounded = true;
            if (interaction.missedJump)
            {
                movementScript.jump();
            }
            if (!interaction.Inputman.enabled)
            {
                interaction.Inputman.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jump()
    {
        isGrounded = false;
    }
}
