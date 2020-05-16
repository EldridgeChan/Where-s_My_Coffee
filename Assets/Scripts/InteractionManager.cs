using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public bool isHooked = false;
    public bool isHookRevoking = true;

    [SerializeField]
    private GameObject player;
    public GameObject Player{ get{return player;} }

    [SerializeField]
    private HingeJoint2D playerJoint;
    public HingeJoint2D PlayerJoint { get { return playerJoint; } }

    [SerializeField]
    private GameObject hookPrefabs;
    public GameObject HookPrefabs { get { return hookPrefabs; } }

    [SerializeField]
    private GameObject ropePrefabs;
    public GameObject RopePrefabs { get { return ropePrefabs; } }

    [SerializeField]
    private InputManager inputMan;
    public InputManager Inputman { get { return inputMan; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyRopes(Collider2D collision)
    {
        inputMan.currHook.destroyRope(collision);
    }
}
