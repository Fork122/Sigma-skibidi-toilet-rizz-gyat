using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BetterSlider : MonoBehaviour
{
    public Vector3[] positions;
    public float speed;
    public float pauseTime;
    float time;
    bool isMoving;

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Move(Vector3[] dests)
    {
        foreach (Vector3 dest in dests)
        {
            isMoving = true;
            time = 0;
            Vector3 position = transform.position;
            while (transform.position != dest)
            {
                yield return null;
                time += Time.deltaTime;
                float percentageCompleate = time / speed;
                transform.position = Vector3.Lerp(position, dest, percentageCompleate);
            }
            isMoving = false;
            
        }
    }
    void Pause(int pauseTime)
    {
        float time = 0;
        while (time < pauseTime)
        {
            time += Time.deltaTime;
        }
    }
