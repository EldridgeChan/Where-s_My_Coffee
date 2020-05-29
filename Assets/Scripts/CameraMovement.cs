using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.position.x, 0.5f), Mathf.Lerp(transform.position.y, player.position.y, 0.5f) + 3f, -10f);
    }
}
