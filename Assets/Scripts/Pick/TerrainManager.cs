using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public Terrain Terrain; //ในอนาคตทำเป็น array สุ่ม terrain ได้ เหมือนFoodPrefab
    private TerrainData TerrainData; 
    Vector3 TerrainPos; //ตำแหน่งของTerrain ในโลก
    public FoodObject[] FoodPrefab;
    public Enemy[] EnemyPrefab;

    Vector3 RandomPosObj()
    {
        float x = Random.Range(0, TerrainData.size.x) + TerrainPos.x;
        float z = Random.Range(0, TerrainData.size.z) + TerrainPos.z;
        float y = Terrain.SampleHeight(new Vector3(x, 0, z)) + TerrainPos.y; //ต้อง + ตำแนห่งของterrain ในโลกไปด้วย

        return new Vector3(x, y, z);
    }

    public void GenerateFood()
    {
        for (int i = 0; i < 5; i++) 
        {
            int RandomFoodIndex = Random.Range(0, FoodPrefab.Length);
            FoodObject NewFood = Instantiate(FoodPrefab[RandomFoodIndex]);
            Vector3 FoodPos = RandomPosObj();
            FoodPos.y = NewFood.transform.position.y+FoodPos.y;
            AddObj(NewFood, FoodPos);
        }
    }

    void SpawnEnamy()
    {
        for (int i = 0; i < 1; i++)
        {

        int RandomEnemyIndex = Random.Range(0, EnemyPrefab.Length);
        Enemy NewEnemy = Instantiate(EnemyPrefab[0]);
        Vector3 EnemyPos = new Vector3(2.7f+i,0,27);
        AddObj(NewEnemy, EnemyPos);
        }
    }

    public void init()
    {
        TerrainData = Terrain.terrainData;
        TerrainPos = Terrain.transform.position;
        GenerateFood();
        SpawnEnamy();
    }

    

    void AddObj(CellData3D obj,Vector3 coord)
    {
        obj.transform.position = coord;
        obj.Init(coord);
    }
}
