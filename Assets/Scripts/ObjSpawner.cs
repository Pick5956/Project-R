using UnityEngine;

public class ObjectSpawner : MonoBehaviour {
    public Terrain terrain;          // ใส่ Terrain ใน Inspector
    public GameObject[] objects;     // Prefab ที่จะสุ่มเกิด
    public int spawnCount = 50;      // จำนวนที่จะ spawn

    void Start() {
        SpawnObjects();
    }

    void SpawnObjects() {
        for (int i = 0; i < spawnCount; i++) {
            // สุ่มตำแหน่ง X,Z บน Terrain
            float x = Random.Range(0, terrain.terrainData.size.x);
            float z = Random.Range(0, terrain.terrainData.size.z);

            // อ่านความสูงของ Terrain ที่ตำแหน่งนั้น
            float y = terrain.SampleHeight(new Vector3(x, 0, z));

            // รวมตำแหน่งจริง
            Vector3 pos = new Vector3(x, y, z);

            // สุ่มเลือก Prefab
            int index = Random.Range(0, objects.Length);

            Instantiate(objects[index], pos, Quaternion.identity);
        }
    }
}
