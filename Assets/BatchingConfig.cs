using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchingConfig : MonoBehaviour
{
    public GameObject targetGameObject;

    public bool enableGPUInstancing = false;

    public bool useUV1AsRotateBase = false;

    public bool staticBatching = false;

    public bool combineMesh = false;

    private List<Vector3> cachedList = new List<Vector3>();

    void Start()
    {
        if(targetGameObject != null)
        {
            if (useUV1AsRotateBase)
            {
                var meshFilters = targetGameObject.GetComponentsInChildren<MeshFilter>();
                foreach (var meshFilter in meshFilters)
                {
                    FillMeshUI1(meshFilter);
                }
            }

            var meshRenderers = targetGameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (var meshRender in meshRenderers)
            {
                meshRender.sharedMaterial.enableInstancing = enableGPUInstancing;
                if (useUV1AsRotateBase)
                {
                    meshRender.sharedMaterial.EnableKeyword("_ROTATE_BASE_UV1");
                }
                else
                {
                    meshRender.sharedMaterial.DisableKeyword("_ROTATE_BASE_UV1");
                }
            }

            if (staticBatching)
            {
                StaticBatchingUtility.Combine(targetGameObject);
            }

            if (combineMesh)
            {
                CombineMesh();
            }
        }
    }

    private void FillMeshUI1(MeshFilter meshFilter)
    {
        int vertCount = meshFilter.mesh.vertexCount;
        Vector3 worldPos = meshFilter.transform.position;
        cachedList.Clear();
        while (cachedList.Count < vertCount)
        {
            cachedList.Add(worldPos);
        }
        meshFilter.mesh.SetUVs(1, cachedList);
        cachedList.Clear();
    }

    private void CombineMesh()
    {
        var meshFilters = targetGameObject.GetComponentsInChildren<MeshFilter>();
        int len = meshFilters.Length;
        var combine = new CombineInstance[len];

        for (int i = 0; i < len; ++ i)
        {
            combine[i].mesh = meshFilters[i].mesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine);
        targetGameObject.AddComponent<MeshFilter>().mesh = mesh;
    }
}
