using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.SlicedObject += Slice;
    }

    private void OnDisable()
    {
        EventManager.SlicedObject -= Slice;
    }

    private void Slice(SliceableObject sliceableobject)
    {
        sliceableobject.Slice();
    }
}
