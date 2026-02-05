using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    // คลาสย่อยสำหรับจัดการ Grid
    public class MapGrid
    {
        public int width;
        public int height;
        public float tileSize;
        public GameObject[,] grid;

        public MapGrid(int w, int h, float size)
        {
            width = w;
            height = h;
            tileSize = size;
            grid = new GameObject[width, height];
        }

        // ฟังก์ชันสำหรับแปลง index เป็นตำแหน่งจริงในโลก
        public Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x * tileSize, 0, z * tileSize);
        }
    }

    // ตัวอย่างการใช้งาน MapGrid
    public GameObject groundPrefab;
    private MapGrid map;

    void Start()
    {
        map = new MapGrid(10, 10, 10f); // สร้าง grid 10x10 tile ขนาด 10 ยูนิต
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int z = 0; z < map.height; z++)
            {
                Vector3 pos = map.GetWorldPosition(x, z);
                GameObject tile = Instantiate(groundPrefab, pos, Quaternion.identity);
                map.grid[x, z] = tile; // เก็บ tile ลง array
            }
        }
    }
    
}
