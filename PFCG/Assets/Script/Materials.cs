using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials 
{
    private List<Material> materialsList;

    public Materials()
    {
        materialsList = new List<Material>();

        // Create materials with the Standard Shader
        Material redMaterial = CreateStandardMaterial(Color.red);
        Material blueMaterial = CreateStandardMaterial(Color.blue);
        Material greenMaterial = CreateStandardMaterial(Color.green);
        Material yellowMaterial = CreateStandardMaterial(Color.yellow);
        Material whiteMaterial = CreateStandardMaterial(Color.white);
        Material blackMaterial = CreateStandardMaterial(Color.black);

        // Add materials to the list
        materialsList.AddRange(new Material[] { redMaterial, blueMaterial, greenMaterial, yellowMaterial, whiteMaterial, blackMaterial });
    }

    private Material CreateStandardMaterial(Color color)
    {
        Material material = new Material(Shader.Find("Standard"));
        material.color = color;
        material.SetFloat("_Metallic", 2f); 
        material.SetFloat("_Glossiness", 0.5f); 

        return material;
    }

    public List<Material> GetMaterialsList()
    {
        return materialsList;
    }
}
