using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TerrainTextureData{
    public Texture2D terrainTexture;
    public Vector2 tileSize;
    public float minHeight;
    public float maxHeight;
}

[System.Serializable]
public class TreeData{
    public GameObject treeMesh;
    public float minHeight;
    public float maxHeight;

}
public class RandomHeihtsGen : MonoBehaviour
{

    private Terrain terrain;

    private TerrainData terrainData;

    [SerializeField]
    [Range(0f, 1f)]
    private float minRandomHeightRange = 0f;

    [SerializeField]
    [Range(0f, 1f)]
    private float maxRandomHeightRange = 0f;

    [SerializeField]
    private bool flattenTerrain = true;

    [Header("PerlinNoise")]
    [SerializeField]
    private bool perlinNoise = false;

    [SerializeField]
    private float perlinNoiseWidthScale = 0.01f;

    [SerializeField]
    private float perlinNoiseHeightScale = 0.01f;

    [Header("Texture Data")]
    [SerializeField]
    private List<TerrainTextureData> terrainTextureData;

    [SerializeField]
    private bool addTerrainTexture = false;

    [SerializeField]
    private float terrainTextureBlendOffset = 0.01f;

    [Header("Tree Data")]
    [SerializeField]
    private List<TreeData> treeData;

    [SerializeField]
    private int maxTrees = 2000;

    [SerializeField]
    private int treeSpacing = 10;

    [SerializeField]
    private bool addTrees = false;

    [SerializeField]
    private int terrainLayerIndex;

    [Header("Water")]
    [SerializeField]
    private GameObject water;

    [SerializeField]
    private float waterHeight = 0.3f;

    [Header("Path")]
    [SerializeField]
    private float pathWidth = 10f;
    [SerializeField]
    private float pathDepth = 0.02f;
    private int numberOfPathPoints = 5;
    [Header("Cloud")]
    [SerializeField]
    private GameObject cloudPrefab; 
    [SerializeField]
    private int numberOfClouds = 10;
    [SerializeField]
    private Vector2 cloudHeightRange = new Vector2(100f, 200f); 
    [SerializeField]
    private Vector2 cloudSizeRange = new Vector2(10f, 50f); 

    [Header("Sky")]
    [SerializeField]
    private Material skyboxMaterial;

    [Header("Rain")]
    [SerializeField]
    private GameObject rainPrefab;
    [SerializeField]
    private Vector2 rainHeightRange = new Vector2(40, 60);

    [SerializeField]
    private float rainScale = 1f;

    [Header("Fog")]
    [SerializeField]
    private GameObject fogPrefab;
    [SerializeField]
    private Vector3 fogPositionOffset = new Vector3(0, 0, 0);
    [SerializeField]
    private float fogScale = 1f;
    


    void Start()
    {
       if(terrain == null){
        terrain = this.GetComponent<Terrain>();
       }
       if(terrainData == null){
        terrainData = Terrain.activeTerrain.terrainData;
       }
       
       GenerateHeights();
       GeneratePath();
       AddTerrainTexture();
       AddTrees();
       AddClouds();
       AddWater();
       AddSky();
       AddRain();
       AddFog();
       
       //SpawnPlaneAtFixedHeight();


    }

    void Update(){

        //AddSky();
    }

    /* private void SpawnPlaneAtFixedHeight()
    {
        
        float randomX = Random.Range(-terrainWidth / 2, terrainWidth / 2);
        float randomZ = Random.Range(-terrainLength / 2, terrainLength / 2);
        Vector3 spawnPosition = new Vector3(randomX, fixedHeight, randomZ);

        Instantiate(planePrefab, spawnPosition, Quaternion.identity);
    }
*/

    private void AddFog()
{
    if (fogPrefab != null)
    {
        
        GameObject fogGameObject = Instantiate(fogPrefab, this.transform.position + fogPositionOffset, Quaternion.identity);
        fogGameObject.name = "Fog";
    
        fogGameObject.transform.localScale = new Vector3(fogScale, fogScale, fogScale);
        fogGameObject.transform.parent = this.transform; 
    }
}

