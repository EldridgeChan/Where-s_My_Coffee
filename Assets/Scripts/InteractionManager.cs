using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public bool isHooked = false;   //state indicate if the hook is hooked to the platform
    public bool isHookRevoking = true;  //state indicate if the hook is revoking and revoked

    [SerializeField]
    private GameObject player; 
    public GameObject Player{ get{return player;} } //Property to player gameobject

    [SerializeField]
    private HingeJoint2D playerJoint;
    public HingeJoint2D PlayerJoint { get { return playerJoint; } } //Property to player's HingeJoint component

    [SerializeField]
    private GameObject hookPrefabs;
    public GameObject HookPrefabs { get { return hookPrefabs; } }   //Property to HookPrefabs

    [SerializeField]
    private GameObject ropePrefabs;
    public GameObject RopePrefabs { get { return ropePrefabs; } }   //Property to RopePreFabs

    [SerializeField]
    private InputManager inputMan;
    public InputManager Inputman { get { return inputMan; } }   //Property to inputManager script

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //A method to pass the collision between player and rope to the hookaction script
    public void destroyRopes(Collider2D collision)  
    {
        inputMan.currHook.destroyRope(collision);
    }
}
