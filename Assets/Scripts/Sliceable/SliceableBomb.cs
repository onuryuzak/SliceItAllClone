using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceableBomb : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;
    private float power = 10000f;
    void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        setSlicedObjects(_rigidbodies, true);
        SetRandomMaterial(transform.childCount);
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
    
    public void Slice()
    {
        setSlicedObjects(_rigidbodies, false);
        GetComponent<Collider>().enabled = false;
        
        GameManager.instance.Fail();
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
}
