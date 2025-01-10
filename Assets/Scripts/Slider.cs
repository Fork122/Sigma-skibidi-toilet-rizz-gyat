using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public Vector3 finalPosition;
    private Vector3 startPosition;
    public float duration = 2f;
    public float pauseDuration;
    private float time;
    private float startTime;
    private bool moving = false;
    // Start is called before the first frame update
    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
       if (transform.position == startPosition && !moving )
        {
            StartCoroutine(move(finalPosition));


        }
       else if(!moving)
        {
            StartCoroutine(move(startPosition));
        }
    }
    IEnumerator move(Vector3 dest)
    {
        moving = true;
        startTime = Time.time;
        Vector3 position = transform.position;
        while (transform.position != dest)
        {
            yield return null;
            time = Time.time;
            // Lerp durration related
            
            float percentageComplete = (time - startTime) / duration;
            transform.position = Vector3.Lerp(position, dest, percentageComplete);
        }
        moving = false;
    }
}
