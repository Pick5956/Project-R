using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    public int width = 256;
    public int height = 256;
    public float scale = 200f;

    void Start() {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    float EvaluateFalloff(int x, int z, int width, int height) {
        float nx = (float)x / width * 2 - 1;   // normalize ให้อยู่ระหว่าง -1 ถึง 1
        float nz = (float)z / height * 2 - 1;
        float value = Mathf.Max(Mathf.Abs(nx), Mathf.Abs(nz)); 
        return Mathf.Clamp01(value); // ค่าตั้งแต่ 0 (กลาง) ถึง 1 (ขอบ)
    }

    TerrainData GenerateTerrain(TerrainData terrainData) {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, 50, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }
    public int sss = 200;
    float[,] GenerateHeights() {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                

                float xCoord = (float)x / width * scale;
                float zCoord = (float)z / height * scale;

                // ชั้น noise ที่ราบ
                float baseNoise = Mathf.PerlinNoise(xCoord * 0.5f, zCoord * 0.5f) * 0.3f;

                // ชั้น noise ภูเขา
                float mountainNoise = Mathf.PerlinNoise(xCoord * 2f, zCoord * 2f) * 0.7f;

                // falloff ที่ขอบ
                float falloff = EvaluateFalloff(sss, sss, width, height);

                // รวมค่า (ใช้ blend แทนการลบตรง ๆ)
                float heightValue = (baseNoise + mountainNoise) * (1 - falloff);

                // ถ้าค่าต่ำกว่า 0.3 → บังคับให้เป็นที่ราบ
                if (heightValue < 0.3f) {
                    heightValue = 0.3f;
                }

                heights[x, z] = Mathf.Clamp01(heightValue);


                // กันค่าไม่เกิน 0–1
                heights[x, z] = Mathf.Clamp01(heightValue);
            }
        }
        return heights;
    }
}
