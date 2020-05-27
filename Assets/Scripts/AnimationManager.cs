using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private SpriteRenderer playerSpriteRen;
    [SerializeField]
    private Animator playerAnimator;  //Player animator
    [SerializeField]
    private InteractionManager interaction;

    private void Awake()
    {
        playerSpriteRen = playerAnimator.GetComponent<SpriteRenderer>();
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

        if (interaction.Inputman.Control < 0)
        {
            playerSpriteRen.flipX = true;
        }
        else if (interaction.Inputman.Control > 0)
        {
            playerSpriteRen.flipX = false;
        }
    }
}
