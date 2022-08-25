using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class DaggerController : MonoBehaviour
{
    public Rigidbody RigidBody;
    public float jumpY;
    public float jumpZ;
    public float _rotationX;

    private Vector3 _daggerDirection = ((Vector3.forward * 4) + (Vector3.down * 2)).normalized;
    private Vector3 _daggerInitPosition = new Vector3(0, 0.74f, -0.6f);
    bool _isMove = true;
    public bool _daggerOnGround;

    private void OnEnable()
    {
        RigidBody = GetComponent<Rigidbody>();
        EventManager.TouchGround += OnTouchGround;
        EventManager.LevelLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        EventManager.TouchGround -= OnTouchGround;
        EventManager.LevelLoaded -= OnLevelLoaded;
    }

    void Start()
    {
        
        RigidBody.maxAngularVelocity = 10f;
    }

    void Update()
    {
        if (!GameManager.instance.canPlay) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            RigidBody.isKinematic = false;
            MoveState();
        }

        float daggerAngle = Vector3.Angle(_daggerDirection, transform.forward);
        if (_isMove && daggerAngle < 40)
        {
            RigidBody.maxAngularVelocity = 2f;
        }
        else RigidBody.maxAngularVelocity = 10f;
    }

    private void FixedUpdate()
    {
        RigidBody.inertiaTensorRotation = Quaternion.identity;
    }


    private void OnTouchGround()
    {
        MoveState(false);
    }

    public void MoveState(bool moveZAxis = true)
    {
        if (moveZAxis)
        {
            _isMove = false;

            RigidBody.AddForce(new Vector3(0, jumpY, jumpZ), ForceMode.Impulse);
            RigidBody.AddTorque(new Vector3(_rotationX, 0, 0), ForceMode.Acceleration);
            RigidBody.angularVelocity = Vector3.zero;
            RigidBody.velocity = Vector3.zero;
            DOVirtual.DelayedCall(0.4f, (() => _isMove = true));
        }
        else
        {
            RigidBody.velocity = Vector3.zero;
            RigidBody.AddForce(new Vector3(0, jumpY / 2, -jumpZ), ForceMode.Impulse);
            RigidBody.angularVelocity = Vector3.zero;
            RigidBody.AddTorque(new Vector3(-_rotationX, 0, 0), ForceMode.Acceleration);
        }
    }


    private void OnLevelLoaded()
    {
        RigidBody.isKinematic = true;
        transform.position = _daggerInitPosition;
        transform.rotation = Quaternion.Euler(90f,0,0); 
    }
}