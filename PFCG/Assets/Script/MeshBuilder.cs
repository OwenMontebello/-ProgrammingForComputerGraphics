using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBuilder
{

    private List<Vector3> vertices = new List<Vector3>();

    private List<int> triangleIndices = new List<int>();

    private List<Vector3> normals = new List<Vector3>();

    private List<Vector2> uvs = new List<Vector2>();

    private List<int>[] submeshIndices = new List<int>[] {};


    public MeshBuilder(int submeshCount)
    {

        submeshIndices = new List<int>[submeshCount];

        for(int i =0; i < submeshCount; i++){
            submeshIndices[i] = new List<int>();
        }

    }

    public void BuildTriangle(Vector3 p0, Vector3 p1, Vector3 p2, int submeshIndex){
        Vector3 normal = Vector3.Cross(p1 - p0, p2 - p0).normalized; // perpendicular angle 
        BuildTriangle(p0, p1, p2, normal, submeshIndex);
    }

    public void BuildTriangle(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 normal, int submeshIndex)
    {

        int p0Index = vertices.Count;
        int p1Index = vertices.Count + 1;
        int p2Index = vertices.Count + 2;

        //1. Add the points to the vertices list
        vertices.Add(p0);
        vertices.Add(p1);
        vertices.Add(p2);

        //2. Add the indices to the points to the trinagle index list
        triangleIndices.Add(p0Index);
        triangleIndices.Add(p1Index);
        triangleIndices.Add(p2Index);

        //3. Add the normal for each point
        normals.Add(normal);
        normals.Add(normal);
        normals.Add(normal);

        //4. Add each uv coordinate to the uv list
        uvs.Add(new Vector2(0,0));
        uvs.Add(new Vector2(0,1));
        uvs.Add(new Vector2(1,1));


        //5. Add the submesh index for each point within the triangle
        submeshIndices[submeshIndex].Add(p0Index);
        submeshIndices[submeshIndex].Add(p1Index);
        submeshIndices[submeshIndex].Add(p2Index);
    }


    public Mesh CreateMesh(){
        Mesh mesh = new Mesh();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangleIndices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uvs.ToArray();
        
        mesh.subMeshCount = submeshIndices.Length;

        for(int i = 0; i < submeshIndices.Length; i++){
            if(submeshIndices[i].Count < 3){
                mesh.SetTriangles(new int[3]{0,0,0}, i);
            }
            else
            {
                mesh.SetTriangles(submeshIndices[i].ToArray(), i);
            }
        }


        return mesh;
    }

}
