using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerHandleTriggerCheck : MonoBehaviour
{
    private const string ground = "Ground";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ground))
        {
            EventManager.OnTouchGround();
        }
    }
}