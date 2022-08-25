using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DaggerSharpBehaviour : MonoBehaviour
{
    private DaggerController _daggerController;
    private const string ground = "Ground";
    private const string sliceable = "Sliceable";
    private const string sliceableBomb = "SliceableBomb";
    private const string finishPlatform = "FinishPlatform";

    private void Start()
    {
        _daggerController = GetComponentInParent<DaggerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_daggerController._daggerOnGround)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(sliceable))
            {
                EventManager.OnSliceObject(other.GetComponent<SliceableObject>());
            }

            if (other.gameObject.layer == LayerMask.NameToLayer(sliceableBomb))
            {
                other.gameObject.GetComponent<SliceableBomb>().Slice();
            }

            if (other.gameObject.layer == LayerMask.NameToLayer(finishPlatform))
            {
                _daggerController._daggerOnGround = true;
                _daggerController.RigidBody.isKinematic = true;
                GameManager.instance.Success();
                
            }

            if (other.gameObject.layer == LayerMask.NameToLayer(ground))
            {
                _daggerController._daggerOnGround = true;
                _daggerController.RigidBody.isKinematic = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(ground))
        {
            _daggerController._daggerOnGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer((ground)))
        {
            DOVirtual.DelayedCall(0.2f, (() => _daggerController._daggerOnGround = false));
            
        }
        
    }
}