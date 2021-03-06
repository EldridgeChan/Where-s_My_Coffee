﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookActions : MonoBehaviour
{
    const float pullForce = 1000f;
    const float hookSpeed = 150f;  //How fast the hook flying off from the character
    const int maxRopes = 20;   //How long the rope can be
    const int revokeSpeed = 2;
    const int wrapSpeed = 1;
    const float releaseVelocity = 65f;
    //Values above control the hook
    private Vector2 travelDir;  //A variable to store what direction should the hook travel
    private HingeJoint2D hookJoint;  //The HingeJont of Hook object
    private Rigidbody2D lastRopeRig;  //The rigidbody of the last rope segment object
    private Rigidbody2D hookRig;  //the irgidbocy of the Hook object
    private InteractionManager interaction;   //interaction manager
    private LineRenderer hookLineRen;
    private List<HingeJoint2D> ropesJoint = new List<HingeJoint2D>();
    private List<Rigidbody2D> ropes = new List<Rigidbody2D>();  // a list to store all the rope segment

    private void Awake()
    {
        interaction = GameObject.FindWithTag("GameManager").GetComponent<InteractionManager>();
        hookLineRen = GetComponent<LineRenderer>();
        hookJoint = GetComponent<HingeJoint2D>();
        lastRopeRig = GetComponent<Rigidbody2D>();
        hookRig = lastRopeRig;
    }

    // Start is called before the first frame update
    void Start()
    {
        travelDir = travelDirection();   //calculate the direction the hook should travel
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When the Hook is traveling and hit a platform to attach to the platform
        if ((collision.gameObject.tag == "Platform" || collision.gameObject.tag == "HookPoint") && !InteractionManager.isHooked && !InteractionManager.isHookRevoking)
        {
            InteractionManager.isHooked = true;    //change staus
            interaction.isHookStoped = true;
            hookJoint.enabled = true;   //activate the Joint component
            hookJoint.connectedBody = collision.GetComponent<Rigidbody2D>();    //get the hook attach to the platform
            if (ropes.Count == 0)
            {
                buildropes();
            }
            attachPlayer();
            pullRopes();
        } else if (collision.tag == "Player" && interaction.isHookPulling && InteractionManager.isHooked && interaction.Inputman.playersMovement.onGround.IsGrounded)
        {
            InteractionManager.isHookRevoking = true;
            interaction.isHookTraveling = false;
        }
    }

    private void FixedUpdate()
    {
        if (InteractionManager.isHookRevoking) //if whook is revoking
        {
            revokingRope();
        }
        else if (InteractionManager.wraped && !InteractionManager.isHooked)
        {
            wraping();
        }
        else if (interaction.isHookPulling && InteractionManager.isHooked)
        {
            pullRopes();
        }
        else if (Vector2.Distance(transform.position, interaction.Player.transform.position) < maxRopes && !interaction.isHookStoped && interaction.isHookTraveling)
        {   //the hook before revoke
            travel();
        }
        else if (ropes.Count == 0)
        {
            buildropes();
        }
        else if (!InteractionManager.isHooked)
        {
            ropes[ropes.Count - 1].MovePosition(interaction.Player.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateLineRenPos();
    }

    public void deleteRope()
    {
        for (int i = ropes.Count - 1; i >= 0; i--)
        {
            Destroy(ropes[i].gameObject);
        }
        Destroy(gameObject);
    }

    private void travel()   //make hook move
    {
        //hookRig.velocity = travelDir * hookSpeed;
        hookRig.velocity = (travelDir * hookSpeed) + interaction.PlayerRig.velocity;
    }

    private void buildropes()
    {
        interaction.isHookStoped = true;
        hookRig.velocity = Vector2.zero;
        hookRig.constraints = RigidbodyConstraints2D.None;

        int ropeSegs = Mathf.RoundToInt(Vector2.Distance(transform.position, interaction.Player.transform.position));
        Rigidbody2D currRopeRig;
        for (int i = 1; i < ropeSegs; i++)
        {
            //create rope segment object and add it into the list
            ropes.Add(currRopeRig = Instantiate(interaction.RopePrefabs, nextRopePosition(i, ropeSegs), Quaternion.identity).GetComponent<Rigidbody2D>());
            ropesJoint.Add(currRopeRig.GetComponent<HingeJoint2D>());
            ropesJoint[ropesJoint.Count - 1].connectedBody = lastRopeRig;   //attach the rope to the previout rope or hook
            currRopeRig.velocity = interaction.PlayerRig.velocity;
            lastRopeRig = currRopeRig;
            hookLineRen.positionCount++;
            hookLineRen.SetPosition(hookLineRen.positionCount - 2, ropes[ropes.Count - 1].transform.position);
        }
        hookRig.velocity = interaction.PlayerRig.velocity;
    }

    private void attachPlayer()
    {
        for (int i = 0; i < ropes.Count; i++)
        {
            ropes[i].velocity *= Mathf.Lerp(0.65f, 1f, i/ropes.Count);

        }
        interaction.PlayerJoint.enabled = true; //activate player's joint coponent
        interaction.PlayerJoint.connectedBody = lastRopeRig;    //attach the player to the last rope segment
    }

    private void updateLineRenPos()
    {
        hookLineRen.SetPosition(0, transform.position);
        for (int i = 0; i < ropes.Count; i++)
        {
            hookLineRen.SetPosition(i + 1, ropes[i].transform.position);
        }
        hookLineRen.SetPosition(hookLineRen.positionCount - 1, interaction.Player.transform.position);
    }

    private void revokingRope()
    {
        if (interaction.isHookStoped || InteractionManager.isHooked)
        {
            interaction.PlayerJoint.connectedBody = null;
            interaction.PlayerJoint.enabled = false;
            hookJoint.connectedBody = null;
            hookJoint.enabled = false;
            if (InteractionManager.isHooked && transform.position.y > interaction.Player.transform.position.y)
            {
                interaction.PlayerRig.velocity = new Vector2(minReleaseSpeed(), interaction.PlayerRig.velocity.y);
            }
            InteractionManager.isHooked = false;
            interaction.isHookStoped = false;
        }

        for (int i = 0; i < ropes.Count; i++)
        {
            ropes[i].position = Vector3.Lerp(ropes[i].transform.position, Vector3.Lerp(transform.position, interaction.Player.transform.position, (float)i / ropes.Count), 0.5f);
        }
        if (ropes.Count >= 2 * revokeSpeed) {
            hookRig.position = ropes[revokeSpeed - 1].transform.position;
            for (int i = 0; i < ropes.Count - revokeSpeed; i++)
            {
                ropes[i].position = ropes[i + revokeSpeed].transform.position;
            }
            hookLineRen.positionCount -= revokeSpeed;
            for (int i = 0; i < revokeSpeed; i++) {
                Rigidbody2D temp = ropes[ropes.Count - 1 - i];
                ropes.RemoveAt(ropes.Count - 1 - i);
                ropesJoint.RemoveAt(ropesJoint.Count - 1 - i);
                Destroy(temp.gameObject);
            }
        } else
        {
            for (int i = ropes.Count - 1; i >= 0; i--)
            {
                Rigidbody2D temp = ropes[i];
                ropes.RemoveAt(i);
                ropesJoint.RemoveAt(i);
                Destroy(temp.gameObject);
            }
            interaction.isHookStoped = true;
            InteractionManager.wraped = false;
            Destroy(gameObject);
        }
        updateLineRenPos();
    }

    private float minReleaseSpeed()
    {
        if (Mathf.Abs(interaction.PlayerRig.velocity.x) > releaseVelocity)
        {
            return interaction.PlayerRig.velocity.x;
        } else
        {
            return interaction.Inputman.Control * releaseVelocity;
        }
    }

    private void pullRopes()
    {
        float distance = Vector2.Distance(hookRig.position, interaction.PlayerRig.position);
        Rigidbody2D temp;
        interaction.PlayerRig.AddForce((hookRig.position - interaction.PlayerRig.position) / Vector2.Distance(hookRig.position, interaction.PlayerRig.position) * pullForce);
        for (int i = ropes.Count - 1; i > distance; i--) {
            temp = ropes[i];
            ropes.RemoveAt(i);
            ropesJoint.RemoveAt(i);
            hookLineRen.positionCount--;
            Destroy(temp.gameObject);
        }
        //temp = ropes[0];
        for (int i = 0; i < ropes.Count; i++)
        {
            ropes[i].position = Vector3.Lerp(ropes[i].transform.position, Vector3.Lerp(transform.position, interaction.Player.transform.position, (float)i / ropes.Count), 0.5f);
            /*if (i == 0)
            {
                ropesJoint[i].connectedBody = hookRig;
            } else
            {
                ropesJoint[i].connectedBody = temp;
                temp = ropes[i];
            }*/
        }
        interaction.PlayerJoint.connectedBody = ropes[ropes.Count - 1];
        updateLineRenPos();
    }

    private void wraping()
    {
        int j = 1;
        hookRig.MovePosition(ropes[wrapSpeed - 1].position);
        for (int i = 0; i < ropes.Count; i++)
        {
            if (i < ropes.Count - wrapSpeed) {
                ropes[i].MovePosition(ropes[i + wrapSpeed].position);
            } else
            {
                ropes[i].MovePosition(Vector2.Lerp(ropes[ropes.Count - wrapSpeed - 1].position, interaction.PlayerRig.position,j / wrapSpeed));
                j++;
            }
        }
    }

    private Vector2 travelDirection()   //return an unit vecter2 in the direction the hook should travel with its rotation
    {
        float theta = Mathf.Deg2Rad * -gameObject.transform.rotation.eulerAngles.z;
        return new Vector2(Mathf.Sin(theta), Mathf.Cos(theta));
    }

    private Vector3 nextRopePosition(int i, int maxRange)
    {
        return Vector3.Lerp(transform.position, interaction.Player.transform.position, (float)i / maxRange);
    }
}
