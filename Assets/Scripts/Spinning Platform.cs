/*
 * Name: Erik Helmers
 * Date: 1/12/25
 * Desc: A basic platform that rotates
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatform : MonoBehaviour
{
    [Tooltip("Positive values rotate counter clockwise and negative values rotate clockwise")]
    public float rotationSpeed = 60f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,0,1), Time.deltaTime * rotationSpeed);
    }
}
