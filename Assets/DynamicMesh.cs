using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMesh : MonoBehaviour
{
    public Material indentMaterial; // Material with the indentation shader
    public float indentDepth = 0.1f; // Match this with the shader's _IndentDepth

    private MeshCollider meshCollider;
    private MeshFilter meshFilter;

    void Start()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshFilter = GetComponent<MeshFilter>();
        indentDepth = indentMaterial.GetFloat("_IndentDepth");
        UpdateColliderMesh();
    }

    private void Update()
    {
        UpdateColliderMesh();
    }

    void UpdateColliderMesh()
    {
        if (indentMaterial == null || meshFilter == null || meshCollider == null) return;

        Texture2D indentMap = (Texture2D)indentMaterial.GetTexture("_Indentmap");
        if (indentMap == null) return;

        Mesh mesh = Instantiate(meshFilter.sharedMesh);
        Vector3[] vertices = mesh.vertices;

        // Assuming the UVs match the indentMap exactly
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector2 uv = mesh.uv[i];
            float indentValue = indentMap.GetPixelBilinear(uv.x, uv.y).r; // Assuming the indent value is stored in the red channel
            vertices[i].z += indentValue * indentDepth; // Modify this line to match how the shader adjusts the vertex position
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        meshCollider.sharedMesh = mesh;
    }
}
