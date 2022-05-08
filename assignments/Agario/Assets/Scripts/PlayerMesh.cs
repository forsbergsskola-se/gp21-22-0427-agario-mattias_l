using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour
{
    Mesh playerMesh;
    MeshCollider meshCollider;
    [NonSerialized] List<Vector3> vertices = new(); 
    [NonSerialized] private List<int> triangles= new();
    [NonSerialized] private List<Color> _colors= new();
 
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = playerMesh = new Mesh();
        meshCollider = new MeshCollider();
        BuildAMesh();
    }

    private void BuildAMesh()
    {
        Vector3 center = transform.position;
        Vector3 extent1 = center + new Vector3(-1,0,0);
        Vector3 extent2 = center + new Vector3(0,0,1);
        
        AddTriangle(extent2,center, extent1);
        
        for (int i = 0; i < 7; i++)
        {
            
            Debug.Log(GetCircleEdge(i * 45, center, 3));
        }
    }

    public Vector3 GetCircleEdge(float degree, Vector3 center, float extent)
    {
        var cos = Math.Cos(Mathf.PI / 180f * degree) * extent;
        var sin = Math.Sin(Mathf.PI / 180f * degree) * extent;
     

        return center + new Vector3((float)cos, 0, (float) sin);
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
        playerMesh.Clear();
        playerMesh.SetVertices(vertices);
        playerMesh.SetTriangles(triangles, 0);
        playerMesh.RecalculateNormals();
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
