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
        if(transform.position == positions[0] && !isMoving)
        {
            StartCoroutine(MoveF(positions));
        }
        else if(transform.position == positions[positions.Length -1] && !isMoving)
        {
            StartCoroutine(MoveB(positions));
        }
    }
    IEnumerator MoveF(Vector3[] dests)
    {
        isMoving = true;
        foreach (Vector3 dest in dests)
        {
            
            time = 0;
            Vector3 position = transform.position;
            while (transform.position != dest)
            {
                yield return null;
                time += Time.deltaTime;
                float percentageCompleate = time / speed;
                transform.position = Vector3.Lerp(position, dest, percentageCompleate);
            }
            
            Pause(pauseTime);
        }
        isMoving = false;
    }
    IEnumerator MoveB(Vector3[] dests)
    {
        isMoving = true;
        for (int i = dests.Length; i >= 1; i--)
        {
            Vector3 dest = dests[i - 1];
            time = 0;
            Vector3 position = transform.position;
            while (transform.position != dest)
            {
                yield return null;
                time += Time.deltaTime;
                float percentageCompleate = time / speed;
                transform.position = Vector3.Lerp(position, dest, percentageCompleate);
            }
            Pause(pauseTime);
        }
        isMoving = false;
    }
    void Pause(float pauseTime)
    {
        float time = 0;
        while (time < pauseTime)
        {
            time += Time.deltaTime;
        }
    }
}
    
