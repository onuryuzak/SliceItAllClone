using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableObject : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;
    private float power = 15000f;
    private GameManager _gmInstance;

    private void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        setSlicedObjects(_rigidbodies, true);
        SetRandomMaterial(transform.childCount);
        _gmInstance = GameManager.instance;
    }

    public void Slice()
    {
        FloatingText floatingText = Instantiate(GameManager.instance.FloatingText);
        floatingText.transform.position = transform.position - Vector3.right*0.5f;
        floatingText.setText(_gmInstance.currentMoneyValue);
        InventoryManager.instance.addMoney(_gmInstance.currentMoneyValue);
        setSlicedObjects(_rigidbodies, false);
        GetComponent<Collider>().enabled = false;
    }

    private void SetRandomMaterial(int childCount)
    {
        for (int i = 0; i < childCount; i++)
        {
            Material[] mats = transform.GetChild(i).GetComponent<Renderer>().materials;
            mats[1] = GameManager.instance.GetRandomMaterial();
            transform.GetChild(i).GetComponent<Renderer>().materials = mats;
        }
    }

    private void setSlicedObjects(Rigidbody[] rigidbodies, bool kinematicState)
    {
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            Rigidbody rb = rigidbodies[i];
            rb.isKinematic = kinematicState;
            rb.AddForce(rb.transform.localPosition.y * power * Vector3.up);
        }
    }
}