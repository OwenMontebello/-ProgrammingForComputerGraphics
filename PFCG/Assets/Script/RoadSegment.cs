using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class RoadSegment : MonoBehaviour
{
    [SerializeField]
    private Vector3 size = Vector3.one;
    private int subMeshCount = 1;
    private int subMeshIndex = 0;

    private List<Material> materialsList = new List<Material>();

    public void SetSize(Vector3 size){
        this.size = size;
    }


    // Start is called before the first frame update
    void Start()
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        MeshBuilder meshBuilder = new MeshBuilder(subMeshCount);
        MeshCollider meshCollider = this.GetComponent<MeshCollider>();


        // define points
        //top points
        Vector3 t0 = new Vector3(size.x, size.y, -size.z);
        Vector3 t1 = new Vector3(-size.x, size.y, -size.z);
        Vector3 t2 = new Vector3(-size.x, size.y, size.z);
        Vector3 t3 = new Vector3(size.x, size.y, size.z);

        //bottom points
        Vector3 b0 = new Vector3(size.x, -size.y, -size.z);
        Vector3 b1 = new Vector3(-size.x, -size.y, -size.z);
        Vector3 b2 = new Vector3(-size.x, -size.y, size.z);
        Vector3 b3 = new Vector3(size.x, -size.y, size.z);

        // create triangles
        
        //top square
        meshBuilder.BuildTriangle(t0, t1, t2, subMeshIndex);
        meshBuilder.BuildTriangle(t0, t2, t3, subMeshIndex);

        //bottom square
        meshBuilder.BuildTriangle(b2, b1, b0, subMeshIndex);
        meshBuilder.BuildTriangle(b3, b2, b0, subMeshIndex);


        //front square
        meshBuilder.BuildTriangle(b2, t3, t2, subMeshIndex);
        meshBuilder.BuildTriangle(b2, b3, t3, subMeshIndex);


        //back square
        meshBuilder.BuildTriangle(b0, t1, t0, subMeshIndex);
        meshBuilder.BuildTriangle(b0, b1, t1, subMeshIndex);


        //left square
        meshBuilder.BuildTriangle(b1, t2, t1, subMeshIndex);
        meshBuilder.BuildTriangle(b1, b2, t2, subMeshIndex);


        //right square
        meshBuilder.BuildTriangle(b3, t0, t3, subMeshIndex);
        meshBuilder.BuildTriangle(b3, b0, t0, subMeshIndex);

        meshFilter.mesh = meshBuilder.CreateMesh();

        MeshRenderer meshRenderer = this.GetComponent<MeshRenderer>();
        meshRenderer.materials = materialsList.ToArray();

        meshCollider.sharedMesh = meshFilter.mesh;
    }

    public void UpdateMaterialsList(List<Material> materials){
        materialsList = materials;
    }
}
