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
    //new
    public GameObject player;
    private bool onBlock = false;
    private Rigidbody2D myRB;
    // Start is called before the first frame update
    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
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

        //new
        if (onBlock)
        {
            player.transform.SetParent(transform);
       }


        while ((dest - transform.position).sqrMagnitude > 0.1f)
        {
            yield return null;
            time = Time.time;
            // Lerp durration related

            //float percentageComplete = (time - startTime) / duration;
            //transform.position = Vector3.Lerp(position, dest, percentageComplete);
            Vector3 toddestination = dest - transform.position;
            toddestination.Normalize();
            myRB.velocity = new Vector2(toddestination.x * duration, toddestination.y * duration);
        }
        finalPosition = startPosition;
        startPosition = transform.position;
        //new
       
        if (!onBlock)
        {
            player.transform.SetParent(null);
        }
        moving = false;

    }
    //new
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("OnCollisionEnter2D");
        if (collision.gameObject == gameObject.CompareTag("Player"))
        {
            onBlock = true;
        }
    }
    //new
    private void OnCollisionExit2D(Collision2D collision)
    {
        print("OnCollisionExit2D");
        if (collision.gameObject == gameObject.CompareTag("Player"))
        {
            onBlock = false;
        }
    }
    */
}
