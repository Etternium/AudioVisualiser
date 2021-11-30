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

    public ParticleSystem warpDrive;
    ParticleSystemRenderer systemRenderer;
    float duration, rateOverTime, startSpeed, lengthScale, power;

    void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        systemRenderer = warpDrive.GetComponent<ParticleSystemRenderer>();
        power = 0f;
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        offsetX += Time.deltaTime * speed;

        SpeedChange();
        WarpDriveValues();
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
        if (speed > 2)
            warpDrive.Play();

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            speed += 0.75f;
            power++;
            IncrementWarpDriveValues(7.5f, 8.5f, 1f);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            speed -= 0.75f;
            power--;
            IncrementWarpDriveValues(7.5f, 8.5f, -1f);
        }

        if (speed >= 10)
        {
            speed = 10;
            power = 11f;
            SetWarpDriveValues(0.5f, 500f, 100f, 100f);
        }

        if (speed <= 2)
        {
            speed = 2;
            power = 0f;
            SetWarpDriveValues(5f, 35f, 25f, 15f);

            warpDrive.Stop();
        }

        if (speed != 10)
            duration = 5f;
    }

    void WarpDriveValues()
    {
        var main = warpDrive.main;
        var em = warpDrive.emission;


        main.duration = duration;
        em.rateOverTime = rateOverTime;
        main.startSpeed = startSpeed;
        systemRenderer.lengthScale = lengthScale;
    }

    void SetWarpDriveValues(float D, float RoT, float sS, float lS)
    {
        duration = D;
        rateOverTime = RoT;
        startSpeed = sS;
        lengthScale = lS;
    }

    void IncrementWarpDriveValues(float sS, float lS, float factor)
    {
        rateOverTime = Mathf.Pow(1.75f, power) + 50f;
        startSpeed += sS * factor;
        lengthScale += lS * factor;
    }
}