    private void AddRain()
{
    if (rainPrefab != null)
    {
    
        float randomHeight = Random.Range(rainHeightRange.x, rainHeightRange.y);
        Vector3 rainPositionOffset = new Vector3(0, randomHeight, 0);

        GameObject rainGameObject = Instantiate(rainPrefab, this.transform.position + rainPositionOffset, Quaternion.identity);
        rainGameObject.name = "Rain";
        rainGameObject.transform.localScale = new Vector3(rainScale, rainScale, rainScale);
        rainGameObject.transform.parent = this.transform;
    }
}

   

private void AddSky()
{
    if(skyboxMaterial != null)
    {
        Material skyboxMaterialInstance = new Material(skyboxMaterial);
        RenderSettings.skybox = skyboxMaterialInstance;

        //float rotation = Random.Range(0f, 360f);
        //skyboxMaterialInstance.SetFloat("_Rotation", rotation);

        //DynamicGI.UpdateEnvironment();
    }
}


    private void GeneratePath()
{
    float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
    
  
    List<Vector2> pathPoints = new List<Vector2>();
    for (int i = 0; i < numberOfPathPoints; i++)
    {
        pathPoints.Add(new Vector2(Random.Range(0, terrainData.heightmapResolution), Random.Range(0, terrainData.heightmapResolution)));
    }
    

    for (int i = 0; i < pathPoints.Count - 1; i++)
    {
        Vector2 startPoint = pathPoints[i];
        Vector2 endPoint = pathPoints[i + 1];
        Vector2 currentPoint = startPoint;
        
        while ((currentPoint - endPoint).sqrMagnitude > 1)
        {
            int x = Mathf.RoundToInt(currentPoint.x);
            int z = Mathf.RoundToInt(currentPoint.y);
            
           
            for (int zOffset = -Mathf.RoundToInt(pathWidth / 2); zOffset <= Mathf.RoundToInt(pathWidth / 2); zOffset++)
            {
                for (int xOffset = -Mathf.RoundToInt(pathWidth / 2); xOffset <= Mathf.RoundToInt(pathWidth / 2); xOffset++)
                {
                    int xIndex = Mathf.Clamp(x + xOffset, 0, terrainData.heightmapResolution - 1);
                    int zIndex = Mathf.Clamp(z + zOffset, 0, terrainData.heightmapResolution - 1);
                    heightMap[zIndex, xIndex] = Mathf.Max(heightMap[zIndex, xIndex] - pathDepth, 0);
                }
            }
            
          
            currentPoint = Vector2.MoveTowards(currentPoint, endPoint, 1);
        }
    }
    
    terrainData.SetHeights(0, 0, heightMap);
}



private void SmoothPathEdges()
{
    float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
    terrainData.SetHeights(0, 0, heightMap);
}
    

   private void GenerateHeights(){
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for(int width = 0; width < terrainData.heightmapResolution; width++){
            for(int height = 0; height < terrainData.heightmapResolution; height++){

                /*
                if(perlinNoise)
                {
                        heightMap[width, height] = Mathf.PerlinNoise(width * perlinNoiseWidthScale, height * perlinNoiseHeightScale) ;
                }
                else
                {
                heightMap[width,height] = Random.Range(minRandomHeightRange, maxRandomHeightRange);
                }
                */
                heightMap[width, height] = Mathf.PerlinNoise(width * perlinNoiseWidthScale, height * perlinNoiseHeightScale) ;
                heightMap[width,height] += Random.Range(minRandomHeightRange, maxRandomHeightRange);

            }
        }

        terrainData.SetHeights(0,0, heightMap);
    }

      private void FlattenTerrain(){
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for(int width = 0; width < terrainData.heightmapResolution; width++){
            for(int height = 0; height < terrainData.heightmapResolution; height++){

                heightMap[width,height] = 0;

            }
        }

        terrainData.SetHeights(0,0, heightMap);
    }

    private void AddTerrainTexture(){

        TerrainLayer[] terrainLayers = new TerrainLayer[terrainTextureData.Count];

        for(int i = 0 ; i < terrainTextureData.Count; i++){

            if(addTerrainTexture){
                
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = terrainTextureData[i].terrainTexture;
                terrainLayers[i].tileSize = terrainTextureData[i].tileSize;
            }else{
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = null;
            }
        }

        terrainData.terrainLayers = terrainLayers;

        float[,] heightMap = terrainData.GetHeights(0,0,terrainData.heightmapResolution, terrainData.heightmapResolution);

        float[, , ] alphamapList = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        for(int height = 0; height < terrainData.alphamapHeight; height++){

            for(int width = 0; width < terrainData.alphamapWidth; width++){

              float[] alphamap = new float[terrainData.alphamapLayers];

              for(int i = 0; i < terrainTextureData.Count; i++){

                float heightBegin = terrainTextureData[i].minHeight - terrainTextureBlendOffset;
                float heightEnd = terrainTextureData[i].maxHeight - terrainTextureBlendOffset;

                if(heightMap[width, height] >= heightBegin && heightMap[width,height] <= heightEnd){
                    alphamap[i] = 1;
                }

              }
              Blend(alphamap);

              for(int j =0; j < terrainTextureData.Count; j++){
                alphamapList[width,height, j] = alphamap[j];
              }
            }
        }

        terrainData.SetAlphamaps(0,0, alphamapList);


    }

