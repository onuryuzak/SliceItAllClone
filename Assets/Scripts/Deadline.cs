using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadline : MonoBehaviour
{
    private const string PlayerCollider = "PlayerCollider";
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(PlayerCollider))
        {
            GameManager.instance.Fail();

        }
    }
}
