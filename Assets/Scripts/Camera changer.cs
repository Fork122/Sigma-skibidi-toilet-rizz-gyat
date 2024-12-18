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
    public float SpeedOfCamera = 0.01f;
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
        Vector3 targetPos = new Vector3(x[arrNum], y[arrNum], transform.position.z);

        
        while(transform.position != targetPos)
        {
            if(timer <= SpeedOfCamera)
            {
                timer += Time.deltaTime;
                
            }
            else
            {
                timer = 0;
                if(cam.orthographicSize <= size[arrNum])
                {
                    cam.orthographicSize += 0.01f;
                }
            }
            yield return null;
            transform.position = Vector3.Lerp(transform.position, targetPos, 5);
        }
        playerController.RemoveControls(false);
    }
        
}
