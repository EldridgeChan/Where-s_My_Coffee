using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private SpriteRenderer playerSpriteRen;
    private OnGround onGround;
    [SerializeField]
    private Animator playerAnimator;  //Player animator
    [SerializeField]
    private InteractionManager interaction;

    private void Awake()
    {
        playerSpriteRen = playerAnimator.GetComponent<SpriteRenderer>();
        onGround = playerAnimator.GetComponentInChildren<OnGround>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        characterAnimation();
    }

    private void characterAnimation()
    {
        playerAnimator.SetFloat("Speed", Mathf.Abs(interaction.Inputman.Control));
        playerAnimator.SetBool("isHooked", interaction.isHooked);
        playerAnimator.SetBool("OnGround", onGround.IsGrounded);
        playerAnimator.SetBool("isJumped", interaction.isJumped);

        if (interaction.isHooked && !onGround.IsGrounded && !interaction.isWin)
        {
            if (playerSpriteRen.flipX)
            {
                playerSpriteRen.flipX = false;
            }
            if (interaction.Inputman.Control < 0)
            {
                playerSpriteRen.flipY = true;
            } else if (interaction.Inputman.Control > 0)
            {
                playerSpriteRen.flipY = false;
            }
            interaction.PlayerRig.rotation = zRotationToHook();
            onGround.transform.eulerAngles = Vector3.zero;
        } else
        {
            if (playerSpriteRen.flipY)
            {
                playerSpriteRen.flipY = false;
            }
            if (interaction.Inputman.Control < 0)
            {
                playerSpriteRen.flipX = true;
            }
            else if (interaction.Inputman.Control > 0)
            {
                playerSpriteRen.flipX = false;
            }
            interaction.PlayerRig.rotation = 0f;
            onGround.transform.eulerAngles = Vector3.zero;
        }
    }

    private float zRotationToHook()
    { 
        return Mathf.Rad2Deg * Mathf.Atan2(interaction.Inputman.currHook.transform.position.y - playerAnimator.transform.position.y, interaction.Inputman.currHook.transform.position.x - playerAnimator.transform.position.x);
    }
}
