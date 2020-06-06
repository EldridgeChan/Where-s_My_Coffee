using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Vector2[] targetPos;
    [SerializeField]
    private float speed = 2f;

    private const float fixedframeTime = 0.02f;

    private float lerpT = 0f;
    private float dis;
    private int targetIndex = 1;
    private Rigidbody2D selfRig;

    private void Awake()
    {
        selfRig = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = targetPos[0];
        dis = Vector2.Distance(targetPos[0], targetPos[1]);
    }

    private void FixedUpdate()
    {
        movePlatform();
    }

    private void movePlatform()
    {
        updateT();
        if (lerpT >= 1f)
        {
            targetIndex = (targetIndex + 1) % targetPos.Length;
            lerpT = 0f;
        }
        selfRig.MovePosition(Vector2.Lerp(targetPos[(targetIndex + targetPos.Length - 1) % targetPos.Length], targetPos[targetIndex], lerpT));
    }

    private void updateT()
    {
        Mathf.Clamp01(lerpT += speed * fixedframeTime / dis);
    }
}
