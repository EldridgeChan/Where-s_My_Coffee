using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopesWarp : MonoBehaviour
{
    private Rigidbody2D ropeRig;

    private void Awake()
    {
        ropeRig = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HookPoint" && !InteractionManager.isHooked && !InteractionManager.isHookRevoking) { 
            InteractionManager.wraped = true;
            ropeRig.velocity *= -1f;
        }
    }
}
