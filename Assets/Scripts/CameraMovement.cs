using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float catchUpSpeed = 0.3f;

    [SerializeField]
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.position.x, catchUpSpeed), Mathf.Lerp(transform.position.y, player.position.y, catchUpSpeed) + 3f, -10f);
    }
}
