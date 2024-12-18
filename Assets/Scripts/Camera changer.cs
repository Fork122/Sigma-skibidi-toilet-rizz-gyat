using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camerachanger : MonoBehaviour
{
    public float[] size;
    public float[] x;
    public float[] y;
    public float SpeedOfCamera = 0.1f;
    float timer;
    int posNum;
    GameObject player;
    Camera cam;
    PlayerController playerController;
    private void Start()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if(cam == null)
        {
            cam = Camera.main;
        }
        if(playerController == null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }
    void FixedUpdate()
    {
        foreach (RespawnPoint RP in FindObjectsOfType<RespawnPoint>())
        {
            if (RP.active)
            {
                if(posNum != RP.checkPointNum)
                {
                    posNum = RP.checkPointNum;
                    MoveCamera(posNum);
                    playerController.RemoveControls(true);
                }
            }
        }
    }

    void MoveCamera(int arrNum)
    {
        Vector3 pos = transform.position;
        while(transform.position.x <= x[arrNum] && transform.position.y <= y[arrNum] && cam.orthographicSize <= size[arrNum])
        {
            if(timer < SpeedOfCamera)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                pos.x += 1;
                pos.y += 0.2f;
                transform.position = pos;
                cam.orthographicSize += 0.2f;
            }
            
        }
        playerController.RemoveControls(false);
    }
}
