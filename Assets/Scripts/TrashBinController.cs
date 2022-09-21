using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinController : MonoBehaviour
{
    public int prefabId;

    public GameObject representedObject;
    public void Setup(OrganizeObject objectToRepresent)
    {
        prefabId = objectToRepresent.id;

        MeshFilter meshFilter = representedObject.GetComponent<MeshFilter>();
        meshFilter.mesh = objectToRepresent.prefab.GetComponent<MeshFilter>().sharedMesh;
        
        MeshRenderer meshRenderer = representedObject.GetComponent<MeshRenderer>();
        meshRenderer.material = objectToRepresent.prefab.GetComponent<MeshRenderer>().sharedMaterial;

        representedObject.transform.localScale = Vector3.one * 0.5f;
    }
}
