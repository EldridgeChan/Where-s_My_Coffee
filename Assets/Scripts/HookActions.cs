using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookActions : MonoBehaviour
{
    const float hookSpeed = 120f;
    const int maxRopes = 25;
    private Vector2 travelDir;
    private HingeJoint2D hookJoint;
    private Rigidbody2D lastRopeRig;
    private Rigidbody2D hookRig;
    private List<Rigidbody2D> ropes = new List<Rigidbody2D>();
    private InteractionManager interaction;

    private void Awake()
    {
        interaction = GameObject.FindWithTag("GameManager").GetComponent<InteractionManager>();
        hookJoint = GetComponent<HingeJoint2D>();
        lastRopeRig = GetComponent<Rigidbody2D>();
        hookRig = lastRopeRig;
    }

    // Start is called before the first frame update
    void Start()
    {
        travelDir = travelDirection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" && ropes.Count < maxRopes && !interaction.isHooked && !interaction.isHookRevoking)
        {
            interaction.isHooked = true;
            hookRig.constraints = RigidbodyConstraints2D.None;
            hookJoint.enabled = true;
            hookJoint.connectedBody = collision.GetComponent<Rigidbody2D>();
            interaction.PlayerJoint.enabled = true;
            interaction.PlayerJoint.connectedBody = lastRopeRig;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && interaction.isHookRevoking)
        {
            destroyHook();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interaction.isHookRevoking)
        {
            revokeRope();
        } else if (ropes.Count < maxRopes && !interaction.isHooked) {
            travel();
            addrope();
        }
    }

    private void travel()
    {        
        hookRig.velocity = travelDir * hookSpeed;
    }

    private void addrope()
    {
        while (Vector3.Distance(lastRopeRig.transform.position, interaction.Player.transform.position) > 1f)
        {
            Rigidbody2D currRopeRig;
            ropes.Add(currRopeRig = Instantiate(interaction.RopePrefabs, nextRopePosition(), lastRopeRig.transform.rotation).GetComponent<Rigidbody2D>());
            currRopeRig.GetComponent<HingeJoint2D>().connectedBody = lastRopeRig;
            lastRopeRig = currRopeRig;
            if (ropes.Count == maxRopes)
            {
                interaction.PlayerJoint.enabled = true;
                interaction.PlayerJoint.connectedBody = lastRopeRig;
            }
        }
    }

    public void revokeRope()
    {

        if (interaction.isHooked)
        {
            interaction.isHooked = false;
        }
        if (interaction.PlayerJoint.enabled)
        {
            interaction.PlayerJoint.connectedBody = null;
            interaction.PlayerJoint.enabled = false;
            hookJoint.enabled = false;
        }
        
        if (ropes.Count > 0)
        {
            ropes[ropes.Count - 1].MovePosition(interaction.Player.transform.position);
        } else
        {
            hookRig.MovePosition(interaction.Player.transform.position);
        }
    }

    public void destroyRope(Collider2D collision)
    {
        ropes.Remove(collision.GetComponent<Rigidbody2D>());
        Destroy(collision.gameObject);
    }

    private void destroyHook()
    {
        if (ropes.Count > 0)
        {
            for (int i = ropes.Count - 1; i >= 0; i--)
            {
                Rigidbody2D temp = ropes[i];
                ropes.Remove(temp);
                Destroy(temp.gameObject);
            }
        }
        Destroy(gameObject);
    }

    private Vector2 travelDirection()
    {
        float theta = Mathf.Deg2Rad * -gameObject.transform.rotation.eulerAngles.z;
        return new Vector2(Mathf.Sin(theta), Mathf.Cos(theta));
    }

    private Vector3 nextRopePosition ()
    {
        Vector3 temp = lastRopeRig.transform.position - interaction.Player.transform.position;
        return lastRopeRig.transform.position - new Vector3(temp.x/temp.magnitude, temp.y/temp.magnitude, 0f);
    }
}
