using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 20;
    public int width = 256;
    public int height = 256;
    [Space]
    public float scale = 20f;
    public float offsetX = 100f;
    public float offsetY = 100f;
    [Space]
    public float speed = 2f;

    void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        offsetX += Time.deltaTime * speed;

        SpeedChange();
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCord = (float)x / width * scale + offsetX;
        float yCord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCord, yCord);
    }

    void SpeedChange()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            speed += 0.75f;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            speed -= 0.75f;

        if (speed >= 10)
            speed = 10;

        if (speed <= 2)
            speed = 2;
    }
}
