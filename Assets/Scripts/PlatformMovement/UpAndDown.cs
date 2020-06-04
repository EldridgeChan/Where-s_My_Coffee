using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpAndDown : MonoBehaviour
{
    public float speed = 2.0f;
    public float movementDistance = 10.0f;
    private int direction = 1;

    private float originalPositionY;
    private bool goBack = false;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GameObject.Find("SpikePlatform").GetComponent<Rigidbody2D>();
        originalPositionY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //print(transform.position.y);
        //print(originalPositionY - movementDistance);
        if (transform.position.y < originalPositionY + movementDistance && goBack == false)
        {
            goBack = false;
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            goBack = true;
            transform.Translate(Vector3.down * direction * speed * Time.deltaTime);
            if (transform.position.y < originalPositionY)
            {
                goBack = false;
            }

        }



    }

    void OnCollisionEnter2D(Collision2D other)
    {
        print(other.transform.tag);
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
