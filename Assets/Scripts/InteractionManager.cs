using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public bool isHooked = false;   //state indicate if the hook is hooked to the platform
    public bool isHookRevoking = true;  //state indicate if the hook is revoking and revoked
    public bool isHookStoped = true;
    public bool isHookPulling = false;
    public bool isJumped = false;
    public bool isHookTraveling = false;

    [SerializeField]
    private GameObject player; 
    public GameObject Player{ get{return player;} } //Property to player gameobject

    [SerializeField]
    private HingeJoint2D playerJoint;
    public HingeJoint2D PlayerJoint { get { return playerJoint; } } //Property to player's HingeJoint component

    [SerializeField]
    private Rigidbody2D playerRig;
    public Rigidbody2D PlayerRig { get { return playerRig; } }

    [SerializeField]
    private GameObject hookPrefabs;
    public GameObject HookPrefabs { get { return hookPrefabs; } }   //Property to HookPrefabs

    [SerializeField]
    private GameObject ropePrefabs;
    public GameObject RopePrefabs { get { return ropePrefabs; } }   //Property to RopePreFabs

    [SerializeField]
    private InputManager inputMan;
    public InputManager Inputman { get { return inputMan; } }   //Property to inputManager script

    [SerializeField]
    private Respawn respawnScript;
    public Respawn RespawnScript { get { return respawnScript; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

}
