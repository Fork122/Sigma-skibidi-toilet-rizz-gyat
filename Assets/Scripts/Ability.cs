using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            boxCollider.isTrigger = !boxCollider.isTrigger;
            if (boxCollider.isTrigger)
            {
                gameObject.layer = 0;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
            }
            if (!boxCollider.isTrigger)
            {
                gameObject.layer = 8;
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }
    }
}
