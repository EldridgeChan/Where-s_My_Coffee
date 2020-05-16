using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public HookActions currHook;    //Current Hook is exsist

    [SerializeField]
    private InteractionManager interaction; //interaction manager script

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        throwHook();    //Check mouse input

    }

    private void throwHook()
    {
        if (Input.GetMouseButtonDown(0))    //When left click pressed
        {
            if (currHook == null)   //If no hook already thorwn
            {
                interaction.isHookRevoking = false;
                //Creat a new Hook gameObject
                currHook = Instantiate(interaction.HookPrefabs, interaction.Player.transform.position + Vector3.up, Quaternion.FromToRotation(Vector2.up, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10) - interaction.Player.transform.position)).GetComponent<HookActions>();
            }
        }
        if (Input.GetMouseButtonUp(0))  //When left click released
        {
            interaction.isHookRevoking = true;  //change state
        }
    }
}
