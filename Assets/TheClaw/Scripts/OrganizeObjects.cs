using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "OrganizeObjects", menuName = "ScriptableObjects/OrganizeObjects", order = 1)]
public class OrganizeObjects : ScriptableObject
{
    [SerializeField]
    public List<OrganizeObject> objects;
    
    [ContextMenu("Generate")]
    void Generate()
    {
        Debug.Log("Generating [OrganizeObjects]");

        objects = new List<OrganizeObject>();

        string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[]{"Assets/Prefabs/OrganizeObjects"});

        int idCount = 0;
        foreach (var guid in guids)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));
            objects.Add(new OrganizeObject()
            {
                id = idCount++,
                prefab = prefab
            });
        }
    }
}

[Serializable]
public class OrganizeObject
{
    [SerializeField] public int id;
    [SerializeField] public GameObject prefab;
}
