using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizeObjectController : MonoBehaviour
{
    public int prefabId;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ToggleCollision(bool shouldCollide)
    {
        _rigidbody.isKinematic = !shouldCollide;
    }
}
