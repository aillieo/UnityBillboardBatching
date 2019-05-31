using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateForest : MonoBehaviour
{

    public GameObject tree;

    public int lines = 100;

    public float distance = 0.5f;

    [ContextMenu("CreateForest")]
    public void CreateAll()
    {
        if (tree == null)
        {
            return;
        }

        GameObject root = new GameObject("Forest");

        int total = lines * lines;

        for(int i = 0; i < total; ++i)
        {
            GameObject newTree = PrefabUtility.InstantiatePrefab(tree) as GameObject;
            newTree.name = "Tree";
            newTree.transform.localPosition = new Vector3((i / lines - lines / 2) * distance, 0, (i % lines - lines / 2) * distance);
            newTree.transform.SetParent(root.transform);
        }

    }
}
