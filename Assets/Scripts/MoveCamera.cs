/*Name: Idan Shaviner
 *  Date: 12/17/2024
 * Desb: Slide camera over when player reaches checkpoint (freeze player control during sliding)
 * 
 */using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    public float time;

    private bool isSliding = false;
    private Array cam_x_loc = new Array[0, 20];
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
