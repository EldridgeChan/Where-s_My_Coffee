using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public HookActions currHook;

    [SerializeField]
    private GameObject hook;
    [SerializeField]
    private InteractionManager interaction;
    [SerializeField]
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        throwHook();

    }

    private void throwHook()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currHook == null)
            {
                interaction.isHookRevoking = false;
                currHook = Instantiate(hook, player.position + Vector3.up, Quaternion.FromToRotation(Vector2.up, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10) - player.position)).GetComponent<HookActions>();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            interaction.isHookRevoking = true;
        }
    }
}
