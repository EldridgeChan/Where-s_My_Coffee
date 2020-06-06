using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private float control;
    public float Control { get { return control; } }

    public HookActions currHook;    //Current Hook is exsist

    [SerializeField]
    private InteractionManager interaction; //interaction manager script
    [SerializeField]
    private Movement playersMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!interaction.isWin) {
            control = Input.GetAxisRaw("Horizontal");
            throwHook();    //Check mouse input
            jump();
        }

    }

    public void setZero()
    {
        control = 0;
    }

    private void throwHook()
    {
        if (Input.GetMouseButtonDown(0))    //When left click pressed
        {
            if (currHook == null)   //If no hook already thorwn
            {
                interaction.isHookTraveling = true;
                InteractionManager.isHookRevoking = false;
                interaction.isHookStoped = false;
                //Creat a new Hook gameObject
                currHook = Instantiate(interaction.HookPrefabs, interaction.Player.transform.position + Vector3.up, Quaternion.FromToRotation(Vector2.up, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10) - interaction.Player.transform.position)).GetComponent<HookActions>();
            }
        }
        if (Input.GetMouseButtonUp(0))  //When left click released
        {
            InteractionManager.isHookRevoking = true;  //change state
            interaction.isHookTraveling = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            interaction.isHookPulling = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            interaction.isHookPulling = false;
        }
    }

    private void jump()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (interaction.isHookPulling && playersMovement.onGround.IsGrounded && InteractionManager.isHooked && currHook.transform.position.y < playersMovement.transform.position.y)
            {
                InteractionManager.isHookRevoking = true;
                interaction.isHookTraveling = false;
            }
            playersMovement.jump();
        }
    }
}
