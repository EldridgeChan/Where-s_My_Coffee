﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    private float respawnHight;

    private int spawnPointNum = 0;
    public int SpawnPointNum { get { return spawnPointNum; } }

    public Vector2[] RespawnPoints;

    [SerializeField]
    private float[] FallPositionY;
    [SerializeField]
    private InteractionManager interaction;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fallOffMap();
    }

    //trigger death of the character
    //Also called by Movement when collied wilth trap
    public void characterDie()
    {
        if (interaction.Inputman.currHook != null)
        {
            interaction.Inputman.currHook.deleteRope();
        }
        InteractionManager.isHooked = false;
        InteractionManager.wraped = false;
        interaction.isHookStoped = true;
        interaction.isHookPulling = false;
        interaction.PlayerRig.velocity = Vector3.zero;
        interaction.Player.transform.position = new Vector2(RespawnPoints[SpawnPointNum].x, RespawnPoints[SpawnPointNum].y + respawnHight);
        interaction.Inputman.enabled = false;
        interaction.Inputman.setZero();
    }
    public void updateCheckPoint()
    {
        spawnPointNum++;
    }

    private void fallOffMap()
    {
        if (RespawnPoints.Length > 0 && interaction.Player.transform.position.y < FallPositionY[SpawnPointNum] && !interaction.isWin)
        {
            characterDie();
        }
    }

}
