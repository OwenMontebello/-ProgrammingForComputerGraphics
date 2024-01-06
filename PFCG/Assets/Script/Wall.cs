using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private Vector3 WallSize = new Vector3(50, 20, 1);

    void Start()
    {
       GameObject cubeGameObject = new GameObject();
       cubeGameObject.name = "Cube";
       Cube cube = cubeGameObject.AddComponent<Cube>();
       cubeGameObject.GetComponent<Cube>().SetSize(WallSize);
       cubeGameObject.transform.position = this.transform.position;
       cubeGameObject.transform.rotation = this.transform.rotation;
       cubeGameObject.transform.localScale = this.transform.localScale;
       cubeGameObject.transform.parent = this.transform;
    }
}
