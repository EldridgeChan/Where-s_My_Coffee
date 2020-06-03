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
    void Start()
    {
        originalPositionX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
     
        if (transform.position.x > originalPositionX - movementDistance && goBack == false)
        {
            goBack = false;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
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
}
