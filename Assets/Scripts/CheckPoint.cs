using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private Respawn respawnScript;

    // Start is called before the first frame update
    void Start()
    {
        if (respawnScript.RespawnPoints.Length > 1) {
            transform.position = respawnScript.RespawnPoints[1];
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            respawnScript.updateCheckPoint();
            if (respawnScript.SpawnPointNum + 1 < respawnScript.RespawnPoints.Length) {
                transform.position = respawnScript.RespawnPoints[respawnScript.SpawnPointNum + 1];
            } else
            {
                gameObject.active = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
