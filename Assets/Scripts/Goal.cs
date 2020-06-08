using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private float flyOffForce = 70f;
    private Vector2 flyOffDir;

    private InteractionManager interaction;

    private void Awake()
    {
        interaction = GameObject.FindWithTag("GameManager").GetComponent<InteractionManager>();        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interaction.isWin = true;
            interaction.Inputman.enabled = false;
            interaction.RespawnScript.enabled = false;
            interaction.Player.GetComponent<BoxCollider2D>().enabled = false;
            float distance = interaction.Player.transform.position.x - transform.position.x;
            flyOffDir = new Vector2(distance / Mathf.Abs(distance), 0.3f);
            GameObject.FindWithTag("Timer").GetComponent<TimeCounter>().win();
            GameObject.FindWithTag("SceneController").GetComponent<SceneController>().loadAfterWin(5f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (interaction.isWin)
        {
            flyOff();
        }
    }

    private void flyOff()
    {
        interaction.PlayerRig.velocity = flyOffDir * flyOffForce;
    }
}