    private void Blend(float[] alphamap){
        float total = 0;

        for(int i = 0; i < alphamap.Length; i++){
            total += alphamap[i];
        }

        for(int i = 0; i<alphamap.Length; i++){
            alphamap[i] = alphamap[i] / total;
        }

    }

     private void AddTrees(){

        TreePrototype[] trees = new TreePrototype[treeData.Count];

        for(int i = 0; i < treeData.Count; i++)
        {
            trees[i] = new TreePrototype();
            trees[i].prefab = treeData[i].treeMesh;

        }

        terrainData.treePrototypes = trees;

        List<TreeInstance> treeInstanceList = new List<TreeInstance>();

        if(addTrees){
            for(int z = 0; z < terrainData.size.z; z += treeSpacing){

                for(int x = 0; x < terrainData.size.x; x += treeSpacing){
                    
                    for(int treeIndex = 0; treeIndex < trees.Length; treeIndex++){
                        if(treeInstanceList.Count < maxTrees){
                            float currentHeight = terrainData.GetHeight(x,z) / terrainData.size.y;

                            if(currentHeight >= treeData[treeIndex].minHeight && currentHeight <= treeData[treeIndex].maxHeight){

                                float randomX = (x + Random.Range(-5.0f, 5.0f)) / terrainData.size.x;

                                float randomZ = (z + Random.Range(-5.0f, 5.0f)) / terrainData.size.z;

                                Vector3 treePosition = new Vector3(randomX * terrainData.size.x, currentHeight * terrainData.size.y, randomZ * terrainData.size.z) + this.transform.position;

                                RaycastHit raycastHit;

                                int layerMask = 1 << terrainLayerIndex;

                                if(Physics.Raycast(treePosition, -Vector3.up, out raycastHit, 100, layerMask) || 
                                   Physics.Raycast(treePosition, Vector3.up, out raycastHit, 100, layerMask)){


                                float treeDistance = (raycastHit.point.y - this.transform.position.y) / terrainData.size.y;


                                TreeInstance treeInstance = new TreeInstance();

                                treeInstance.position = new Vector3(randomX, treeDistance, randomZ);
                                treeInstance.rotation = Random.Range(0, 360);
                                treeInstance.prototypeIndex = treeIndex;
                                treeInstance.color = Color.white;
                                treeInstance.lightmapColor = Color.white;
                                treeInstance.heightScale = 0.95f;
                                treeInstance.widthScale = 0.95f;

                                treeInstanceList.Add(treeInstance);

                                   }
                                

                            }
                        }
                    }

                }

            }
        }

        terrainData.treeInstances = treeInstanceList.ToArray();

     }

     private void AddWater(){
        GameObject waterGameObject = Instantiate(water, this.transform.position, this.transform.rotation);
        waterGameObject.name = "Water";
        waterGameObject.transform.position = this.transform.position + new Vector3(terrainData.size.x / 2, waterHeight * terrainData.size.y,
        terrainData.size.z / 2);
        waterGameObject.transform.localScale = new Vector3(terrainData.size.x, 1, terrainData.size.z);
     }

     private void AddClouds()
{
    for (int i = 0; i < numberOfClouds; i++)
    {
       
        float xPosition = Random.Range(0, terrainData.size.x);
        float zPosition = Random.Range(0, terrainData.size.z);
        float yPosition = Random.Range(cloudHeightRange.x, cloudHeightRange.y);

        Vector3 cloudPosition = new Vector3(xPosition, yPosition, zPosition) + this.transform.position;

        float cloudScale = Random.Range(cloudSizeRange.x, cloudSizeRange.y);

      
        GameObject cloud = Instantiate(cloudPrefab, cloudPosition, Quaternion.identity);
        cloud.transform.localScale = new Vector3(cloudScale, cloudScale, cloudScale);
        cloud.transform.parent = this.transform; 
    }
}

     private void OnDestroy(){

        if(flattenTerrain){
             FlattenTerrain();
        }
       

    }


}
