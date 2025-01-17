/*
 * Nathan Anderson
 * 1/17/25
 * Destroys the bullets the turrets shoot
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
            Destroy(gameObject, 0.01f);
    }
}
