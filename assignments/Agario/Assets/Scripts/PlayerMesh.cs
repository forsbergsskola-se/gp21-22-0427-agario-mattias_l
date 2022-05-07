using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour
{
    Mesh thePLayerMesh;
    MeshCollider meshCollider;
    [NonSerialized] List<Vector3> vertices;
    [NonSerialized] private List<int> triangles;
    [NonSerialized] private List<Color> _colors;
   // public List<MeshTiles> MeshTilesList = new ();

    public Vector3 start;
    public Vector3 direction;
    public bool startMoving = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
        UpdateMesh();
    }
    
    public Vector3 GetEdgePosition(int vertOne, int vertTwo, float alpha)
    {
        return Vector3.Lerp(vertices[vertOne], vertices[vertTwo], alpha);
    }
    
    public void UpdateMesh()
    {
        thePLayerMesh.Clear();
        thePLayerMesh.SetVertices(vertices);
        thePLayerMesh.SetTriangles(triangles, 0);
        thePLayerMesh.RecalculateNormals();
    }
    
    private void AddTriangle(Vector3 v1,Vector3 v2, Vector3 v3)
    {
        var vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
        
        UpdateMesh();
    }


    public int AddQuadWithMeshPanel(List<Vector3> pointList)
    {
        int vertexIndex = vertices.Count;
        
        vertices.Add(pointList[0]);
        vertices.Add(pointList[1]);
        vertices.Add(pointList[2]);
        vertices.Add(pointList[3]);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 3);
        UpdateMesh();

        return vertexIndex;
    }
    
    public void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) 
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        vertices.Add(v4);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 3);
    }
}
