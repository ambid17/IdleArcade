using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public OrganizeObjects objectsContainer;
    public Transform objectParent;
    public int objectTypeCount = 3;
    public int spawnCount;
    public Bounds spawnBounds;
    
    private List<OrganizeObject> objectsToSpawn;
    
    void Start()
    {
        GetObjectsToSpawn();
        SpawnObjects();
    }

    private void GetObjectsToSpawn()
    {
        objectsToSpawn = new List<OrganizeObject>();

        List<OrganizeObject> remainingObjects = new List<OrganizeObject>(objectsContainer.objects);

        for (int i = 0; i < objectTypeCount; i++)
        {
            int index = Random.Range(0, remainingObjects.Count);
            objectsToSpawn.Add(remainingObjects[index]);
            remainingObjects.RemoveAt(index);
        } 
    }

    private void SpawnObjects()
    {
        foreach (var organizeObject in objectsToSpawn)
        {
            for (int count = 0; count < spawnCount; count++)
            {
                GameObject objInstance = Instantiate(organizeObject.prefab, RandomPointInBounds(), Random.rotation);
                objInstance.transform.parent = objectParent;
            }
        }
    }

    private Vector3 RandomPointInBounds() {
        return new Vector3(
            Random.Range(spawnBounds.min.x, spawnBounds.max.x),
            Random.Range(spawnBounds.min.y, spawnBounds.max.y),
            Random.Range(spawnBounds.min.z, spawnBounds.max.z)
        );
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(spawnBounds.center, spawnBounds.size);
    }
}
