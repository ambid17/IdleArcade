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
    public GameObject trashPrefab;

    [SerializeField] private LayerMask objectLayerMask;
    private List<OrganizeObject> objectsToSpawn;
    private Camera mainCamera;

    private OrganizeObjectController currentObject;
    
    void Start()
    {
        mainCamera = Camera.main;
        GetObjectsToSpawn();
        SpawnObjects();
        SpawnTrashBins();
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
                
                OrganizeObjectController objectController = objInstance.GetComponent<OrganizeObjectController>();
                objectController.prefabId = organizeObject.id;
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

    private void SpawnTrashBins()
    {
        for (int x = - objectTypeCount / 2; x <= objectTypeCount / 2; x++)
        {
            Vector3 position = new Vector3(x * 6,-5, -15);
            GameObject trashInstance = Instantiate(trashPrefab, position, Quaternion.identity);
        } 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100,
                objectLayerMask))
            {
                currentObject = hit.collider.gameObject.GetComponent<OrganizeObjectController>();
                currentObject.ToggleCollision(false);
            }
        }

        if (Input.GetMouseButtonUp(0) && currentObject != null)
        {
            currentObject.ToggleCollision(true);
            currentObject = null;
        }

        if (currentObject != null)
        {
            // follow the mouse
            Vector3 objectPosition = GetObjectMousePosition();
            currentObject.gameObject.transform.position = objectPosition;
        }
        
    }

    private Vector3 GetObjectMousePosition()
    {
        // x-z plane
        var plane = new Plane(Vector3.zero, new Vector3(1, 0,0), new Vector3(1,0,1));
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (plane.Raycast(ray, out float distance)){
            return ray.GetPoint(distance);
        }

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(spawnBounds.center, spawnBounds.size);
    }
}
