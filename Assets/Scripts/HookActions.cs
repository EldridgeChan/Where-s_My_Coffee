using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookActions : MonoBehaviour
{
    const float hookSpeed = 120f;  //How fast the hook flying off from the character
    const int maxRopes = 25;   //How long the rope can be
    //Values above control the hook
    private Vector2 travelDir;  //A variable to store what direction should the hook travel
    private HingeJoint2D hookJoint;  //The HingeJont of Hook object
    private Rigidbody2D lastRopeRig;  //The rigidbody of the last rope segment object
    private Rigidbody2D hookRig;  //the irgidbocy of the Hook object
    private List<Rigidbody2D> ropes = new List<Rigidbody2D>();  // a list to store all the rope segment
    private InteractionManager interaction;   //interaction manager

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
        travelDir = travelDirection();   //calculate the direction the hook should travel
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When the Hook is traveling and hit a platform to attach to the platform
        if (collision.gameObject.tag == "Platform" && ropes.Count < maxRopes && !interaction.isHooked && !interaction.isHookRevoking)
        {
            interaction.isHooked = true;    //change staus
            hookRig.constraints = RigidbodyConstraints2D.None;  //enable the hook to rotate
            hookJoint.enabled = true;   //activate the Joint component
            hookJoint.connectedBody = collision.GetComponent<Rigidbody2D>();    //get the hook attach to the platform
            interaction.PlayerJoint.enabled = true; //activate the joint component of player
            interaction.PlayerJoint.connectedBody = lastRopeRig;    //make player's joint attach to the last rope
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // When revoking the hook and hit with player
        if (collision.gameObject.tag == "Player" && interaction.isHookRevoking)
        {
            destroyHook();
        }
    }

    private void FixedUpdate()
    {
        if (interaction.isHookRevoking) //if whook is revoking
        {
            revokeRope();
        }
        else if (ropes.Count < maxRopes && !interaction.isHooked)
        {   //the hook before revoke
            travel();
            addrope();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void travel()   //make hook move
    {        
        hookRig.velocity = travelDir * hookSpeed;
    }

    private void addrope()  //add rope sement between hook and character
    {
        //when more than a unit between player and last rope segment
        while (Vector3.Distance(lastRopeRig.transform.position, interaction.Player.transform.position) > 1f)    
        {
            Rigidbody2D currRopeRig;
            //create rope segment object and add it into the list
            ropes.Add(currRopeRig = Instantiate(interaction.RopePrefabs, nextRopePosition(), lastRopeRig.transform.rotation).GetComponent<Rigidbody2D>());
            currRopeRig.GetComponent<HingeJoint2D>().connectedBody = lastRopeRig;   //attach the rope to the previout rope or hook
            lastRopeRig = currRopeRig;
            if (ropes.Count == maxRopes)    //last rope segment have been created
            {
                interaction.PlayerJoint.enabled = true; //activate player's joint coponent
                interaction.PlayerJoint.connectedBody = lastRopeRig;    //attach the player to the last rope segment
            }
        }
    }

    public void revokeRope()    //call when the player release mouse left click
    {

        if (interaction.isHooked)
        {
            //Makes player to move on ground state (caped speed) and change the staus back
            interaction.isHooked = false;   
        }
        if (interaction.PlayerJoint.enabled)
        {
            //deattach the player to the rope
            interaction.PlayerJoint.connectedBody = null;
            interaction.PlayerJoint.enabled = false; //Deactivate the joint componet so that the player can walk 
            hookJoint.enabled = false;  //Deattach the hook from platform
        }
        
        if (ropes.Count > 0)
        {
            //make the last rope move toward player
            ropes[ropes.Count - 1].MovePosition(interaction.Player.transform.position); 
        } else
        {
            //If the rope left, move hook toward player
            hookRig.MovePosition(interaction.Player.transform.position);
        }
    }

    public void destroyRope(Collider2D collision)   //called what the rope collide with player in Movement script
    {
        ropes.Remove(collision.GetComponent<Rigidbody2D>());    //remove rope segment from the list
        Destroy(collision.gameObject);  //destroy rope segment object
    }

    private void destroyHook()  //called when the hook is revoking and hit player
    {
        if (ropes.Count > 0)
        {
            //destroy every rope segment which are left
            //because the rope is referened by the list in this script if destriy this there will be no reference to the rope segment
            for (int i = ropes.Count - 1; i >= 0; i--)
            {
                Rigidbody2D temp = ropes[i];
                ropes.Remove(temp);
                Destroy(temp.gameObject);
            }
        }
        Destroy(gameObject);    //Destroy the Hook
    }

    private Vector2 travelDirection()   //return an unit vecter2 in the direction the hook should travel with its rotation
    {
        float theta = Mathf.Deg2Rad * -gameObject.transform.rotation.eulerAngles.z;
        return new Vector2(Mathf.Sin(theta), Mathf.Cos(theta));
    }

    private Vector3 nextRopePosition () //return the position where the next rope should be instantialated with the lastroperig variable
    {
        Vector3 temp = lastRopeRig.transform.position - interaction.Player.transform.position;
        return lastRopeRig.transform.position - new Vector3(temp.x/temp.magnitude, temp.y/temp.magnitude, 0f);
    }
}
