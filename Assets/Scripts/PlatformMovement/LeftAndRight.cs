using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LeftAndRight : MonoBehaviour
{
    public float speed = 2.0f;
    public float movementDistance = 10.0f;
    private int direction = 1;

    private float originalPositionX;
    private bool goBack = false;
    public Rigidbody2D rb;

    //public GameObject gameObject;
    void Start()
    {
        rb = GameObject.Find("SpikePlatform").GetComponent<Rigidbody2D>();
        //rb.isKinematic = true;

        //player = FindObjectOfType<GameObject>();
        originalPositionX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
     
        if (transform.position.x > originalPositionX - movementDistance && goBack == false)
        {
            goBack = false;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            //rb.MovePosition((transform.position + Vector3.left) * speed * Time.fixedDeltaTime);
        }
        else
        {
            goBack = true;
            transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
            if (transform.position.x > originalPositionX)
            {
                goBack = false;
            }

        }



    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //print(other.transform.tag);
        if (other.transform.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

}
