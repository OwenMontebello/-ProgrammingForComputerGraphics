using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField]
    Vector3 scale = new Vector3(150f, 0.1f, 150f);

    void Start()
    {
        GameObject cube = new GameObject();
        cube.name = "Cube";
        cube.AddComponent<Cube>();
        cube.transform.rotation = this.transform.rotation;
        cube.transform.localScale = scale;
        cube.transform.position = this.transform.position;
        cube.transform.parent = this.transform;
    }
}
