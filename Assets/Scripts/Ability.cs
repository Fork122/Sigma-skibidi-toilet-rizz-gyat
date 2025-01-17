/*
 * Nathan Anderson
 * 1/17/25
 * The main ability for the game
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Ability : MonoBehaviour
{
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    private float switch_cooldown = 0.05f;
    private float timer = 0;
    private int layer;
    

    // Start is called before the first frame update
    void Start()
    {
        // grabs all needed variables and sets the colors of the boxes corectly
        layer = gameObject.layer;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }
        if (boxCollider.isTrigger)
        {
            gameObject.layer = 0;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
        }
        if (!boxCollider.isTrigger)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // delay to not allow switchig super fast
        if (timer <= switch_cooldown)
        {
            timer += Time.deltaTime;
        }

        // Gets user input to activate the ability
        else if (Input.GetButtonUp("Interact"))
        {

            timer = 0;
            boxCollider.isTrigger = !boxCollider.isTrigger;
            if (boxCollider.isTrigger)
            {
                gameObject.layer = 0;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
            }
            if (!boxCollider.isTrigger)
            {
                gameObject.layer = layer;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }
    }
}
