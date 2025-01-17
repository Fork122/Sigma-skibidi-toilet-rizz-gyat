/*
 * Nathan Anderson
 * 1/17/25
 * Moves the camera
 */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camerachanger : MonoBehaviour
{
    // arrays for the diferent check points camera variables
    public float[] size;
    public float[] x;
    public float[] y;

    // speed and duration of the camera change
    public float SpeedOfCameraSize = 0.01f;
    public float duration = 5f;
    float elapsedTime;
    float timer;

    // Cheackpoint num
    int posNum = 0;

    // Other
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
        
        // checks if player has gotten into a new cheakpoint
        foreach (RespawnPoint RP in FindObjectsOfType<RespawnPoint>())
        {
            if (RP.active)
            {
                if(posNum != RP.checkPointNum)
                {
                    posNum = RP.checkPointNum;
                    StopAllCoroutines();
                    StartCoroutine(MoveCamera(posNum));
                }
            }
        }
    }

    // Moves the camera
    IEnumerator MoveCamera(int arrNum)
    {
        // Sets the target positions based on what check point the palayer is at and lerps to them based on where the camerea was
        Vector3 targetPos = new Vector3(x[arrNum], y[arrNum], transform.position.z);
        Vector3 startPos = transform.position;
        Vector3 camSizeStartPos = new Vector3(cam.orthographicSize, 0, 0);
        Vector3 camSizeEndPos = new Vector3(size[arrNum], 0, 0);

        // Resets the time it takes to lerp to the new location
        elapsedTime = 0;

        while (transform.position != targetPos || cam.orthographicSize != size[arrNum])
        {
            yield return null;
            // Lerp durration related
            elapsedTime += Time.deltaTime;
            float percentageCompleate = elapsedTime / duration;

            // Lerps the cameras size and scale
            Vector3 tempVar = Vector3.Lerp(camSizeStartPos, camSizeEndPos, percentageCompleate);
            cam.orthographicSize = tempVar.x;
            transform.position = Vector3.Lerp(startPos, targetPos, percentageCompleate);


            // Resets the cameras screen bounds and the deathzone
            playerController.screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            playerController.deathZonePos = new Vector3(Camera.main.transform.position.x, (Camera.main.transform.position.y * 2) + -playerController.screenBounds.y - 1, Camera.main.transform.position.z);
        }

    }
        
}
