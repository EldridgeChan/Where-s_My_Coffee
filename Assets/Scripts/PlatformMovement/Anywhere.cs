using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nexPos;
    [SerializeField]
    public Transform childTransform;
    [SerializeField]
    public Transform transformB;

    public float speed;

    void Start()
    {
        nexPos = posB;
    }

    void Update()
    {
        Move();
    }
    private void Move()
    {
        var originalChildPos = childTransform.localPosition;
        if (childTransform.localPosition != nexPos)
        {
            childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nexPos, speed * Time.deltaTime);
        }
        else
        {
            childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nexPos*2, speed * Time.deltaTime);
        }
    }
}