// Nathan Anderson
// 1/9/25
// Changes the gameObject on and off for a defined duration
/*
 * Nathan Anderson
 * 1/17/25
 * Turns on and of a boxcollider  for a set duration
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public float activeDuration = 1;
    public float inactiveDuration = .3f;
    private int layer;
    private float time;
    private bool active = true;
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        layer = gameObject.layer;
        if (boxCollider.isTrigger)
        {
            gameObject.layer = layer;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
        }
        if (!boxCollider.isTrigger)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
    }

    // changes the gameobject on and off
    void Update()
    {
        // code for time when off
        if (boxCollider.isTrigger)
        {
            if (time < activeDuration)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = 0;
                active = !active;
                boxCollider.isTrigger = !boxCollider.isTrigger;
                boxCollider.enabled = true;
                if (active)
                {
                    gameObject.layer = layer;
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                }
                else
                {
                    gameObject.layer = layer;
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);

                }
            }
        }
        // code for time when on
        else
        {
            if (time < inactiveDuration)
            {
                time += Time.deltaTime;
            }
            else
            {
                time = 0;
                active = !active;
                boxCollider.isTrigger = !boxCollider.isTrigger;
                boxCollider.enabled = false;
                if (active)
                {
                    gameObject.layer = layer;
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
                }
                else
                {
                    gameObject.layer = layer;
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);

                }
            }
        }
        
    }
        
}
