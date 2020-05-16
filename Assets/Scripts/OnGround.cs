using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    private bool isGrounded = false;  //a bool to indicate if the character is on ground
    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jumping()
    {
        isGrounded = false;
    }
}
