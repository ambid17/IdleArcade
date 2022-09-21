using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrganizeObjectController : MonoBehaviour
{
    public int prefabId;

    private Rigidbody _rigidbody;
    private Collider _collider;

    public static readonly int TrashBinLayer = 7;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _rigidbody.AddForce(Random.onUnitSphere * 3, ForceMode.Impulse);
    }

    public void ToggleCollision(bool shouldCollide)
    {
        _rigidbody.isKinematic = !shouldCollide;
        _collider.isTrigger = !shouldCollide;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == TrashBinLayer)
        {
            TrashBinController trashBinController = other.gameObject.GetComponent<TrashBinController>();

            if (trashBinController.prefabId == prefabId)
            {
                Destroy(gameObject);
                // add to score
            }
        }
    }
}
