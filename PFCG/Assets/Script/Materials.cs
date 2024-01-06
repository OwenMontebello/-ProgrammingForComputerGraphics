using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials 
{
    private List<Material> MaterialsList;

    public Materials(){
        
        MaterialsList = new List<Material>();

        Material redMaterial = new Material(Shader.Find("Specular"));
        redMaterial.color = Color.red;

        Material blueMaterial = new Material(Shader.Find("Specular"));
        blueMaterial.color = Color.blue;

        Material greenMaterial = new Material(Shader.Find("Specular"));
        greenMaterial.color = Color.green;

        Material yellowMaterial = new Material(Shader.Find("Specular"));
        yellowMaterial.color = Color.yellow;       

        Material whiteMaterial = new Material(Shader.Find("Specular"));
        whiteMaterial.color = Color.white; 

        Material blackMaterial = new Material(Shader.Find("Specular"));
        blackMaterial.color = Color.black; 

        MaterialsList.Add(redMaterial);
        MaterialsList.Add(blueMaterial);
        MaterialsList.Add(greenMaterial);
        MaterialsList.Add(yellowMaterial);
        MaterialsList.Add(whiteMaterial);
        MaterialsList.Add(blackMaterial);
    }

    public List<Material> GetMaterialsList(){
        return MaterialsList;
    }
}
