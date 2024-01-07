using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBorder : MonoBehaviour
{
    float depth = 1f;

    float pavementWidth = 0.5f;
    float pavementHeight = 0.2f;

    float laneWidth = 2f;
    float laneHeight = 0.1f;

    float roadMarkingWidth = 0.05f;
    float roadMarkingHeight = 0.1f;


    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        GameObject leftRoad = new GameObject();
        leftRoad.name = "Left Road";
        leftRoad.transform.position = new Vector3(
            this.transform.position.x - 427f,
            this.transform.position.y -552f,
            this.transform.position.z + 434.3f);
        leftRoad.transform.parent = this.transform;
        CreateRoadSegment(leftRoad);
        leftRoad.transform.localScale = new Vector3(4f, 1f, 500f);

        GameObject rightRoad = new GameObject();
        rightRoad.name = "Right Road";
        rightRoad.transform.position = new Vector3(
            this.transform.position.x + 548.5f,
            this.transform.position.y -552f,
            this.transform.position.z + 434.3f);
        rightRoad.transform.parent = this.transform;
        CreateRoadSegment(rightRoad);
        rightRoad.transform.localScale = new Vector3(4f, 1f, 500f);

        GameObject topRoad = new GameObject();
        topRoad.name = "Top Road";
        topRoad.transform.position = new Vector3(
            this.transform.position.x + 79f,
            this.transform.position.y -552f,
            this.transform.position.z - 27.6f);
        topRoad.transform.parent = this.transform;
        CreateRoadSegment(topRoad);
        topRoad.transform.localScale = new Vector3(4f, 1f, 470f);
        topRoad.transform.Rotate(0f, 90f, 0f, Space.Self);

         GameObject bottomRoad = new GameObject();
        bottomRoad.name = "Bottom Road";
        bottomRoad.transform.position = new Vector3(
            this.transform.position.x + 79f,
            this.transform.position.y -552f,
            this.transform.position.z + 970f);
        bottomRoad.transform.parent = this.transform;
        CreateRoadSegment(bottomRoad);
        bottomRoad.transform.localScale = new Vector3(4f, 1f, 508f);
        bottomRoad.transform.Rotate(0f, 90f, 0f, Space.Self);
    }


   private void CreateRoadSegment(GameObject parentRoad)
{
    // Define material properties
    Material pavementMaterial = new Material(Shader.Find("Specular"));
    pavementMaterial.color = Color.red;

    Material laneMaterial = new Material(Shader.Find("Specular"));
    laneMaterial.color = Color.black;

    Material roadMarkingMaterial = new Material(Shader.Find("Specular"));
    roadMarkingMaterial.color = Color.white;

    // Create lists of materials
    List<Material> pavementMaterialList = new List<Material> { pavementMaterial };
    List<Material> laneMaterialList = new List<Material> { laneMaterial };
    List<Material> roadMarkingMaterialList = new List<Material> { roadMarkingMaterial };

    // Set the tag for the parent road
    parentRoad.tag = "Road";

    // Create and setup left pavement
    GameObject leftPavement = CreateRoadComponent("Left Pavement", new Vector3(pavementWidth, pavementHeight, depth), pavementMaterialList, parentRoad.transform.position, parentRoad);

    // Create and setup left lane
    GameObject leftLane = CreateRoadComponent("Left Lane", new Vector3(laneWidth, laneHeight, depth), laneMaterialList, new Vector3(leftPavement.transform.position.x + pavementWidth + laneWidth, leftPavement.transform.position.y, leftPavement.transform.position.z), parentRoad);

    // Create and setup road marking
    GameObject roadMark = CreateRoadComponent("Road Mark", new Vector3(roadMarkingWidth, roadMarkingHeight, depth), roadMarkingMaterialList, new Vector3(leftLane.transform.position.x + laneWidth + roadMarkingWidth, leftLane.transform.position.y, leftLane.transform.position.z), parentRoad);

    // Create and setup right lane
    GameObject rightLane = CreateRoadComponent("Right Lane", new Vector3(laneWidth, laneHeight, depth), laneMaterialList, new Vector3(roadMark.transform.position.x + roadMarkingWidth + laneWidth, roadMark.transform.position.y, roadMark.transform.position.z), parentRoad);

    // Create and setup right pavement
    GameObject rightPavement = CreateRoadComponent("Right Pavement", new Vector3(pavementWidth, pavementHeight, depth), pavementMaterialList, new Vector3(rightLane.transform.position.x + laneWidth + pavementWidth, rightLane.transform.position.y, rightLane.transform.position.z), parentRoad);

    // Function to create and setup road components
    GameObject CreateRoadComponent(string name, Vector3 size, List<Material> materials, Vector3 position, GameObject parent)
    {
        GameObject component = new GameObject();
        component.name = name;
        component.tag = "Road"; // Set the tag for each road component
        component.AddComponent<RoadSegment>();
        component.GetComponent<RoadSegment>().SetSize(size);
        component.GetComponent<RoadSegment>().UpdateMaterialsList(materials);
        component.transform.position = position;
        component.transform.parent = parent.transform;
        return component;
    }
}


}