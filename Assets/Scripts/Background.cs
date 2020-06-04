using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float length, startpos, length2, startpos2;
    public GameObject camera;
    public float horizentalParallaxEffect;
    public float verticalParallaxEffect;
    void Start ()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        startpos2 = transform.position.y;
        length2 = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate ()
    {
        float temp = (camera.transform.position.x * (1 - horizentalParallaxEffect));
        float dist = (camera.transform.position.x * horizentalParallaxEffect);

        float temp2 = (camera.transform.position.y * (1 - verticalParallaxEffect));
        float dist2 = (camera.transform.position.y * verticalParallaxEffect);

        transform.position = new Vector3(startpos + dist, startpos2 + dist2, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;

        if (temp2 > startpos2 + length2) startpos2 += length2;
        else if (temp2 < startpos2 - length2) startpos2 -= length2;
    }
}
