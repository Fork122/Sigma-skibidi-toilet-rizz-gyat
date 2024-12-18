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
                    StartCoroutine(MoveCamera(posNum));
                    playerController.RemoveControls(true);
                }
            }
        }
    }

    IEnumerator MoveCamera(int arrNum)
    {
        Vector3 pos = transform.position;
        while(transform.position.x <= x[arrNum] || transform.position.y <= y[arrNum] || cam.orthographicSize <= size[arrNum])
        {
            if(timer <= SpeedOfCamera)
            {
                timer += Time.deltaTime;
                
            }
            else
            {
                timer = 0;
                if(transform.position.x <= x[arrNum])
                {
                    pos.x += 0.2f;
                    transform.position = pos;
                }
                if (transform.position.y <= y[arrNum])
                {
                    pos.y += 0.2f;
                    transform.position = pos;
                }
                if(cam.orthographicSize <= size[arrNum])
                {
                    cam.orthographicSize += 0.2f;
                }
            }
            yield return null;
        }
        playerController.RemoveControls(false);
    }
}
