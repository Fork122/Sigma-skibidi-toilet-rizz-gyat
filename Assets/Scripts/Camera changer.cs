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
    public float SpeedOfCameraSize = 0.01f;
    public float duration = 5f;
    float elapsedTime;
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
        Vector3 startPos = transform.position;
        Vector3 camSizeStartPos = new Vector3(cam.orthographicSize, 0, 0);
        Vector3 camSizeEndPos = new Vector3(size[arrNum], 0, 0);

        while (transform.position != targetPos)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            float percentageCompleate = elapsedTime / duration;
            Vector3 tempVar = Vector3.Lerp(camSizeStartPos, camSizeEndPos, percentageCompleate);
            cam.orthographicSize = tempVar.x;
            transform.position = Vector3.Lerp(startPos, targetPos, percentageCompleate);
        }
        playerController.RemoveControls(false);
        playerController.screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
        
}
