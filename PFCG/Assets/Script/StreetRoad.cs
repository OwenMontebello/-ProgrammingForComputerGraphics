using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetRoad : MonoBehaviour
{
    float depth = 20f;

    float pavementWidth = 10f;
    float pavementHeight = 2f;

    float laneWidth = 40f;
    float laneHeight = 2f;

    float roadMarkingWidth = 1f;
    float roadMarkingHeight = 2f;

    
    void Start()
    {
        GameObject leftRoad = new GameObject();
        leftRoad.name = "Left Road";
        leftRoad.transform.position = new Vector3(this.transform.position.x - 666f, this.transform.position.y - 364.62f, this.transform.position.z + 513f);
        leftRoad.transform.parent = this.transform;
        CreateRoadSegment(leftRoad);
        leftRoad.transform.localScale = new Vector3(5f, 1f, 25f);

        GameObject rightRoad = new GameObject();
        rightRoad.name = "Right Road";
        rightRoad.transform.position = new Vector3(this.transform.position.x - 666f, this.transform.position.y - 364.62f, this.transform.position.z + 513f);
        rightRoad.transform.parent = this.transform;
        CreateRoadSegment(rightRoad);
        rightRoad.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    private void CreateRoadSegment(GameObject parentRoad)
    {
        Material pavementMaterial = CreateStandardMaterial(Color.grey);
        Material laneMaterial = CreateStandardMaterial(Color.black);
        Material roadMarkingMaterial = CreateStandardMaterial(Color.white);

      
        GameObject leftPavement = CreateSegment("Left Pavement", pavementWidth, pavementHeight, depth, parentRoad.transform.position, parentRoad, laneMaterial);

       
        Vector3 leftLanePos = new Vector3(leftPavement.transform.position.x + pavementWidth + laneWidth, leftPavement.transform.position.y, leftPavement.transform.position.z);
        GameObject leftLane = CreateSegment("Left Lane", laneWidth, laneHeight, depth, leftLanePos, parentRoad, laneMaterial);

    
        Vector3 roadMarkPos = new Vector3(leftLane.transform.position.x + laneWidth + roadMarkingWidth, leftLane.transform.position.y, leftLane.transform.position.z);
        GameObject roadMark = CreateSegment("Road Mark", roadMarkingWidth, roadMarkingHeight, depth, roadMarkPos, parentRoad, roadMarkingMaterial);

  
        Vector3 rightLanePos = new Vector3(roadMark.transform.position.x + roadMarkingWidth + laneWidth, roadMark.transform.position.y, roadMark.transform.position.z);
        GameObject rightLane = CreateSegment("Right Lane", laneWidth, laneHeight, depth, rightLanePos, parentRoad, laneMaterial);

        
        Vector3 rightPavementPos = new Vector3(rightLane.transform.position.x + laneWidth + pavementWidth, rightLane.transform.position.y, rightLane.transform.position.z);
        GameObject rightPavement = CreateSegment("Right Pavement", pavementWidth, pavementHeight, depth, rightPavementPos, parentRoad, laneMaterial);
    }

    private GameObject CreateSegment(string name, float width, float height, float depth, Vector3 position, GameObject parent, Material material)
    {
        GameObject segment = new GameObject(name);
        segment.AddComponent<RoadSegment>();
        segment.GetComponent<RoadSegment>().SetSize(new Vector3(width, height, depth));
        segment.GetComponent<RoadSegment>().UpdateMaterialsList(new List<Material>() { material });
        segment.transform.position = position;
        segment.transform.parent = parent.transform;
        return segment;
    }

    private Material CreateStandardMaterial(Color color)
    {
        Material material = new Material(Shader.Find("Standard"));
        material.color = color;
        material.SetFloat("_Metallic", 2.5f); 
        material.SetFloat("_Glossiness", 0.5f); 
        return material;
    }
}
